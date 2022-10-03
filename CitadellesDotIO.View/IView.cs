using System.Collections.Generic;
using CitadellesDotIO.Enums.TurnChoices;
using CitadellesDotIO.Model;
using CitadellesDotIO.Model.Characters;
using CitadellesDotIO.Model.Districts;

namespace CitadellesDotIO.View
{
    public interface IView
    {
        public Character PickCharacter(List<Character> characters);

        public MandatoryTurnChoice PickMandatoryTurnChoice();

        public UnorderedTurnChoice PickUnorderedTurnChoice(List<UnorderedTurnChoice> availableChoices);

        public List<District> PickDistrictsFromPool(int pickCount, List<District> pool);

    }
}