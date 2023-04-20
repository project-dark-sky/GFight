using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Movment : MonoBehaviour
{

    [SerializeField] float speed = 4.0f;
    [SerializeField] float jumpForce = 7.5f;

    [SerializeField] InputAction moveHorizontal = new InputAction(type: InputActionType.Button);
    [SerializeField] InputAction jumpButton = new InputAction(type: InputActionType.Button);

    [SerializeField] string groundTag;

    private Animator animator;
    private Rigidbody2D body2d;
    private bool combatIdle = false;
    private bool isOnGround = true;


    // Use this for initialization
    void Start()
    {
        moveHorizontal.Enable();
        jumpButton.Enable();
        animator = GetComponent<Animator>();
        body2d = GetComponent<Rigidbody2D>();
    }



    // Update is called once per frame
    void Update()
    {
        float horizontal = moveHorizontal.ReadValue<float>();


        // Swap direction of sprite depending on walk direction
        if (horizontal != 0)
            transform.localScale = new Vector3(-horizontal, 1.0f, 1.0f);


        // Move
        body2d.velocity = new Vector2(horizontal * speed, body2d.velocity.y);

        //Set AirSpeed in animator
        animator.SetFloat("AirSpeed", body2d.velocity.y);


        //Jump
        if (jumpButton.WasPerformedThisFrame() && isOnGround)
        {
            body2d.velocity = new Vector2(body2d.velocity.x, jumpForce);
            //isOnGround = false;
        }

        //Run
        else if (Mathf.Abs(horizontal) > 0 && isOnGround)
            animator.SetInteger("AnimState", 2);
        //Combat Idle
        else if (combatIdle)
            animator.SetInteger("AnimState", 1);

        //Idle
        else
            idle();

    }


    // update to idle state animation
    void idle()
    {
        animator.SetInteger("AnimState", 0);
    }

    // when touching the ground upate isOnGround to true
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == groundTag)
        {
            isOnGround = true;
            animator.SetBool("Grounded", true);
        }

    }

    // when jumping upate isOnGround to false
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == groundTag)
        {
            isOnGround = false;
            animator.SetTrigger("Jump");
            animator.SetBool("Grounded", false);
        }
    }


}
