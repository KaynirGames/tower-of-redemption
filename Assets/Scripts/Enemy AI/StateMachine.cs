namespace KaynirGames.AI
{
    public class StateMachine<TKey>
    {
        private BaseState<TKey> _currentState;

        public StateMachine(BaseState<TKey> defaultState)
        {
            _currentState = defaultState;
            _currentState.EnterState();
        }

        public void Update()
        {
            BaseState<TKey> nextState = _currentState.UpdateState();
            SwitchState(nextState);
        }

        public void TransitionNext(TKey transitionKey)
        {
            BaseState<TKey> nextState = _currentState.GetTransitionState(transitionKey);
            SwitchState(nextState);
        }

        private void SwitchState(BaseState<TKey> nextState)
        {
            if (nextState != null)
            {
                _currentState.ExitState();
                _currentState = nextState;
                _currentState.EnterState();
            }
        }
    }
}
