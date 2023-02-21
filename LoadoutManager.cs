using UnityEngine;

public class LoadoutManager : MonoBehaviour
{
    public Weapon_SO[] primaryWeapons;
    public Weapon_SO[] secondaryWeapons;
    public Weapon_SO[] explosiveWeapons;
    public Weapon_SO[] killstreaks;

    private int selectedPrimaryIndex;
    private int selectedSecondaryIndex;
    private int selectedExplosiveIndex;
    private int selectedKillstreakIndex1;
    private int selectedKillstreakIndex2;

    private const string PRIMARY_WEAPON_KEY = "SelectedPrimary";
    private const string SECONDARY_WEAPON_KEY = "SelectedSecondary";
    private const string EXPLOSIVE_WEAPON_KEY = "SelectedExplosive";
    private const string KILLSTREAK_KEY_1 = "SelectedKillstreak1";
    private const string KILLSTREAK_KEY_2 = "SelectedKillstreak2";

    private void Awake()
    {
        LoadSelectedLoadout();
        ApplySelectedLoadout();
    }

    public void SaveSelectedLoadout()
    {
        PlayerPrefs.SetInt(PRIMARY_WEAPON_KEY, selectedPrimaryIndex);
        PlayerPrefs.SetInt(SECONDARY_WEAPON_KEY, selectedSecondaryIndex);
        PlayerPrefs.SetInt(EXPLOSIVE_WEAPON_KEY, selectedExplosiveIndex);
        PlayerPrefs.SetInt(KILLSTREAK_KEY_1, selectedKillstreakIndex1);
        PlayerPrefs.SetInt(KILLSTREAK_KEY_2, selectedKillstreakIndex2);
        PlayerPrefs.Save();
    }

    public void LoadSelectedLoadout()
    {
        selectedPrimaryIndex = PlayerPrefs.GetInt(PRIMARY_WEAPON_KEY, 0);
        selectedSecondaryIndex = PlayerPrefs.GetInt(SECONDARY_WEAPON_KEY, 0);
        selectedExplosiveIndex = PlayerPrefs.GetInt(EXPLOSIVE_WEAPON_KEY, 0);
        selectedKillstreakIndex1 = PlayerPrefs.GetInt(KILLSTREAK_KEY_1, 0);
        selectedKillstreakIndex2 = PlayerPrefs.GetInt(KILLSTREAK_KEY_2, 0);
    }

    public void ApplySelectedLoadout()
    {
        // Instantiate the selected weapons and killstreaks
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponentInChildren<WeaponController>().initialWeapons.SetValue(GetSelectedPrimaryWeapon(), 0);
        player.GetComponentInChildren<WeaponController>().initialWeapons.SetValue(GetSelectedSecondaryWeapon(), 1);
        player.GetComponentInChildren<WeaponController>().initialWeapons.SetValue(GetSelectedExplosiveWeapon(), 2);
        player.GetComponentInChildren<WeaponController>().initialWeapons.SetValue(GetSelectedKillstreak(), 3);
        player.GetComponentInChildren<WeaponController>().initialWeapons.SetValue(GetSelectedKillstreak2(), 4);

    }

    public void SelectPrimaryWeapon(int index)
    {
        selectedPrimaryIndex = index;
        PlayerPrefs.SetInt("SelectedPrimaryIndex", index);
    }

    public void SelectSecondaryWeapon(int index)
    {
        selectedSecondaryIndex = index;
        PlayerPrefs.SetInt("SelectedSecondaryIndex", index);
    }

    public void SelectExplosiveWeapon(int index)
    {
        selectedExplosiveIndex = index;
        PlayerPrefs.SetInt("SelectedExplosiveIndex", index);
    }

    public void SelectKillstreak(int index)
    {
        selectedKillstreakIndex1 = index;
        PlayerPrefs.SetInt("selectedKillstreakIndex1", index);
    }

    public void SelectKillstreak2(int index)
    {
         selectedKillstreakIndex2 = index;
        PlayerPrefs.SetInt(" selectedKillstreakIndex2", index);
    }

    public Weapon_SO GetSelectedPrimaryWeapon()
    {
        return primaryWeapons[PlayerPrefs.GetInt("SelectedPrimaryIndex", 0)] ;
        
    }

    public Weapon_SO GetSelectedSecondaryWeapon()
    {
        return secondaryWeapons[PlayerPrefs.GetInt("SelectedSecondaryIndex", 0)] ;
    }

    public Weapon_SO GetSelectedExplosiveWeapon()
    {
        return explosiveWeapons[PlayerPrefs.GetInt("SelectedExplosiveIndex", 0)] ;
    }

    public Weapon_SO GetSelectedKillstreak()
    {
        return killstreaks[PlayerPrefs.GetInt("SelectedKillstreakIndex", 0)] ;
    }

    public Weapon_SO GetSelectedKillstreak2()
    {
        return killstreaks[PlayerPrefs.GetInt("SelectedKillstreakIndex2", 0)] ;
    }
}
