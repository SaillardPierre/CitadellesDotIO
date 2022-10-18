using CitadellesDotIO.Engine;
using CitadellesDotIO.Engine.Spells;
using CitadellesDotIO.Enums;
using CitadellesDotIO.Engine.Passives;
using CitadellesDotIO.Engine.Targets;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitadellesDotIO.Engine.Districts
{
    public abstract class District : IDealable
    {
        public virtual string Name { get; set; }
        public virtual int BuildingCost { get; set; }
        public virtual int ScoreValue => this.BuildingCost;
        public virtual int DestructionCost => this.BuildingCost - 1;
        public virtual bool CanBeDestroyed => IsBuilt && Owner != null;
        public bool IsBuilt { get; set; }
        public Player Owner { get; set; }
        public virtual DistrictType DistrictType { get; set; }
        public virtual Spell Spell { get; set; }
        public bool HasSpell => this.Spell != null;
        public virtual Passive Passive { get; set; }
        public bool HasPassive => this.Passive != null;

        public void Reset()
        {
            if (this.Owner != null)
            {               
                if(this.Owner.City.Contains(this))
                {
                    this.Owner.City.Remove(this);
                }
                this.Owner = null;
            }
            if (this.HasSpell)
            {
                this.Spell.Caster = null;
            }
            if (this.HasPassive)
            {
                this.Passive.Player = null;
            }

            this.IsBuilt = false;
        }
    }
}