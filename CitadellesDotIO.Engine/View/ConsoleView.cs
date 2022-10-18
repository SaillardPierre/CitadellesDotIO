using CitadellesDotIO.Engine.Characters;
using CitadellesDotIO.Engine.Districts;
using CitadellesDotIO.Engine.Targets;
using CitadellesDotIO.Enums.TurnChoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.View
{
    public class ConsoleView : IView
    {

        private async Task<District> PickDistrict(List<District> districts)
        {
            int i = 0;
            Console.WriteLine("Pick District to by index :");
            foreach (District district in districts)
            {
                Console.WriteLine($"[{i}] {district.Name}");
                i++;
            }
            string input = await Task.Run(() => Console.ReadKey(true).KeyChar.ToString());
            if (int.TryParse(input, out int index) && index < districts.Count && index > -1)
            {
                return districts[index];
            }
            else
            {
                Console.WriteLine("Please enter a valid choice index");
                return await this.PickDistrictToBuild(districts);
            }
        }

        public async Task<Character> PickCharacter(List<Character> characters)
        {
            int i = 0;
            Console.WriteLine("Pick character by index :");
            characters.ForEach(c =>
            {
                Console.WriteLine($"[{i}] {c.Name}");
                i++;
            });
            string input = await Task.Run(() => Console.ReadKey(true).KeyChar.ToString());
            if (int.TryParse(input, out int index) && index < characters.Count && index > -1)
            {
                return characters[index];                
            }
            Console.WriteLine("Please enter a valid character index");
            return await this.PickCharacter(characters);
        }

        public async Task<List<District>> PickDistrictsFromPool(int pickCount, List<District> pool)
        {
            List<District> pickeds = new List<District>();
            while (pickeds.Count < pickCount)
            {
                District picked = await this.PickDistrict(pool);
                pool.Remove(picked);
                pickeds.Add(picked);
            }
            return pickeds;
        }

        public async Task<District> PickDistrictSpellSource(List<District> spellSources)
        {
            int i = 0;
            Console.WriteLine("Pick DistrictSpellSource by index :");
            foreach (District district in spellSources)
            {
                Console.WriteLine($"[{i}] {district.Name}");
                i++;
            }
            string input = await Task.Run(() => Console.ReadKey(true).KeyChar.ToString());
            if (int.TryParse(input, out int index) && index < spellSources.Count && index > -1)
            {
                return spellSources[index];
            }
            else
            {
                Console.WriteLine("Please enter a valid choice index");
                return await this.PickDistrictToBuild(spellSources);
            }
        }

        public async Task<District> PickDistrictToBuild(List<District> buildables)
        {
            return await this.PickDistrict(buildables);
        }

        public async Task<MandatoryTurnChoice> PickMandatoryTurnChoice()
        {
            int i = 0;
            Console.WriteLine("Pick Mandatory turn choice by index :");
            foreach (string choice in Enum.GetNames(typeof(MandatoryTurnChoice)))
            {
                Console.WriteLine($"[{i}] {choice}");
                i++;
            }
            string input = await Task.Run(() => Console.ReadKey(true).KeyChar.ToString());
            if (int.TryParse(input, out int index) && index < 2 && index > -1)
            {
                return (MandatoryTurnChoice)index;
            }
            else
            {
                Console.WriteLine("Please enter a valid choice index");
                return await this.PickMandatoryTurnChoice();
            }
        }

        public async Task<ITarget> PickSpellTarget(List<ITarget> targets)
        {
            int i = 0;
            Console.WriteLine("Pick Spell Target by index :");
            foreach (ITarget target in targets)
            {
                Console.WriteLine($"[{i}] {target.Name}");
                i++;
            }
            string input = await Task.Run(() => Console.ReadKey(true).KeyChar.ToString());
            if (int.TryParse(input, out int index) && index < targets.Count && index > -1)
            {
                return targets[index];
            }
            else
            {
                Console.WriteLine("Please enter a valid choice index");
                return await this.PickSpellTarget(targets);
            }
        }

        public async Task<UnorderedTurnChoice> PickUnorderedTurnChoice(List<UnorderedTurnChoice> availableChoices)
        {
            int i = 0;
            Console.WriteLine("Pick Unordered turn choice by index :");
            foreach (string choice in availableChoices.Select(ac=>ac.ToString()))
            {
                Console.WriteLine($"[{i}] {choice}");
                i++;
            }
            string input = await Task.Run(() => Console.ReadKey(true).KeyChar.ToString());
            if (int.TryParse(input, out int index) && index < availableChoices.Count && index > -1)
            {
                return availableChoices[index];
            }
            else
            {
                Console.WriteLine("Please enter a valid choice index");
                return await this.PickUnorderedTurnChoice(availableChoices);
            }
        }
    }
}
