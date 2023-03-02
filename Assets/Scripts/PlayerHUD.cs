using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyCounter;
    [SerializeField] private PlayerBank _playerBank;

    private void Awake()
    {
        _playerBank.OnMoneyChange += UpdateMoneyCounter;
        UpdateMoneyCounter(_playerBank.Money);
    }

    private void UpdateMoneyCounter(int value)
    {
        _moneyCounter.text = value.ToString();
    }
}
