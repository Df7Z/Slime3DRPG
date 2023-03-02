using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntityHPBar : MonoBehaviour
{
    [SerializeField] private Image _bar;
    [SerializeField] private TextMeshProUGUI _textHp;

    private void Awake()
    {
        _bar.fillAmount = 1.0f;
    }

    public void UpdateBar(int currentHealth, int maxHealth)
    {
        _textHp.text = currentHealth + "/" + maxHealth;
        _bar.fillAmount = (float) currentHealth / maxHealth;
    }
}
