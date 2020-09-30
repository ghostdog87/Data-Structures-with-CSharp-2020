namespace _03.PriorityQueue
{
    using System;
    using System.Collections.Generic;

    public class PriorityQueue<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> _elements { get; set; }

        public PriorityQueue()
        {
            this._elements = new List<T>();
        }

        public int Size => this._elements.Count;

        public void Add(T element)
        {
            this._elements.Add(element);

            this.HeapifyUp();
        }

        public T Peek()
        {
            this.IsListValid();
            return this._elements[0];
        }

        private void IsListValid()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Heap is empty!");
            }         
        }

        public T Dequeue()
        {
            this.IsListValid();

            var removedElement = this._elements[0]; // get first element

            this.SwapElements(0, this.Size - 1);
            this._elements.RemoveAt(this.Size - 1); // remove last element

            // HeapifyDown new first element
            this.HeapifyDown();

            return removedElement;
        }

        private void HeapifyDown()
        {
            //check left and right elements, if they are greater, swap them
            var currentIndex = 0;

            while (true)
            {
                var leftChildIndex = (2 * currentIndex) + 1;
                var rightChildIndex = (2 * currentIndex) + 2;

                if (this.IsValidIndex(leftChildIndex) && this.IsLeftChildGreater(currentIndex, leftChildIndex))
                {
                    var greatestIndex = leftChildIndex;

                    if (this.IsValidIndex(rightChildIndex) && this.IsRightChildGreater(leftChildIndex, rightChildIndex))
                    {

                        greatestIndex = rightChildIndex;
                    }

                    this.SwapElements(currentIndex, greatestIndex);
                    currentIndex = greatestIndex;
                }
                else
                {
                    return;
                }
            }
        }

        private bool IsValidIndex(int index)
        {
            return index < this.Size;
        }

        private bool IsRightChildGreater(int currentIndex, int rightChildIndex)
        {
            return this._elements[currentIndex].CompareTo(this._elements[rightChildIndex]) < 0;
        }

        private bool IsLeftChildGreater(int currentIndex, int leftChildIndex)
        {
            return this._elements[currentIndex].CompareTo(this._elements[leftChildIndex]) < 0;
        }

        private void HeapifyUp()
        {
            var currentIndex = this.Size - 1;

            var parentIndex = this.GetParentIndex(currentIndex);

            while (currentIndex != 0 && this.IsGreater(currentIndex, parentIndex))
            {
                this.SwapElements(currentIndex, parentIndex);
                currentIndex = parentIndex;
                parentIndex = this.GetParentIndex(currentIndex);
            }
        }

        private void SwapElements(int currentIndex, int parentIndex)
        {
            var temp = this._elements[parentIndex];
            this._elements[parentIndex] = this._elements[currentIndex];
            this._elements[currentIndex] = temp;
        }

        private bool IsGreater(int currentIndex, int parentIndex)
        {
            return this._elements[currentIndex].CompareTo(this._elements[parentIndex]) > 0;
        }

        private int GetParentIndex(int currentElement)
        {
            return (currentElement - 1) / 2;
        }
    }
}
