using CitadellesDotIO.DeckAssembly.EventArgs.Enums;

namespace CitadellesDotIO.DeckAssembly.EventArgs;

public class DropEventArgs
{
    public int PickIndex { get; set; }
    public string PickSource { get; set; }
    public DropEventSource DropEventSource { get; set; }
    public string Destination { get; set; }
}
