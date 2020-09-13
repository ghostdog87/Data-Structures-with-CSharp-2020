namespace Problem01.List
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class List<T> : IAbstractList<T>
    {
        private const int DEFAULT_CAPACITY = 4;
        private T[] _items;

        public List()
            : this(DEFAULT_CAPACITY) {
        }

        public List(int capacity)
        {
            this._items = new T[capacity];
            this.Count = 0;
        }

        public T this[int index]
        {
            get
            {
                ValidateIndex(index);
                return this._items[index];
            }
            set
            {
                ValidateIndex(index);
                this._items[index] = value;
            }
        }


        public int Count { get; private set; }

        public void Add(T item)
        {
            if(this.Count >= this._items.Length)
            {
                this.Grow();
            }

            this._items[this.Count] = item;

            this.Count++;
        }

        public bool Contains(T item)
        {
            for(var i = 0; i < this.Count; i++)
            {
                if (this._items[i].Equals(item))
                {
                    return true;
                }
            }
            return false;
        }


        public int IndexOf(T item)
        {
            for (var i = 0; i < this.Count; i++)
            {
                if (this._items[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            ValidateIndex(index);

            if (this.Count + 1 >= this._items.Length)
            {
                this.Grow();
            }

            this.ShiftRight(index);
            this._items[index] = item;

            this.Count++;
        }


        public bool Remove(T item)
        {
            for (var i = 0; i < this.Count; i++)
            {
                if (this._items[i].Equals(item))
                {
                    this.ShiftLeft(i);
                    this._items[this.Count - 1] = default;

                    this.Count--;

                    return true;
                }
            }
            return false;
        }


        public void RemoveAt(int index)
        {
            ValidateIndex(index);

            this.ShiftLeft(index);
            this._items[this.Count - 1] = default;

            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this._items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void Grow()
        {
            var newArr = new T[this._items.Length * 2];
            this._items.CopyTo(newArr, 0);
            this._items = newArr;
        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException("Give index is out of the list range.");
            }
        }

        private void ShiftRight(int index)
        {
            for (int i = this.Count - 1; i >= index; i--)
            {
                this._items[i] = this._items[i - 1];
            }
        }

        private void ShiftLeft(int index)
        {
            for (int i = index; i < this.Count; i++)
            {
                this._items[i - 1] = this._items[i];
            }
        }
    }
}