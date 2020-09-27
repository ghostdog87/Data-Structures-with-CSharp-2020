namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        public Tree(T key, params Tree<T>[] children)
        {
            this.Key = key;
            this._children = new List<Tree<T>>();
            this._children.AddRange(children);
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }


        public IReadOnlyCollection<Tree<T>> Children
            => this._children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            this._children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
        }

        public string GetAsString()
        {
            var indented = 0;

            var sb = new StringBuilder();

            var result = this.GetAsStringDFS(this, indented, sb);

            return result.TrimEnd();
        }

        public List<T> GetLeafKeys()
        {
            var listOfNodes = new List<Tree<T>>();

            this.GetLeafsWithDFS(this, listOfNodes);

            return listOfNodes.Select(x =>x.Key).ToList();
            
        }

        public List<T> GetMiddleKeys()
        {
            var listOfNodes = new List<Tree<T>>();

            this.GetMiddleKeysWithDFS(this, listOfNodes);

            return listOfNodes.Select(x => x.Key).ToList();
        }

        public Tree<T> GetDeepestLeftomostNode()
        {
            var DictionaryOfNodes = new Dictionary<int,List<Tree<T>>>();

            var dept = 0;

            this.GetDeepestLeftomostNodeWithDFS(this, DictionaryOfNodes, dept);
           
            return DictionaryOfNodes[DictionaryOfNodes.Count][0];
        }

        public List<T> GetLongestPath()
        {
            var DictionaryOfNodes = new Dictionary<int, List<Tree<T>>>();

            var dept = 0;

            this.GetDeepestLeftomostNodeWithDFS(this, DictionaryOfNodes, dept);

            var lastElementInLongestPath = DictionaryOfNodes[DictionaryOfNodes.Count][0];

            var longestPath = new List<T>();

            var currentElementInLongestPath = lastElementInLongestPath;

            while (currentElementInLongestPath != null)
            {
                longestPath.Add(currentElementInLongestPath.Key);
                currentElementInLongestPath = currentElementInLongestPath.Parent;
            }

            longestPath.Reverse();

            return longestPath;
        }

        public List<List<T>> PathsWithGivenSum(int sum)
        {
            var result = new List<List<T>>();

            var currentPath = new List<T>();
            currentPath.Add(this.Key);

            int currentSum = Convert.ToInt32(this.Key);

            this.PathsWithGivenSumWithDFS(this, currentPath, result, ref currentSum, sum);

            return result;
        }

        public List<Tree<T>> SubTreesWithGivenSum(int sum)
        {
            var result = new List<Tree<T>>();
            var listOfNodes = this.GetAllNodesWithBFS(this);

            foreach (var node in listOfNodes)
            {
                int subtreeSum = this.GetSubtreeSumWithDFS(node);

                if (subtreeSum == sum)
                {
                    result.Add(node);
                }
            }

            return result;

        }

        private List<Tree<T>> GetAllNodesWithBFS(Tree<T> tree)
        {
            var result = new List<Tree<T>>();
            var nodes = new Queue<Tree<T>>();

            nodes.Enqueue(this);

            while (nodes.Count > 0)
            {
                var currentNode = nodes.Dequeue();
                result.Add(currentNode);

                foreach (var child in currentNode._children)
                {
                    nodes.Enqueue(child);
                }
            }

            return result;
        }

        private int GetSubtreeSumWithDFS(Tree<T> parentNode)
        {
            int currentSum = Convert.ToInt32(parentNode.Key);
            int childSum = 0;

            foreach (var child in parentNode._children)
            {
                childSum += this.GetSubtreeSumWithDFS(child);
            }

            return currentSum + childSum;
        }

        private void GetAllNodesWithDFS(Tree<T> currentNode, List<Tree<T>> listOfNodes)
        {
            foreach (var childNode in currentNode._children)
            {
                listOfNodes.Add(childNode);

                this.GetLeafsWithDFS(childNode, listOfNodes);
            }
        }

        private void PathsWithGivenSumWithDFS(
            Tree<T> currentNode,
            List<T> currentPath,
            List<List<T>> expectedPath,
            ref int currentSum,
            int expectedSum)
        {
            foreach (var child in currentNode._children)
            {
                currentPath.Add(child.Key);
                currentSum += Convert.ToInt32(child.Key);
                PathsWithGivenSumWithDFS(child, currentPath, expectedPath, ref currentSum, expectedSum);
            }

            if (currentSum == expectedSum)
            {
                expectedPath.Add(new List<T>(currentPath));
            }

            currentPath.RemoveAt(currentPath.Count - 1);
            currentSum -= Convert.ToInt32(currentNode.Key);
        }

        private string GetAsStringDFS(Tree<T> currentNode, int indented, StringBuilder sb)
        {
            sb.AppendLine(new String(' ', indented) + currentNode.Key);
            indented += 2;

            foreach (var childNode in currentNode._children)
            {
                this.GetAsStringDFS(childNode, indented, sb);
            }

            return sb.ToString();
        }

        private void GetLeafsWithDFS(Tree<T> currentNode, List<Tree<T>> listOfNodes)
        {
            foreach (var childNode in currentNode._children)
            {
                if (childNode._children.Count == 0)
                {
                    listOfNodes.Add(childNode);
                }

                this.GetLeafsWithDFS(childNode, listOfNodes);
            }
        }

        private void GetMiddleKeysWithDFS(Tree<T> currentNode, List<Tree<T>> listOfNodes)
        {
            foreach (var childNode in currentNode._children)
            {
                if (childNode._children.Count > 0 && childNode.Parent != null)
                {
                    listOfNodes.Add(childNode);
                }

                this.GetMiddleKeysWithDFS(childNode, listOfNodes);
            }
        }

        private void GetDeepestLeftomostNodeWithDFS(Tree<T> currentNode, Dictionary<int, List<Tree<T>>> listOfNodes, int dept)
        {
            dept++;

            foreach (var childNode in currentNode._children)
            {
                if (!listOfNodes.ContainsKey(dept))
                {
                    listOfNodes.Add(dept, new List<Tree<T>>());
                }

                listOfNodes[dept].Add(childNode);

                this.GetDeepestLeftomostNodeWithDFS(childNode, listOfNodes, dept);
            }
        }
    }
}
