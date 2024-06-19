using CitadellesDotIO.DeckAssembly.Model;
using Microsoft.JSInterop;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CitadellesDotIO.DeckAssembly;

public class DragManager
{
    public static Position UpdatePositionFromMovement(Position itemPosition, Position itemMovementPosition)
        => new Position()
        {
            X = itemPosition.X + itemMovementPosition.X,
            Y = itemPosition.Y + itemMovementPosition.Y
        };

    public static int GetFutureIndex(Position selfCoordinates, IEnumerable<Position> coordinates)
    {
        // Si le pointeur a des coordonnées superieures en x ou y, on est + vers la droite
        int index = 0;
        foreach (Position coord in coordinates)
        {
            if (coord.X < selfCoordinates.X) index++;
        }
        return index;
    }
}
