using System;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachineBehaviour
{
    public class StateMachine : MonoBehaviour
    {
        StateNode _current;
        Dictionary<Type, StateNode> _nodes = new();
        HashSet<ITransition> _anyTransitions = new HashSet<ITransition>();

        public void Update()
        {
            var transition = GetTransition();
            if (transition != null)
                ChangeState(transition.To);

            _current.State?.Update();
        }

        public void FixedUpdate()
        {
            _current.State?.FixedUpdate();
        }

        public void SetState(IState state)
        {
            _current = _nodes[state.GetType()];
            _current.State?.OnEnter();
        }

        public void ChangeState(IState state)
        {
            if (state == _current.State) return;

            var previousState = _current.State;
            var nextState = _nodes[state.GetType()].State;
        }

        ITransition GetTransition()
        {
            foreach (var transition in _anyTransitions)
            {
                if (transition.Condition.Evaluate())
                    return transition;
            }

            foreach (var transition in _current.Transitions)
            {
                if (transition.Condition.Evaluate())
                    return transition;
            }

            return null;
        }

        public void AddTransition(IState from, IState To, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(To).State, condition);
        }

        public void AddAnyTransition(IState To, IPredicate condition)
        {
            _anyTransitions.Add(new Transition(To, condition));
        }

        StateNode GetOrAddNode(IState state)
        {
            var node = _nodes.GetValueOrDefault(state.GetType());

            if (node == null)
            {
                node = new StateNode(state);
                _nodes.Add(state.GetType(), node);
            }

            return node;
        }

        class StateNode
        {
            public IState State { get; }
            public HashSet<ITransition> Transitions { get; }

            public StateNode(IState state)
            {
                State = state;
                Transitions = new HashSet<ITransition>();
            }

            public void AddTransition(IState to, IPredicate condition)
            {
                Transitions.Add(new Transition(to, condition));
            }
        }
    }
}