using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWaveIdx = 0;
    [SerializeField] bool looping = false;  
    private IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }

    IEnumerator SpawnAllWaves()
    {
        for(int i = startingWaveIdx; i < waveConfigs.Count; i++)
        {
            WaveConfig wave = waveConfigs[i];
            yield return SpawnAllEnemyInWave(wave);
        }
    }

    IEnumerator SpawnAllEnemyInWave(WaveConfig wave)
    {
        for (int i = 0; i < wave.NumberOfEnemies; i++)
        {
            GameObject enemyPrefabSpawn=
                        Instantiate(wave.EnemyPrefab,
                        wave.GetWayPoints()[0].position,
                        Quaternion.identity);
            enemyPrefabSpawn.GetComponent<EnemyPath>().SetWaveConfig(wave);

            yield return new WaitForSeconds(wave.TimeBetweenSpawns);
        }
    }
}
