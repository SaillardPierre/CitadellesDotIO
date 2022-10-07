using CitadellesDotIO.Enums;
using CitadellesDotIO.Exceptions;
using CitadellesDotIO.Model.Spells;

namespace CitadellesDotIO.Model.Characters
{
    public class Assassin : Character
    {       
        public Assassin(int order) : base(order)
        {
            this.Spell = new Murder();
        }

        public new bool IsMurdered
        {
            get { return false; }
            set
            {
                throw new CharacterBehaviourException("L'assassin ne peut être assassiné");
            }
        }

        public override DistrictType? AssociatedDistrictType => null;

        public override Spell Spell { get; set; }         
    }
}
