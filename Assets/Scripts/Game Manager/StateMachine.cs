using System.Collections.Generic;

namespace StateManagment
{
    public class StateMachine<T>
    {
        private Dictionary<T, State> states;
        private Dictionary<T, List<Transition>> transitions;
        private State currentState = null;

        public StateMachine()
        {
            states = new Dictionary<T, State>();
            transitions = new Dictionary<T, List<Transition>>();
        }

        private void ExecuteTransition(Transition transition)
        {
            states[transition.fromState].Exit();
            states[transition.toState].Enter();
            currentState = states[transition.toState];
        }

        private void Default() { }

        public void Tick()
        {
            if (currentState == null)
                throw new System.Exception("Current state is null, make sure you have set an entry state!");

            foreach (Transition transition in transitions[currentState.name])
            {
                if (transition.isTriggered())
                {
                    ExecuteTransition(transition);
                    return;
                }
            }
            currentState.Tick();
        }

        public void AddState(T stateName, State.Method method, bool isEntryState = false, State.Method enter = null, State.Method exit = null)
        {
            State state = new State(stateName, method);
            state.Enter = (enter != null ? enter : Default);
            state.Exit = (exit != null ? exit : Default);
            states.Add(stateName, state);
            transitions[stateName] = new List<Transition>();
            if (isEntryState)
                currentState = state;
        }

        public void AddTransition(T from, T to, Transition.Condition condition)
        {
            if (!states.ContainsKey(from) || !states.ContainsKey(to))
                throw new System.Exception("Attempted to create invalid transition, make sure source and destination states exist!");

            Transition transition = new Transition(from, to, condition);
            transitions[from].Add(transition);
        }


        public class State
        {
            public T name;
            public delegate void Method();

            public Method Enter;
            public Method Tick;
            public Method Exit;

            public State(T stateName, Method method)
            {
                name = stateName;
                Tick = method;
            }
        }

        public class Transition
        {
            public T fromState, toState;
            public delegate bool Condition();
            public Condition isTriggered;

            public Transition(T from, T to, Condition condition)
            {
                fromState = from;
                toState = to;
                isTriggered = condition;
            }
        }
    }
}