using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Animator))]
public class PlayerCompat : MonoBehaviour
{
    [SerializeField] InputAction hitButton = new InputAction(type: InputActionType.Button);

    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] int maxDamage = 100;
    [SerializeField] int defualtAttackDamage = 20;
    [SerializeField] float attackRate = 2f;

    private int attackDamage;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        attackDamage = defualtAttackDamage;
        animator = GetComponent<Animator>();
        hitButton.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (hitButton.WasPerformedThisFrame())
        {
            attack();
            StartCoroutine(delay());  // start delay
        }
    }

    public void setMaxDamage()
    {
        this.attackDamage = maxDamage;
    }

    public void setDefualtDamage()
    {
        this.attackDamage = defualtAttackDamage;
    }

    private IEnumerator delay()
    {   // co-routines
        enabled = false;
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
            Debug.Log("we hit " + enemy.name);
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




