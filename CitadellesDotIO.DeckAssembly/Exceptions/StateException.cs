using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace CitadellesDotIO.DeckAssembly.Exceptions
{
    public class StateException : ArgumentNullException
    {
        public static void ThrowIfNotNull(object? argument, string? paramName = null)
        {
            if (argument is not null)
            {
                throw new ArgumentException($"The argument '{paramName}' has to be null.", paramName);
            }
        }

        public static void ThrowIfTrue(bool argument, string? paramName = null)
        {
            if (argument) throw new ArgumentException($"The argument '{paramName}' has to be false.", paramName);
        }
        public static void ThrowIfFalse(bool argument, string? paramName = null)
        {
            if (!argument) throw new ArgumentException($"The argument '{paramName}' has to be false.", paramName);
        }
    }
}
