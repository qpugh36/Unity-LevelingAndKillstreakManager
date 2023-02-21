using UnityEngine;
using TMPro;

public class ShowPlayerName : MonoBehaviour
{
    public TMP_Text playerNameText; // Reference to the TextMeshPro component
    public GameObject GameManager; // Reference to the player object

    void Update()
    {
        // Get the player name and set it as the text of the TextMeshPro component
        string playerName = GameManager.GetComponent<LevelingSystem>().playerID;
        string playerRank = GameManager.GetComponent<LevelingSystem>().GetMilitaryRank();
        playerNameText.text = playerRank+ " - " + playerName;
    }
}