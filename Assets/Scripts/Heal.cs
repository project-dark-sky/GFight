using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField]
    private string healTargetTag;

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == healTargetTag)
        {
            var health = other.GetComponent<Health>();
            if (health)
            {
                health.fillHealth();
            }
            Destroy(gameObject);
        }
    }
}
