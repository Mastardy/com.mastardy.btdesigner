namespace BTDesigner
{
    public abstract class UtilityNode : Node
    {
        public Node child;

        public override Node Clone()
        {
            var node = Instantiate(this);
            node.child = child.Clone();
            return node;
        }
    }
}
