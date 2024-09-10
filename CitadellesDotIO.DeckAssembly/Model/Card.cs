namespace CitadellesDotIO.DeckAssembly.Model;

public class Card : ICloneable
{
    public int Id { get; set; }
    public int Index { get; set; }
    public int? ZIndex { get; set; }
    public Position Transform { get; set; }
    public Position? Position { get; set; }
    public bool IsDragged { get; set; }
    public bool IsDirectNeighbour { get; set; }
    public bool IsHovered { get; set; }
    public Card(int id)
    {
        Id = id;
        Transform = new Position();
    }

    public void Reset()
    {
        Transform = new Position(0,0);
        IsDragged = false;
        IsHovered = false;
        IsDirectNeighbour = false;
        ZIndex = null;
    }

    public void SetDraggedState(bool isDragged)
    {
        IsDragged = isDragged;
        if (IsDragged) ZIndex = CardParameters.DraggedCardZIndex;
    }

    public void UpdatePosition(Position movementPosition)
    {
        Transform.X += movementPosition.X;
        Transform.Y += movementPosition.Y;
    }

    public object Clone()
    {
        Card card = new(Id);
        card.Index = Index;
        return card;
    }
}