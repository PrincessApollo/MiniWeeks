using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    public Vector3 velocity;
    public Vector3 staticVelocity;
    [Space(20)]
    public float movementSpeedMultiplier = 10;
    Rigidbody2D rb;

    public bool isGrounded; float jumpForce = 80;

    public float savedDir = 1;
    public float dashRange = 0.5f, dashSwitch;
    private enum State
    {
        Normal,
        Dashing,
        Crouching,
    }
    private enum Keycodes
    {
        set1,
        set2,
    }
    Keycodes inputPrefs;
    string usedInput;
    [SerializeField]
    private State activeState;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        activeState = State.Normal;
        inputPrefs = Keycodes.set1;
    }
    private void Start()
    {
        switch (inputPrefs)
        {
            case Keycodes.set1:
                usedInput = "Horizontal";
                break;
            case Keycodes.set2:
                usedInput = "Vertical";
                break;
        }
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

                #region transitions
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    dashSwitch = dashRange;
                    //activeState = State.Dashing;
                }

                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    //activeState = State.Crouching;
                }
                break;
                #endregion
        }
    }
    private void OnDrawGizmosSelected()
    {   Gizmos.color = Color.green;
    }
}