namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> _head;

        public int Count { get; private set; }

        public SinglyLinkedList()
        {
            this.Count = 0;
        }

        public SinglyLinkedList(Node<T> node)
        {
            this._head = node;
            this.Count = 1;
        }

        public void AddFirst(T item)
        {
            var node = new Node<T>()
            {
                Value = item,
                Next = this._head
            };

            this._head = node;

            this.Count++;
        }

        public void AddLast(T item)
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

        public T GetFirst()
        {
            this.ValidateNotEmpty();

            return this._head.Value;
        }

        public T GetLast()
        {
            this.ValidateNotEmpty();

            var lastNode = this._head;

            while (lastNode.Next != null)
            {
                lastNode = lastNode.Next;
            }

            return lastNode.Value;
        }

        public T RemoveFirst()
        {
            this.ValidateNotEmpty();

            var firstNode = this._head.Value;
            this._head = this._head.Next;
            this.Count--;

            return firstNode;
        }

        public T RemoveLast()
        {
            this.ValidateNotEmpty();

            var lastNode = this._head;
            var result = lastNode.Value;

            if(this.Count == 1)
            {
                this._head = null;
            }
            else
            {
                while (lastNode.Next.Next != null)
                {
                    lastNode = lastNode.Next;
                }
                result = lastNode.Next.Value;
                lastNode.Next = null;
            }

            this.Count--;
            return result;
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
                throw new InvalidOperationException("Singly linked list is empty!");
            }
        }
    }
}