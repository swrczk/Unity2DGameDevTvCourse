using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileLifetime = 5f;
    [SerializeField] private float firingRate = 1f;
    [SerializeField] private float firingRateVariance = 0f;

    public bool isFiring;

    private Coroutine firingCoroutine;

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            var rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }
            
            AudioPlayer.Instance.PlayShootingClip();

            Destroy(projectile, projectileLifetime);
            yield return new WaitForSeconds(GetRandomSpawnTime());
        }
    }

    private float GetRandomSpawnTime()
    {
        var spawnTime = Random.Range(firingRate - firingRateVariance / 2, firingRate + firingRateVariance / 2);
        return Mathf.Clamp(spawnTime, 0f, float.MaxValue);
    }
}