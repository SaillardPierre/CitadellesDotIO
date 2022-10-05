using CitadellesDotIO.Enums;
using CitadellesDotIO.Exceptions;
using CitadellesDotIO.Model.Spells;

namespace CitadellesDotIO.Model.Characters
{
    public class Assassin : Character
    {       
        public Assassin(int order) : base(order)
        {
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

        public override ISpell<ITarget> Spell => new Murder<Character>() as ISpell<ITarget>;        

    }
}
