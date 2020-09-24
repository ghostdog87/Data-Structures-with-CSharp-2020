namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        public Tree(T value)
        {
            this.Value = value;
            this.Parent = null;
            this._children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {

            foreach (var child in children)
            {
                child.Parent = this;
                this._children.Add(child);
            }
        }

        public T Value { get; private set; }
        public Tree<T> Parent { get; private set; }
        public IReadOnlyCollection<Tree<T>> Children => this._children.AsReadOnly();
        public bool IsRootDeleted { get; private set; }

        public ICollection<T> OrderBfs()
        {
            var result = new List<T>();

            if (this.IsRootDeleted) return result;

            var tree = new Queue<Tree<T>>();

            tree.Enqueue(this);

            while (tree.Count != 0)
            {
                Tree<T> subtree = tree.Dequeue();
                result.Add(subtree.Value);

                foreach (Tree<T> child in subtree.Children)
                {
                    tree.Enqueue(child);
                }
            }

            return result;
        }

        public ICollection<T> OrderDfs()
        {
            var result = new List<T>();

            if (this.IsRootDeleted) return result;

            result = this.Dfs(this, result);

            return result;
        }

        public void AddChild(T parentKey, Tree<T> child)
        {
            Tree<T> searchedNode = this.FindNodeWithBfs(parentKey);

            this.CheckIfNodeExist(searchedNode);

            searchedNode._children.Add(child);
        }

        public void RemoveNode(T nodeKey)
        {
            Tree<T> searchedNode = this.FindNodeWithBfs(nodeKey);

            this.CheckIfNodeExist(searchedNode);

            if (searchedNode.Parent != null)
            {
                var parentNode = searchedNode.Parent;
                parentNode._children.Remove(searchedNode);
            }
            else
            {               
                searchedNode._children.Clear();
                this.IsRootDeleted = true;
            }
        }

        public void Swap(T firstKey, T secondKey)
        {
            Tree<T> firstNode = this.FindNodeWithBfs(firstKey);
            Tree<T> secondNode = this.FindNodeWithBfs(secondKey);

            this.CheckIfNodeExist(firstNode);
            this.CheckIfNodeExist(secondNode);

            var parentOfFirstNode = firstNode.Parent;
            var childrenOfFirstNode = firstNode._children;

            var parentOfSecondNode = secondNode.Parent;
            var childrenOfSecondNode = secondNode._children;

            var positionOfFirstNode = 0;
            var positionOfSecondNode = 0;

            if (firstNode.Parent != null && secondNode.Parent != null)
            {
                positionOfFirstNode = firstNode.Parent._children.IndexOf(firstNode);
                positionOfSecondNode = secondNode.Parent._children.IndexOf(secondNode);
            }

            if (firstNode.Parent == null)
            {
                this.Parent = null;
                this.Value = secondNode.Value;
                this._children.Clear();
                this._children.AddRange(childrenOfSecondNode);
            }
            else if(firstNode._children.Count == 0 || secondNode._children.Count == 0)
            {
                firstNode.Parent._children[positionOfFirstNode] = secondNode;
                secondNode.Parent._children[positionOfSecondNode] = firstNode;
            }
            else
            {
                firstNode.Parent = parentOfSecondNode;
                secondNode.Parent = parentOfFirstNode;

                firstNode.Parent._children[positionOfSecondNode] = firstNode;
                secondNode.Parent._children[positionOfFirstNode] = secondNode;
            }

        }

        private List<T> Dfs(Tree<T> subtree, List<T> result)
        {

            foreach (Tree<T> child in subtree.Children)
            {
                this.Dfs(child, result);
            }

            result.Add(subtree.Value);

            return result;
        }

        private void CheckIfNodeExist(Tree<T> searchedNode)
        {
            if (searchedNode == null)
            {
                throw new ArgumentNullException("Node does not exist.");
            }
        }

        private Tree<T> FindNodeWithBfs(T parentKey)
        {
            var treeQueue = new Queue<Tree<T>>();

            treeQueue.Enqueue(this);

            while (treeQueue.Count != 0)
            {
                var currentNode = treeQueue.Dequeue();

                if (currentNode.Value.Equals(parentKey))
                {
                    return currentNode;
                }

                foreach (var child in currentNode.Children)
                {
                    treeQueue.Enqueue(child);
                }

            }

            return null;
        }
    }
}
