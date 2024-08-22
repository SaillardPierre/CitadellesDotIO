using CitadellesDotIO.DeckAssembly.EventArgs.Enums;
using CitadellesDotIO.DeckAssembly.Model;

namespace CitadellesDotIO.DeckAssembly;

public static class DragManager
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
        throw new ArgumentOutOfRangeException(nameof(DragHoverTarget));
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


    public static void SetOverlapFromIndexes(List<Card> cards, int? leftIndex, int? rightIndex)
    {
        bool anchorLeft = false;
        bool anchorRight = false;
        int BaseTransformRatio = 0;
        int transformRatio = BaseTransformRatio;

        // a gauche
        if (leftIndex.HasValue)
        {
            int zIndex = CardParameters.DraggedCardZIndex - (leftIndex.Value + 1);
            transformRatio = transformRatio + leftIndex.Value;
            for (int i = 0; i != leftIndex + 1; i++)
            {
                Card target = cards[i];
                if (!target.IsDragged)
                {
                    target.Reset();
                    target.ZIndex = zIndex;
                    if (!anchorLeft)
                    {
                        target.Transform = new(transformRatio * 20, 0);
                    }
                    transformRatio--;
                }
                zIndex++;
            }
            cards[leftIndex.Value].IsDirectNeighbour = true;
        }




        // a droite
        if (rightIndex.HasValue)
        {
            transformRatio = BaseTransformRatio;
            int zIndex = CardParameters.DraggedCardZIndex;
            for (int i = rightIndex.Value; i < cards.Count; i++)
            {
                zIndex--;
                Card target = cards[i];
                if (!target.IsDragged)
                {
                    target.Reset();
                    target.ZIndex = zIndex;
                    if (!anchorRight)
                    {
                        target.Transform = new(transformRatio * (-20), 0);
                    }
                    transformRatio++;
                }
            }
            cards[rightIndex.Value].IsDirectNeighbour = true;
        }
    }
}
