using CitadellesDotIO.DeckAssembly.EventArgs.Enums;

namespace CitadellesDotIO.DeckAssembly.EventArgs.DraggableEventArgs
{
    public class DraggableDropEventArgs : DraggableBaseEventArgs
    {
        public DropEventSource DropEventSource { get; set; }
        public string DestinationDropzone { get; set; }
    }
}
