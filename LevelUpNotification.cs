using UnityEngine;
using TMPro;
using System.Collections;

public class LevelUpNotification : MonoBehaviour
{
    public TMP_Text levelUpText;
    public AudioClip levelUpSound;
    public float displayDuration = 2f;
    public AnimationCurve scaleAnimation;

    private LevelingSystem levelingSystem;
    private AudioSource audioSource;
    private Vector3 originalScale;
    private int previousLevel;

    private void Start()
    {
        levelingSystem = GetComponent<LevelingSystem>();
        audioSource = GetComponent<AudioSource>();
        originalScale = levelUpText.rectTransform.localScale;
        previousLevel = levelingSystem.currentLevel;
        levelUpText.enabled = false;
    }

    private void Update()
    {
        if (levelingSystem.currentLevel > levelingSystem.levelUpExp.Length) return;

        if (levelingSystem.currentLevel > previousLevel)
        {
            StartCoroutine(ShowLevelUpText());
            previousLevel = levelingSystem.currentLevel;
        }
    }

    private IEnumerator ShowLevelUpText()
    {
        levelUpText.text = "Level " + levelingSystem.currentLevel;
        levelUpText.enabled = true;
        audioSource.PlayOneShot(levelUpSound);

        float elapsedTime = 0f;
        while (elapsedTime < displayDuration)
        {
            float scale = scaleAnimation.Evaluate(elapsedTime / displayDuration);
            levelUpText.rectTransform.localScale = originalScale * scale;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        levelUpText.enabled = false;
        levelUpText.rectTransform.localScale = originalScale;
    }
}