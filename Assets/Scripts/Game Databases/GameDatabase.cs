using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Игровая база данных.
/// </summary>
public class GameDatabase<TDatabaseObject> : ScriptableObject
{
    [SerializeField] protected List<TDatabaseObject> _objects = new List<TDatabaseObject>();

    public virtual int Count => _objects.Count;

    public virtual void Add(TDatabaseObject obj)
    {
        if (!_objects.Contains(obj))
        {
            _objects.Add(obj);
        }
    }

    public virtual void Remove(TDatabaseObject obj)
    {
        if (_objects.Contains(obj))
        {
            _objects.Remove(obj);
        }
    }

    public virtual TDatabaseObject Find(Predicate<TDatabaseObject> match)
    {
        return _objects.Find(match);
    }

    public virtual TDatabaseObject GetByIndex(int index)
    {
        return _objects[index];
    }
}