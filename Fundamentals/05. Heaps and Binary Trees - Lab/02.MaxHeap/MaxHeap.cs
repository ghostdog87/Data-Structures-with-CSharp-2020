namespace _02.MaxHeap
{
    using System;
    using System.Collections.Generic;

    public class MaxHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> _elements;

        public MaxHeap()
        {
            this._elements = new List<T>();
        }

        public T Value { get; set; }

        public int Size => this._elements.Count;

        public void Add(T element)
        {
            this._elements.Add(element);

            this.HeapifyUp();
        }

        public T Peek()
        {
            this.IsListEmpty();
            return this._elements[0];
        }
        private void HeapifyUp()
        {
            int currentIndex = this.Size - 1;
            int parentIndex = this.GetParentIndex(currentIndex);

            while (this.IsIndexValid(currentIndex) && this.IsGreater(currentIndex, parentIndex))
            {
                this.SwapElements(currentIndex, parentIndex);

                currentIndex = parentIndex;
                parentIndex = this.GetParentIndex(currentIndex);
            }
        }

        private void SwapElements(int currentIndex, int parentIndex)
        {
            var tempElement = this._elements[currentIndex];
            this._elements[currentIndex] = this._elements[parentIndex];
            this._elements[parentIndex] = tempElement;
        }

        private bool IsGreater(int childIndex, int parentIndex)
        {
            return this._elements[childIndex].CompareTo(this._elements[parentIndex]) > 0;
        }

        private int GetParentIndex(int childIndex)
        {
            return (childIndex - 1) / 2;
        }

        private bool IsIndexValid(int index)
        {
            return index > 0;
        }

        private void IsListEmpty()
        {
            if(this.Size == 0)
            {
                throw new InvalidOperationException("Heap List is empty!");
            }
        }
    }
}
