namespace KaynirGames.AI
{
    /// <summary>
    /// Конечный автомат, управляющий поведением игровых объектов.
    /// </summary>
    /// <typeparam name="TKey">Ключ перехода в состояние.</typeparam>
    public class StateMachine<TKey>
    {
        private BaseState<TKey> _currentState; // Текущее состояние конечного автомата.

        public StateMachine(BaseState<TKey> defaultState)
        {
            _currentState = defaultState;
        }
        /// <summary>
        /// Обработать текущее состояние конечного автомата.
        /// </summary>
        public void Update()
        {
            BaseState<TKey> nextState = _currentState.UpdateState();
            SwitchState(nextState);
        }
        /// <summary>
        /// Перейти к следующему состоянию согласно ключу перехода.
        /// </summary>
        public void TransitionNext(TKey transitionKey)
        {
            BaseState<TKey> nextState = _currentState.GetTransitionState(transitionKey);
            SwitchState(nextState);
        }
        /// <summary>
        /// Сменить текущее состояние.
        /// </summary>
        private void SwitchState(BaseState<TKey> nextState)
        {
            if (nextState != null)
            {
                _currentState?.ExitState();
                _currentState = nextState;
                _currentState.EnterState();
            }
        }
    }
}
