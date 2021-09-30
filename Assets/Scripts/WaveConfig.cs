using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="EnemyWaveConfig")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.2f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] int moveSpeed = 2;

    public GameObject EnemyPrefab { get { return enemyPrefab; } }
    public float TimeBetweenSpawns { get { return timeBetweenSpawns; } }
    public float SpawnRandomFactor { get { return spawnRandomFactor; } }
    public int NumberOfEnemies { get { return numberOfEnemies; } }
    public int MoveSpeed { get { return moveSpeed; } }
    public List<Transform> GetWayPoints()
    {
        List<Transform> waypoints = new List<Transform>();

        foreach(Transform child in pathPrefab.transform)
        {
            waypoints.Add(child);
        }

        return waypoints;
    }
}
