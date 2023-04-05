using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField] string targetTag;
    [SerializeField] private float followTightness;

    private Vector3 wanted_position;
    private Animator animator;
    private Transform target;
    private Health health;


    public void setFollowTightness(float val)
    {
        followTightness = Mathf.Clamp(val, 0, 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag(targetTag).GetComponent<Transform>();
        health = target.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health.isDead())
        {
            enabled = false;
        }
        wanted_position = target.position;
        wanted_position.z = transform.position.z;
        wanted_position.y = transform.position.y;
        transform.position = Vector3.Lerp(transform.position, wanted_position, Time.deltaTime * followTightness);

        if (wanted_position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        animator.SetInteger("AnimState", 2);

    }
}
