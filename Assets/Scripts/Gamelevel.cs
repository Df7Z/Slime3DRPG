using System;
using UnityEngine;

public class Gamelevel : MonoBehaviour
{
  [Header("Все игровые сцены на уровне")] 
  [SerializeField] private GameScene[] _levelGameScenes;
  [SerializeField] private GameMenu _gameMenu;
  [SerializeField] private Player _player;
  private int _currentSceneIndex;
  public static Gamelevel Instance;
  public static Action OnLevelRestart;
  public Action OnGameSceneComplete;
  public Action OnGameOver;
  public Action OnLevelComplete;
  
  private void Awake() 
  {
    if (Instance == null)
    {
      transform.parent = null;
      Instance = this;
      DontDestroyOnLoad(Instance.gameObject);
      Init();
    }
    else
    {
      Destroy(gameObject);
    } 
  }

  private void Init()
  {
    OnGameOver +=_gameMenu.GameOver;
    OnLevelComplete += _gameMenu.LevelCompete;
    _player.OnPlayerDead += GameOver;
  }
  
  public void StartGame()
  {
    if (_levelGameScenes.Length == 0) Debug.LogWarning("Не указаны игровые сцены уровня!");
    StartScene(0);
  }

  public void NextScene()
  {
    _levelGameScenes[_currentSceneIndex].OnSceneComplete -= NextScene;
    OnGameSceneComplete?.Invoke();

    if (_currentSceneIndex + 1 > _levelGameScenes.Length - 1)
    {
      EndLevel();
      return;
    }

    _currentSceneIndex++;
    StartScene(_currentSceneIndex);
  }

  private void StartScene(int index)
  {
    _levelGameScenes[index].StartScene(_player);
    _levelGameScenes[index].OnSceneComplete += NextScene;
    _player.MoveToNextScene(_levelGameScenes[index]);
  }

  private void GameOver()
  {
    OnGameOver?.Invoke();
    Debug.LogWarning("Проигрыш!");
  }
  
  private void EndLevel()
  {
    OnLevelComplete?.Invoke();
    Debug.LogWarning("Уровень пройден");
  }

  public void RestartLevel()
  {
    _player.ResetData();
    _levelGameScenes[_currentSceneIndex].OnSceneComplete -= NextScene;
    
    foreach (GameScene gameScene in _levelGameScenes)
    {
      gameScene.ResetScene();
    }
    
    OnLevelRestart?.Invoke();
    _currentSceneIndex = 0;
    StartScene(_currentSceneIndex);
  }
  
}
