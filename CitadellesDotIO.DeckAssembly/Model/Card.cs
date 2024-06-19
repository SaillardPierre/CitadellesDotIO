namespace CitadellesDotIO.DeckAssembly.Model;

public class Card
{
    public int Id { get; set; }
    public Position Position { get; set; }
    public Card(int id)
    {
        Id = id;
    }
}
