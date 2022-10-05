using CitadellesDotIO.Enums;
using CitadellesDotIO.Exceptions;
using CitadellesDotIO.Model.Spells;

namespace CitadellesDotIO.Model.Characters
{
    public class Thief : Character
    {
        public Thief(int order) : base(order)
        {
        }

        public new bool IsStolen
        {
            get { return false; }
            set { throw new CharacterBehaviourException("Le voleur ne peut être volé"); }
        }

        public override DistrictType? AssociatedDistrictType => null;

        public override ISpell<ITarget> Spell => new Steal<Character>() as ISpell<ITarget>;
    }
}
