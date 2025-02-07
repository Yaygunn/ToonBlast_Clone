using UnityEngine;

namespace YBlast.Systems
{
    public static class SaveSystem
    {
        private const string _levelSaveString = "LevelSaveString";

        public static void IncreaseLevel()
        {
            int currentLevel = PlayerPrefs.GetInt(_levelSaveString);
            
            PlayerPrefs.SetInt(_levelSaveString ,currentLevel +1);
        }

        public static int GetLevelIndex()
        {
            return PlayerPrefs.GetInt(_levelSaveString);
        }
    }
}
