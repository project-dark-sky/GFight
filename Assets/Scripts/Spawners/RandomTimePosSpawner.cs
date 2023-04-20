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
    [Tooltip("The number of instances to be shown at the same time")][SerializeField] int numberOfParallelInstance = 3;
    [SerializeField] Health player;

    private List<GameObject> gameObjects;

    // Start is called before the first frame update
    void Start()
    {
        gameObjects = new List<GameObject>();
        this.StartCoroutine(SpawnRoutine());    // co-routines
    }

    // unity will make sure that once an object gets destroyed all the pointers to that object will be updated to null 
    void cleanListFromNulls()
    {
        List<GameObject> newList = new List<GameObject>();
        foreach (GameObject g in gameObjects)
        {
            if (g != null)
                newList.Add(g);
        }
        gameObjects = newList;
    }

    IEnumerator SpawnRoutine()
    {
        GameObject prefab = prefabs[Random.Range(0, prefabs.Count)];
        Debug.Log(prefab.name + "  " + prefabs.Count);

        //Instantiate(prefab, transform.position, Quaternion.identity);
        while (!player.isDead())
        {
            float timeBetweenSpawnsInSeconds = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
            yield return new WaitForSeconds(timeBetweenSpawnsInSeconds);       // co-routines

            cleanListFromNulls();
            if (gameObjects.Count == numberOfParallelInstance)
            {
                continue;
            }


            prefab = prefabs[Random.Range(0, prefabs.Count)];
            float x = Random.Range(transform.position.x - maxHDistance, transform.position.x + maxHDistance);
            float y = Random.Range(transform.position.y - maxVDistance, transform.position.y + maxVDistance);

            Vector3 pos = new Vector3(x, y, transform.position.z);
            GameObject gob = Instantiate(prefab, pos, Quaternion.identity);
            gameObjects.Add(gob);
        }
    }


    // draw power ups spawn range
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(maxHDistance * 2, maxVDistance * 2, 0));
    }
}
