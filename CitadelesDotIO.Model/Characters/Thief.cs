using System;
using System.Collections.Generic;
using System.Text;

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
            set { throw new Exception("Le voleur ne peut être volé"); }
        }
    }
}
