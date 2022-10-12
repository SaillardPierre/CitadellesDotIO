using System.Collections;

namespace CitadellesDotIO.Model
{
    public class Deck<T> : Queue, ISwappable
    {
        public Deck() : base() { }
        public Deck(ICollection values) : base (values) { }
        public T PickCard() => (T)this.Dequeue();
    }
}
