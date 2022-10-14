using System.Collections;
using System.Collections.Generic;

namespace CitadellesDotIO.Model
{
    public class Deck<T> : Queue, ISwappable, IDealable
    {
        public Deck() : base() { }
        public Deck(ICollection values) : base (values) { }
        public T PickCard() => (T)this.Dequeue();

        public IEnumerable<T> PickCards(int cardsCount)
        {
            for(int i = 0; i < cardsCount; i++)
            {
                yield return (T) this.Dequeue();
            }
        } 
    }
}
