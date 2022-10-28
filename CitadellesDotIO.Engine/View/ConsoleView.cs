using CitadellesDotIO.Engine.Characters;
using CitadellesDotIO.Engine.Districts;
using CitadellesDotIO.Engine.Targets;
using CitadellesDotIO.Enums.TurnChoices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.View
{
    [JsonObject(IsReference = true)]
    public class ConsoleView : IView
    {
        public void SetPlayer(Player player)
        {
            this.Player = player;
        }
        public Player Player{ get; set; }
        private async Task<int> GetIndexAsync(List<dynamic> items)
        {
            int i = 0;
            foreach (object item in items)
            {
                string name = (item is string) ? item as string : item.GetType().GetProperties().Single(p => p.Name == "Name").GetValue(item).ToString();
                Console.WriteLine($"[{i}] {name}");
                i++;
            }
            string input = await Task.Run(() => Console.ReadKey(true).KeyChar.ToString());
            if (int.TryParse(input, out int index) && index < items.Count && index > -1)
            {
                return index;
            }
            else
            {
                Console.WriteLine($"Please enter a valid choice index (between 0 and {items.Count})");
                return await this.GetIndexAsync(items);
            }
        }

        public async Task<District> PickDistrict(List<District> districts)
        {
            Console.WriteLine("Pick District by index :");
            return districts[await this.GetIndexAsync(new(districts))];
        }

        public async Task<Character> PickCharacter(List<Character> characters)
        {
            Console.WriteLine($"Start of {this.Player.Name}'s Turn to pick his character !");
            Console.WriteLine("Pick Character by index :");
            return characters[await this.GetIndexAsync(new(characters))];
        }

        public async Task<List<District>> PickDistrictsFromPool(int pickCount, List<District> pool)
        {
            Console.WriteLine($"Pick {pickCount} Districts from Pool");
            List<District> pickeds = new List<District>();
            while (pickeds.Count < pickCount)
            {
                District picked = await this.PickDistrict(pool);
                pool.Remove(picked);
                pickeds.Add(picked);
            }
            return pickeds;
        }

        public async Task<MandatoryTurnChoice> PickMandatoryTurnChoice()
        {
            Console.WriteLine($"Start of {this.Player.Name}'s Turn, playing {this.Player.Character.Name} !"); 
            Console.WriteLine("Pick Mandatory turn choice by index :");
            return (MandatoryTurnChoice)await this.GetIndexAsync(new(Enum.GetNames(typeof(MandatoryTurnChoice))));
        }

        public async Task<ITarget> PickSpellTarget(List<ITarget> targets)
        {
            Console.WriteLine("Pick Spell Target by index :");
            return targets[await this.GetIndexAsync(new(targets))];
        }

        public async Task<UnorderedTurnChoice> PickUnorderedTurnChoice(List<UnorderedTurnChoice> availableChoices)
        {
            Console.WriteLine("Pick Unordered turn choice by index :");            
            return availableChoices[await this.GetIndexAsync(new(availableChoices.Select(ac=>ac.ToString())))];
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
    }
}
