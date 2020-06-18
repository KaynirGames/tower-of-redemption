using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Конечный автомат, обрабатывающий поведение игрового объекта.
/// </summary>
public class StateMachine : MonoBehaviour
{
    /// <summary>
    /// Словарь доступных состояний игрового объекта для конечного автомата.
    /// </summary>
    private Dictionary<Type, BaseState> availableStates;
    /// <summary>
    /// Текущее состояние игрового объекта.
    /// </summary>
    public BaseState CurrentState { get; private set; }
    /// <summary>
    /// Записать словарь доступных состояний игрового объекта.
    /// </summary>
    /// <param name="availableStates">Доступные состояния игрового объекта.</param>
    public void SetStates(Dictionary<Type, BaseState> availableStates)
    {
        this.availableStates = availableStates;
    }

    private void Update()
    {
        if (CurrentState == null)
        {
            BaseState defaultState = availableStates.Values.FirstOrDefault(state => state.IsDefault);
            CurrentState = defaultState != null ? defaultState : availableStates.Values.First();
        }

        Type nextStateType = CurrentState?.Handle();

        if (nextStateType != null && nextStateType != CurrentState.GetType())
        {
            SwitchState(nextStateType);
        }
    }

    private void SwitchState(Type nextStateType)
    {
        CurrentState = availableStates[nextStateType];
    }
}
