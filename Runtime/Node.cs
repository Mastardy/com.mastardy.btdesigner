using System;
using UnityEngine;

namespace BTDesigner
{
    public enum NodeState
    {
        Failure,
        Success,
        Running
    }
    
    public abstract class Node : ScriptableObject
    {
        [HideInInspector] public NodeState state = NodeState.Running;
        [NonSerialized] public bool started;
        
        public NodeState Update()
        {
            if (!started)
            {
                OnStart();
                started = true;
            }

            state = OnUpdate();

            if (state is NodeState.Failure or NodeState.Success)
            {
                OnEnd();
                started = false;
            }

            return state;
        }

        protected abstract void OnStart();
        protected abstract NodeState OnUpdate();
        protected abstract void OnEnd();
    }
}