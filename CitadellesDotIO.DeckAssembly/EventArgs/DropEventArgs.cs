using CitadellesDotIO.DeckAssembly.EventArgs.Enums;

namespace CitadellesDotIO.DeckAssembly.EventArgs;

public class DropEventArgs
{
    public DropEventSource DropEventSource { get; set; }
    public string Destination { get; set; }
}
