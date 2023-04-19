using System.Collections.Generic;
using _Scripts.Grid;
using _Scripts.Helpers;
using UnityEngine;

namespace _Scripts.Snake
{
    [RequireComponent(typeof(SnakeInput))]
    [RequireComponent(typeof(SnakeMovement))]
    [RequireComponent(typeof(SnakeRenderer))]
    public class SnakeInitializer : MonoBehaviour
    {
        [SerializeField] private int length = 3;
        
        private readonly Direction direction = Direction.Top();
    
        private SnakeRenderer snakeRenderer;
        private SnakeMovement snakeMovement;
        private SnakeInput snakeInput;
        private GridSystem gridSystem;
        // ReSharper disable once InconsistentNaming
        private SnakeInitInfo _snakeInitInfo;

        public void Init(SnakeInitInfo snakeInitInfo, Vector2Int position)
        {
            _snakeInitInfo = snakeInitInfo;
            gridSystem = FindObjectOfType<GridSystem>();
            snakeInput = GetComponent<SnakeInput>();
            snakeMovement = GetComponent<SnakeMovement>();
            snakeRenderer = GetComponent<SnakeRenderer>();
            
            XLogger.Log(Category.Snake, $"snake init with info : {snakeInitInfo}");
            snakeInput.SetControls(snakeInitInfo.control);
            snakeRenderer.SetColors(snakeInitInfo.color, snakeInitInfo.color);
            
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
        
        public Color GetColor()
        {
            return _snakeInitInfo.color;
        }

        public int GetLength()
        {
            return snakeMovement.GetLength();
        }
    }
}
