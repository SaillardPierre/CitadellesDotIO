using Stride.Engine;

namespace CitadellesDotIO.Stride
{
    class CitadellesDotIO_StrideApp
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.Run();
            }
        }
    }
}
