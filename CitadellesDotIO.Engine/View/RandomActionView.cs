using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using CitadellesDotIO.Engine.Targets;
using CitadellesDotIO.Enums.TurnChoices;
using CitadellesDotIO.Extensions;
using CitadellesDotIO.Engine.Characters;
using CitadellesDotIO.Engine.Districts;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.View
{
    /// <summary>
    /// Classe héritant de IView proposant des réponses aléatoires aux comportements attendus
    /// </summary>
    public class RandomActionView : IView
    {
        public Task<Character> PickCharacter(List<Character> characters)
            => Task.FromResult(characters[RandomNumberGenerator.GetInt32(0, characters.Count)]);

        public Task<MandatoryTurnChoice> PickMandatoryTurnChoice()
            => Task.FromResult((MandatoryTurnChoice)RandomNumberGenerator.GetInt32(0, Enum.GetNames(typeof(MandatoryTurnChoice)).Length));

        public Task<UnorderedTurnChoice> PickUnorderedTurnChoice(List<UnorderedTurnChoice> availableChoices)
            => Task.FromResult(availableChoices.Count != 1 ? availableChoices[Dice.Roll(availableChoices.Count)] : availableChoices.Single());

        public Task<List<District>> PickDistrictsFromPool(int pickCount, List<District> pool)
            => Task.FromResult(pool.OrderBy(d => Dice.Roll(pool.Count)).Take(pickCount).ToList());

        public Task<District> PickDistrictToBuild(List<District> buildables)
            => Task.FromResult(buildables[RandomNumberGenerator.GetInt32(0, buildables.Count)]);

        public Task<ITarget> PickSpellTarget(List<ITarget> targets)
            => Task.FromResult(targets.Count != 1 ? targets[Dice.Roll(targets.Count)] : targets.Single());

        public Task<District> PickDistrictSpellSource(List<District> spellSources)
            => Task.FromResult(spellSources.Count != 1 ? spellSources[Dice.Roll(spellSources.Count)] : spellSources.Single());
    }
}