namespace Problem01.FasterQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class FastQueue<T> : IAbstractQueue<T>
    {
        private Node<T> _head;
        private Node<T> _tail;

        public FastQueue()
        {
            this.Count = 0;
        }
        public FastQueue(Node<T> node)
        {
            this._head = node;
            this._tail = node;
            this.Count = 1;
        }

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            var lastNode = this._head;

            while (lastNode != null)
            {
                if (lastNode.Item.Equals(item))
                {
                    return true;
                }
                lastNode = lastNode.Next;
            }

            return false;
        }
        public void Enqueue(T item)
        {
            var newNode = new Node<T>()
            {
                Next = null,
                Item = item
            };

            if (this.Count == 0)
            {
                this._head = newNode;
            }
            else
            {
                this._tail.Next = newNode;
            }
            
            this._tail = newNode;

            this.Count++;
        }

        public T Dequeue()
        {
            this.ValidateNotEmpty();

            var firstNode = this._head.Item;
            this._head = this._head.Next;
            this.Count--;

            return firstNode;
        }

        public T Peek()
        {
            this.ValidateNotEmpty();

            return this._head.Item;
        }


        public IEnumerator<T> GetEnumerator()
        {
            var lastNode = this._head;

            while (lastNode != null)
            {
                yield return lastNode.Item;

                lastNode = lastNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void ValidateNotEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty!");
            }
        }
    }
}