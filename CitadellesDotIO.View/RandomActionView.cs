using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using CitadellesDotIO.Enums;
using CitadellesDotIO.Enums.TurnChoices;
using CitadellesDotIO.Extensions;
using CitadellesDotIO.Model;
using CitadellesDotIO.Model.Districts;

namespace CitadellesDotIO.View
{
    /// <summary>
    /// Classe héritant de IView proposant des réponses aléatoires aux comportements attendus
    /// </summary>
    public class RandomActionView : IView
    {
        public Character PickCharacter(List<Character> characters)
            => characters[RandomNumberGenerator.GetInt32(0, characters.Count)];

        public MandatoryTurnChoice PickMandatoryTurnChoice()
            => (MandatoryTurnChoice)RandomNumberGenerator.GetInt32(0, Enum.GetNames(typeof(MandatoryTurnChoice)).Length);

        public UnorderedTurnChoice PickUnorderedTurnChoice(List<UnorderedTurnChoice> availableChoices)
            => availableChoices.Count != 1 ? availableChoices[Dice.Roll(availableChoices.Count)] : availableChoices.Single();

        public List<District> PickDistrictsFromPool(int pickCount, List<District> pool)
            => pool.OrderBy(d => Dice.Roll(pool.Count)).Take(pickCount).ToList();

        public District PickDistrictToBuild(List<District> buildables)
        {
            if (buildables.Count == 0)
            {
                return null;
            }
            else if (buildables.Count == 1)
            {
                return buildables.Single();
            }
            return buildables[RandomNumberGenerator.GetInt32(0, buildables.Count)]; 
        }

        public ITarget PickSpellTarget(List<ITarget> targets)
            => targets.Count != 1 ? targets[Dice.Roll(targets.Count)] : targets.Single();
    }
}