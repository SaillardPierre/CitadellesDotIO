using CitadellesDotIO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Factories
{
    public static class PlayersFactory
    {
        public static IEnumerable<Player> BuddiesPlayerList(int buddiesCount)
        {
            List<string> playerNames = new List<string>() { "Pierre", "Thomas", "Ryan", "Maze", "Vincent", "Danaé", "Amélie", "Paul" };
            for (int i = 0; i < buddiesCount; i++)
            {
               yield return new Player(playerNames[i]);
            }
        }
    }
}
