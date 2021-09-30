using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    SpriteRenderer sprite;
    SceneLoader sceneLoader;

    [Header("Prefabs")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject explosionParticlePrefab;
    
    [Header("Attributes")]
    [SerializeField] float speed = 5f;
    [SerializeField] float health = 500f;
    public float Health
    {
        get
        {
            return health;
        }
    }

    [Header("Firing Rate")]
    [SerializeField] float fireDelayTimer = 0.1f;

    [Header("Audio")]
    [SerializeField] AudioClip laserAudio;
    [Range(0,1)]
    [SerializeField] float laserAudioVolume = 0.25f;
    [SerializeField] AudioClip deathAudio;
    [Range(0, 1)]
    [SerializeField] float deathAudioVolume = 0.25f;

    [Header("Score")]
    [SerializeField] int score;
    int highScore;

    Vector2 xClampAmt;
    Vector2 yClampAmt;

    bool isFiring=false;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        sceneLoader = FindObjectOfType<SceneLoader>();
        SetMovementBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        UpdateHighScore();
    }

    private void Fire()
    {
        Vector3 laserOffset = new Vector3(0f, sprite.bounds.size.y / 2, 0f);
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)) && !isFiring)
        {
            StartCoroutine(RapidFiring(laserOffset));
        }
    }

    IEnumerator RapidFiring(Vector3 laserOffset)
    {
        isFiring = true;

        GameObject  laserPrefabSpawn = Instantiate(laserPrefab, transform.position + laserOffset, Quaternion.identity);
        AudioSource.PlayClipAtPoint(laserAudio, Camera.main.transform.position,laserAudioVolume);

        yield return new WaitForSeconds(fireDelayTimer);
        isFiring = false;
    }

    void SetMovementBoundaries()
    {
        Camera mainCamera = Camera.main;

        xClampAmt[0] = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + sprite.bounds.size.x / 2;
        xClampAmt[1] = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - sprite.bounds.size.x / 2;

        yClampAmt[0] = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + sprite.bounds.size.y / 2;
        yClampAmt[1] = (mainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).y) - sprite.bounds.size.y / 2;

    }

    private void Move()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        Vector3 translation = new Vector3(horizontalMove, verticalMove, 0).normalized * speed * Time.deltaTime;

        Vector3 newPosition = transform.position + translation;
        newPosition = new Vector3(Mathf.Clamp(newPosition.x, xClampAmt[0], xClampAmt[1]),
                                  Mathf.Clamp(newPosition.y, yClampAmt[0], yClampAmt[1]), 
                                  newPosition.z);

        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer) {
            health -= damageDealer.Damage;
            damageDealer.Hit();
            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        GameObject explosionParticlePrefabSpawn = Instantiate(explosionParticlePrefab, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(deathAudio, Camera.main.transform.position, deathAudioVolume);
        Destroy(explosionParticlePrefabSpawn, 0.3f);
        Destroy(this.gameObject);
        sceneLoader.GameOverScreen();
    }

    public void UpdateScore(int score)
    {
        this.score += score;
        UIManager.Instance.UpdateScore(this.score);
        if (highScore < this.score)
        {
            highScore = this.score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    void UpdateHighScore()
    {
        UIManager.Instance.UpdateHighScore(highScore);
    }
}
