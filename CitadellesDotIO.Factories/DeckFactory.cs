using CitadellesDotIO.Model.Districts;
using CitadellesDotIO.Model;

namespace CitadellesDotIO.Factories
{
    public static class DeckFactory
    {
        public static List<District> VanillaDistrictsDeck()
        {
            List<District> deck = new()
            {
                // Ajout des cartes uniques au deck
                new CourtOfMiracles(),
                new Laboratory(),
                new Manufacture(),
                new Observatory(),
                new Graveyard(),
                new Library(),
                new MagicAcademy(),
                new University(),
                new DragonPort()
            };

            // Ajout des cartes à deux exemplaires au deck
            for (int i = 0; i<2 ; i++)
            {
                deck.Add(new Cathedral());
                deck.Add(new Palace());
                deck.Add(new CityHall());
                deck.Add(new Fortress());
                deck.Add(new Dungeon());
            }

            // Ajout des cartes à trois exemplaires au deck
            for (int i = 0; i < 3; i++)
            {
                deck.Add(new Temple());
                deck.Add(new Monastery());
                deck.Add(new Shop());
                deck.Add(new Counter());
                deck.Add(new Harbor());
                deck.Add(new Watchtower());
                deck.Add(new Prison());
                deck.Add(new Barracks());
            }
            // Ajout des cartes à quatre exemplaires au deck
            for (int i = 0; i < 4; i++)
            {
                deck.Add(new Church());
                deck.Add(new Castle());
                deck.Add(new Market());

            }

            // Ajout des cartes à cinq exemplaires au deck
            for (int i = 0; i < 5; i++)
            {
                deck.Add(new Manor());
                deck.Add(new Tavern());
            }
            
            return deck;
        }

        public static IEnumerable<District> Laboratories()
        {
            for (int i=0;i<500;i++)
            {
                yield return new Laboratory();
            }
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
