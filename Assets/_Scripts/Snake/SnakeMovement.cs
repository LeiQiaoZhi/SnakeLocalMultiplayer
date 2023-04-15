using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SnakeRenderer))]
public class SnakeMovement : MonoBehaviour
{
    /// <summary>
    /// coordinates of body cells in grid system
    /// </summary>
    public List<Vector2Int> bodyPositions = new();

    private Direction currentDirection = Direction.Top();
    private SnakeRenderer snakeRenderer;

    private void Awake()
    {
        snakeRenderer = GetComponent<SnakeRenderer>();
    }

    public List<Vector2Int> GetPositions()
    {
        return bodyPositions;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    /// <summary>
    /// moves snake in given direction
    /// </summary>
    /// <param name="direction"></param>
    public void Move(Direction direction)
    {
        if (direction.Opposite().Equals(currentDirection))
        {
            // not valid movement
            direction = currentDirection;
        }

        currentDirection = direction;

        // first is head
        bodyPositions.Insert(0, bodyPositions[0] + direction.changeOfCoord);
        bodyPositions.RemoveAt(bodyPositions.Count - 1);

        snakeRenderer.RenderSnake(bodyPositions);
    }

    public void SetPositions(List<Vector2Int> positions)
    {
        bodyPositions = positions;
    }
}