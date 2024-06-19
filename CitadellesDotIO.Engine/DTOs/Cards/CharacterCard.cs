namespace CitadellesDotIO.Engine.DTOs.Cards
{
    public class CharacterCard : Card
    {
        public CharacterDto Character { get; set; }
        public PlayerDto Player { get; set; }
        public Status Status { get; set; }
        public CharacterCard(int id, CharacterDto character) : base(id)
        {
            Status = Status.Left;
            Character = character;
        }
        public CharacterCard(int id, CharacterDto character, PlayerDto player) : this(id, character)
        {
            Player = player;
        }

    }


}
