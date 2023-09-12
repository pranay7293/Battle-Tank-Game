using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private TextMeshPro healthText;

    public void UpdateHealth(int currentHealth)
    {
        healthText.text = currentHealth.ToString(); ;
    }
}
