using CitadellesDotIO.Enums;
using CitadellesDotIO.Exceptions;
using CitadellesDotIO.Model.Spells;

namespace CitadellesDotIO.Model.Characters
{
    public class Assassin : Character
    {
        protected Assassin() : base()
        {
            this.Spell = new Murder(this.Player);
        }
        public Assassin(int order) : base(order)
        {
            this.Spell = new Murder(this.Player);
        }

        #pragma warning disable CA1822 // Marquer les membres comme étant static
        public new bool IsMurdered       
        {
            get { return false; }
            set
            {
                throw new CharacterBehaviourException("L'assassin ne peut être assassiné");
            }
        }

        public new bool IsStolen
        {
            get { return false; }
            set
            {
                throw new CharacterBehaviourException("L'assassin ne peut être détroussé");
            }
        }
        #pragma warning restore CA1822 // Marquer les membres comme étant static
        public override DistrictType? AssociatedDistrictType => null;

        public override Spell Spell { get; set; }
    }
}
