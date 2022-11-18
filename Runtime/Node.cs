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
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;
        
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

        public virtual Node Clone()
        {
            return Instantiate(this);
        }
        
        protected abstract void OnStart();
        protected abstract NodeState OnUpdate();
        protected abstract void OnEnd();
    }
}