using CitadellesDotIO.Model.Districts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CitadellesDotIO.Model.Spells
{
    public class Demolish<T> : ISpell<T> where T : District
    {    
        public void Cast(ref T target)
        {
            target = null;
        }
    }
}
