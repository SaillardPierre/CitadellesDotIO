
using System.Security.Cryptography;

namespace CitadellesDotIO.Controllers
{
    public static class Dice
    {
        public static int Roll(int diceSize) => RandomNumberGenerator.GetInt32(0, diceSize);
    }
}
