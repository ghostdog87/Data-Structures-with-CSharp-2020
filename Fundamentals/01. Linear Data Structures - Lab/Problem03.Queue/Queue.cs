namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
    {
        private Node<T> _head;

        public Queue()
        {
            this.Count = 0;
        }
        public Queue(Node<T> node)
        {
            this._head = node;
            this.Count = 1;
        }

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            var lastNode = this._head;

            while (lastNode != null)
            {
                if (lastNode.Value.Equals(item))
                {
                    return true;
                }
                lastNode = lastNode.Next;
            }

            return false;
        }
        public void Enqueue(T item)
        {
            
            var lastNode = this._head;

            var newNode = new Node<T>()
            {
                Next = null,
                Value = item
            };

            if (this.Count == 0)
            {
                this._head = newNode;
            }
            else
            {
                while (lastNode != null)
                {
                    if (lastNode.Next == null)
                    {
                        lastNode.Next = newNode;
                        break;
                    }
                    lastNode = lastNode.Next;
                }
            }

            this.Count++;
        }

        public T Dequeue()
        {
            this.ValidateNotEmpty();

            var firstNode = this._head.Value;
            this._head = this._head.Next;
            this.Count--;

            return firstNode;
        }

        public T Peek()
        {
            this.ValidateNotEmpty();

            return this._head.Value;
        }


        public IEnumerator<T> GetEnumerator()
        {
            var lastNode = this._head;

            while (lastNode != null)
            {
                yield return lastNode.Value;

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