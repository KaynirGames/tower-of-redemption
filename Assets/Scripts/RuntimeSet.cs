using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Набор объектов заданного типа на сцене.
/// </summary>
/// <typeparam name="T">Тип объектов в наборе.</typeparam>
public abstract class RuntimeSet<T> : ScriptableObject
{
    /// <summary>
    /// Список объектов в наборе.
    /// </summary>
    [SerializeField] protected List<T> objects = new List<T>();
    /// <summary>
    /// Добавить в набор, если такого объекта еще нет.
    /// </summary>
    /// <param name="obj">Объект для добавления.</param>
    public virtual void Add(T obj)
    {
        if (!objects.Contains(obj))
        {
            objects.Add(obj);
        }
    }
    /// <summary>
    /// Убрать существующий объект из набора.
    /// </summary>
    /// <param name="obj">Объект для удаления.</param>
    public virtual void Remove(T obj)
    {
        if (objects.Contains(obj))
        {
            objects.Remove(obj);
        }
    }
    /// <summary>
    /// Найти первый объект, удовлетворяющий условиям.
    /// </summary>
    /// <param name="match">Условия для соответствия.</param>
    /// <returns></returns>
    public virtual T Find(Predicate<T> match)
    {
        return objects.Find(match);
    }
    /// <summary>
    /// Получить объект с указанным индексом (нумерация начинается с 0).
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public virtual T GetObject(int index)
    {
        return objects[index];
    }
    /// <summary>
    /// Получить количество объектов в наборе.
    /// </summary>
    /// <returns></returns>
    public virtual int Count => objects.Count;
}
