using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantKill : MonoBehaviour
{
    [SerializeField]
    private string triggeringTag;

    [SerializeField]
    private float time = 5;

    private Coroutine coroutine;

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == triggeringTag)
        {
            // hide the power up
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            var compat = other.GetComponent<PlayerCompat>();
            if (compat)
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine); // disable old power up
                }

                coroutine = StartCoroutine(startInstantKill(compat));
            }
        }
    }

    IEnumerator startInstantKill(PlayerCompat compat)
    {
        compat.setMaxDamage();
        yield return new WaitForSeconds(time);
        compat.setDefualtDamage();
        Destroy(gameObject);
    }

}
