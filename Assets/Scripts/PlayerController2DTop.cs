using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController2DTop : MonoBehaviour
{
    public float MoveSpeed = 10f;

    private Rigidbody2D rb2D;
    private Vector2 velocity;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void OnMove(InputValue context)
    {
        // calculate moving direction and speed
        var value = context.Get<Vector2>();
        velocity = value * MoveSpeed;

        //  should i flip the sprite (moving left/right)
        if (value.x > 0) spriteRenderer.flipX = false;
        else if (value.x < 0) spriteRenderer.flipX = true;

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