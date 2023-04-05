using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomTimePosSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabs;
    [Tooltip("Minimum time between consecutive spawns, in seconds")][SerializeField] float minTimeBetweenSpawns = 1f;
    [Tooltip("Maximum time between consecutive spawns, in seconds")][SerializeField] float maxTimeBetweenSpawns = 3f;
    [Tooltip("Maximum time between consecutive spawns, in seconds")][SerializeField] float maxHDistance = 3f;
    [Tooltip("Maximum time between consecutive spawns, in seconds")][SerializeField] float maxVDistance = 3f;
    [SerializeField] Health player;


    // Start is called before the first frame update
    void Start()
    {
        this.StartCoroutine(SpawnRoutine());    // co-routines
    }

    IEnumerator SpawnRoutine()
    {
        GameObject prefab = prefabs[Random.Range(0, prefabs.Count)];
        Debug.Log(prefab.name + "  " + prefabs.Count);

        Instantiate(prefab, transform.position, Quaternion.identity);
        while (!player.isDead())
        {
            float timeBetweenSpawnsInSeconds = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
            yield return new WaitForSeconds(timeBetweenSpawnsInSeconds);       // co-routines

            prefab = prefabs[Random.Range(0, prefabs.Count)];
            float x = Random.Range(transform.position.x - maxHDistance, transform.position.x + maxHDistance);
            float y = Random.Range(transform.position.y - maxVDistance, transform.position.y + maxVDistance);

            Vector3 pos = new Vector3(x, y, transform.position.z);
            Instantiate(prefab, pos, Quaternion.identity);
        }
    }
}
