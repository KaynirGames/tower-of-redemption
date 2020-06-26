using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaynirGames.Pathfinding2D
{
    public class PathfindingManager : MonoBehaviour
    {
        private Pathfinder _activePathfinder;

        private Queue<PathRequest> pathRequestQueue;

        public bool requestInProcess;

        private PathRequest currentRequest;

        private void Awake()
        {
            Pathfinder.OnActivePathfinderChanged += SetActivePathfinder;
        }

        private void Start()
        {
            pathRequestQueue = new Queue<PathRequest>();
        }

        /// <summary>
        /// Установить действующего поисковика пути.
        /// </summary>
        private void SetActivePathfinder(Pathfinder pathfinder)
        {
            _activePathfinder = pathfinder;
        }

        public void AddPathRequest(Vector2 startPoint, Vector2 endPoint, Action<Vector2[], bool> onSearchComplete)
        {
            PathRequest newRequest = new PathRequest(startPoint, endPoint, onSearchComplete);
            pathRequestQueue.Enqueue(newRequest);
            TryProcessNext();
        }

        private void TryProcessNext()
        {
            if (!requestInProcess && pathRequestQueue.Count > 0)
            {
                currentRequest = pathRequestQueue.Dequeue();
                requestInProcess = true;
                _activePathfinder.StartFindPath(currentRequest, CompleteRequest);
            }
        }

        private void CompleteRequest()
        {
            requestInProcess = false;
            TryProcessNext();
        }
    }
}
