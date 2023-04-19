using System.Collections.Generic;
using _Scripts.Grid;
using _Scripts.Helpers;
using UnityEngine;

namespace _Scripts.Snake
{
    public class SnakeRenderer : MonoBehaviour
    {
        public GameObject snakeHeadPrefab;
        public GameObject snakeBodyPrefab;
        [Header("Color")] 
        private Color headColor;
        private Color bodyColor;

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
                var body = Instantiate((i == 0) ? snakeHeadPrefab : snakeBodyPrefab, transform);
                body.GetComponentInChildren<SpriteRenderer>().color = (i == 0) ? headColor : bodyColor;
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

        public void Grow()
        {
            var body = Instantiate(snakeBodyPrefab, transform);
            body.GetComponentInChildren<SpriteRenderer>().color = bodyColor;
            snakeBodies.Add(body);
        }
        
        public void SetColors(Color headColor, Color bodyColor)
        {
            this.headColor = headColor;
            this.bodyColor = bodyColor;
        }
    }
}