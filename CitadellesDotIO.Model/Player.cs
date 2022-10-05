using CitadellesDotIO.Enums.TurnChoices;
using CitadellesDotIO.Model.Characters;
using CitadellesDotIO.Model.Districts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitadellesDotIO.Model
{
    public class Player
    {
        public string Name { get; set; }
        public int Gold { get; set; }
        public bool IsCurrentKing { get; set; }
        public Character Character { get; set; }
        public bool HasPickedCharacter => this.Character != null;
        public bool HasPlayed { get; set; }
        public bool CanPlay => this.HasPickedCharacter && !this.HasPlayed;
        public List<District> BuiltDistricts { get; set; }
        public List<District> DistrictsDeck { get; set; }
        public void PickCharacter(Character character)
        {
            this.Character = character;
            character.Player = this;
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

                // Le personnage a un type de quartier associé et au moins un d'entre eux est construit
                if (this.Character.HasAssociatedDistrictType &&
                    BuiltDistricts.Any(d => d.DistrictType == this.Character.AssociatedDistrictType))
                {
                    choices.Add(UnorderedTurnChoice.BonusIncome);
                }

                // Le personnage dispose d'un pouvoir à utiliser
                if (this.Character.HasSpell)
                {
                    choices.Add(UnorderedTurnChoice.UseCharacterSpell);
                }

                // La joueur a assez d'or pour constuire un quartier qui n'existe pas dans sa cité
                if (this.DistrictsDeck.Any(d => d.BuildingCost <= this.Gold && !BuiltDistricts.Any(bd => bd.Name == d.Name)))
                {
                    choices.Add(UnorderedTurnChoice.BuildDistrict);
                } 

                return choices.Except(TakenChoices).ToList();
            }
        }
    }
}
