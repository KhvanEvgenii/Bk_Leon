using System.IO;
using DarkenDinosaur.Data;
using UnityEngine;
using UnityEngine.Events;

namespace DarkenDinosaur.Managers
{
    public class SaveManager : MonoBehaviour
    {
        [Header("Game data")] 
        [Tooltip("Game data structure.")]
        [SerializeField] private GameData gameData;

        [Header("Settings")] 
        [Tooltip("Save file name.")]
        [SerializeField] private string saveFileName;
        
        [Tooltip("Save directory name.")]
        [SerializeField] private string saveDirectoryName;
        
        [Tooltip("Path to save directory.")]
        [SerializeField] private string saveDirectoryPath;
        
        [Tooltip("Path to save file.")]
        [SerializeField] private string saveFilePath;

        [SerializeField] private UnityEvent<GameData> dataLoaded;
        [SerializeField] private UnityEvent<GameData> dataSaved;
        
        /// <summary>
        /// Restart level event handler.
        /// </summary>
        public void OnRestartLevel()
        {
            SaveGame();
        }

        private void SaveGame()
        {
            string json = JsonUtility.ToJson(this.gameData);
            File.WriteAllText(this.saveFilePath, json);
            this.dataSaved?.Invoke(this.gameData);
        }

        private void Awake()
        {
#if UNITY_ANDROID || UNITY_IOS
            this.saveDirectoryPath = Path.Combine(Application.persistentDataPath, this.saveDirectoryName);
            this.saveFilePath = Path.Combine(Application.persistentDataPath, this.saveDirectoryName, this.saveFileName);
#endif

#if UNITY_STANDALONE || UNITY_EDITOR
            this.saveDirectoryPath = Path.Combine(Application.dataPath, this.saveDirectoryName);
            this.saveFilePath = Path.Combine(Application.dataPath, this.saveDirectoryName, this.saveFileName);
#endif

            bool initGame = false;

            if (Directory.Exists(this.saveDirectoryPath) == false)
            {
                Directory.CreateDirectory(this.saveDirectoryPath);
                initGame = true;
            }

            if (File.Exists(this.saveFilePath))
            {
                string json = File.ReadAllText(this.saveFilePath);
                GameData gameDataFromJson = JsonUtility.FromJson<GameData>(json);
                this.gameData = gameDataFromJson;
                if (initGame)
                {
                    this.gameData.highScoreCount = 0;
                    this.gameData.balance = 5000;
                }
                this.dataLoaded?.Invoke(this.gameData);
            }
        }

        /// <summary>
        /// High score changed event handler.
        /// </summary>
        /// <param name="highScore">High score count (int)</param>
        public void OnHighScoreChanged(int highScore) => this.gameData.highScoreCount = highScore;
        public void OnBalanceChanged(int balance) {
            this.gameData.balance -= balance; 
            SaveGame();
        }
    }
}