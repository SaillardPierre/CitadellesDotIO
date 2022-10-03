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
        }

        public List<UnorderedTurnChoice> TakenChoices { get; set; }
        public List<UnorderedTurnChoice> AvailableChoices => Consts.UnorderedTurnChoices.Except(this.TakenChoices).ToList();
    }
}
