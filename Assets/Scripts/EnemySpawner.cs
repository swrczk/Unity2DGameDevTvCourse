using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfigSO> waveConfigs;
    [SerializeField] private float timeBetweenWaves = 0f;
    [SerializeField] private bool isLooping = false;
    private int waveIndex = 0;

    private void Start()
    {
        StartCoroutine(nameof(SpawnEnemiesWaves));
    }

    public WaveConfigSO GetCurrentWave() => waveConfigs[waveIndex];

    private IEnumerator SpawnEnemiesWaves()
    {
        do
        {
            for (waveIndex = 0; waveIndex < waveConfigs.Count; waveIndex++)
            {
                var currentWave = waveConfigs[waveIndex];
                for (var index = 0; index < currentWave.GetEnemyCount(); index++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(index),
                        currentWave.GetStartingWaypoint().position,
                        Quaternion.Euler(0, 0, 180),
                        transform);
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }

                yield return new WaitForSeconds(timeBetweenWaves);
            }
        } while (isLooping);
    }
}