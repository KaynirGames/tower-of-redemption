using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaynirGames.Pathfinding2D
{
    [RequireComponent(typeof(NodeGrid))]
    public class Pathfinder : MonoBehaviour
    {
        /// <summary>
        /// Событие на изменение действующего поисковика пути.
        /// </summary>
        public static event Action<Pathfinder> OnActivePathfinderChanged = delegate { };
        /// <summary>
        /// Сетка узлов.
        /// </summary>
        private NodeGrid _grid;

        private void Awake()
        {
            _grid = GetComponent<NodeGrid>();
        }
        /// <summary>
        /// Начать поиск оптимального пути.
        /// </summary>
        /// <param name="pathRequest">Запрос на поиск пути.</param>
        /// <param name="onRequestComplete">Действие по завершению запроса.</param>
        public void StartFindPath(PathRequest pathRequest, Action onRequestComplete)
        {
            StartCoroutine(FindPath(pathRequest, onRequestComplete));
        }
        /// <summary>
        /// Найти оптимальный путь.
        /// </summary>
        /// <param name="pathRequest">Запрос на поиск пути.</param>
        /// <param name="onRequestComplete">Действие по завершению запроса.</param>
        private IEnumerator FindPath(PathRequest pathRequest, Action onRequestComplete)
        {
            Node startNode = _grid.GetNode(pathRequest.StartPoint); // Начальный узел сетки.
            Node endNode = _grid.GetNode(pathRequest.EndPoint); // Конечный узел сетки.

            bool pathFound = false; // Найден ли путь?
            List<Vector2> waypoints = new List<Vector2>(); // Точки найденного пути.

            if (startNode.IsObstacle || endNode.IsObstacle)
            {
                pathRequest.OnSearchComplete?.Invoke(waypoints.ToArray(), pathFound);
                onRequestComplete?.Invoke();
                yield break;
            }

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

                // Выходим из цикла при достижении нужного узла.
                if (currentNode == endNode)
                {
                    pathFound = true;
                    break;
                }

                List<Node> neighbours = _grid.GetNeighbours(currentNode);
                // Выполняем проверку соседей текущего узла.
                foreach (Node neighbour in neighbours)
                {
                    if (neighbour.IsObstacle || closedSet.Contains(neighbour))
                        continue;
                    // Рассчитываем длину пути до соседнего узла.
                    int newPathToNeighbour = currentNode.GCost + currentNode.GetDistance(neighbour);

                    if (newPathToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
                    {
                        neighbour.GCost = newPathToNeighbour;
                        neighbour.HCost = neighbour.GetDistance(endNode);
                        neighbour.Parent = currentNode;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }

            yield return null;

            if (pathFound)
            {
                waypoints = RetracePath(startNode, endNode);
            }
            pathRequest.OnSearchComplete?.Invoke(waypoints.ToArray(), pathFound);
            onRequestComplete?.Invoke();
        }
        /// <summary>
        /// Записать координаты точек найденного пути.
        /// </summary>
        private List<Vector2> RetracePath(Node startNode, Node endNode)
        {
            List<Vector2> waypoints = new List<Vector2>();
            Node currentNode = endNode; // Начинаем с конца пути.

            while (currentNode != startNode)
            {
                waypoints.Add(currentNode.WorldPosition);
                currentNode = currentNode.Parent;
            }
            // Переворачиваем путь, так как шли с конца.
            waypoints.Reverse();

            return waypoints;
        }

        private void OnEnable()
        {
            OnActivePathfinderChanged?.Invoke(this);
        }
    }
}

