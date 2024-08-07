﻿namespace CitadellesDotIO.DeckAssembly.Model;

public class Card
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
        Transform = new Position();
        IsDragged = false;
        IsHovered = false;
        IsDirectNeighbour = false;
        ZIndex = null;
    }

    public void UpdatePosition(Position movementPosition)
    {
        Transform.X += movementPosition.X;
        Transform.Y += movementPosition.Y;
    }
}
