using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRouteGenerator : MonoBehaviour
{
    public List<Vector2Int> SelectedGridPositions = new List<Vector2Int>();

    private readonly List<Vector2Int> possibleDirections = new List<Vector2Int>() { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

    private int routeCount;
    private int maxRouteCount;
    private int maxRouteLength;

    public void InitializeGenerator(DungeonStage dungeonStage)
    {
        maxRouteCount = Random.Range(dungeonStage.MinRouteCount, dungeonStage.MaxRouteCount + 1);
        maxRouteLength = Random.Range(dungeonStage.MinRouteLength, dungeonStage.MaxRouteLength + 1);
        Debug.Log($"Максимальное число коридоров: {maxRouteCount}");
        Debug.Log($"Максимальная длина коридора: {maxRouteLength}");

        CreateRoute(Vector2Int.zero, 0);
    }

    public void CreateRoute(Vector2Int currentPos, int routeLength)
    {
        // Создаем новые коридоры, если не достигли их максимального числа на этаже подземелья.
        while (routeCount < maxRouteCount)
        {     
            // Углубляем коридор, пока не достигли его максимальной длины.
            if (routeLength < maxRouteLength)
            {
                routeLength++;

                SelectedGridPositions.Add(currentPos);

                // Вычисляем свободные направления относительно текущей точки.
                List<Vector2Int> freeDirections = GetFreeDirections(currentPos);

                // Выбираем дальнейшее направление и создаем новый коридор.
                if (freeDirections.Count > 0)
                {                  
                    CreateRoute(currentPos + ChooseRandomDirection(freeDirections), Random.Range(routeLength, maxRouteLength));
                }
                else
                {
                    routeCount++;
                }
            }
            else
            {
                routeCount++;
                routeLength = 0;
            }
        }
    }

    /// <summary>
    /// Найти свободные направления на сетке координат относительно текущей точки.
    /// </summary>
    /// <param name="currentPos">Предыдущая точка.</param>
    /// <returns></returns>
    private List<Vector2Int> GetFreeDirections(Vector2Int currentPos)
    {
        //if (DoesPositionTaken(previousPos))
        //    return new List<Vector2Int>();

        List<Vector2Int> freeDirections = new List<Vector2Int>();

        foreach (var direction in possibleDirections)
        {
            if (!DoesPositionTaken(currentPos + direction))
            {
                freeDirections.Add(direction);
            }
        }
        return freeDirections;
    }

    private Vector2Int ChooseRandomDirection(List<Vector2Int> freeDirections)
    {
        return freeDirections[Random.Range(0, freeDirections.Count)];
    }

    /// <summary>
    /// Занята ли позиция в сетке подземелья?
    /// </summary>
    private bool DoesPositionTaken(Vector2Int gridPosition)
    {
        return SelectedGridPositions.Exists(pos => pos == gridPosition);
    }
}
