using System.Collections.Generic;
using CitadellesDotIO.Model.Characters;

namespace CitadellesDotIO.View
{
    public interface IView
    {
        public Character PickCharacter(List<Character> characters);
    }
}