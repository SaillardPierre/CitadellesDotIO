using CitadellesDotIO.Enums;
using CitadellesDotIO.Enums.TurnChoices;
using CitadellesDotIO.Model.Characters;
using CitadellesDotIO.Model.Districts;
using CitadellesDotIO.Model.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitadellesDotIO.Model
{
    public class Player : ISwappable
    {
        public string Name { get; set; }
        public int Gold { get; set; }
        public bool IsCurrentKing { get; set; }
        public Character Character { get; set; }
        public bool HasPickedCharacter => this.Character != null;
        public bool IsFirstReachingDistrictThreshold { get; set; }
        public bool HasReachedDistrictThreshold => this.BuiltDistricts.Count >= this.DistrictThreshold;
        public bool HasAllDistrictTypesBonus => this.BuiltDistricts.Select(d => d.DistrictType).Distinct().Count() == Enum.GetNames(typeof(DistrictType)).Length;
        public int DistrictThreshold { get; set; }
        private const int BasePoolSize = 2;
        private const int BasePickSize = 1;
        private const int BaseTurnBuildingCap = 1;
        public int PoolSize { get; set; }
        public int PickSize { get; set; }
        public int TurnBuildingCap { get; set; }
        public void ResetPoolSize() => this.PoolSize = BasePoolSize;
        public void ResetPickSize() => this.PickSize = BasePickSize;
        public void ResetTurnBuildingCap() => this.TurnBuildingCap = BaseTurnBuildingCap;

        public int Score { get; set; }

        public List<District> City { get; set; }
        public List<District> BuiltDistricts => GetBuiltDistricts();            
        private List<District> GetBuiltDistricts() => this.City.Where(d => d.IsBuilt).ToList();
        public List<District> BuildableDistricts => this.GetBuildableDistricts();
        private List<District> GetBuildableDistricts()
            => this.DistrictsDeck.Where(
                    d => d.BuildingCost <= this.Gold &&
                    !this.BuiltDistricts.Any(bd => bd.Name == d.Name)).ToList();
        public IEnumerable<District> DistrictSpellSources => GetDistrictSpellSources();
        private IEnumerable<District> GetDistrictSpellSources() =>
            this.BuiltDistricts.Where(d => d.HasSpell && !TakenChoices.Contains(d.Name)).ToList();
        public IEnumerable<District> DistrictPassiveSources => GetDistrictPassiveSources();
        private IEnumerable<District> GetDistrictPassiveSources()
            => this.BuiltDistricts.Where(d => d.HasPassive);

        public List<District> DistrictsDeck { get; set; }
        public void PickCharacter(Character character)
        {
            this.Character = character;
            character.Player = this;
            if (character.HasSpell)
            {
                character.Spell.Caster = this;
            }
            if(character.HasPassive)
            {
                character.Passive.Player = this;
            }
            this.TakenChoices = new();
        }

        public Player() : this("AnonymousPlayer") { }
        public Player(string name)
        {
            this.Name = name;
            this.IsCurrentKing = false;
            this.City = new();
            this.DistrictsDeck = new();
            this.TakenChoices = new();
            this.Score = 0;
            this.PickSize = BasePickSize;
            this.PoolSize = BasePoolSize;
            this.TurnBuildingCap = BaseTurnBuildingCap;
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
                if (this.DistrictSpellSources.Any(d => d.Spell.HasTargets))
                {
                    choices.Add(UnorderedTurnChoice.CastDistrictSpell);
                }

                if (this.Character != null)
                {
                    // Le personnage a un type de quartier associé et au moins un d'entre eux est construit
                    if (this.Character.HasAssociatedDistrictType &&
                        BuiltDistricts.Any(d => d.DistrictType == this.Character.AssociatedDistrictType))
                    {
                        choices.Add(UnorderedTurnChoice.BonusIncome);
                    }

                    // Le personnage dispose d'un pouvoir à utiliser
                    if (this.Character.HasSpell && this.Character.Spell.HasTargets)
                    {
                        choices.Add(UnorderedTurnChoice.CastCharacterSpell);
                    }

                    choices.RemoveAll(c => TakenChoices.Contains(c.ToString()));

                    // La joueur a assez d'or pour constuire un quartier qui n'existe pas dans sa cité et il n'a pas atteint le maximum de constructions pour ce tour
                    if (this.DistrictsDeck.Any(d => d.BuildingCost <= this.Gold && !this.BuiltDistricts.Any(bd => bd.Name == d.Name)) &&
                        this.TurnBuildingCap > TakenChoices.Count(c=>c.Equals(UnorderedTurnChoice.BuildDistrict.ToString())))
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
            this.Score = this.BuiltDistricts.Sum(d => d.ScoreValue);

            // Si le joueur est le premier a atteindre le seuil de quartiers
            if (this.IsFirstReachingDistrictThreshold)
            {
                this.Score += 4;
            }

            // Si le joueur a atteint le seuil de quartiers
            if (this.HasReachedDistrictThreshold)
            {
                this.Score += 2;
            }

            // Si la cité contient des quartiers de 5 couleurs différentes
            if (this.HasAllDistrictTypesBonus)
            {
                this.Score += 3;
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
            this.Gold -= district.BuildingCost;
            this.DistrictsDeck.Remove(district);
            this.City.Add(district);
        }
        public void PickDistrict(District district)
        {
            district.Reset();
            district.Owner = this;
            if(district.HasSpell)
            {
                district.Spell.Caster = this;
            }
            if(district.HasPassive)
            {
                district.Passive.Player = this;
            }
            this.DistrictsDeck.Add(district);
        }
        public void PickDistricts(List<District> districts) => districts.ForEach(d => this.PickDistrict(d));
    }
}
