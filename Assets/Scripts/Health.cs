using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Health : MonoBehaviour
{
    [SerializeField] public float maxHealth = 100;
    [SerializeField] bool destroyOnDying = true;
    [SerializeField] float destroyAfter = 6f;
    [SerializeField] string scoreLogicTag;

    [SerializeField] Bar bar;

    public float currentHealth { get; private set; }

    private Animator animator;
    private bool hasDied = false;
    private ScoreLogic scoreLogic;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        scoreLogic = GameObject.FindGameObjectWithTag(scoreLogicTag).GetComponent<ScoreLogic>();
    }


    public void increaseHealth(int hp)
    {
        Debug.Log("health increase by " + hp + " for " + gameObject.name);
        if (currentHealth + hp < currentHealth)
        {
            animator.SetTrigger("Hurt");
        }

        currentHealth += hp;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
        bar.setBar(currentHealth);
    }

    public void fillHealth()
    {
        bar.setBar(maxHealth);
    }


    private void Die()
    {
        DisableComponents();
        enabled = false;
        animator.SetTrigger("Death");
        hasDied = true;
        if (destroyOnDying)
            StartCoroutine(waitAndDestroy());
    }

    // observable pattern may be a best fit for this functionalty for SOC
    virtual protected void DisableComponents()
    {


        // AI Player
        if (gameObject.tag == "Enemy")
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Tracker>().enabled = false;
            AutoPlayerCompat autoPlayer = GetComponent<AutoPlayerCompat>();
            autoPlayer.StopAllCoroutines(); // becuase we use delay with coroutines
            autoPlayer.enabled = false;

            // update score
            scoreLogic.AddPoints(gameObject);
        }

        // Player
        else if (gameObject.tag == "Player")
        {
            GetComponent<Movment>().enabled = false;
            PlayerCompat compat = GetComponent<PlayerCompat>();
            compat.StopAllCoroutines(); // becuase we use delay with coroutines
            compat.enabled = false;
        }

        Debug.Log(gameObject.name + " is dead " + hasDied);
    }


    private IEnumerator waitAndDestroy()
    {
        yield return new WaitForSeconds(destroyAfter);
        Destroy(gameObject);
    }


    public bool isDead()
    {
        return this.hasDied;
    }


}
