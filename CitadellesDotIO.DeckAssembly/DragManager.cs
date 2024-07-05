using CitadellesDotIO.DeckAssembly.Model;

namespace CitadellesDotIO.DeckAssembly;

public class DragManager
{
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
