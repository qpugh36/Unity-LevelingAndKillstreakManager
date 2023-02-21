using UnityEngine;
using EmeraldAI;

public class ChopperGunner : MonoBehaviour
{
    public ParticleSystem muzzleFlash;
    public Transform target;
    public Transform firePoint;
    public Transform spawnPoint;
    public float speed = 10f;
    public float detectionRange = 10f;
    public float damage = 10f;
    public float duration = 60f;
    public float fireRate = 0.5f;
    public float altitude = 10f;
    public float circleRadius = 10f;

    private float timer;
    private float fireTimer;

    private void Start()
    {
        transform.position = spawnPoint.position;
        transform.position = new Vector3(transform.position.x, altitude, transform.position.z);
        target = FindClosestEnemy();
        timer = duration;
        fireTimer = fireRate;
    }

    private void Update()
    {
        target = FindClosestEnemy();
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= detectionRange)
            {
                Shoot();
                CircleAroundTarget();
                PointTowardsEnemy();
            }
        }

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
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


    private void Shoot()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0)
        {
            muzzleFlash.Play();

            Vector3 direction = (target.position - firePoint.position).normalized;
            

            EmeraldAISystem enemyAI = target.GetComponent<EmeraldAISystem>();
            enemyAI.Damage((int)damage,EmeraldAI.EmeraldAISystem.TargetType.Player, this.transform, 1000);

            fireTimer = fireRate;
        }
    }

    private void CircleAroundTarget()
    {
        Vector3 targetPosition = target.position;
        targetPosition.y = transform.position.y;

        float angle = Time.time * speed;
        float x = Mathf.Cos(angle) * circleRadius;
        float z = Mathf.Sin(angle) * circleRadius;

        transform.position = targetPosition + new Vector3(x, 0, z);
transform.position = new Vector3(transform.position.x, altitude, transform.position.z);
}

private void PointTowardsEnemy()
{
    Vector3 direction = (target.position - transform.position).normalized;
    Quaternion lookRotation = Quaternion.LookRotation(direction);
    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speed * Time.deltaTime);
}

}
