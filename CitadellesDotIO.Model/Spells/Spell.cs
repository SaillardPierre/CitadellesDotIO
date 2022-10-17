﻿using CitadellesDotIO.Exceptions;
using CitadellesDotIO.Model.Targets;
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
        public virtual bool HasToPickTargets => true;
        public virtual bool HasTargets => this.Targets != null && this.Targets.Count > 0;
        public List<ITarget> Targets { get; set; }
        public virtual void Cast(ITarget target)
        {
            if (this.HasToPickTargets && target == null)
            {
                throw new SpellTargetException("Le Spell doit avoir une cible mais le paramêtre est null");
            }
        }
        public virtual void Cast()
        {
            if (this.HasToPickTargets)
            {
                throw new SpellTargetException("Le Spell devrait avoir une cible mais la signature sans paramètres à été appelée");
            }
        }
        public virtual void GetAvailableTargets(List<ITarget> targets)
        {
            if (targets.Any(t => !TargetType.IsInstanceOfType(t)))
                throw new SpellTargetException("Un parametre passé au Spell " + GetType().Name + " n'est pas de type " + TargetType.Name);

        }
        public virtual void GetAvailableTargets()
        {
            GetAvailableTargets(new List<ITarget>());
        }
    }
}
