using UnityEngine;
using EmeraldAI;

public class Drone : MonoBehaviour
{
    public Transform target;
    public float speed = 45f;
    public float detectionRange = 0.1f;
    public int damage = 100;
    public GameObject explosionPrefab;
    public AudioClip explosionSFX;
    public int explosionDamage;
    public int explosionRadius;
    public ParticleSystem explosionVFX;
    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= detectionRange)
            {
                Explode();
            }
        }
        else
        {
            target = FindClosestEnemy();
        }
    }

    private Transform FindClosestEnemy()
    {
        EmeraldAISystem[] enemies = FindObjectsOfType<EmeraldAISystem>();
        Transform closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (EmeraldAISystem enemy in enemies)
        {
            if (enemy.CurrentHealth > 0)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestEnemy = enemy.transform;
                    closestDistance = distance;
                }
            }
        }

        return closestEnemy;
    }

private void Explode()
{
    Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
    foreach (Collider collider in colliders)
    {
        EmeraldAISystem enemyAI = collider.GetComponent<EmeraldAISystem>();
        if (enemyAI != null)
        {
            enemyAI.Damage(explosionDamage, null, transform);
        }
    }

    ParticleSystem effect = Instantiate(explosionVFX, transform.position, Quaternion.identity);
    AudioSource.PlayClipAtPoint(explosionSFX, transform.position);

    Destroy(effect.gameObject, effect.main.duration);
    Destroy(gameObject);
}
}
