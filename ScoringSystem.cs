using UnityEngine;

public class ScoringSystem : MonoBehaviour
{
    public int killsNeededForTomahawk = 10;
    public int killsNeededForChopperGunner = 20;
    public int killsNeededForDroneSwarm = 30;

    public Transform tomahawkSpawnPoint;
    public GameObject tomahawkPrefab;

    public Transform chopperGunnerSpawnPoint;
    public GameObject chopperGunnerPrefab;

    public Transform droneSwarmSpawnPoint;
    public GameObject droneSwarmPrefab;

    public int currentKills = 0;

    public void OnKill()
    {
        currentKills++;

        if (currentKills == killsNeededForTomahawk)
        {
            TriggerTomahawk();
        }
        else if (currentKills == killsNeededForChopperGunner)
        {
            TriggerChopperGunner();
        }
        else if (currentKills == killsNeededForDroneSwarm)
        {
            TriggerDroneSwarm();
        }
    }

    private void TriggerTomahawk()
    {
        GameObject tomahawk = Instantiate(tomahawkPrefab, tomahawkSpawnPoint.position, tomahawkSpawnPoint.rotation);
        TomahawkMissile TomahawkMissile = tomahawk.GetComponent<TomahawkMissile>();
        TomahawkMissile.spawnPoint = tomahawkSpawnPoint;
    }

    private void TriggerChopperGunner()
    {
        GameObject chopperGunner = Instantiate(chopperGunnerPrefab, chopperGunnerSpawnPoint.position, chopperGunnerSpawnPoint.rotation);
        ChopperGunner chopperGunnerScript = chopperGunner.GetComponent<ChopperGunner>();
        chopperGunnerScript.spawnPoint = chopperGunnerSpawnPoint;
    }

    private void TriggerDroneSwarm()
    {
        GameObject droneSwarm = Instantiate(droneSwarmPrefab, droneSwarmSpawnPoint.position, droneSwarmSpawnPoint.rotation);
        DroneSwarm droneSwarmScript = droneSwarm.GetComponent<DroneSwarm>();
        droneSwarmScript.spawnPoint = droneSwarmSpawnPoint;
    }
}
