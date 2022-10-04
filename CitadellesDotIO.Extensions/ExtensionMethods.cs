using System.Collections.Generic;

namespace CitadellesDotIO.Extensions
{
    public static class ExtensionMethods
    {
        public static void SetFirstElement<T>(this List<T> list, T newFirst)
        {
            int indexOfNewFirst = list.IndexOf(newFirst);
            List<T> firstHalf = list.GetRange(0, indexOfNewFirst);
            list.RemoveRange(0, indexOfNewFirst);
            list.AddRange(firstHalf);
        }
    }
}