namespace CitadellesDotIO.DeckAssembly.Model
{
    public class Position
    {
        public float X { get; set; } = default;
        public float Y { get; set; } = default;
       
        public Position()
        {
        }

        public Position(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}
