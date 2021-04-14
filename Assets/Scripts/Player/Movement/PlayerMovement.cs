using PrincessApollo.Controls;
using UnityEngine;
using UnityEngine.UI;
using static System.Convert;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector3 velocity;
    [Space(20)]
    [SerializeField] float movementSpeedMultiplier = 10;

    float dashRange = 1f, dashSwitch = 1, dashVelocity;
    public float savedDir = 1, jumpForce = 200;
    
    Rigidbody2D rb; 
    BoxCollider2D v_coll;
    Animator a_anim;

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

    //TEMP??
    public Text tt;
    public Slider sl;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        v_coll = GetComponent<BoxCollider2D>();
        a_anim = GetComponent<Animator>();
    }
    public void resetVelocity()
    {
        velocity = Vector3.zero;
    }
    void Update()
    {
        #region ???
        if (velocity.x != 0)
        {
            savedDir = velocity.x / Mathf.Abs(velocity.x);
        }
        if(savedDir <= 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if(velocity.x == 0)
        {
            if(activeState != State.Crouching)
            {
                tt.text = " STATE = IDLE";
            }

            if (activeState == State.Normal)
            {
                a_anim.SetTrigger("Idle");
            }
        }

        #endregion
        rb.velocity = new Vector2(velocity.x + dashVelocity * savedDir, rb.velocity.y);

        switch (activeState)
        {
            case State.Normal:

                //TEMP??
                sl.value += Time.deltaTime;
                //

                if (velocity.x != 0)
                {
                    tt.text = " STATE = NORMAL";
                    a_anim.SetTrigger("Walking");
                }

                velocity.x = (ToInt32(Input.GetKey(Controls.Scheme.GetCodeFromKey($"{controlSet}-Right"))) - ToInt32(Input.GetKey(Controls.Scheme.GetCodeFromKey($"{controlSet}-Left")))) * movementSpeedMultiplier;

                dashVelocity = 0;

                if (Input.GetKey(Controls.Scheme.GetCodeFromKey($"{controlSet}-Forward")) || Input.GetKeyDown(KeyCode.Space) && rb.velocity.y >= 0)
                {
                    rb.AddForce(jumpForce * transform.up);
                }

                #region transitions
                if (Input.GetKeyDown(Controls.Scheme.GetCodeFromKey($"{controlSet}-Dash")) && sl.value > 1)
                {
                    dashSwitch = dashRange;
                    a_anim.SetBool("isDashing", true);
                    activeState = State.Dashing;

                    sl.value--;
                }
                if (Input.GetKeyDown(Controls.Scheme.GetCodeFromKey($"{controlSet}-Back")) && rb.velocity.y == 0 && rb.velocity.y >= 0)
                {
                    //activeState = State.Crouching;
                }
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    activeState = State.Crouching;
                }
                break;
            case State.Dashing:
                tt.text = "STATE = DASHING";
                newDashTImer -= Time.deltaTime;
                if(newDashTImer <= 0)
                {
                    dashVelocity = 60;
                    dashSwitch -= Time.deltaTime * 3.5f;
                }
                else
                {
                    resetVelocity();
                    dashVelocity = 0;
                }
                if(dashSwitch <= 0)
                {
                    newDashTImer = .7f;
                    dashSwitch = 1;
                    a_anim.SetBool("isDashing", false);
                    activeState = State.Normal;
                }
                break;
            case State.Crouching:
                tt.text = "STATE = CROUCING";
                a_anim.SetBool("Crouching", true);

                resetVelocity();

                if (Input.GetKeyUp(KeyCode.LeftControl))
                {
                    activeState = State.Normal;
                    a_anim.SetBool("Crouching", false);
                }
                break;
                #endregion
        }
    }
    float newDashTImer = .7f;
}