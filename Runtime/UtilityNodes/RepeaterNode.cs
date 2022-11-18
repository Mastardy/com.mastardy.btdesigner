namespace BTDesigner
{
    public class RepeaterNode : UtilityNode
    {
        protected override void OnStart() { }

        protected override NodeState OnUpdate()
        {
            child.Update();
            return NodeState.Running;
        }

        protected override void OnEnd() { }
    }
}
