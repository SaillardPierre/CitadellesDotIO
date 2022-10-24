using CitadellesDotIO.Engine.Spells;
using CitadellesDotIO.Enums;

namespace CitadellesDotIO.Engine.Characters
{
    public sealed class Assassin : Character
    {
        public Assassin() : base()
        {
            this.Spell = new Murder(this.Player);
        }
        public Assassin(int order) : base(order)
        {
            this.Spell = new Murder(this.Player);
        }

        #pragma warning disable CA1822 // Marquer les membres comme étant static
        public new bool IsMurdered => false;
        public new bool IsStolen => false;        
        #pragma warning restore CA1822 // Marquer les membres comme étant static
        public override DistrictType? AssociatedDistrictType => null;

        public override Spell Spell { get; set; }
    }
}
