namespace Svatky.Models
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Grouping<TKey, TElement> : IGrouping<TKey, TElement>
    {
        private readonly List<TElement> elements;

        public Grouping(TKey key, IEnumerable<TElement> items)
        {
            this.Key = key;
            this.elements = items.ToList();
        }

        public Grouping(TKey key) : this(key, Enumerable.Empty<TElement>())
        {
        }

        public TKey Key
        {
            get;
            private set;
        }

        public void Add(TElement element)
        {
            this.elements.Add(element);
        }

        public System.Collections.Generic.IEnumerator<TElement> GetEnumerator()
        {
            return this.elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.elements.GetEnumerator();
        }
    }
}
