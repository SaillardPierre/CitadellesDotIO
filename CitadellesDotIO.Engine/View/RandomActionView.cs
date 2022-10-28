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
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CitadellesDotIO.Engine.View
{
    /// <summary>
    /// Classe héritant de IView proposant des réponses aléatoires aux comportements attendus
    /// </summary>
    [JsonObject(IsReference = true)]
    public class RandomActionView : IView
    {
        public Task<Character> PickCharacter(List<Character> characters)
        {
            Character picked = characters[RandomNumberGenerator.GetInt32(0, characters.Count)];
            Console.WriteLine($"{this.Player.Name} picked {picked.Name} as Character");
            return Task.FromResult(picked);
        }

        public Task<MandatoryTurnChoice> PickMandatoryTurnChoice()
        {
            MandatoryTurnChoice picked = (MandatoryTurnChoice)RandomNumberGenerator.GetInt32(0, Enum.GetNames(typeof(MandatoryTurnChoice)).Length);
            Console.WriteLine($"{this.Player.Name} picked {picked.ToString()} as MandatoryTurnChoice");
            return Task.FromResult(picked);
        }        

        public Task<UnorderedTurnChoice> PickUnorderedTurnChoice(List<UnorderedTurnChoice> availableChoices)
        {
            UnorderedTurnChoice picked = availableChoices.Count != 1 ? availableChoices[Dice.Roll(availableChoices.Count)] : availableChoices.Single();
            Console.WriteLine($"{this.Player.Name} picked {picked.ToString()} as UnorderedTurnChoice");
            return Task.FromResult(picked);
        }

        public Task<List<District>> PickDistrictsFromPool(int pickCount, List<District> pool)
        {
            List<District> pickeds = pool.OrderBy(d => Dice.Roll(pool.Count)).Take(pickCount).ToList();
            string message = $"{this.Player.Name} picked";
            pickeds.ForEach(p => message += $" {p.Name}");
            Console.WriteLine($"{message} from DistrictPool");
            return Task.FromResult(pickeds); 
        }

        public Task<ITarget> PickSpellTarget(List<ITarget> targets)
        {
            ITarget picked = targets.Count != 1 ? targets[Dice.Roll(targets.Count)] : targets.Single();
            Console.WriteLine($"{this.Player.Name} picked {picked.ToString()} as Target");
            return Task.FromResult(picked);
        }

        public Task<District> PickDistrict(List<District> districts)
        {
            District picked = districts[RandomNumberGenerator.GetInt32(0, districts.Count)];
            Console.WriteLine($"{this.Player.Name} picked {picked.ToString()} as District");
            return Task.FromResult(picked);
        }

        public async Task DisplayRanking(Player player, int rank)
        {
            Console.WriteLine($"{rank} : {player.Name} with {player.Score} points and {player.City.Count} districts");
            player.City.ToList().ForEach(d =>
            {
                Console.WriteLine($"\t {d.Name} {d.ScoreValue}");
            });
            await Task.CompletedTask;
        }


        public void SetPlayer(Player player)
        {
            this.Player = player;
        }
        public Player Player { get; set; }
    }
}