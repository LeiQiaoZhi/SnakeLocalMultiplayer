using System;
using System.Collections.Generic;
using _Scripts.Grid;
using _Scripts.Helpers;
using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Snake
{
    /// <summary>
    /// singleton class that manages snakes
    /// </summary>
    public class SnakeGameManager : MonoBehaviour
    {
        public List<SnakeMovement> snakes = new List<SnakeMovement>();
        private GridSystem gridSystem;
        private int height;
        private int width;

        public static SnakeGameManager Instance { get; private set; }

        private void Awake()
        {
            gridSystem = FindObjectOfType<GridSystem>();
            height = gridSystem.height;
            width = gridSystem.width;

            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            Time.timeScale = 1;
        }

        /// <returns>positions that are in bound and not occupied by snakes</returns>
        public List<Vector2Int> GetFreePositions()
        {
            var freePositions = new List<Vector2Int>();
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    if (IsColliding(x, y))
                    {
                        continue;
                    }

                    freePositions.Add(new Vector2Int(x, y));
                }
            }

            return freePositions;
        }


        /// <summary>
        /// Given a position, returns true if collision happens
        /// 1. there is a snake in that position
        /// 2. out of bounds
        /// </summary>
        public bool IsColliding(int x, int y)
        {
            if (x < 0 || x >= width || y < 0 || y >= height)
            {
                return true;
            }

            foreach (var snake in snakes)
            {
                foreach (var bodyPosition in snake.GetPositions())
                {
                    if (bodyPosition.x == x && bodyPosition.y == y)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsColliding(Vector2Int newHeadPosition)
        {
            return IsColliding(newHeadPosition.x, newHeadPosition.y);
        }

        public void GameOver(GameObject loser)
        {
            var winner = snakes.Find(snake => snake.name != loser.name);
            var text = $"{winner.name} wins!";
            var winColor = winner.GetComponentsInChildren<SpriteRenderer>()[0].color;
            XLogger.LogWarning(Category.Snake, text);
            UIManager.Instance.SetEnableGameOverScreen(true, text, winColor);
            Time.timeScale = 0;
        }
    }
}