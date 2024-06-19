using CitadellesDotIO.DeckAssembly.Model;
using Microsoft.JSInterop;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CitadellesDotIO.DeckAssembly;

public class DragManager
{
    public static async ValueTask<string> OnCardMoveWithString(float dx, float dy, string initialTransform)
    {
        float x = 0;
        float y = 0;

        if (!string.IsNullOrWhiteSpace(initialTransform))
        {
            string pattern = @"translate\(([^,]+)px,\s*([^,]+)px\)";

            Match match = Regex.Match(initialTransform, pattern);

            if (match.Success)
            {
                x = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
                y = float.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);
            }
        }

        x += dx;
        y += dy;

        return $"translate({x}px, {y}px)";
    }
    // ne pas faire en static mais on verra bref

    public static void OnCardMoveWithPosition(Position position, float dx, float dy)
    {
        position.X += dx;
        position.Y += dy; 
    }

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
