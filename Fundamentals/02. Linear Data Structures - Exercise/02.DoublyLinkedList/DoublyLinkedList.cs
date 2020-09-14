namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> _head;
        private Node<T> _tail;

        public int Count { get; private set; }

        public DoublyLinkedList()
        {
            this.Count = 0;
        }

        public DoublyLinkedList(Node<T> node)
        {
            this._head = node;
            this._tail = node;
            this.Count = 1;
        }

        public void AddFirst(T item)
        {
            var node = new Node<T>()
            {
                Next = this._head,
                Item = item,                
                Previous = null
            };

            if (this.Count == 0)
            {
                this._tail = node;
            }
            else
            {
                this._head.Previous = node;
            }

            this._head = node;

            this.Count++;
        }

        public void AddLast(T item)
        {

            var newNode = new Node<T>()
            {
                Next = null,
                Item = item,
                Previous = this._tail
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

        public T GetFirst()
        {
            this.ValidateNotEmpty();

            return this._head.Item;
        }

        public T GetLast()
        {
            this.ValidateNotEmpty();

            return this._tail.Item;
        }

        public T RemoveFirst()
        {
            this.ValidateNotEmpty();

            var firstNode = this._head.Item;

            if (this.Count == 1)
            {
                this._head = null;
            }
            else
            {
                var nextElement = this._head.Next;
                nextElement.Previous = null;
                this._head = nextElement;               
            }

            this.Count--;

            return firstNode;
        }

        public T RemoveLast()
        {
            this.ValidateNotEmpty();

            var lastNode = this._tail.Item;

            if (this.Count == 1)
            {
                this._tail = null;
            }
            else
            {
                var previousElement = this._tail.Previous;
                previousElement.Next = null;
                this._tail = previousElement;
            }

            this.Count--;

            return lastNode;
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
                throw new InvalidOperationException("Singly linked list is empty!");
            }
        }
    }
}