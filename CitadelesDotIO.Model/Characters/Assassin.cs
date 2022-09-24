using System;
using System.Collections.Generic;
using System.Text;

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
                throw new Exception("L'assassin ne peut être assassiné");
            }
        }

        public void KillCharacter(Character target)
        {
            try
            {
                target.IsAlive = false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
