using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class EnemySpawnSettings
    {
        public GameObject Prefab;
        public float Weight;
        public int Limit;
    }

    [SerializeField]
    TMP_Text waveDisplay;

    [SerializeField]
    GameObject asteroid;

    [SerializeField]
    float cooldown;

    [SerializeField]
    float initialDelay;

    [SerializeField]
    int spawnCount = 1;

    [SerializeField]
    EnemySpawnSettings[] enemies;

    float currentCooldown;


    int level = 1;
    SpawnArea[] spawnAreas;
    GameObject[] spawns;

    List<Asteroid> activeSpawns = new List<Asteroid>();

    void Start()
    {
        spawnAreas = GameObject.FindObjectsOfType<SpawnArea>();
        spawns = GameObject.FindGameObjectsWithTag("Spawn");

        currentCooldown = initialDelay;
    }


    // Update is called once per frame
    void Update()
    {
        currentCooldown -= Time.deltaTime;

        if (currentCooldown <= 0)
        {
            currentCooldown = cooldown;

            for (var i = 0; i < spawnCount; i++)
            {
                var source = GetSpawn();
                var target = GetSpawnArea();

                var spawnedAsteroid = Instantiate(asteroid, source.position, Quaternion.identity).GetComponent<Asteroid>();
                spawnedAsteroid.SetTarget(target.transform.position);
                
                for(var j = 0; j < enemies.Length; j++)
                {
                    var count = enemies[j].Weight * level;

                    if (count > 0)
                    {
                        spawnedAsteroid.AddToPayload(
                            enemies[j].Prefab,
                            Random.Range(1, Mathf.Min((int)count, enemies[j].Limit))
                        );
                    }
                }

                level++;
                waveDisplay.text = level.ToString();
            }
        }
    }

    Transform GetSpawn()
    {
        var spawnIndex = Random.Range(0, spawns.Length);

        return spawns[spawnIndex].transform;
    }

    SpawnArea GetSpawnArea()
    {
        var activeAreas = this.spawnAreas.Where(s => s.active).ToArray();
        var spawnIndex = Random.Range(0, activeAreas.Length);

        return activeAreas[spawnIndex];
    }
}
