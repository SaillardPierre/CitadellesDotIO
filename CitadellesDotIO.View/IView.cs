using System.Collections.Generic;
using CitadellesDotIO.Enums.TurnChoices;
using CitadellesDotIO.Model;
using CitadellesDotIO.Model.Districts;

namespace CitadellesDotIO.View
{
    public interface IView
    {
        public Character PickCharacter(List<Character> characters);

        public MandatoryTurnChoice PickMandatoryTurnChoice();

        public UnorderedTurnChoice PickUnorderedTurnChoice(List<UnorderedTurnChoice> availableChoices);

        public List<District> PickDistrictsFromPool(int pickCount, List<District> pool);

        public District PickDistrictToBuild(List<District> buildables);
        public ITarget PickSpellTarget(List<ITarget> targets);
        public District PickDistrictSpellSource(List<District> spellSources);
    }
}