using CitadellesDotIO.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitadellesDotIO.Model.Spells
{
    public abstract class Spell
    {
        public Player Caster { get; set; }
        public virtual Type TargetType => null;
        public bool HasTargetType => TargetType != null;
        public abstract void Cast(ITarget target);
        public virtual void GetAvailableTargets(List<ITarget> targets)
        {
            if (targets.Any(t => !TargetType.IsInstanceOfType(t)))
                throw new SpellTargetException("Un parametre passé au Spell " + GetType().Name + " n'est pas de type " + TargetType.Name);

        }
        public virtual void GetAvailableTargets()
        {
            GetAvailableTargets(new List<ITarget>());
        }
        public List<ITarget> Targets { get; set; }
        public bool HasTargets => Targets?.Count > 0;
    }
}
