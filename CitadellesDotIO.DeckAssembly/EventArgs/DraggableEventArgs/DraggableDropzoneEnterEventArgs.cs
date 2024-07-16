using CitadellesDotIO.DeckAssembly.EventArgs.Enums;

namespace CitadellesDotIO.DeckAssembly.EventArgs.DraggableEventArgs
{
    public class DraggableDropzoneEnterEventArgs
    {
        public DragHoverTarget DragHoverTarget { get; set; }
        public string DropzoneHoverSource { get; set; }
    }
}
