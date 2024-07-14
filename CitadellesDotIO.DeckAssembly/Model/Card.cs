namespace CitadellesDotIO.DeckAssembly.Model;

public class Card
{
    public int Id { get; set; }
    public int Index { get; set; }
    public int? ZIndex { get; set; }
    public string? MarginLeft { get; set; }
    public string? MarginRight{ get; set; }
    public Position Transform { get; set; }
    public bool IsDragged { get; set; }

    /// <summary>
    /// Est pour 'linstant 'neighbour'
    /// </summary>
    public bool IsDirectNeighbour { get; set; }
    public Card(int id)
    {
        Id = id;
        Transform = new Position();
    }

    public void Reset()
    {
        Transform = new Position();
        IsDragged = false;
        IsDirectNeighbour = false;
        MarginLeft = null;
        MarginRight = null;
        ZIndex = null;
    }

    public void UpdatePosition(Position movementPosition)
    {
        IsDragged = true;
        Transform.X += movementPosition.X;
        Transform.Y += movementPosition.Y;
    }
}
