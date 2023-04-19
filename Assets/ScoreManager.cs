using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Helpers;
using _Scripts.Snake;
using UnityEngine;

/// <summary>
/// singleton class that manages score 
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public RectTransform scoreBoard;
    public GameObject scoreObjectPrefab;
    private List<SnakeInitializer> snakes = new List<SnakeInitializer>();
    private List<ScoreItem> scoreItems = new List<ScoreItem>();
    
    public static ScoreManager Instance { get; private set; }

    private void Awake()
    {
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
    /// called by the GameStart event
    /// </summary>
    public void InitScoreBoard()
    {
        snakes = SnakeGameManager.Instance.GetSnakes();
        XLogger.Log(Category.UI,"init score board with snakes count: " + snakes.Count);

        foreach (var snake in snakes)
        {
            var scoreObject = Instantiate(scoreObjectPrefab, scoreBoard);
            var scoreItem = scoreObject.GetComponent<ScoreItem>();
            scoreItem.SetIconColor(snake.GetColor());
            scoreItems.Add(scoreItem);
        }
        
        UpdateScore();
    }
    
    /// <summary>
    /// called by the SnakeGrow event
    /// </summary>
    public void UpdateScore()
    {
        for (var i = 0; i < snakes.Count; i++)
        {
            var snake = snakes[i];
            var scoreItem = scoreItems[i];
            scoreItem.SetScore(snake.GetLength());
        }
    }

    public List<SnakeInitializer> GetHighestScoreSnakes()
    {
        var highestScore = snakes.Max(snake => snake.GetLength());
        return snakes.Where(snake => snake.GetLength() == highestScore).ToList();
    }
}