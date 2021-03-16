using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    public Vector3 velocity;
    public Vector3 staticVelocity;
    public float movementSpeedMultiplier = 10;
    public Rigidbody2D rb;


    public bool isGrounded; float jumpForce = 80;

    public Slider staminaBar;

    public float savedDir = 1;
    public float dashRange = 0.5f, dashSwitch;
    private enum State
    {
        Normal,
        Dashing,
        Crouching
    }
    [SerializeField]
    private State activeState;
    void Awake()
    {
        activeState = State.Normal;
    }
    public void resetVelocity()
    {
        velocity = Vector3.zero;
    }
    void Update()
    {
        if(velocity.x != 0)
        {
            savedDir = velocity.x / Mathf.Abs(velocity.x);
        }
        if (rb.velocity.y == 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        switch (activeState)
        {
            case State.Normal:
                staticVelocity = rb.velocity;

                velocity.x = Input.GetAxisRaw("Horizontal") * movementSpeedMultiplier;
                rb.velocity = new Vector2(velocity.x, rb.velocity.y);

                if (Input.GetKey(KeyCode.Space) && isGrounded)
                {
                    rb.AddForce(jumpForce * transform.up);
                }

                staminaBar.value += Time.deltaTime;

                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    staminaBar.value -= 2;
                    dashSwitch = dashRange;
                    activeState = State.Dashing;
                }

                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    activeState = State.Crouching;
                }
                break;
            case State.Dashing:
                dashSwitch -= Time.deltaTime;

                rb.velocity = 50 * savedDir * transform.right;
                if (dashSwitch < 0)
                {
                    activeState = State.Normal;
                }
                break;
            case State.Crouching:

                break;
        }
    }
    private void OnDrawGizmosSelected()
    {   Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, GetComponentInChildren<BoxCollider2D>().size);
    }
}