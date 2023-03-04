using CitadellesDotIO.Engine.Targets;
using CitadellesDotIO.Enums;
using CitadellesDotIO.Enums.TurnChoices;
using CitadellesDotIO.Engine.Characters;
using CitadellesDotIO.Engine.Districts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.ComponentModel.DataAnnotations;
using CitadellesDotIO.Engine.View;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Threading.Tasks;
using CitadellesDotIO.Engine.DTOs;

namespace CitadellesDotIO.Engine
{
    [JsonObject(IsReference = true)]
    public class Player : ISwappable
    {
        [Required(ErrorMessage = "Your G@M3R74G is required !")]
        [StringLength(50, ErrorMessage = "The length of your nickname must be between 5 and 50 characters !", MinimumLength = 5)]
        public string Name { get; set; }
        public string Id { get; set; }
        public IView View { get; set; }
        public int Gold { get; set; }
        public bool IsCurrentKing { get; set; }
        public bool IsHost { get; set; }
        public Character Character { get; set; }
        public bool HasPickedCharacter => Character != null;
        public bool IsFirstReachingDistrictThreshold { get; set; }
        public bool HasReachedDistrictThreshold => BuiltDistricts.Count >= DistrictThreshold;
        public bool HasAllDistrictTypesBonus => BuiltDistricts.Select(d => d.DistrictType).Distinct().Count() == Enum.GetNames(typeof(DistrictType)).Length;
        public int DistrictThreshold { get; set; }
        private const int BasePoolSize = 2;
        private const int BasePickSize = 1;
        private const int BaseTurnBuildingCap = 1;
        public int PoolSize { get; set; }
        public int PickSize { get; set; }
        public int TurnBuildingCap { get; set; }
        public void ResetPoolSize() => PoolSize = BasePoolSize;
        public void ResetPickSize() => PickSize = BasePickSize;
        public void ResetTurnBuildingCap() => TurnBuildingCap = BaseTurnBuildingCap;

        public int Score { get; set; }

        public List<District> City { get; set; }
        public List<District> BuiltDistricts => GetBuiltDistricts();
        private List<District> GetBuiltDistricts() => City.Where(d => d.IsBuilt).ToList();
        public List<District> BuildableDistricts => GetBuildableDistricts();
        private List<District> GetBuildableDistricts()
            => DistrictsDeck.Where(
                    d => d.BuildingCost <= Gold &&
                    !BuiltDistricts.Any(bd => bd.Name == d.Name)).ToList();
        public IEnumerable<District> DistrictSpellSources => GetDistrictSpellSources();
        private IEnumerable<District> GetDistrictSpellSources() =>
            BuiltDistricts.Where(d => d.HasSpell && !TakenChoices.Contains(d.Name)).ToList();
        public IEnumerable<District> DistrictPassiveSources => GetDistrictPassiveSources();
        private IEnumerable<District> GetDistrictPassiveSources()
            => BuiltDistricts.Where(d => d.HasPassive);

        public List<District> DistrictsDeck { get; set; }
        public void PickCharacter(Character character)
        {
            Character = character;
            character.Player = this;
            if (character.HasSpell)
            {
                character.Spell.Caster = this;
            }
            if (character.HasPassive)
            {
                character.Passive.Player = this;
            }
            TakenChoices = new();
        }
        public Player() : this(string.Empty) { }
        public Player(string name)
        {
            Name = name;
            IsCurrentKing = false;
            City = new();
            DistrictsDeck = new();
            TakenChoices = new();
            Score = 0;
            PickSize = BasePickSize;
            PoolSize = BasePoolSize;
            TurnBuildingCap = BaseTurnBuildingCap;
        }

        public Player(string name, IView view) : this(name)
        {
            this.View = view;
        }

        public List<string> TakenChoices { get; set; }

        public List<UnorderedTurnChoice> AvailableChoices
        {
            get
            {
                // Le joueur peut toujours choisir de terminer son tour
                List<UnorderedTurnChoice> choices = new() {
                    UnorderedTurnChoice.EndTurn
                };

                // Le joueur dispose d'un quartier disposant d'un pouvoir à utiliser
                if (DistrictSpellSources.Any(d => d.Spell.HasTargets))
                {
                    choices.Add(UnorderedTurnChoice.CastDistrictSpell);
                }

                if (Character != null)
                {
                    // Le personnage a un type de quartier associé et au moins un d'entre eux est construit
                    if (Character.HasAssociatedDistrictType &&
                        BuiltDistricts.Any(d => d.DistrictType == Character.AssociatedDistrictType))
                    {
                        choices.Add(UnorderedTurnChoice.BonusIncome);
                    }

                    // Le personnage dispose d'un pouvoir à utiliser
                    if (Character.HasSpell && Character.Spell.HasTargets)
                    {
                        choices.Add(UnorderedTurnChoice.CastCharacterSpell);
                    }

                    choices.RemoveAll(c => TakenChoices.Contains(c.ToString()));

                    // La joueur a assez d'or pour constuire un quartier qui n'existe pas dans sa cité et il n'a pas atteint le maximum de constructions pour ce tour
                    if (DistrictsDeck.Any(d => d.BuildingCost <= Gold && !BuiltDistricts.Any(bd => bd.Name == d.Name)) &&
                        TurnBuildingCap > TakenChoices.Count(c => c.Equals(UnorderedTurnChoice.BuildDistrict.ToString())))
                    {
                        choices.Add(UnorderedTurnChoice.BuildDistrict);
                    }
                }

                return choices;
            }
        }

        public void ComputeScore()
        {
            // Somme des valeurs des quartiers de la cité
            Score = BuiltDistricts.Sum(d => d.ScoreValue);

            // Si le joueur est le premier a atteindre le seuil de quartiers
            if (IsFirstReachingDistrictThreshold)
            {
                Score += 4;
            }

            // Si le joueur a atteint le seuil de quartiers
            if (HasReachedDistrictThreshold)
            {
                Score += 2;
            }

            // Si la cité contient des quartiers de 5 couleurs différentes
            if (HasAllDistrictTypesBonus)
            {
                Score += 3;
            }
        }
        public void BuildDistrict(District district)
        {
            district.IsBuilt = true;
            if (district.HasSpell)
            {
                district.Spell.Caster = this;
            }
            if (district.HasPassive)
            {
                district.Passive.Player = this;
            }
            Gold -= district.BuildingCost;
            DistrictsDeck.Remove(district);
            City.Add(district);
        }
        public void PickDistrict(District district)
        {
            district.Reset();
            district.Owner = this;
            if (district.HasSpell)
            {
                district.Spell.Caster = this;
            }
            if (district.HasPassive)
            {
                district.Passive.Player = this;
            }
            DistrictsDeck.Add(district);
        }
        public void PickDistricts(List<District> districts) => districts.ForEach(d => PickDistrict(d));

        public void ApplyPassives()
        {
            ResetPickSize();
            ResetPoolSize();
            ResetTurnBuildingCap();
            if (Character.HasPassive)
            {
                Character.Passive.Apply();
            }
            foreach (District district in DistrictPassiveSources)
            {
                district.Passive.Apply();
            }
        }

        public PlayerDto ToPlayerDto()
        => new(this.Id, this.Name, this.IsHost, this.IsCurrentKing);
    }
}
