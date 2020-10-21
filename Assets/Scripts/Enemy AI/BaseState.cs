using System.Collections.Generic;

namespace KaynirGames.AI
{
    public abstract class BaseState<TKey>
    {
        private Dictionary<TKey, BaseState<TKey>> _transitions = new Dictionary<TKey, BaseState<TKey>>();

        public void AddTransition(TKey transitionKey, BaseState<TKey> state)
        {
            _transitions[transitionKey] = state;
        }

        public BaseState<TKey> GetTransitionState(TKey transitionKey)
        {
            return _transitions.ContainsKey(transitionKey)
                ? _transitions[transitionKey]
                : null;
        }

        public abstract void EnterState();

        public abstract BaseState<TKey> UpdateState();

        public abstract void ExitState();
    }
}
