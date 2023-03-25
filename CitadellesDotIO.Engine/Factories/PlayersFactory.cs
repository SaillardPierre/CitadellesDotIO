using CitadellesDotIO.Engine;
using System.Collections.Generic;

namespace CitadellesDotIO.Engine.Factories
{
    public static class PlayersFactory
    {
        public static IEnumerable<Player> BuddiesPlayerList(int buddiesCount)
        {
            List<string> playerNames = new List<string>() { "Pierre", "Thomas", "Ryan", "Maze", "Vincent", "Amélie", "Paul" };
            for (int i = 0; i < buddiesCount; i++)
            {
               yield return new Player(playerNames[i]);
            }
        }
    }
}
