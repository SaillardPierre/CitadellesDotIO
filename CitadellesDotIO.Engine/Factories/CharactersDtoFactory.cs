using CitadellesDotIO.Engine.Characters;
using CitadellesDotIO.Engine.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace CitadellesDotIO.Engine.Factories;

public static class CharactersDtoFactory
{
    public static List<CharacterDto> VanillaCharactersList()
    {
        return CharactersFactory.VanillaCharactersList.Select(x=>x.ToCharacterDto()).ToList();
    }
}
