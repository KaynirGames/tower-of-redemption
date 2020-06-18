using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Базовое состояние игрового объекта.
/// </summary>
public abstract class BaseState
{
    /// <summary>
    /// Игровой объект, обладающий текущим состоянием.
    /// </summary>
    protected GameObject gameObject;
    /// <summary>
    /// Это состояние игрового объекта по умолчанию?
    /// </summary>
    public bool IsDefault { get; private set; }

    public BaseState(GameObject gameObject, bool isDefault)
    {
        this.gameObject = gameObject;
        IsDefault = isDefault;
    }
    /// <summary>
    /// Обработать текущее состояние игрового объекта.
    /// </summary>
    /// <returns></returns>
    public abstract Type Handle();
}
