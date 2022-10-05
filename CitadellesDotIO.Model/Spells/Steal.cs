using CitadellesDotIO.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CitadellesDotIO.Model.Spells
{
    public class Steal<T> : ISpell<T> where T : Character
    {
        public void Cast(ref T target)
        {
            target.IsStolen = true;
        }
    }
}
