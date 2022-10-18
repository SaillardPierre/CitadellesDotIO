using System.Collections.Generic;
using CitadellesDotIO.Engine.Targets;
using CitadellesDotIO.Enums.TurnChoices;
using CitadellesDotIO.Engine.Characters;
using CitadellesDotIO.Engine.Districts;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.View
{
    public interface IView
    {
        public Task<Character> PickCharacter(List<Character> characters);
        public Task<MandatoryTurnChoice> PickMandatoryTurnChoice();
        public Task<UnorderedTurnChoice> PickUnorderedTurnChoice(List<UnorderedTurnChoice> availableChoices);
        public Task<List<District>> PickDistrictsFromPool(int pickCount, List<District> pool);
        public Task<ITarget> PickSpellTarget(List<ITarget> targets);
        public Task<District> PickDistrict(List<District> districts);

    }
}