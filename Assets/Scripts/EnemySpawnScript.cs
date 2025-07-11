using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] spawnablePrefabs;
    public int spawnNumber;
    public List<GameObject> spawnedObjects = new List<GameObject>();
    public float ofs;
    public SpawnType spawnType;
    public enum SpawnType { Waves, OnlyAtStart }

    private void Start()
    {
        if (spawnType == SpawnType.OnlyAtStart)
        {
            for (int i = 0; i < spawnNumber; i++)
            {
                ofs += 1f;
                Spawn();
            }
        }
    }

    private void Update()
    {
        if (spawnType == SpawnType.Waves)
        {
            if (spawnedObjects.Count == 0)
            {
                for (int i = 0; i < spawnNumber; i++)
                {
                    ofs += 1f;
                    Spawn();
                }
            }
        }
    }

    public void Spawn()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject spawnable = spawnablePrefabs[Random.Range(0, spawnablePrefabs.Length)];
        spawnPoint.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y + ofs, spawnPoint.position.z);

        GameObject obj = Instantiate(spawnable, spawnPoint.position, Quaternion.identity);
        spawnedObjects.Add(obj);
    }
}
