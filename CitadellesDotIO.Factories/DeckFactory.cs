using CitadellesDotIO.Model.Districts;
using CitadellesDotIO.Model;

namespace CitadellesDotIO.Factories
{
    public static class DeckFactory
    {
        public static Deck<District> VanillaDistrictsDeck()
        {
            Deck<District> deck = new();

            // Ajout des cartes uniques au deck
            deck.Enqueue(new CourtOfMiracles());
            deck.Enqueue(new Laboratory());
            deck.Enqueue(new Manufacture());
            deck.Enqueue(new Observatory());
            deck.Enqueue(new Graveyard());
            deck.Enqueue(new Library());
            deck.Enqueue(new MagicAcademy());
            deck.Enqueue(new University());
            deck.Enqueue(new DragonPort());
            
            // Ajout des cartes à deux exemplaires au deck
            for (int i = 0; i<2 ; i++)
            {
                deck.Enqueue(new Cathedral());
                deck.Enqueue(new Palace());
                deck.Enqueue(new CityHall());
                deck.Enqueue(new Fortress());
                deck.Enqueue(new Dungeon());
            }

            // Ajout des cartes à trois exemplaires au deck
            for (int i = 0; i < 3; i++)
            {
                deck.Enqueue(new Temple());
                deck.Enqueue(new Monastery());
                deck.Enqueue(new Shop());
                deck.Enqueue(new Counter());
                deck.Enqueue(new Harbor());
                deck.Enqueue(new Watchtower());
                deck.Enqueue(new Prison());
                deck.Enqueue(new Barracks());
            }
            // Ajout des cartes à quatre exemplaires au deck
            for (int i = 0; i < 4; i++)
            {
                deck.Enqueue(new Church());
                deck.Enqueue(new Castle());
                deck.Enqueue(new Market());

            }

            // Ajout des cartes à cinq exemplaires au deck
            for (int i = 0; i < 5; i++)
            {
                deck.Enqueue(new Manor());
                deck.Enqueue(new Tavern());
            }
            
            return deck;
        }

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
