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
        [SerializeField] private LayerMask obstacleMask; // Для определения, какие объекты являются препятствием на пути.
        [SerializeField] private Vector2 gridWorldSize = Vector2.one; // Величина сетки в юнитах.
        [SerializeField] private float nodeSize = 1; // Величина узла в сетке.

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
        /// Радиус узла.
        /// </summary>
        private float nodeRadius;

        public List<Node> Path = new List<Node>();

        private void Awake()
        {
            nodeCountX = Mathf.RoundToInt(gridWorldSize.x / nodeSize);
            nodeCountY = Mathf.RoundToInt(gridWorldSize.y / nodeSize);
            nodeRadius = nodeSize / 2;

            CreateGrid();
        }
        /// <summary>
        /// Сформировать сетку узлов.
        /// </summary>
        public void CreateGrid()
        {
            grid = new Node[nodeCountX, nodeCountY];
            // Для удобства заполняем сетку, начиная с левого нижнего угла.
            Vector2 bottomLeft = new Vector2(transform.position.x - gridWorldSize.x / 2f, transform.position.y - gridWorldSize.y / 2f);

            for (int x = 0; x < nodeCountX; x++)
            {
                for (int y = 0; y < nodeCountY; y++)
                {
                    // При формировании позиции также учитываем смещение центра узла на величину его радиуса.
                    Vector2 nodeWorldPos = new Vector2(bottomLeft.x + nodeSize * x + nodeRadius, bottomLeft.y + nodeSize * y + nodeRadius);
                    bool isObstacle = Physics2D.OverlapCircle(nodeWorldPos, nodeRadius, obstacleMask);
                    grid[x, y] = new Node(nodeWorldPos, new Vector2Int(x, y), isObstacle);
                }
            }
        }
        /// <summary>
        /// Получить узел, исходя из глобальной позиции объекта.
        /// </summary>
        /// <param name="worldPos">Позиция объекта на сцене.</param>
        /// <returns></returns>
        public Node GetNode(Vector2 worldPos)
        {
            // Определяем относительное расположение мировой позиции в сетке узлов.
            float relativeX = Mathf.Clamp01((worldPos.x + gridWorldSize.x / 2) / gridWorldSize.x);
            float relativeY = Mathf.Clamp01((worldPos.y + gridWorldSize.y / 2) / gridWorldSize.y);
            // Находим индекс подходящего узла.
            int x = Mathf.RoundToInt((gridWorldSize.x - 1) * relativeX);
            int y = Mathf.RoundToInt((gridWorldSize.y - 1) * relativeY);

            return grid[x, y];
        }
        /// <summary>
        /// Получить соседей выбранного узла сетки.
        /// </summary>
        /// <param name="node">Узел сетки.</param>
        /// <returns></returns>
        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();
            // Проверяем позиции вокруг заданного узла (блок 3х3).
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    // Пропускаем центр блока 3х3 (позиция заданного узла).
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
            Gizmos.DrawWireCube(transform.position, new Vector2(gridWorldSize.x, gridWorldSize.y));

            if (grid != null)
            {
                foreach (Node node in grid)
                {
                    Gizmos.color = node.IsObstacle ? Color.red : Color.white;
                    Gizmos.DrawWireCube(node.WorldPosition, Vector2.one * (nodeSize - 0.1f));
                    if (Path != null)
                    {
                        if (Path.Contains(node))
                        {
                            Gizmos.color = Color.black;
                            Gizmos.DrawCube(node.WorldPosition, Vector2.one * (nodeSize - 0.1f));
                        }
                    }
                }
            }
        }
    }
}
