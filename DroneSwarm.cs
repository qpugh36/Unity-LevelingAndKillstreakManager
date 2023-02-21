using UnityEngine;

public class DroneSwarm : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject dronePrefab;
    public float speed = 45f;
    public float detectionRange = 50f;
    public int numberOfDrones = 5;

    private void Start()
    {
        for (int i = 0; i < numberOfDrones; i++)
        {
            GameObject drone = Instantiate(dronePrefab, spawnPoint.position, Quaternion.identity);
            Drone droneScript = drone.GetComponent<Drone>();
            droneScript.speed = speed;
            droneScript.detectionRange = detectionRange;
        }
    }
}
