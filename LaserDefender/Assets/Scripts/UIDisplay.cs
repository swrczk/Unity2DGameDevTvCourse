using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private ScoreKeeper scoreKeeper;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    [Header("Player Health")]
    [SerializeField] private Health playerHealth;
    [SerializeField] private Slider healthSlider;

    private void Start()
    {
        healthSlider.maxValue = playerHealth.MaxHealth;
    }

    private void Update()
    {
        scoreText.text = scoreKeeper.Score.ToString("000000000");
        healthSlider.value = playerHealth.CurrentHealth;
    }
}