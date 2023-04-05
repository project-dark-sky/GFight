using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlowEnemies : MonoBehaviour
{
    [SerializeField]
    private string triggerTag;

    [SerializeField]
    private string affectedTargetTag;

    [SerializeField]
    private float defaultTightness = .7f;

    [SerializeField]
    private float slowedTightness = .2f;

    [SerializeField]
    private float time = 5;

    private Coroutine coroutine;


    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == triggerTag)
        {
            Movment movment = other.GetComponent<Movment>();
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            coroutine = StartCoroutine(slowMotionStart());
        }
    }


    IEnumerator slowMotionStart()
    {
        slowEnemies();
        yield return new WaitForSeconds(time);
        defaultEnemies();
        Destroy(gameObject);
    }



    void slowEnemies()
    {
        Debug.Log("Enemies slowed down");
        GameObject[] objects = GameObject.FindGameObjectsWithTag(affectedTargetTag);
        foreach (var obj in objects)
        {
            obj.GetComponent<Tracker>().setFollowTightness(slowedTightness);
        }
    }

    void defaultEnemies()
    {
        Debug.Log("Enemies back to defualt");
        GameObject[] objects = GameObject.FindGameObjectsWithTag(affectedTargetTag);
        foreach (var obj in objects)
        {
            obj.GetComponent<Tracker>().setFollowTightness(defaultTightness);
        }
    }
}
