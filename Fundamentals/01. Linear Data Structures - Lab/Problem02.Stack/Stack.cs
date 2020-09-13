namespace Problem02.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IAbstractStack<T>
    {
        private Node<T> _top;

        public Stack()
        {
            this.Count = 0;
        }

        public Stack(Node<T> item)
        {
            this._top = item;
            this.Count = 1;
        }


        public int Count { get; private set; }

        public void Push(T item)
        {
            var node = new Node<T>()
            {
                Value = item,
                Next = this._top
            };

            this._top = node;

            this.Count++;
        }

        public bool Contains(T item)
        {
            var currentNode = this._top;

            while (currentNode != null)
            {
                if (currentNode.Value.Equals(item))
                {
                    return true;
                }
                currentNode = currentNode.Next;
            }
            return false;
        }

        public T Peek()
        {
            this.ValidateNotEmpty();

            return this._top.Value;
        }


        public T Pop()
        {
            this.ValidateNotEmpty();

            var result = this._top.Value;
            this._top = this._top.Next;

            this.Count--;

            return result;        
        }


        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = this._top;

            while (currentNode.Next != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() 
            => this.GetEnumerator();

        private void ValidateNotEmpty()
        {
            if(this.Count == 0)
            {
                throw new InvalidOperationException("Stack is empty!");
            }
        }
    }
}