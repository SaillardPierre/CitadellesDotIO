using CitadellesDotIO.DeckAssembly.EventArgs.Enums;
using CitadellesDotIO.DeckAssembly.Model;

namespace CitadellesDotIO.DeckAssembly.EventArgs.DraggableEventArgs
{
    public class DraggableMoveEventArgs : DraggableBaseEventArgs
    {
        public DragHoverTarget DragHoverTarget { get; set; }
        public Position DraggablePosition { get; set; }
        public Position DragMoveDirection { get; set; }
        public List<Position> TargetNeighboursPositions { get; set; }       
    }
}
