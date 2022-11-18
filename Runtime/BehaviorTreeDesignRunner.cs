using System;
using UnityEngine;

namespace BTDesigner
{
    [AddComponentMenu("BTDesign/BehaviorTreeDesignRunner")]
    public class BehaviorTreeDesignRunner : MonoBehaviour
    {
        public BehaviorTreeDesign tree;

        private void Start()
        {
            tree = ScriptableObject.CreateInstance<BehaviorTreeDesign>();

            var root = ScriptableObject.CreateInstance<RootNode>();
            tree.rootNode = root;

            var repeater = ScriptableObject.CreateInstance<RepeaterNode>();
            root.child = repeater;
            
            var sequencer = ScriptableObject.CreateInstance<SequencerNode>();
            repeater.child = sequencer;
            
            var waitLog1 = ScriptableObject.CreateInstance<WaitLog>();
            sequencer.children.Add(waitLog1);

            var waitLog2 = ScriptableObject.CreateInstance<WaitLog>();
            waitLog2.duration = 2;
            sequencer.children.Add(waitLog2);
        }

        private void Update()
        {
            if (tree) tree.Update();
        }
    }
}
