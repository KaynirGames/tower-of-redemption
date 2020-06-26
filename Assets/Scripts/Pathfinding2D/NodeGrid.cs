using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaynirGames.Pathfinding2D
{
    /// <summary>
    /// Сетка узлов для поиска пути.
    /// </summary>
    public class NodeGrid : MonoBehaviour
    {
        [SerializeField] private LayerMask _obstacleMask; // Для определения, какие объекты являются препятствием на пути.
        [SerializeField] private Vector2 _gridWorldSize = Vector2.one; // Величина сетки в юнитах.
        [SerializeField] private Vector2 nodeSize = Vector2.one; // Величина узла в сетке.
        [SerializeField] private bool displayGridGizmos = false; // Визуальное отображение сетки.
        /// <summary>
        /// Сетка узлов.
        /// </summary>
        private Node[,] grid;
        /// <summary>
        /// Количество узлов по оси X.
        /// </summary>
        private int nodeCountX;
        /// <summary>
        /// Количество узлов по оси Y.
        /// </summary>
        private int nodeCountY;
        /// <summary>
        /// Радиус узла по оси X.
        /// </summary>
        private float nodeRadiusX;
        /// <summary>
        /// Радиус узла по оси Y.
        /// </summary>
        private float nodeRadiusY;

        private void Awake()
        {
            nodeCountX = Mathf.RoundToInt(_gridWorldSize.x / nodeSize.x);
            nodeCountY = Mathf.RoundToInt(_gridWorldSize.y / nodeSize.y);
            nodeRadiusX = nodeSize.x / 2;
            nodeRadiusY = nodeSize.y / 2;
        }
        /// <summary>
        /// Сформировать сетку узлов.
        /// </summary>
        public void CreateGrid()
        {
            grid = new Node[nodeCountX, nodeCountY];
            // Для удобства заполняем сетку, начиная с левого нижнего угла.
            Vector2 bottomLeft = new Vector2(transform.position.x - _gridWorldSize.x / 2f, transform.position.y - _gridWorldSize.y / 2f);

            for (int x = 0; x < nodeCountX; x++)
            {
                for (int y = 0; y < nodeCountY; y++)
                {
                    // При формировании позиции также учитываем смещение центра узла на величину его радиуса.
                    Vector2 nodeWorldPos = new Vector2(bottomLeft.x + nodeSize.x * x + nodeRadiusX, bottomLeft.y + nodeSize.y * y + nodeRadiusY);
                    bool isObstacle = Physics2D.OverlapBox(nodeWorldPos, nodeSize, 0, _obstacleMask);
                    grid[x, y] = new Node(nodeWorldPos, new Vector2Int(x, y), isObstacle);
                }
            }
        }
        /// <summary>
        /// Получить узел, исходя из позиции объекта на сцене.
        /// </summary>
        /// <param name="worldPos">Позиция объекта на сцене.</param>
        /// <returns></returns>
        public Node GetNode(Vector2 worldPos)
        {
            // Определяем относительное расположение мировой позиции в сетке узлов.
            float relativeX = Mathf.Clamp01((worldPos.x + _gridWorldSize.x / 2) / _gridWorldSize.x);
            float relativeY = Mathf.Clamp01((worldPos.y + _gridWorldSize.y / 2) / _gridWorldSize.y);
            // Находим индекс подходящего узла.
            int x = Mathf.RoundToInt((_gridWorldSize.x - 1) * relativeX);
            int y = Mathf.RoundToInt((_gridWorldSize.y - 1) * relativeY);

            return grid[x, y];
        }
        /// <summary>
        /// Получить соседей узла сетки.
        /// </summary>
        /// <param name="node">Узел сетки.</param>
        /// <returns></returns>
        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();
            // Проверяем позиции вокруг узла (блок 3х3).
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    // Пропускаем центр блока 3х3 (позиция самого узла).
                    if (x == 0 && y == 0)
                        continue;
                    // Вычисляем индексы соседнего узла в сетке. 
                    int checkX = node.GridPosition.x + x;
                    int checkY = node.GridPosition.y + y;
                    // Проверяем, чтобы индексы не выходили за границы массива.
                    if (checkX >= 0 && checkX < nodeCountX && checkY >= 0 && checkY < nodeCountY)
                    {
                        neighbours.Add(grid[checkX, checkY]);
                    }
                }
            }
            return neighbours;
        }

        private void OnDrawGizmos()
        {
            if (displayGridGizmos)
            {
                Gizmos.DrawWireCube(transform.position, new Vector2(_gridWorldSize.x, _gridWorldSize.y));
                if (grid != null)
                {
                    foreach (Node node in grid)
                    {
                        Gizmos.color = node.IsObstacle ? Color.red : Color.white;
                        Gizmos.DrawWireCube(node.WorldPosition, new Vector2(nodeSize.x - 0.2f, nodeSize.y - 0.2f));
                    }
                }
            }
        }
    }
}
