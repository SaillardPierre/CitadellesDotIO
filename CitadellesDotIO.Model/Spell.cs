using CitadellesDotIO.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitadellesDotIO.Model
{
    public abstract class Spell
    {
        public Player Caster { get; set; }
        public abstract Type TargetType { get; }        
        public abstract void Cast(ITarget target);
        public virtual void GetAvailableTargets(List<ITarget> targets)
        {
            if (targets.Any(t => !TargetType.IsInstanceOfType(t)))
                throw new SpellTargetException("Un parametre passé au Spell "+this.GetType().Name+" n'est pas de type "+this.TargetType.Name);
            this.Targets = targets;
        }
        public List<ITarget> Targets { get; set; }
        public bool HasTargets => this.Targets.Count > 0;
    }
}
