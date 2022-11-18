using System.Collections.Generic;

namespace BTDesigner
{
    public abstract class CompositionNode : Node
    {
        public List<Node> children = new();
        
        public override Node Clone()
        {
            var node = Instantiate(this);
            node.children = children.ConvertAll(c => c.Clone());
            return node;
        }
    }
}
