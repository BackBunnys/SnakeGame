using System.Collections.Generic;

namespace SnakeGame.Engine
{
    class StateMachine
    {
            private readonly Stack<IState> states;

            public StateMachine()
            {
                states = new Stack<IState>();
            }

            public StateMachine(IState initialState) : this()
            {
                PushState(initialState);
            }

            public void Clear()
            {
                states.Clear();
            }

            public bool IsEmpty()
            {
                return states.Count == 0;
            }

            public void PopState()
            {
                if (!IsEmpty())
                {
                    states.Pop();
                }
            }

            public void PushState(IState state)
            {
                states.Push(state);
                state.Init();
            }

            public void ReplaceState(IState state)
            {
                PopState();
                PushState(state);
            }

            public IState GetCurrentState()
            {
                return IsEmpty() ? null : states.Peek();
            }
        }
}
