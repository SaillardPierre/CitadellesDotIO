using CitadellesDotIO.DeckAssembly.EventArgs.Enums;
using CitadellesDotIO.DeckAssembly.Model;

namespace CitadellesDotIO.DeckAssembly.EventArgs;

public class DragMoveEventArgs
{
    public DragHoverTarget DragHoverTarget { get; set; }
    public int PickIndex { get; set; }
    public string PickSource { get; set; }
    public Position? DraggablePosition { get; set; }
    public Position? DraggableMovementPosition { get; set; }
    public List<Position>? SourceNeighboursPositions { get; set; }
    public List<Position>? TargetNeighboursPositions { get; set; }
}

