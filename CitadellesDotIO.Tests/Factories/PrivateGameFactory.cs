using CitadellesDotIO.Engine;
using CitadellesDotIO.Engine.Factories;
using PrivateObjectExtension;
using System;
using System.Linq;

namespace CitadellesDotIO.Tests.Factories
{
    public static class PrivateGameFactory
    {
        public static Tuple<Game, PrivateObject> GetPrivateGame(int playerCount) 
        {
            Game game = Engine.Factory.GameFactory.VanillaGame(PlayersFactory.BuddiesPlayerList(playerCount).ToList());
            PrivateObject privateGame = new (game);
            return Tuple.Create(game, privateGame);
        }
    }
}
