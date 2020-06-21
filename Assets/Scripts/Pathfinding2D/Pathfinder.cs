using System;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;

namespace KaynirGames.Pathfinding2D
{
    [RequireComponent(typeof(NodeGrid))]
    public class Pathfinder : MonoBehaviour
    {
        /// <summary>
        /// Сетка узлов.
        /// </summary>
        private NodeGrid grid;

        public Transform seeker, target;

        private Stopwatch pathTime = new Stopwatch();

        private void Awake()
        {
            grid = GetComponent<NodeGrid>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FindPath(seeker.position, target.position);
            }
        }
        /// <summary>
        /// Найти оптимальный путь.
        /// </summary>
        /// <param name="startPos">Начальная позиция.</param>
        /// <param name="targetPos">Конечная позиция.</param>
        public void FindPath(Vector2 startPos, Vector2 targetPos)
        {
            pathTime.Start();

            Node startNode = grid.GetNode(startPos);
            Node targetNode = grid.GetNode(targetPos);

            List<Node> openSet = new List<Node>(); // Набор узлов для проверки.
            HashSet<Node> closedSet = new HashSet<Node>(); // Набор проверенных узлов.

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet[0];

                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].FCost < currentNode.FCost || openSet[i].FCost == currentNode.FCost && openSet[i].HCost < currentNode.HCost)
                    {
                        currentNode = openSet[i];
                    }
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    grid.Path = RetracePath(startNode, targetNode);
                    pathTime.Stop();
                    UnityEngine.Debug.Log($"Путь найден за: {pathTime.ElapsedMilliseconds} мс.");
                    return;
                }

                List<Node> neighbours = grid.GetNeighbours(currentNode);

                foreach (Node neighbour in neighbours)
                {
                    if (neighbour.IsObstacle || closedSet.Contains(neighbour))
                        continue;

                    int newPathToNeighbour = currentNode.GCost + currentNode.GetDistance(neighbour);

                    if (newPathToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
                    {
                        neighbour.GCost = newPathToNeighbour;
                        neighbour.HCost = neighbour.GetDistance(targetNode);
                        neighbour.Parent = currentNode;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }
        }
        /// <summary>
        /// Записать найденный путь.
        /// </summary>
        /// <param name="startNode">Начальный узел.</param>
        /// <param name="endNode">Конечный узел.</param>
        /// <returns></returns>
        private List<Node> RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode; // Начинаем с конца пути.

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }
            // Переворачиваем путь, так как шли с конца.
            path.Reverse(); 

            return path;
        }
    }
}

