namespace CitadellesDotIO.DeckAssembly
{
    public class ArgumentInvalidException : ArgumentException
    {
        public static void ThrowIfEqual<TEnum>(TEnum value, TEnum invalidValue, string? paramName = null)
       where TEnum : struct, Enum
        {
            if (EqualityComparer<TEnum>.Default.Equals(value, invalidValue))
            {
                throw new ArgumentException($"The argument '{paramName}' cannot be '{invalidValue}'.", paramName);
            }
        }
    }
}
