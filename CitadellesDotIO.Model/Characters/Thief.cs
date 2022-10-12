using CitadellesDotIO.Enums;
using CitadellesDotIO.Exceptions;
using CitadellesDotIO.Model.Spells;

namespace CitadellesDotIO.Model.Characters
{
    public class Thief : Character
    {        
        protected Thief() : base()
        {
            this.Spell = new Murder(this.Player);
        }
        public Thief(int order) : base(order)
        {
            this.Spell = new Steal(this.Player);
        }

        #pragma warning disable CA1822 // Marquer les membres comme étant static
        public new bool IsStolen
        #pragma warning restore CA1822 // Marquer les membres comme étant static
        {
            get { return false; }
            set { throw new CharacterBehaviourException("Le voleur ne peut être volé"); }
        }

        public override DistrictType? AssociatedDistrictType => null;

        public override Spell Spell { get; set; }

    }
}
