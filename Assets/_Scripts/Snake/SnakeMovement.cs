using System.Collections.Generic;
using _Scripts.GameEventSystem;
using _Scripts.Helpers;
using UnityEngine;

namespace _Scripts.Snake
{
    [RequireComponent(typeof(SnakeRenderer))]
    public class SnakeMovement : MonoBehaviour
    {
        public GameEvent growEvent;
        /// <summary>
        /// coordinates of body cells in grid system
        /// </summary>
        private List<Vector2Int> bodyPositions = new();

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
            var gameOver = false;
            var newHeadPosition = bodyPositions[0] + direction.changeOfCoord;
            if (SnakeGameManager.Instance.IsColliding(newHeadPosition))
            {
                XLogger.LogWarning(Category.Snake, "Snake collided with itself, others or out of bounds");
                gameOver = true;
            }
            else if (PickupManager.Instance.IsOccupiedByPickup(newHeadPosition))
            {
                // apply effect
                PickupManager.Instance.ApplyPickup(newHeadPosition, gameObject);
            }

            bodyPositions.Insert(0, newHeadPosition);
            bodyPositions.RemoveAt(bodyPositions.Count - 1);

            snakeRenderer.RenderSnake(bodyPositions);
            if (gameOver)
            {
                SnakeGameManager.Instance.SnakeDies(this);
            }
        }

        public void SetPositions(List<Vector2Int> positions)
        {
            bodyPositions = positions;
        }

        public void Grow()
        {
            bodyPositions.Add(bodyPositions[^1] + bodyPositions[^1] - bodyPositions[^2]);
            snakeRenderer.Grow();
            snakeRenderer.RenderSnake(bodyPositions);
            growEvent.Raise();
        }
        
        public int GetLength()
        {
            return bodyPositions.Count;
        }
    }
}