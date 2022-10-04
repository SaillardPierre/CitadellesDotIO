using CitadellesDotIO.Enums;
using System;

namespace CitadellesDotIO.Model.Characters
{
    public abstract class Character
    {        
        public Character() { }
        public Character(int order)
        {
            this.Order = order;
        }
        public string Name => this.GetType().Name;
        public Player Player { get; set; }
        public int Order { get; set; }
        public abstract DistrictType? AssociatedDistrictType { get;}
        public bool HasAssociatedDistrictType => this.AssociatedDistrictType.HasValue;
        public abstract Spell Spell { get; }
        public bool HasSpell => this.Spell != null;
        public bool IsPicked => this.Player != null;
        public bool IsMurdered{ get; set; }
        public bool IsStolen { get; set; }

        public bool IsVisible { get; set; }
        public void Flip()
        {
            this.IsVisible = !this.IsVisible;
        }
    }
}
