using System;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

public class Health : MonoBehaviour
{
    [SerializeField] private bool isPlayer;
    [SerializeField] private int health = 50;
    [SerializeField] private int score = 50;
    [SerializeField] private ParticleSystem hitEffect;

    private CameraShake cameraShake;
    [SerializeField] private bool applyCameraShake;
    [SerializeField] private LevelManager levelManager;

    public int CurrentHealth => health;
    public int MaxHealth { get; private set; }

    private void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        MaxHealth = health;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer == null) return;

        damageDealer.Hit();
        AudioPlayer.Instance.PlayDamageClip();
        PlayHitEffect();
        ShakeCamera();
        TakeDamage(damageDealer.Damage);
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private async Task Die()
    {
        if (!isPlayer)
        {
            ScoreKeeper.Instance.Score += score;
            Debug.Log($"Score: {ScoreKeeper.Instance.Score}");
        }
        Destroy(gameObject);
        if (isPlayer)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            levelManager.LoadGameOver();
        }
    }

    private void PlayHitEffect()
    {
        if (hitEffect == null) return;
        var instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
    }

    private void ShakeCamera()
    {
        if (cameraShake == null || !applyCameraShake) return;
        cameraShake.Play();
    }
}