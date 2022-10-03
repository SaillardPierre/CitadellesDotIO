using CitadellesDotIO.Enums;

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
        public int? PlayerId { get; set; }
        public Player Player { get; set; }
        public int Order { get; set; }
        public DistrictType? AssociatedDistrictType { get;}
        public bool HasAssociatedDistrictType => this.AssociatedDistrictType.HasValue;
        public bool IsPicked => this.Player != null;
        public bool IsAlive { get; set; }
        public bool IsStolen { get; set; }

        public bool IsVisible { get; set; }
        public void Flip()
        {
            this.IsVisible = !this.IsVisible;
        }
    }
}
