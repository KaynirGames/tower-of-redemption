using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Игровая база данных.
/// </summary>
public class GameDatabase<TDatabaseObject> : ScriptableObject
{
    [SerializeField] protected List<TDatabaseObject> _objects = new List<TDatabaseObject>();
    /// <summary>
    /// Количество объектов в базе.
    /// </summary>
    public virtual int Count => _objects.Count;
    /// <summary>
    /// Добавить объект в базу данных.
    /// </summary>
    public virtual void Add(TDatabaseObject obj)
    {
        if (!_objects.Contains(obj))
        {
            _objects.Add(obj);
        }
    }
    /// <summary>
    /// Убрать существующий объект из базы данных.
    /// </summary>
    public virtual void Remove(TDatabaseObject obj)
    {
        if (_objects.Contains(obj))
        {
            _objects.Remove(obj);
        }
    }
    /// <summary>
    /// Найти первый объект, удовлетворяющий условиям.
    /// </summary>
    public virtual TDatabaseObject Find(Predicate<TDatabaseObject> match)
    {
        return _objects.Find(match);
    }
    /// <summary>
    /// Получить объект с индексом (нумерация начинается с 0).
    /// </summary>
    public virtual TDatabaseObject GetByIndex(int index)
    {
        return _objects[index];
    }
}