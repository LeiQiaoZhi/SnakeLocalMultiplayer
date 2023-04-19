using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Grid;
using _Scripts.Helpers;
using _Scripts.Managers;
using _Scripts.GameEventSystem;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Scripts.Snake
{
    /// <summary>
    /// singleton class that manages snakes
    /// </summary>
    public class SnakeGameManager : MonoBehaviour
    {
        public GameEvent gameStartEvent;
        public GameObject snakePrefab;
        
        private List<SnakeInitializer> snakes = new List<SnakeInitializer>();
        private GridSystem gridSystem;
        private int height;
        private int width;
        private List<SnakeInitializer> aliveSnakes= new List<SnakeInitializer>();

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

        /// <summary>
        /// starts the game
        /// </summary>
        public void StartGame(List<SnakeInitInfo> snakeInitInfos)
        {
            var spawnPositions = gridSystem.GetSpawnPositions(snakeInitInfos.Count);
            for (var i = 0; i < snakeInitInfos.Count; i++)
            {
                var snakeInitInfo = snakeInitInfos[i];
                var snake = Instantiate(snakePrefab, transform);
                snake.name = snakeInitInfo.name;
                var snakeInitializer = snake.GetComponent<SnakeInitializer>();
                snakeInitializer.Init(snakeInitInfo, spawnPositions[i]);
                snakes.Add(snakeInitializer);
            }
            
            aliveSnakes = snakes.ToList();

            gameStartEvent.Raise();
        }
        
        public List<SnakeInitializer> GetSnakes()
        {
            return snakes;
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

            foreach (var snake in aliveSnakes)
            {
                var snakeMovement = snake.GetComponent<SnakeMovement>();
                foreach (var bodyPosition in snakeMovement.GetPositions())
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

        private void GameOver(SnakeInitializer winner)
        {
            var text = $"{winner.gameObject.name} wins!";
            var winColor = winner.GetComponentsInChildren<SpriteRenderer>()[0].color;
            XLogger.LogWarning(Category.Snake, text);
            UIManager.Instance.SetEnableGameOverScreen(true, text, winColor);
            Time.timeScale = 0;
        }

        private void Draw(List<SnakeInitializer> winners)
        {
            // concat winners' names
            var text = winners.Aggregate("", (current, winner) => current + (winner.gameObject.name + ","));
            text += $"Draw between {text}";
            var winColor = Color.white;
            XLogger.LogWarning(Category.Snake, text);
            UIManager.Instance.SetEnableGameOverScreen(true, text, winColor);
            Time.timeScale = 0;
        }

        public void SnakeDies(SnakeMovement deadMovement)
        {
            aliveSnakes.Remove(deadMovement.GetComponent<SnakeInitializer>());
            if (aliveSnakes.Count == 0)
            {
                var candidates = ScoreManager.Instance.GetHighestScoreSnakes();
                if (candidates.Count == 1)
                {
                    GameOver(candidates[0]);
                    return;
                }

                // if there are more than one snakes with the same highest score and there is one last snake alive
                if (deadMovement.GetComponent<SnakeInitializer>().GetLength() == candidates[0].GetLength())
                {
                    GameOver(deadMovement.GetComponent<SnakeInitializer>());
                    return;
                }
                
                // draw between already dead snakes
                Draw(candidates);
                return;

            }
            
            deadMovement.gameObject.SetActive(false);
        }
    }
}