using CitadellesDotIO.DeckAssembly.EventArgs.Enums;
using CitadellesDotIO.DeckAssembly.Model;

namespace CitadellesDotIO.DeckAssembly.EventArgs;

public class DragEnterEventArgs
{
    public DragEnterEventSource Source { get; set; }
    public int PickIndex { get; set; }
    public Position DraggablePosition { get; set; }
    public List<Position> NeighboursPositions { get; set; }
}

