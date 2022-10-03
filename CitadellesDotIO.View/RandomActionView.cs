using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using CitadellesDotIO.Enums;
using CitadellesDotIO.Enums.TurnChoices;
using CitadellesDotIO.Model;
using CitadellesDotIO.Model.Characters;
using CitadellesDotIO.Model.Districts;

namespace CitadellesDotIO.View
{
    public class RandomActionView : IView
    {
        public Character PickCharacter(List<Character> characters)
            => characters[RandomNumberGenerator.GetInt32(0, characters.Count)];

        public MandatoryTurnChoice PickMandatoryTurnChoice()
            => (MandatoryTurnChoice)RandomNumberGenerator.GetInt32(0, Enum.GetNames(typeof(MandatoryTurnChoice)).Length);

        public UnorderedTurnChoice PickUnorderedTurnChoice(List<UnorderedTurnChoice> availableChoices)
            => (UnorderedTurnChoice)RandomNumberGenerator.GetInt32(0, availableChoices.Count);

        public List<District> PickDistrictsFromPool(int pickCount, List<District> pool)
        {
            return pool.OrderBy(d => RandomNumberGenerator.GetInt32(0, pool.Count)).Take(pickCount).ToList();
        }
    }
}