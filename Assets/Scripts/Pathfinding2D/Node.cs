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
        public Vector2 WorldPosition { get; private set; }
        /// <summary>
        /// Позиция узла на сетке.
        /// </summary>
        public Vector2Int GridPosition { get; private set; }
        /// <summary>
        /// Является ли узел препятствием на пути?
        /// </summary>
        public bool IsObstacle { get; private set; }
        /// <summary>
        /// Расстояние от начальной точки пути.
        /// </summary>
        public int GCost { get; set; }
        /// <summary>
        /// Расстояние от конечной точки пути.
        /// </summary>
        public int HCost { get; set; }
        /// <summary>
        /// Суммарное расстояние от начальной и конечной точек пути.
        /// </summary>
        public int FCost => GCost + HCost;
        /// <summary>
        /// Родительский узел, из которого осуществлен переход.
        /// </summary>
        public Node Parent { get; set; }
        /// <summary>
        /// Новый узел сетки.
        /// </summary>
        /// <param name="worldPos">Позиция узла на сцене.</param>
        /// <param name="gridPos">Позиция узла на сетке.</param>
        /// <param name="isObstacle">Является ли узел препятствием?</param>
        public Node(Vector2 worldPos, Vector2Int gridPos, bool isObstacle)
        {
            WorldPosition = worldPos;
            GridPosition = gridPos;
            IsObstacle = isObstacle;
        }
        /// <summary>
        /// Получить расстояние до желаемого узла.
        /// </summary>
        /// <param name="targetNode">Желаемый узел.</param>
        /// <returns></returns>
        public int GetDistance(Node targetNode)
        {
            // Находим расстояние (в клетках) между узлами.
            int distanceX = Mathf.Abs(GridPosition.x - targetNode.GridPosition.x);
            int distanceY = Mathf.Abs(GridPosition.y - targetNode.GridPosition.y);
            // Возвращаем расстояние до цели в юнитах (сумма диагональных и прямых шагов),
            // где 1.4 * 10 = 14 - шаг по диагонали, 1 * 10 = 10 - прямой шаг (умножаем на 10 для работы с int значениями).
            return distanceX > distanceY
                ? 14 * distanceY + 10 * (distanceX - distanceY)
                : 14 * distanceX + 10 * (distanceY - distanceX);
        }
    }
}
