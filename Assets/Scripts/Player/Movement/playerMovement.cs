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

    public Transform groundObject;
    public bool isGrounded; float groundCheckRadius = 0.05f, jumpForce = 200f;

    public Slider staminaBar;

    BoxCollider2D playerCollider;
    Vector2 collSize;

    public bool animatedDash = true;
    void Start()
    {
        playerCollider = GetComponentInChildren<BoxCollider2D>();
        collSize = playerCollider.size;
    }
    void Update()
    {
        staticVelocity = rb.velocity;

        #region movement_directional
        velocity.x = Input.GetAxisRaw("Horizontal")* movementSpeedMultiplier;
        rb.velocity = new Vector2(velocity.x, rb.velocity.y);
        #endregion

        #region movement_jump
        if (rb.velocity.y == 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        if (Input.GetButtonDown("Jump")&&isGrounded)
        {
            rb.AddForce(jumpForce * transform.up);
        }
        #endregion

        #region movement_daash
        staminaBar.value += Time.deltaTime;
        if (animatedDash)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && velocity.x != 0 && staminaBar.value > 1)
            {
                staminaBar.value -= 2;
                rb.velocity = Mathf.Lerp(200, 0, 2f) * transform.right;
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.LeftShift)&&velocity.x != 0 && staminaBar.value > 1)
            {
                staminaBar.value-=2;
                rb.AddForce(700 * velocity.x * transform.right);
            }
        }
        #endregion

        #region Crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {

        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {

        }
        #endregion
    }
    private void OnDrawGizmosSelected()
    {   Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(groundObject.position, groundCheckRadius);

        Gizmos.DrawWireCube(transform.position, GetComponentInChildren<BoxCollider2D>().size);
    }
}