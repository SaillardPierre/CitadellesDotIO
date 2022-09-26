using CitadellesDotIO.Exceptions;

namespace CitadellesDotIO.Model.Characters
{
    public class Assassin : Character
    {
        public Assassin() : base() { }

        public Assassin(int order) : base(order)
        {
        }

        public new bool IsAlive
        {
            get { return true; }
            set
            {
                throw new CharacterBehaviourException("L'assassin ne peut être assassiné");
            }
        }

        public void KillCharacter(Character target)
        {
            try
            {
                target.IsAlive = false;
            }
            catch (CharacterBehaviourException e)
            {
                throw e;
            }
        }

    }
}
