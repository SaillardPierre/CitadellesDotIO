using System.Collections.Generic;
using CitadellesDotIO.Engine.Targets;
using CitadellesDotIO.Enums.TurnChoices;
using CitadellesDotIO.Engine.Characters;
using CitadellesDotIO.Engine.Districts;

namespace CitadellesDotIO.Engine.View
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