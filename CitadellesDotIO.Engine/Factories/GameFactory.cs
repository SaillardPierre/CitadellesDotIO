﻿using CitadellesDotIO.Engine.Factories;
using CitadellesDotIO.Engine.Hubs;
using CitadellesDotIO.Engine.View;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;

namespace CitadellesDotIO.Engine.Factory
{
    public static class GameFactory
    {
        public static Game VanillaGame(List<Player> players, IView view = null)
        {
            return new Game(
                players,
                CharactersFactory.VanillaCharactersList,
                DeckFactory.VanillaDistrictsDeck(),
                Guid.NewGuid().ToString("n"));
        }
        public static Game VanillaGame(string gameName, IHubContext<GameHub> gameHubContext)
        {
            return new Game(
                    gameName,
                    CharactersFactory.VanillaCharactersList,
                    DeckFactory.VanillaDistrictsDeck(),
                    gameHubContext,
                    true, 7
                );
        }
    }
}
