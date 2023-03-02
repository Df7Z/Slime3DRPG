using System;
using UnityEngine;

public class PlayerBank : MonoBehaviour
{
    [SerializeField] private int _startMoney = 20;
    private int _money;
    public int Money => _money;
    public Action<int> OnMoneyChange;

    private void Awake()
    {
        Enemy.OnEnemyDeathReward += AddMoney;
        Gamelevel.OnLevelRestart += ResetData;
        ResetData();
    }

    public void AddMoney(int value)
    {
        _money += value;
        OnMoneyChange?.Invoke(_money);
    }

    public bool TakeMoney(int value)
    {
        if ((Money - value) < 0)
            return false;

        _money -= value;
        OnMoneyChange?.Invoke(_money);
        return true;
    }

    public void ResetData()
    {
        _money = _startMoney;
        OnMoneyChange?.Invoke(_money);
    }
    
}
