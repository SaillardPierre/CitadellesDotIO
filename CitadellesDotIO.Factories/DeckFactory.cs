using CitadellesDotIO.Model.Districts;
using CitadellesDotIO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Factories
{
    public static class DeckFactory
    {
        public static Deck<District> TestDistrictDeck(int deckSize)
        {
            Deck<District> deck = new();
            for (int i = 0; i < deckSize; i++)
            {
                deck.Enqueue(new TestDistrict());
            }

            return deck;
        }
    }
}
