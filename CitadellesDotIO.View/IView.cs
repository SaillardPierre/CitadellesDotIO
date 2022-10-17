using System.Collections.Generic;
using CitadellesDotIO.Enums.TurnChoices;
using CitadellesDotIO.Model.Characters;
using CitadellesDotIO.Model.Districts;
using CitadellesDotIO.Model.Targets;

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