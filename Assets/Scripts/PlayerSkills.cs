using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkills : MonoBehaviour
{
    [SerializeField] private Button _upgradeHealth;
    [SerializeField] private TextMeshProUGUI _upgradeHealthOutText;
    [SerializeField] private Button _upgradeSpeed;
    [SerializeField] private TextMeshProUGUI _upgradeSpeedOutText;
    [SerializeField] private Button _upgradeDamage;
    [SerializeField] private TextMeshProUGUI _upgradeDamageOutText;
    [SerializeField] private PlayerBank _playerBank;
    [SerializeField] private Player _player;
    private int _damageCost = 10;
    private int _speedCost = 10;
    private int _healthCost = 10;
    private const string UpgradeHealthText = "Увеличить здоровье";
    private const string UpgradeDamageText = "Увеличить урон атаки";
    private const string UpgradeSpeedText = "Увеличить скорость атаки";
    private const string SymbolMoney = "$";
    
    private void Awake()
    {
        _upgradeHealth.onClick.AddListener(UpgradeHealth);
        _upgradeSpeed.onClick.AddListener(UpgradeSpeed);
        _upgradeDamage.onClick.AddListener(UpgradeDamage);
        Gamelevel.OnLevelRestart += ResetData;
    }

    private void ResetData()
    {
        _healthCost = 10;
        _speedCost = 10;
        _damageCost = 10;
        
        UpdateSkillInfo(_upgradeHealthOutText, UpgradeHealthText, _healthCost);
        UpdateSkillInfo(_upgradeSpeedOutText, UpgradeSpeedText, _speedCost);
        UpdateSkillInfo(_upgradeDamageOutText, UpgradeDamageText, _damageCost);
    }
    
    private void UpgradeHealth()
    {
        if (_playerBank.TakeMoney(_healthCost))
        {
            _healthCost *= 2;
            _player.UpgradeHealth();
            UpdateSkillInfo(_upgradeHealthOutText, UpgradeHealthText, _healthCost);
        }
    }
    
    private void UpgradeSpeed()
    {
        if (_playerBank.TakeMoney(_speedCost))
        {
            _speedCost *= 2;
            _player.UpgradeSpeed();
            UpdateSkillInfo(_upgradeSpeedOutText, UpgradeSpeedText, _speedCost);
        }
    }
    
    private void UpgradeDamage()
    {
        if (_playerBank.TakeMoney(_damageCost))
        {
            _damageCost *= 2;
            _player.UpgradeDamage();
            UpdateSkillInfo(_upgradeDamageOutText, UpgradeDamageText, _damageCost);
        }
    }

    private void UpdateSkillInfo(TextMeshProUGUI _textMeshPro, string baseText, int cost)
    {
        _textMeshPro.text = baseText + " (" + cost.ToString() + SymbolMoney + ")";
    }
}
