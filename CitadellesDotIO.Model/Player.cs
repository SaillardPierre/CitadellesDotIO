using CitadellesDotIO.Enums.TurnChoices;
using CitadellesDotIO.Model.Districts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitadellesDotIO.Model
{
    public class Player : ITarget, ISwappable
    {
        public string Name { get; set; }
        public int Gold { get; set; }
        public bool IsCurrentKing { get; set; }
        public Character Character { get; set; }
        public bool HasPickedCharacter => this.Character != null;
        public bool HasPlayed { get; set; }
        public bool CanPlay => this.HasPickedCharacter && !this.HasPlayed;
        public List<District> BuiltDistricts { get; set; }
        public List<District> BuildableDistricts =>
            this.DistrictsDeck.Where(
                d => d.BuildingCost <= this.Gold &&
                !this.BuiltDistricts.Any(
                    bd => bd.Name == d.Name &&
                    bd.IsBuilt)).ToList();
        public List<District> DistrictsDeck { get; set; }
        public void PickCharacter(Character character)
        {
            this.Character = character;
            character.Player = this;
            if (character.HasSpell)
            {
                character.Spell.Caster = this;
            }
            this.TakenChoices = new List<UnorderedTurnChoice>();
        }

        public Player(string name)
        {
            Name = name;
            IsCurrentKing = false;
            BuiltDistricts = new List<District>();
            DistrictsDeck = new List<District>();
            this.TakenChoices = new List<UnorderedTurnChoice>();
        }

        public List<UnorderedTurnChoice> TakenChoices { get; set; }
        public List<UnorderedTurnChoice> AvailableChoices
        {
            get
            {
                // Le joueur peut toujours choisir de terminer son tour
                List<UnorderedTurnChoice> choices = new List<UnorderedTurnChoice>() {
                    UnorderedTurnChoice.EndTurn
                };
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
                        choices.Add(UnorderedTurnChoice.UseCharacterSpell);
                    }

                    // La joueur a assez d'or pour constuire un quartier qui n'existe pas dans sa cité
                    if (this.DistrictsDeck.Any(d => d.BuildingCost <= this.Gold && !BuiltDistricts.Any(bd => bd.Name == d.Name && bd.IsBuilt)))
                    {
                        choices.Add(UnorderedTurnChoice.BuildDistrict);
                    }
                }

                choices.RemoveAll(c => TakenChoices.Contains(c));
                return choices;
            }
        }


        public void BuildDistrict(District district)
        {
            district.IsBuilt = true;
            this.Gold -= district.BuildingCost;
            this.BuiltDistricts.Add(district);
        }

        public void PickDistrict(District district)
        {
            district.Reset();
            district.Owner = this;
            this.DistrictsDeck.Add(district);
        }
        public void PickDistricts(List<District> districts) => districts.ForEach(d => this.PickDistrict(d));
    }
}
