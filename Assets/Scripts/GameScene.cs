using System;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    [SerializeField] private Enemy[] _activatedEnemys;
    [SerializeField] private Transform _playerSceneTransform;
    public Enemy[] ActivatedEnemys => _activatedEnemys;
    public Transform PlayerSceneTransform => _playerSceneTransform;
    public Action OnSceneComplete;
    
    private void EnemyDeath()
    {
        if (AllSceneEnemyDead())
        {
            SceneComplete();
        }
    }

    private bool AllSceneEnemyDead()
    {
        foreach (var enemy in _activatedEnemys)
        {
            if (!enemy.IsDead)
                return false;
        }
        
        return true;
    }
    
    public void StartScene(Player player)
    {
        foreach (var enemy in _activatedEnemys)
        {
            enemy.Activate(player);
            enemy.OnEnemyDeath += EnemyDeath;
        }
    }

    private void SceneComplete()
    {
        OnSceneComplete?.Invoke();
    }

    public void ResetScene()
    {
        foreach (var enemy in ActivatedEnemys)
        {
            enemy.gameObject.SetActive(true);
            enemy.ResetData();
        }
    }
}
