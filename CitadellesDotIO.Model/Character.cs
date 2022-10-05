using CitadellesDotIO.Enums;
using CitadellesDotIO.Model.Spells;
using System;

namespace CitadellesDotIO.Model
{
    public abstract class Character : ICharacter
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
        public abstract ISpell<ITarget> Spell { get; }
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
    }
}
