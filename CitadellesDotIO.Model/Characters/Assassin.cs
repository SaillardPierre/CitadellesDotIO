using CitadellesDotIO.Enums;
using CitadellesDotIO.Exceptions;

namespace CitadellesDotIO.Model.Characters
{
    public class Assassin : Character
    {
        public Assassin() : base() { }

        public Assassin(int order) : base(order)
        {
        }

        public new bool IsMurdered
        {
            get { return true; }
            set
            {
                throw new CharacterBehaviourException("L'assassin ne peut être assassiné");
            }
        }

        public override DistrictType? AssociatedDistrictType => null;

        public override Spell Spell => new Spell();

        public void KillCharacter(Character target)
        {
            try
            {
                target.IsMurdered = true;
            }
            catch (CharacterBehaviourException e)
            {
                throw e;
            }
        }

    }
}
