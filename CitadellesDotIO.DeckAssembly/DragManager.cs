using CitadellesDotIO.DeckAssembly.EventArgs.Enums;
using CitadellesDotIO.DeckAssembly.Model;

namespace CitadellesDotIO.DeckAssembly;

public class DragManager
{    
    public static int? GetFutureIndex(DragHoverTarget dragHoverTarget, Position? testedPosition, IEnumerable<Position?>? relativePositions)
    {
        ArgumentNullException.ThrowIfNull(testedPosition);

        if (dragHoverTarget == DragHoverTarget.None) return default(int?);

        if (dragHoverTarget == DragHoverTarget.Self ||
            dragHoverTarget == DragHoverTarget.Target)
        {
            ArgumentNullException.ThrowIfNull(relativePositions);
            int index = 0;
            foreach (Position? pos in relativePositions)
            {
                ArgumentNullException.ThrowIfNull(pos);
                if (pos.X < testedPosition.X) index++;
            }
            return index;
        }
        throw new ArgumentOutOfRangeException(nameof(DragHoverTarget) + "invalid");
    }
    public static Tuple<int?, int?> ComputeLeftRightIndexes(int splitIndex, int itemCount, DragHoverTarget dragHoverTarget, int? draggedItemIndex = default)
    {
        int? leftIndex = default;
        int? rightIndex = default;

        if (itemCount == 0) return new(leftIndex, rightIndex);

        bool isFirst = splitIndex == 0;
        bool isLast;

        if (dragHoverTarget == DragHoverTarget.Target)
        {
            isLast = splitIndex == itemCount;
            if (!isFirst)
            {
                leftIndex = splitIndex - 1;
            }
            if (!isLast)
            {
                rightIndex = splitIndex;
            }
        }
        else if (dragHoverTarget == DragHoverTarget.Self)
        {
            isLast = splitIndex == itemCount - 1;
            if (!isFirst)
            {
                leftIndex = splitIndex - 1;
                // l'item est a droite de son origine
                if (draggedItemIndex.HasValue && draggedItemIndex < splitIndex)
                {
                    leftIndex++;
                }
            }
            if (!isLast)
            {
                rightIndex = splitIndex + 1;
                // l'item est a gauche de son origine
                if (draggedItemIndex.HasValue && draggedItemIndex > splitIndex)
                {
                    rightIndex--;
                }
            }
        }

        return new(leftIndex, rightIndex);
    }
}
