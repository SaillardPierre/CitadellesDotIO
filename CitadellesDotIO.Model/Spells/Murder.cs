using CitadellesDotIO.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CitadellesDotIO.Model.Spells
{
    public class Murder<T> : ISpell<T> where T : Character
    {       
        public void Cast(ref T target)
        {
            target.IsMurdered = true;
        }
    }
}
