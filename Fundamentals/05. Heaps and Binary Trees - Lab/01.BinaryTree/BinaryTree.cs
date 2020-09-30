namespace _01.BinaryTree
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
    {
        public BinaryTree(T value
            , IAbstractBinaryTree<T> leftChild
            , IAbstractBinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
        }

        public T Value { get; private set; }

        public IAbstractBinaryTree<T> LeftChild { get; private set; }

        public IAbstractBinaryTree<T> RightChild { get; private set; }

        public string AsIndentedPreOrder(int indent)
        {
            var sb = new StringBuilder();

            this.AsIndentedPreOrderWithDFS(this,indent, sb);

            return sb.ToString().TrimEnd();
        }

        public List<IAbstractBinaryTree<T>> InOrder()
        {
            var result = new List<IAbstractBinaryTree<T>>();

            this.InOrderWithDFS(this, result);

            return result;
        }

        public List<IAbstractBinaryTree<T>> PostOrder()
        {
            var result = new List<IAbstractBinaryTree<T>>();

            this.PostOrderWithDFS(this, result);

            return result;
        }

        public List<IAbstractBinaryTree<T>> PreOrder()
        {
            var result = new List<IAbstractBinaryTree<T>>();

            this.PreOrderWithDFS(this, result);

            return result;
        }

        public void ForEachInOrder(Action<T> action)
        {
            this.ForEachInOrderWithDFS(this, action);
        }

        private void ForEachInOrderWithDFS(IAbstractBinaryTree<T> currentNode, Action<T> action)
        {
            if (currentNode != null)
            {
                this.ForEachInOrderWithDFS(currentNode.LeftChild, action);

                action.Invoke(currentNode.Value);

                this.ForEachInOrderWithDFS(currentNode.RightChild, action);
            }
        }

        private void AsIndentedPreOrderWithDFS(IAbstractBinaryTree<T> currentNode, int indent, StringBuilder sb)
        {
            if (currentNode != null)
            {
                sb.Append(new String(' ', indent) + currentNode.Value + Environment.NewLine);
                indent += 2;

                this.AsIndentedPreOrderWithDFS(currentNode.LeftChild, indent, sb);
                this.AsIndentedPreOrderWithDFS(currentNode.RightChild, indent, sb);
            }
        }
        private void PreOrderWithDFS(IAbstractBinaryTree<T> currentNode, List<IAbstractBinaryTree<T>> result)
        {
            if (currentNode != null)
            {
                result.Add(currentNode);

                this.PreOrderWithDFS(currentNode.LeftChild, result);
                this.PreOrderWithDFS(currentNode.RightChild, result);
            }
        }
        private void InOrderWithDFS(IAbstractBinaryTree<T> currentNode, List<IAbstractBinaryTree<T>> result)
        {
            if (currentNode != null)
            {
                this.InOrderWithDFS(currentNode.LeftChild, result);
                result.Add(currentNode);
                this.InOrderWithDFS(currentNode.RightChild, result);
            }
        }
        private void PostOrderWithDFS(IAbstractBinaryTree<T> currentNode, List<IAbstractBinaryTree<T>> result)
        {
            if (currentNode != null)
            {
                this.PostOrderWithDFS(currentNode.LeftChild, result);             
                this.PostOrderWithDFS(currentNode.RightChild, result);
                result.Add(currentNode);
            }
        }
    }
}
