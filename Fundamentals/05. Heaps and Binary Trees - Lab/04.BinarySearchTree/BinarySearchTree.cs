namespace _04.BinarySearchTree
{
    using System;

    public class BinarySearchTree<T> : IAbstractBinarySearchTree<T>
        where T : IComparable<T>
    {
        public BinarySearchTree()
        {

        }

        public BinarySearchTree(Node<T> root)
        {
            this.Root = root;
            this.LeftChild = root.LeftChild;
            this.RightChild = root.RightChild;
        }

        public Node<T> Root { get; private set; }

        public Node<T> LeftChild { get; private set; }

        public Node<T> RightChild { get; private set; }

        public T Value => this.Root.Value;

        public bool Contains(T element)
        {
            var current = this.Root;
            while (current != null)
            {
                if (element.CompareTo(current.Value) < 0)
                {
                    current = current.LeftChild;
                }
                else if (element.CompareTo(current.Value) > 0)
                {
                    current = current.RightChild;
                }
                else
                {
                    break;
                }
            }
            return current != null;
        }

        public void Insert(T element)
        {
            var newElement = new Node<T>(element, null, null);

            if (this.Root == null)
            {
                this.Root = newElement;
            }
            else
            {
                var childElement = this.Root;
                Node<T> parentElement = null;

                while (childElement != null)
                {
                    parentElement = childElement;

                    if (newElement.Value.CompareTo(childElement.Value) < 0)
                    {
                        childElement = childElement.LeftChild;
                    }
                    else if (newElement.Value.CompareTo(childElement.Value) > 0)
                    {
                        childElement = childElement.RightChild;
                    }
                    else
                    {
                        return;
                    }
                }

                if(parentElement.Value.CompareTo(newElement.Value) < 0)
                {
                    parentElement.RightChild = newElement;
                }
                else
                {
                    parentElement.LeftChild = newElement;
                }
            }
        }

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            var current = this.Root;

            while (current != null)
            {
                if (element.CompareTo(current.Value) < 0)
                {
                    current = current.LeftChild;
                }
                else if (element.CompareTo(current.Value) > 0)
                {
                    current = current.RightChild;
                }
                else
                {
                    break;
                }
            }

            return new BinarySearchTree<T>(current);
        }
    }
}
