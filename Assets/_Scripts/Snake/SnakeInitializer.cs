using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SnakeInput))]
[RequireComponent(typeof(SnakeMovement))]
[RequireComponent(typeof(SnakeRenderer))]
public class SnakeInitializer : MonoBehaviour
{
    [SerializeField] private int length = 3;
    [SerializeField] private Vector2Int position; 
    private readonly Direction direction = Direction.Top();
    
    private SnakeRenderer snakeRenderer;
    private SnakeMovement snakeMovement;
    private SnakeInput snakeInput;
    private GridSystem gridSystem;

    void Start()
    {
        gridSystem = FindObjectOfType<GridSystem>();
        snakeInput = GetComponent<SnakeInput>();
        snakeMovement = GetComponent<SnakeMovement>();
        snakeRenderer = GetComponent<SnakeRenderer>();
        Init();
    }

    private void Init()
    {
        snakeRenderer.InitBodies(length);
        var positions = new List<Vector2Int>();
        for (int i = 0; i < length; i++)
        {
            positions.Add(position + direction.Opposite().changeOfCoord * i );
        }

        snakeMovement.SetPositions(positions);
        snakeRenderer.RenderSnake(positions);
        snakeInput.StartCoroutine(snakeInput.SnakeInputCoroutine());
    }
}
