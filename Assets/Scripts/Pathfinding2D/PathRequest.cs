using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaynirGames.Pathfinding2D
{
    /// <summary>
    /// Запрос на поиск пути.
    /// </summary>
    public struct PathRequest
    {
        /// <summary>
        /// Начальная точка пути.
        /// </summary>
        public Vector2 StartPoint { get; private set; }
        /// <summary>
        /// Конечная точка пути.
        /// </summary>
        public Vector2 EndPoint { get; private set; }
        /// <summary>
        /// Действие по завершению поиска пути.
        /// </summary>
        public Action<Vector2[], bool> OnSearchComplete { get; private set; }
        /// <summary>
        /// Новый запрос на поиск пути.
        /// </summary>
        /// <param name="startPoint">Начальная точка пути.</param>
        /// <param name="endPoint">Конечная точка пути.</param>
        /// <param name="onSearchComplete">Действие по завершению поиска пути.</param>
        public PathRequest(Vector2 startPoint, Vector2 endPoint, Action<Vector2[], bool> onSearchComplete)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            OnSearchComplete = onSearchComplete;
        }
    }
}
