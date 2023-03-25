using CitadellesDotIO.Engine.Characters;
using CitadellesDotIO.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.DTOs
{
    public class GameDto
    {
        public GameDto(
            string id,
            GameState gameState, 
            string name,
            string secret,
            ReadOnlyCollection<PlayerDto> players, 
            ReadOnlyCollection<CharacterDto> characters,
            bool isStartable)
        {
            Id = id;
            GameState = gameState;
            Name = name;
            Secret = secret;
            Players = players;
            Characters = characters;
            IsStartable = isStartable;
        }

        public string Id { get; }
        public GameState GameState { get; }
        public string Name { get; }
        public string Secret { get; }
        public ReadOnlyCollection<PlayerDto> Players { get; }
        public ReadOnlyCollection<CharacterDto> Characters { get; }
        public bool IsStartable { get; }
    }
}
