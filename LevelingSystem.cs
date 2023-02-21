using System;
using UnityEngine;

public class LevelingSystem : MonoBehaviour
{
    public int currentLevel = 1;
    public int currentExp = 0;
    public int[] levelUpExp = new int[]
    {
        100, 200, 300, 400, 500, 600, 700, 800, 900, 1000,
        1100, 1200, 1300, 1400, 1500, 1600, 1700, 1800, 1900, 2000,
        2100, 2200, 2300, 2400, 2500, 2600, 2700, 2800, 2900, 3000,
        3100, 3200, 3300, 3400, 3500, 3600, 3700, 3800, 3900, 4000,
        4100, 4200, 4300, 4400, 4500, 4600, 4700, 4800, 4900, 5000,
        5100
    };
    public int[] rewardAmount = new int[]
    {
        50, 100, 150, 200, 250, 300, 350, 400, 450, 500,
        550, 600, 650, 700, 750, 800, 850, 900, 950, 1000,
        1050, 1100, 1150, 1200, 1250, 1300, 1350, 1400, 1450, 1500,
        1550, 1600, 1650, 1700, 1750, 1800, 1850, 1900, 1950, 2000,
        2050, 2100, 2150, 2200, 2250, 2300, 2350, 2400, 2450, 2500,
        2550
    };
    public string[] militaryRanks = new string[] {
    "Recruit (RCT)", // Level 1
    "Private (PVT)", // Level 2
    "Specialist (SPC)", // Level 3
    "Corporal (CPL)", // Level 4
    "Sergeant (SGT)", // Level 5
    "Staff Sergeant (SSG)", // Level 6
    "Sergeant First Class (SFC)", // Level 7
    "Master Sergeant (MSG)", // Level 8
    "First Sergeant (1SG)", // Level 9
    "Sergeant Major (SGM)", // Level 10
    "Second Lieutenant (2LT)", // Level 11
    "First Lieutenant (1LT)", // Level 12
    "Captain (CPT)", // Level 13
    "Major (MAJ)", // Level 14
    "Lieutenant Colonel (LTC)", // Level 15
    "Colonel (COL)", // Level 16
    "Brigadier General (BG)", // Level 17
    "Major General (MG)", // Level 18
    "Lieutenant General (LTG)", // Level 19
    "General (GEN)", // Level 20
    "Commander (CDR)", // Level 21
    "Fleet Commander (FLTCDR)", // Level 22
    "Task Force Commander (TFCDR)", // Level 23
    "Brigade Commander (BGCDR)", // Level 24
    "Division Commander (DVCDR)", // Level 25
    "Corps Commander (CRPCDR)", // Level 26
    "Army Commander (ARMCDR)", // Level 27
    "Joint Commander (JNTCDR)", // Level 28
    "Major Commander (MAJCDR)", // Level 29
    "General Commander (GENCDR)", // Level 30
    "First Commander (1CDR)", // Level 31
    "Top Commander (TOPCDR)", // Level 32
    "Elite Commander (ELTCDR)", // Level 33
    "Tactical Commander (TACCDR)", // Level 34
    "Vanguard Commander (VNGCDR)", // Level 35
    "Champion Commander (CHPCDR)", // Level 36
    "Supreme Commander (SUPCDR)", // Level 37
    "Majestic Commander (MJSCDR)", // Level 39
    "Legendary Commander (LGDCDR)", // Level 40
    "War General (WRGEN)", // Level 41
    "Assault General (ASLTGEN)", // Level 42
    "Guardian General (GRDGGEN)", // Level 43
    "Storm General (STRMGEN)", // Level 44
    "Defense General (DFNSGEN)", // Level 45
    "Blood General (BLDGEN)", // Level 46
    "Doom General (DMGEN)", // Level 47
    "Rage General (RGGEN)", // Level 48
    "Chaos General (CHSGEN)", // Level 49
    "Apocalypse General (APCLYGEN)" // Level 50
};

    public int currency = 0;
    public string playerID = "player1";

    private float timeSinceLastSave = 0f;
    private float autoSaveInterval = 300f; // 5 minutes in seconds

    private void Start()
    {
        LoadData();
        SaveData();
    }

    private void Update()
    {
        timeSinceLastSave += Time.deltaTime;
        if (timeSinceLastSave >= autoSaveInterval)
        {
            timeSinceLastSave = 0f;
            SaveData();
        }
    }

    public void AddExp(int exp)
    {
        currentExp += exp;
        int nextLevelExp = levelUpExp[currentLevel - 1];
        if (currentExp >= nextLevelExp)
        {
            currentLevel++;
            currentExp = 0;
            int reward = rewardAmount[currentLevel - 1];
            currency += reward;
            Debug.Log("Level Up! You received " + reward + " coins as a reward.");
        }
    }

    private void OnDestroy()
    {
        SaveData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetString(playerID + "CurrentRank", militaryRanks[currentLevel - 1]);
        PlayerPrefs.SetInt(playerID + "CurrentLevel", currentLevel);
        PlayerPrefs.SetInt(playerID + "CurrentExp", currentExp);
        PlayerPrefs.SetInt(playerID + "Currency", currency);
    }

    public void LoadData()
    {
        PlayerPrefs.GetString(playerID + "CurrentRank", militaryRanks[0]);
        currentLevel = PlayerPrefs.GetInt(playerID + "CurrentLevel", 1);
        currentExp = PlayerPrefs.GetInt(playerID + "CurrentExp", 0);
        currency = PlayerPrefs.GetInt(playerID + "Currency", 0);
    }

    public int GetCurrentLevel()
    {
       return currentLevel;
    }

    public int GetCurrentCurrency()
    {
        return currency;
    }

    public float GetCurrentExperience()
    {
        return currentExp;
    }

    public float GetExperienceNeededForNextLevel()
    {
        return  (levelUpExp[currentLevel - 1]) - currentExp;
    }

    public string GetMilitaryRank()
    {
        
        return  (militaryRanks[currentLevel - 1]);
    }
}