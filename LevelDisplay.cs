using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LevelDisplay : MonoBehaviour
{
    public TMP_Text levelText;
    public TMP_Text currencyText;
    public Slider experienceSlider;
    public LevelingSystem levelingSystem;
    private int currentCurrency;
    private Color originalColor;
    
    private void Start()
    {
        currentCurrency = levelingSystem.GetCurrentCurrency();
        originalColor = currencyText.color;
    }

    private void Update()
    {
        int currentLevel = levelingSystem.GetCurrentLevel();
        float currentExperience = levelingSystem.GetCurrentExperience();
        float maxExperience = levelingSystem.GetExperienceNeededForNextLevel();

        int updatedCurrency = levelingSystem.GetCurrentCurrency();

        if (updatedCurrency > currentCurrency)
        {
            // Animate the increase in currency value
            LeanTween.scale(currencyText.gameObject, Vector3.one * 1.5f, 0.5f).setEaseOutBounce();
            LeanTween.value(currencyText.gameObject, Color.white, Color.yellow, 0.5f).setEaseOutBounce()
                .setOnUpdate((Color val) => { currencyText.color = val; });

            // Schedule a decrease in scale and color after a few seconds
            LeanTween.scale(currencyText.gameObject, Vector3.one, 0.5f).setDelay(2f).setEaseInBounce();
            LeanTween.value(currencyText.gameObject, Color.yellow, originalColor, 0.5f).setDelay(2f).setEaseInBounce()
                .setOnUpdate((Color val) => { currencyText.color = val; });

            currentCurrency = updatedCurrency;
        }

        levelText.text = " " + currentLevel;
        currencyText.text = "$" + currentCurrency;
        experienceSlider.value = currentExperience / maxExperience;
    }
}
