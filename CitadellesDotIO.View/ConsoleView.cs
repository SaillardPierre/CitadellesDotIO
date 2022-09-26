using System;
using System.Collections.Generic;
using CitadellesDotIO.Model.Characters;

namespace CitadellesDotIO.View
{
    public class ConsoleView : IView
    {
        public Character PickCharacter(List<Character> characters)
        {
            Console.WriteLine("Characters available in deck :");
            int index = 0;
            characters.ForEach(c=>{
                Console.WriteLine(c.Name + " "+index);
            });

            Console.WriteLine("Select by index");
            if(int.TryParse(Console.ReadLine(), out int selectedIndex) && selectedIndex <= characters.Count){
                return characters[selectedIndex];
            }
            else {
                Console.WriteLine("Please select a valid index");
                return this.PickCharacter(characters);
            }
        }
    }
}