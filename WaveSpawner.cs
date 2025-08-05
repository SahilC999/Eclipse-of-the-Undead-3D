using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Spawns waves of zombies with scaling difficulty, special zombie types, and reward intervals.
/// Attach this to an empty GameObject in the scene.
/// </summary>
public class WaveSpawner : MonoBehaviour
{
    [Header("Zombie Prefabs")]
    public GameObject regularZombiePrefab;
    public GameObject fastZombiePrefab;
    public GameObject tankZombiePrefab;
    public GameObject bossZombiePrefab;

    [Header("Spawn Settings")]
    public Transform[] spawnPoints;
    public int startingZombies = 5;
    public float timeBetweenWaves = 15f;
    public float spawnDelay = 1.5f;

    [Header("Wave Info")]
    public int currentWave = 0;
    public Text waveText;

    [Header("Rewards")]
    public GameObject ammoCratePrefab;
    public GameObject healthPackPrefab;

    [Header("Pooling System")]
    private Queue<GameObject> zombiePool = new Queue<GameObject>();
    private int poolSize = 30;

    private bool spawningWave = false;
    private List<GameObject> activeZombies = new List<GameObject>();

    void Start()
    {
        // Initialize zombie pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject z = Instantiate(regularZombiePrefab);
            z.SetActive(false);
            zombiePool.Enqueue(z);
        }

        Invoke("StartNextWave", 5f);
    }

    void Update()
    {
        // If all zombies in current wave are dead, trigger next wave
        if (!spawningWave && activeZombies.Count == 0)
        {
            Invoke("StartNextWave", timeBetweenWaves);
            spawningWave = true;
        }
    }

    void StartNextWave()
    {
        currentWave++;
        spawningWave = false;
        if (waveText) waveText.text = "Wave " + currentWave;

        int zombieCount = startingZombies + (currentWave * 2);
        StartCoroutine(SpawnWave(zombieCount));
    }

    System.Collections.IEnumerator SpawnWave(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnZombie(ChooseZombieType());
            yield return new WaitForSeconds(spawnDelay);
        }

        // Every 3 waves, spawn a special boss
        if (currentWave % 3 == 0)
        {
            SpawnZombie(bossZombiePrefab);
        }

        // Every 2 waves, drop a reward
        if (currentWave % 2 == 0)
        {
            DropReward();
        }
    }

    // Chooses zombie type based on wave difficulty
    GameObject ChooseZombieType()
    {
        float chance = Random.value;
        if (currentWave > 10 && chance > 0.8f) return tankZombiePrefab;
        if (currentWave > 5 && chance > 0.6f) return fastZombiePrefab;
        return regularZombiePrefab;
    }

    // Spawns a zombie using pooling
    void SpawnZombie(GameObject zombiePrefab)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject z = GetZombieFromPool(zombiePrefab);
        z.transform.position = spawnPoint.position;
        z.transform.rotation = Quaternion.identity;
        z.SetActive(true);
        activeZombies.Add(z);

        // Register zombie death callback
        ZombieHealth zh = z.GetComponent<ZombieHealth>();
        if (zh) zh.onZombieDeath = () => OnZombieDeath(z);
    }

    // Object pooling logic
    GameObject GetZombieFromPool(GameObject prefab)
    {
        if (zombiePool.Count > 0)
        {
            GameObject z = zombiePool.Dequeue();
            z.GetComponent<ZombieHealth>().ResetHealth();
            return z;
        }
        else
        {
            return Instantiate(prefab);
        }
    }

    // Handles zombie death
    void OnZombieDeath(GameObject zombie)
    {
        zombie.SetActive(false);
        zombiePool.Enqueue(zombie);
        activeZombies.Remove(zombie);
    }

    // Drops rewards after waves
    void DropReward()
    {
        Vector3 dropPos = playerPos() + new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
        Instantiate(ammoCratePrefab, dropPos, Quaternion.identity);

        if (Random.value > 0.5f)
        {
            Vector3 hpPos = dropPos + new Vector3(2, 0, 2);
            Instantiate(healthPackPrefab, hpPos, Quaternion.identity);
        }
    }

    // Gets player position for rewards
    Vector3 playerPos()
    {
        GameObject player = GameObject.FindWithTag("Player");
        return player ? player.transform.position : Vector3.zero;
    }
}
