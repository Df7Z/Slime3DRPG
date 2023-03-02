using System;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button[] _restartButtons;
    [SerializeField] private GameObject _gameOverObject;
    [SerializeField] private GameObject _levelCompeleteObject;
    
    private void Awake()
    {
        foreach (var button in _restartButtons)
        {
            button.onClick.AddListener(RestartLevel);
        }
        _startButton.onClick.AddListener(StartLevel);
    }
    
    private void RestartLevel()
    {
        Gamelevel.Instance.RestartLevel();
    }
    
    private void StartLevel()
    {
        Gamelevel.Instance.StartGame();
    }

    public void GameOver()
    {
        _gameOverObject.SetActive(true);
    }

    public void LevelCompete()
    {
        _levelCompeleteObject.SetActive(true);
    }
    
}
