using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    /// <summary>
    /// coordinates of body cells in grid
    /// </summary>
    private List<Vector2Int> bodyPositions = new List<Vector2Int>();

    private Direction currentDirection = Direction.Top();

    public void Move(Direction direction)
    {
        if (direction.Opposite() == currentDirection)
        {
            // not valid movement
            direction = currentDirection;
        }

        for (int i = 0; i < bodyPositions.Count; i++)
        {
            bodyPositions[i] += direction.changeOfCoord;
        }
    }
}