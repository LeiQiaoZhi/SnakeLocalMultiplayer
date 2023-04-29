using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreItem : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    public void SetIconColor(Color color)
    {
        iconImage.color = color;
    }
    
    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }
    
    public void AddScore(int score)
    {
        var currentScore = int.Parse(scoreText.text);
        scoreText.text = (currentScore + score).ToString();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
