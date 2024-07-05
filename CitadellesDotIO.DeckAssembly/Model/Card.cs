namespace CitadellesDotIO.DeckAssembly.Model;

public class Card
{
    public int Id { get; set; }
    public int Index { get; set; }
    public Position Position { get; set; }
    public bool IsDragged { get; set; }

    /// <summary>
    /// Est pour 'linstant 'neighbour'
    /// </summary>
    public bool IsOverlapped { get; set; }
    public Card(int id)
    {
        Id = id;
        Position = new Position();
    }

    public void Reset()
    {
        Position = new Position();
        IsDragged = false;
        IsOverlapped = false;
    }

    public void UpdatePosition(Position movementPosition)
    {
        IsDragged = true;
        Position.X += movementPosition.X;
        Position.Y += movementPosition.Y;
    }
}
