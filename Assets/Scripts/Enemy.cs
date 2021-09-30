using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] int health = 500;
    [SerializeField] float minShootCounter = 0.5f;
    [SerializeField] float maxShootCounter = 3f;

    [Header("Prefabs")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject explosionParticle;

    [Header("Audio")]
    [SerializeField] AudioClip enemyLaserAudio;
    [Range(0, 1)]
    [SerializeField] float enemyLaserAudioVolume = 0.25f;
    [SerializeField] AudioClip enemyDeathAudio;
    [Range(0, 1)]
    [SerializeField] float enemyDeathAudioVolume = 0.25f;

    float currentCounter;
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        currentCounter = UnityEngine.Random.Range(minShootCounter, maxShootCounter);
    }

    private void Update()
    {
        CountDownShoot();
    }

    private void CountDownShoot()
    {
        currentCounter -= Time.deltaTime;
        if (currentCounter <= 0)
        {
            Fire();
            currentCounter = UnityEngine.Random.Range(minShootCounter,maxShootCounter);
        }
    }

    private void Fire()
    {
        Vector3 laserOffset = new Vector3(0, GetComponent<SpriteRenderer>().bounds.size.y / 2, 0);
        Instantiate(laserPrefab,
                                transform.position - laserOffset,
                                Quaternion.identity);
        AudioSource.PlayClipAtPoint(enemyLaserAudio,Camera.main.transform.position,enemyLaserAudioVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer)
        {
            damageDealer.Hit();
            health -= damageDealer.Damage;
            if (health <= 0)
            {
                player.UpdateScore(10);
                GameObject explositionParticleSpawn = Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
                AudioSource.PlayClipAtPoint(enemyDeathAudio, Camera.main.transform.position, enemyDeathAudioVolume);
                Destroy(explositionParticleSpawn, 0.3f);
                Destroy(this.gameObject);
            }
        }
    }
}
