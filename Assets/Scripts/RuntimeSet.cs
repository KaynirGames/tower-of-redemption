using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Набор объектов заданного типа на сцене.
/// </summary>
/// <typeparam name="T">Тип объектов в наборе.</typeparam>
public class RuntimeSet<T> : ScriptableObject
{
    /// <summary>
    /// Список объектов в наборе.
    /// </summary>
    protected List<T> items = new List<T>();
    /// <summary>
    /// Добавить в набор, если такого объекта еще нет.
    /// </summary>
    /// <param name="obj">Объект для добавления.</param>
    public void Add(T obj)
    {
        if (!items.Contains(obj))
        {
            items.Add(obj);
        }
    }
    /// <summary>
    /// Убрать существующий объект из набора.
    /// </summary>
    /// <param name="obj">Объект для удаления.</param>
    public void Remove(T obj)
    {
        if (items.Contains(obj))
        {
            items.Remove(obj);
        }
    }
    /// <summary>
    /// Найти первый объект, удовлетворяющий условиям.
    /// </summary>
    /// <param name="match">Условия для соответствия.</param>
    /// <returns></returns>
    public T Find(Predicate<T> match)
    {
        return items.Find(match);
    }
    /// <summary>
    /// Получить объект с указанным индексом (нумерация начинается с 0).
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public T GetByIndex(int index)
    {
        return items[index];
    }
    /// <summary>
    /// Получить количество объектов в наборе.
    /// </summary>
    /// <returns></returns>
    public int GetAmount()
    {
        return items.Count;
    }
}
