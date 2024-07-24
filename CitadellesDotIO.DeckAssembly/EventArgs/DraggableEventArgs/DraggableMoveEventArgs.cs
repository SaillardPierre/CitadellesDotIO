using CitadellesDotIO.DeckAssembly.EventArgs.Enums;
using CitadellesDotIO.DeckAssembly.Model;

namespace CitadellesDotIO.DeckAssembly.EventArgs.DraggableEventArgs
{
    public class DraggableMoveEventArgs : DraggableBaseEventArgs
    {
        public Position DragMoveDirection { get; set; }
        public DragHoverTarget DragHoverTarget { get; set; }
    }
}
