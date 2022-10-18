using System.Collections;
using System.Collections.Generic;
using CitadellesDotIO.Engine.Targets;

namespace CitadellesDotIO.Engine
{
    public class Deck<T> : Queue, ISwappable, IDealable, IDeck
    {
        public Deck() : base() { }
        public Deck(ICollection values) : base(values) { }
        public T PickCard() => (T)Dequeue();

        public IEnumerable<T> PickCards(int cardsCount)
        {
            for (int i = 0; i < cardsCount; i++)
            {
                yield return (T)Dequeue();
            }
        }
    }
}
