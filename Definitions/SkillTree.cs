namespace QuasarFramework.Definitions
{
    delegate void Visitor<T>(T nodeData);

    internal class SkillTree<T>
    {
        private T data;

        private LinkedList<SkillTree<T>> _children;

        public SkillTree(T data)
        {
            this.data = data;
            _children = new LinkedList<SkillTree<T>>();
        }

        public void AddChild(T child) => _children.AddFirst(new SkillTree<T>(child));

        public SkillTree<T> GetChild(int i)
        {
            foreach (SkillTree<T> n in _children)
                if (--i == 0)
                    return n;

            return null;
        }

        public void Traverse(SkillTree<T> node, Visitor<T> visitor)
        {
            visitor(node.data);
            foreach (SkillTree<T> child in node._children)
                Traverse(child, visitor);
        }
    }
}