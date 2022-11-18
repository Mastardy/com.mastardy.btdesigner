using System;
using UnityEngine;

namespace BTDesigner
{
    [AddComponentMenu("BTDesigner/BehaviorTreeDesignRunner")]
    public class BehaviorTreeDesignRunner : MonoBehaviour
    {
        public BehaviorTreeDesign tree;

        private void Start()
        {
            tree = tree.Clone();
        }

        private void Update()
        {
            if (tree) tree.Update();
        }
    }
}
