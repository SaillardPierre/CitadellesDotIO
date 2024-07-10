namespace CitadellesDotIO.DeckAssembly.Model;

public class Card
{
    public int Id { get; set; }
    public int Index { get; set; }
    public Position Transform { get; set; }
    public bool IsDragged { get; set; }

    /// <summary>
    /// Est pour 'linstant 'neighbour'
    /// </summary>
    public bool IsOverlapped { get; set; }
    public Card(int id)
    {
        Id = id;
        Transform = new Position();
    }

    public void Reset()
    {
        Transform = new Position();
        IsDragged = false;
        IsOverlapped = false;
    }

    public void UpdatePosition(Position movementPosition)
    {
        IsDragged = true;
        Transform.X += movementPosition.X;
        Transform.Y += movementPosition.Y;
    }
}
