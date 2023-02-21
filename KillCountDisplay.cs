using UnityEngine;
using TMPro;

public class KillCountDisplay : MonoBehaviour
{
    public ScoringSystem scoringSystem;
    public TMP_Text killCountText;

    private void Update()
    {
        killCountText.text = "Kill Count: " + scoringSystem.currentKills;
    }
}