using System;
using System.Collections.Generic;
using System.Text;

namespace CitadellesDotIO.Model
{
    public interface ISpell<T> where T : ITarget
    {
        public abstract void Cast(ref T target);
    }
}
