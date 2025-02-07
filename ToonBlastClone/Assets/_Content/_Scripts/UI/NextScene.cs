using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace YBlast.UI
{
    public class NextScene : MonoBehaviour
    {
        [SerializeField] private GameObject _nextLevelPanel;
        [SerializeField] private Button _nextLevelButton;

        private void OnEnable()
        {
            EventHub.Ev_GameWon += OnGameWon;
            _nextLevelButton.onClick.AddListener(OpenNextLevel);
        }

        private void OnDisable()
        {
            EventHub.Ev_GameWon -= OnGameWon;
            _nextLevelButton.onClick.RemoveListener(OpenNextLevel);
        }

        private void OnGameWon()
        {
            _nextLevelPanel.SetActive(true);
        }
        
        private void OpenNextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
