using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.DTOs
{
    public class PlayerDto
    {
        public PlayerDto(
            string id,
            string name,
            bool isHost,
            bool isCurrentKing)
        {
            Id = id;
            Name = name;
            IsHost = isHost;
            IsCurrentKing = isCurrentKing;
        }
        public string Id { get; }
        public string Name { get; }
        public bool IsHost { get; }
        public bool IsCurrentKing { get; }
        public CharacterDto Character { get; }
    }
}
