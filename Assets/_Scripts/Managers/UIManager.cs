using System;
using _Scripts.Helpers;
using UnityEngine;
using TMPro;

namespace _Scripts.Managers
{
    /// <summary>
    /// singleton class that manages UI
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        public TextMeshProUGUI gameOverText;
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject levelEndScreen;
        [SerializeField] private GameObject pauseScreen;

        public static UIManager Instance { get; private set; }

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

            SetEnableGameOverScreen(false,"Game Over", Color.red);
            SetEnableLevelEndScreen(false);
        }

        public void SetEnableGameOverScreen(bool enable, string gameOverTextContent, Color color)
        {
            gameOverScreen.SetActive(enable);
            gameOverText.color = color;
            gameOverText.text = gameOverTextContent;
        }

        public void SetEnableLevelEndScreen(bool enable)
        {
            levelEndScreen.SetActive(enable);
        }

        public void DisplayAchievementUnlockMessage(int i)
        {
            AchievementManager achievementManager = AchievementManager.instance;
            if (achievementManager.IsAchievementUnlocked(i))
            {
                XLogger.Log(Category.Achievement,
                    $"Achivement {achievementManager.achievementNames[i]} is already unlocked");
                return;
            }

            achievementManager.UnlockAchievement(i);
            MessageManager.Instance.DisplayMessage(
                $"Achievement Unlock: {achievementManager.achievementNames[i].ToUpper()}");
        }

        public void Pause()
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }

        public void Resume()
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
}