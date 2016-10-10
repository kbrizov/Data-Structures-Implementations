using System;
using System.Linq;

namespace HashTable.Implementation
{
    public class KeyValuePair<TKey, TValue>
    {
        private TKey key;
        private TValue value;

        public KeyValuePair(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }

        public TKey Key
        {
            get
            {
                return this.key;
            }
        }

        public TValue Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = value;
            }
        }
    }
}
