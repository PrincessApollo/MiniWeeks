using PrincessApollo.Controls;
using UnityEngine;
using static System.Convert;

public class PlayerMovement : MonoBehaviour
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
    public enum KeySets // Ändra inte namnen, dessa är samma namn som används i kontrollfilen // F
    {
        PlayerOne,
        PlayerTwo,
    }
    [SerializeField]
    private KeySets controlSet;
    [SerializeField]
    private State activeState;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void resetVelocity()
    {
        velocity = Vector3.zero;
    }
    void Update()
    {
        if (velocity.x != 0)
        {
            savedDir = velocity.x / Mathf.Abs(velocity.x);
        }

        switch (activeState)
        {
            case State.Normal:
                staticVelocity = rb.velocity;

                velocity.x = (ToInt32(Input.GetKey(Controls.Scheme.GetCodeFromKey($"{controlSet}-Right"))) - ToInt32(Input.GetKey(Controls.Scheme.GetCodeFromKey($"{controlSet}-Left")))) * movementSpeedMultiplier;
                rb.velocity = new Vector2(velocity.x, rb.velocity.y);

                if (Input.GetKey(Controls.Scheme.GetCodeFromKey($"{controlSet}-Forward")) && isGrounded)
                {
                    rb.AddForce(jumpForce * transform.up);
                }

                #region transitions
                if (Input.GetKeyDown(Controls.Scheme.GetCodeFromKey($"{controlSet}-Dash")))
                {
                    dashSwitch = dashRange;
                    //activeState = State.Dashing;
                }

                if (Input.GetKeyDown(Controls.Scheme.GetCodeFromKey($"{controlSet}-Back")))
                {
                    //activeState = State.Crouching;
                }
                break;
                #endregion
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, GetComponentInChildren<BoxCollider2D>().size);
    }
}