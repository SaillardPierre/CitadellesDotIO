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
        public int Order { get; set; }
        public bool IsPicked => this.PlayerId.HasValue;
        public bool IsAlive { get; set; }
        public bool IsStolen { get; set; }

        public bool IsVisible { get; set; }
        public void Flip()
        {
            this.IsVisible = !this.IsVisible;
        }
    }
}
