using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaynirGames.Pathfinding2D
{
    /// <summary>
    /// Узел сетки, по которому осуществляется поиск пути.
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Позиция узла на сцене.
        /// </summary>
        public Vector2 WorldPosition;
        /// <summary>
        /// Позиция узла в сетке.
        /// </summary>
        public Vector2Int GridPosition;
        /// <summary>
        /// Является ли узел препятствием на пути?
        /// </summary>
        public bool IsObstacle;
        /// <summary>
        /// Расстояние от начальной точки пути.
        /// </summary>
        public int GCost;
        /// <summary>
        /// Расстояние от конечной точки пути.
        /// </summary>
        public int HCost;
        /// <summary>
        /// Суммарное расстояние от начальной и конечной точек пути.
        /// </summary>
        public int FCost => GCost + HCost;
        /// <summary>
        /// Родительский узел, из которого пришли.
        /// </summary>
        public Node Parent;

        public Node(Vector2 worldPos, Vector2Int gridPos, bool isObstacle)
        {
            WorldPosition = worldPos;
            GridPosition = gridPos;
            IsObstacle = isObstacle;
        }
        /// <summary>
        /// Получить расстояние до выбранного узла.
        /// </summary>
        /// <param name="target">Выбранный узел.</param>
        /// <returns></returns>
        public int GetDistance(Node target)
        {
            // Находим расстояние (в клетках) между узлами.
            int distanceX = Mathf.Abs(GridPosition.x - target.GridPosition.x);
            int distanceY = Mathf.Abs(GridPosition.y - target.GridPosition.y);
            // Возвращаем расстояние до цели в юнитах (сумма диагональных и прямых шагов),
            // где 1,4 * 10 = 14 - шаг по диагонали, 1 * 10 = 10 - прямой шаг (умножение на 10 для работы с int значениями).
            return distanceX > distanceY
                ? 14 * distanceY + 10 * (distanceX - distanceY)
                : 14 * distanceX + 10 * (distanceY - distanceX);
        }
    }
}
