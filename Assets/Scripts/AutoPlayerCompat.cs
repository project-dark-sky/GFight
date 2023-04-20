using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Animator))]
public class AutoPlayerCompat : MonoBehaviour
{
    [SerializeField] string targetTag;
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] int attackDamage = 20;
    [SerializeField] float attackRate = 2f;

    private Animator animator;
    private Health playerHealth; // the actual player 
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag(targetTag);
        playerHealth = target.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.isDead())
        {
            animator.SetInteger("AnimState", 0);
            return;
        }
        float distance = Vector3.Distance(target.transform.position, gameObject.transform.position);

        if (distance < attackRange)
        {
            // problem with this approach that the attack function may be excuted more than once before the delay function gets excuted !!
            attack();
            enabled = false;
            StartCoroutine(delay());  // start delay
        }
    }


    private IEnumerator delay()
    {   // co-routines
        yield return new WaitForSeconds(attackRate);
        enabled = true;
    }


    void attack()
    {
        // play an attack animation
        animator.SetTrigger("Attack");


        // detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers); // this maks a circle and detect all objects

        // damage the enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("we hit " + enemy.name + " with damage " + attackDamage);
            enemy.GetComponent<Health>().increaseHealth(-attackDamage);
        }

    }


    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


    public void setAttackRate(float rate)
    {
        this.attackRate = rate;
    }

    public void setAttackDamage(int attackDamage)
    {
        this.attackDamage = attackDamage;
    }

}




