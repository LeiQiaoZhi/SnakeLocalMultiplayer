using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeRenderer : MonoBehaviour
{
    public GameObject snakeHeadPrefab;
    public GameObject snakeBodyPrefab;

    private List<GameObject> snakeBodies;
    private GridSystem gridSystem;

    private void Awake()
    {
        gridSystem = FindObjectOfType<GridSystem>();
    }

    public void InitBodies(int length)
    {
        snakeBodies = new List<GameObject>();
        for (int i = 0; i < length; i++)
        {
            var body = Instantiate((i==0)?snakeHeadPrefab:snakeBodyPrefab, transform);
            snakeBodies.Add(body);
        }
    }

    public void RenderSnake(List<Vector2Int> bodyPositions)
    {
        if (bodyPositions.Count != snakeBodies.Count)
        {
            XLogger.LogError("body positions count != snake bodies count");
        }
        for (int i = 0; i < bodyPositions.Count; i++)
        {
            var worldPos = gridSystem.CellToWorldPosition(
                            bodyPositions[i].x, bodyPositions[i].y);
            snakeBodies[i].transform.position = worldPos;
        }
    }
}