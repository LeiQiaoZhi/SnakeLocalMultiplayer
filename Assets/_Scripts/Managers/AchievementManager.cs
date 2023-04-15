using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Managers
{
    [System.Serializable]
    public class DeathCountAchievement
    {
        public int deathCount;
        public int achievementIndex;
    }

    /// <summary>
    /// Singleton class for managing achievements
    /// Dependencies: <c>MessageManager</c>
    /// </summary>
    public class AchievementManager : MonoBehaviour
    {
        // singleton
        public static AchievementManager instance;

        public List<string> achievementNames;
        public List<DeathCountAchievement> deathCountAchievements;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public int GetDeathCount()
        {
            return PlayerPrefs.GetInt("Death Count", 0);
        }

        public void IncreaseDeathCount()
        {
            int deathCount = PlayerPrefs.GetInt("Death Count", 0)+1;
            PlayerPrefs.SetInt("Death Count", deathCount);
            int achieved = -1;
            foreach (var deathCountAchievement in deathCountAchievements)
            {
                if (deathCountAchievement.deathCount <= deathCount)
                {
                    achieved = deathCountAchievement.achievementIndex;
                } 
            }
            if (achieved >= 0 && !IsAchievementUnlocked(achieved))
            {
                UnlockAchievement(achieved);
                // dependency 
                MessageManager.Instance.DisplayMessage($"Achievement unlocked: {achievementNames[achieved].ToUpper()}");
            }
        }

        public bool IsAchievementUnlocked(string name)
        {
            if (!achievementNames.Contains(name))
            {
                Debug.Log("Achievement is not registered");
                return false;
            }
            bool unlocked = PlayerPrefs.GetInt(name,0) == 1;
            return unlocked;
        }
    
        public bool IsAchievementUnlocked(int index)
        {
            bool unlocked = PlayerPrefs.GetInt(achievementNames[index],0) == 1;
            return unlocked;
        }

        public void UnlockAchievement(int index)
        {
            PlayerPrefs.SetInt(achievementNames[index],1);
        }

        public void UnlockAchievement(string name)
        {
            if (!achievementNames.Contains(name))
            {
                Debug.Log("Achievement is not registered");
                return;
            }
            PlayerPrefs.SetInt(name,1);
        }


    }
}