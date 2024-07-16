using CitadellesDotIO.DeckAssembly.EventArgs;
using CitadellesDotIO.DeckAssembly.EventArgs.DraggableEventArgs;
using CitadellesDotIO.DeckAssembly.EventArgs.Enums;
using CitadellesDotIO.DeckAssembly.Model;

namespace CitadellesDotIO.DeckAssembly;

public class DragManager
{
    public static int? GetFutureIndex(DragMoveEventArgs args)
    {
        if (args.DragHoverTarget == DragHoverTarget.None) return default(int?);

        if (args.DragHoverTarget == DragHoverTarget.Self ||
            args.DragHoverTarget == DragHoverTarget.Target)
        {

            int index = 0;
            foreach (Position coord in args.TargetNeighboursPositions)
            {
                if (coord.X < args.DraggablePosition.X) index++;
            }
            return index;
        }
        throw new ArgumentOutOfRangeException(nameof(DragHoverTarget) + "invalid");
    }

    public static int? GetFutureIndex(DraggableMoveEventArgs args)
    {
        if (args.DragHoverTarget == DragHoverTarget.None) return default(int?);

        if (args.DragHoverTarget == DragHoverTarget.Self ||
            args.DragHoverTarget == DragHoverTarget.Target)
        {

            int index = 0;
            foreach (Position coord in args.TargetNeighboursPositions)
            {
                if (coord.X < args.DraggablePosition.X) index++;
            }
            return index;
        }
        throw new ArgumentOutOfRangeException(nameof(DragHoverTarget) + "invalid");
    }
}
