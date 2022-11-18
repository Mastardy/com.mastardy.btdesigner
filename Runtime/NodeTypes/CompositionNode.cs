using System.Collections.Generic;

namespace BTDesigner
{
    public abstract class CompositionNode : Node
    {
        public List<Node> children = new();
    }
}
