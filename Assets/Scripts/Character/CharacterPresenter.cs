using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPresenter : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI CoinCounter;
    [SerializeField] private TextMeshProUGUI KeyCounter;
    [SerializeField] private Slider HealthSlider;

    public void UpdateCounterUI()
    {
        CoinCounter.text = InventoryManager.Instance.GetCoins().ToString();
        KeyCounter.text = InventoryManager.Instance.GetKeys().ToString();
    }

    public void UpdateHealthUI(float currentHealth)
    {
        HealthSlider.value = currentHealth;
    }
}
