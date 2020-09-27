namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class TreeFactory
    {
        private Dictionary<int, Tree<int>> nodesBykeys;

        public TreeFactory()
        {
            this.nodesBykeys = new Dictionary<int, Tree<int>>();
        }

        public Tree<int> CreateTreeFromStrings(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                var parentKey = int.Parse(input[i].Split(' ')[0]);
                var childKey = int.Parse(input[i].Split(' ')[1]);

                this.AddEdge(parentKey, childKey);
            }

            return this.GetRoot();
        }

        public Tree<int> CreateNodeByKey(int key)
        {
            if (!this.nodesBykeys.ContainsKey(key))
            {
                this.nodesBykeys.Add(key, new Tree<int>(key));
            }

            return this.nodesBykeys[key];
        }

        public void AddEdge(int parent, int child)
        {
            Tree<int> parentNode = this.CreateNodeByKey(parent);
            Tree<int> childNode = this.CreateNodeByKey(child);

            parentNode.AddChild(childNode);
            childNode.AddParent(parentNode);
        }

        private Tree<int> GetRoot()
        {
            foreach (var nodes in this.nodesBykeys)
            {
                if(nodes.Value.Parent == null)
                {
                    return nodes.Value;
                }
            }

            return null;
        }
    }
}
