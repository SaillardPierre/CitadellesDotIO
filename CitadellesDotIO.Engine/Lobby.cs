﻿using CitadellesDotIO.Engine.Factories;
using System;
using System.Collections.Generic;

namespace CitadellesDotIO.Engine
{
    public class Lobby
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Player> Players { get; set; }
        public GameParameters Parameters { get; set; }
        public Game? Game { get; set; }        
        public Lobby(string name, GameParameters? parameters = null)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Name = name;
            this.Players = new List<Player>();
            this.Parameters = parameters ?? new GameParameters()
            {
                DistrictsDeckName = nameof(DeckFactory.VanillaDistrictsDeck),
                CharactersListName = nameof(CharactersFactory.VanillaCharactersList),
                DistrictThreshold = 7,
                ApplyKingShuffleRule = true
            };
        }
    }
}