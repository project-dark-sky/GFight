using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTimeSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [Tooltip("Minimum time between consecutive spawns, in seconds")][SerializeField] float minTimeBetweenSpawns = 1f;
    [Tooltip("Maximum time between consecutive spawns, in seconds")][SerializeField] float maxTimeBetweenSpawns = 3f;
    [SerializeField] Health player;

    // Start is called before the first frame update
    void Start()
    {
        this.StartCoroutine(SpawnRoutine());    // co-routines
    }

    IEnumerator SpawnRoutine()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
        while (!player.isDead())
        {
            float timeBetweenSpawnsInSeconds = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
            yield return new WaitForSeconds(timeBetweenSpawnsInSeconds);       // co-routines
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}
