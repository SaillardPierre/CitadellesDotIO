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
            bool isReady,
            bool isCurrentKing)
        {
            Id = id;
            Name = name;
            IsHost = isHost;
            IsReady = isReady;
            IsCurrentKing = isCurrentKing;
        }
        public string Id { get; }
        public string Name { get; }
        public bool IsHost { get; }
        public bool IsReady { get; set; }
        public bool IsCurrentKing { get; }
        public CharacterDto Character { get; }
    }
}
