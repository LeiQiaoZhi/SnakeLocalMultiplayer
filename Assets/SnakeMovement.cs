using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SnakeRenderer))]
public class SnakeMovement : MonoBehaviour
{
    /// <summary>
    /// coordinates of body cells in grid
    /// </summary>
    private List<Vector2Int> bodyPositions = new List<Vector2Int>();

    private Direction currentDirection = Direction.Top();
    private SnakeRenderer snakeRenderer;

    private void Awake()
    {
        snakeRenderer = GetComponent<SnakeRenderer>();
    }
    
    public void Move(Direction direction)
    {
        if (direction.Opposite() == currentDirection)
        {
            // not valid movement
            direction = currentDirection;
        }
        
        // first is head
        bodyPositions.Insert(0,bodyPositions[0]+direction.changeOfCoord);
        bodyPositions.RemoveAt(bodyPositions.Count - 1);

        snakeRenderer.RenderSnake(bodyPositions);
    }

    public void SetPositions(List<Vector2Int> positions)
    {
        bodyPositions = positions;
    }
}