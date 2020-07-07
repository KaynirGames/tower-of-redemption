using System.Collections.Generic;

namespace KaynirGames.AI
{
    /// <summary>
    /// Базовое состояние конечного автомата.
    /// </summary>
    /// <typeparam name="TKey">Ключ перехода в состояние.</typeparam>
    public abstract class BaseState<TKey>
    {
        private Dictionary<TKey, BaseState<TKey>> _transitions = new Dictionary<TKey, BaseState<TKey>>(); // Словарь переходов в другие состояния.
        /// <summary>
        /// Добавить переход в другое состояние.
        /// </summary>
        /// <param name="transitionKey">Ключ перехода.</param>
        /// <param name="state">Другое состояние.</param>
        public void AddTransition(TKey transitionKey, BaseState<TKey> state)
        {
            _transitions[transitionKey] = state;
        }
        /// <summary>
        /// Получить состояние согласно ключу перехода.
        /// </summary>
        public BaseState<TKey> GetTransitionState(TKey transitionKey)
        {
            return _transitions.ContainsKey(transitionKey)
                ? _transitions[transitionKey]
                : null;
        }
        /// <summary>
        /// Выполнить действия при входе в состояние.
        /// </summary>
        public abstract void EnterState();
        /// <summary>
        /// Обработать состояние.
        /// </summary>
        public abstract BaseState<TKey> UpdateState();
        /// <summary>
        /// Выполнить действия при выходе из состояния.
        /// </summary>
        public abstract void ExitState();
    }
}
