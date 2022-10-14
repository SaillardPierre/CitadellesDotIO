using CitadellesDotIO.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitadellesDotIO.Model.Districts
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
        public virtual DistrictType DistrictType { get; }
        public virtual Spell Spell { get; set; }
        public bool HasSpell => this.Spell != null;
        public void Reset()
        {
            this.IsBuilt = false;
            this.Owner = null;
            if (this.HasSpell)
            {
                this.Spell.Caster = null;
            }
        }
    }
}