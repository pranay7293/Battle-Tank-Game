using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI healthText;

    public void UpdateHealthBar(int currentHealth)
    {
        healthBar.value = currentHealth;
        healthText.text = currentHealth.ToString(); ;
    }
}
