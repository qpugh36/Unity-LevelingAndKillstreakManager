using EmeraldAI;
using UnityEngine;

public class TomahawkMissile : MonoBehaviour
{
    public Transform spawnPoint;
    public float speed = 20f;
    public float explosionRadius = 10f;
    public float explosionDamage = 50f;
    public ParticleSystem explosionVFX;
    public AudioClip explosionSFX;

    private Transform target;

    private void Start()
    {
        target = FindClosestEnemy();
        transform.position = spawnPoint.position;
    }

    private void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            Explode();
        }
    }

    private Transform FindClosestEnemy()
    {
        EmeraldAISystem[] enemies = FindObjectsOfType<EmeraldAISystem>();
        if (enemies.Length == 0)
        {
            return null;
        }

        Transform closest = enemies[0].transform;
        float distance = Vector3.Distance(transform.position, closest.position);
        for (int i = 1; i < enemies.Length; i++)
        {
            float newDistance = Vector3.Distance(transform.position, enemies[i].transform.position);
            if (newDistance < distance)
            {
                closest = enemies[i].transform;
                distance = newDistance;
            }
        }

        return closest;
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            EmeraldAISystem enemyAI = collider.GetComponent<EmeraldAISystem>();
            if (enemyAI != null)
            {
                enemyAI.Damage((int)explosionDamage, EmeraldAISystem.TargetType.Player, transform, 1000);
            }
        }

        ParticleSystem effect = Instantiate(explosionVFX, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(explosionSFX, transform.position);

        Destroy(effect.gameObject, effect.main.duration);
        Destroy(gameObject);
    }
}
