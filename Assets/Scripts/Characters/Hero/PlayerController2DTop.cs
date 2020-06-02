using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController2DTop : MonoBehaviour
{
    //  Public vars
    public float moveSpeed = 10f;

    //  Private vars

    //  Cached objects
    private Rigidbody2D rb2D;
    private Vector2 velocity;
    private Animator animator;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void OnMove(InputValue context)
    {
        // calculate moving direction and speed
        var value = context.Get<Vector2>();
        velocity = value * moveSpeed;

        //  should i flip (moving left/right)
        if (value.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (value.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        //  swith to moving and idle animation 
        if (value != Vector2.zero)
        {
            animator.SetBool("walking", true);
        } else
        {
            animator.SetBool("walking", false);
        }
    }

    void FixedUpdate()
    {
        //  move the body
        rb2D.MovePosition(rb2D.position + velocity * Time.fixedDeltaTime);
    }
}