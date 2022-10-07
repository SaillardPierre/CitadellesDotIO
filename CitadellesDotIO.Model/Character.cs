using CitadellesDotIO.Enums;
using CitadellesDotIO.Model.Spells;
using System;
using System.Linq;

namespace CitadellesDotIO.Model
{
    public abstract class Character : ICharacter, ITarget
    {
        protected Character() { }
        protected Character(int order)
        {
            Order = order;
        }
        public string Name => GetType().Name;
        public Player Player { get; set; }
        public int Order { get; set; }
        public abstract DistrictType? AssociatedDistrictType { get; }
        public bool HasAssociatedDistrictType => AssociatedDistrictType.HasValue;
        public virtual Spell Spell { get; set; }
        public bool HasSpell => Spell != null;
        public bool IsPicked => Player != null;
        public bool IsMurdered { get; set; }
        public bool IsStolen { get; set; }

        public bool IsVisible { get; set; }
        public void Flip()
        {
            IsVisible = !IsVisible;
        }
        public void Reset()
        {
            IsMurdered = false;
            IsVisible = false;
            IsStolen = false;
            Player = null;
        }

        public void PercieveBonusIncome()
        {
            if (this.HasAssociatedDistrictType)
            {
                int bonusIncome = this.Player.BuiltDistricts.Count(d => d.DistrictType == this.AssociatedDistrictType);
                this.Player.Gold += bonusIncome;
            }
        }

    }
}
