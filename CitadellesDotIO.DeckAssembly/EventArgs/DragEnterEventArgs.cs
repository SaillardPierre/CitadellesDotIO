using CitadellesDotIO.DeckAssembly.EventArgs.Enums;

namespace CitadellesDotIO.DeckAssembly.EventArgs
{
    public class DragEnterEventArgs
    {
        public string PickSource { get; set; }
        public DragHoverTarget DragHoverTarget { get; set; }
        public string HoverSource{ get; set; }
    }
}
