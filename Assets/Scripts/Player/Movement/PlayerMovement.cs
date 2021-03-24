using PrincessApollo.Controls;
using UnityEngine;
using UnityEngine.UI;
using static System.Convert;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector3 velocity;
    [Space(20)]
    [SerializeField] float movementSpeedMultiplier = 10;
    
    bool isGrounded;
    float dashRange = 0.5f, dashSwitch = 1, dashVelocity, savedDir = 1, jumpForce = 80;
    
    Rigidbody2D rb; 
    BoxCollider2D v_coll;
    Vector2 o_coll;

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

        o_coll = v_coll.size;
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
        #endregion
        rb.velocity = new Vector2(velocity.x + dashVelocity * savedDir, rb.velocity.y);

        switch (activeState)
        {
            case State.Normal:

                //TEMP??
                sl.value += Time.deltaTime;

                tt.text = " STATE = NORMAL";
                dashVelocity = 0;
                velocity.x = (ToInt32(Input.GetKey(Controls.Scheme.GetCodeFromKey($"{controlSet}-Right"))) - ToInt32(Input.GetKey(Controls.Scheme.GetCodeFromKey($"{controlSet}-Left")))) * movementSpeedMultiplier;

                if (Input.GetKey(Controls.Scheme.GetCodeFromKey($"{controlSet}-Forward")) && isGrounded)
                {
                    rb.AddForce(jumpForce * transform.up);
                }

                #region transitions
                if (Input.GetKeyDown(Controls.Scheme.GetCodeFromKey($"{controlSet}-Dash")) && sl.value > 1)
                {
                    dashSwitch = dashRange;
                    activeState = State.Dashing;

                    sl.value--;
                }
                if (Input.GetKeyDown(Controls.Scheme.GetCodeFromKey($"{controlSet}-Back")))
                {
                    activeState = State.Crouching;
                }
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    activeState = State.Crouching;
                }
                break;
            case State.Dashing:
                tt.text = "STATE = DASHING";

                dashVelocity = 20;
                dashSwitch -= Time.deltaTime * 3.5f;

                if(dashSwitch <= 0)
                {
                    dashSwitch = 1;
                    activeState = State.Normal;
                }
                break;
            case State.Crouching:
                tt.text = "STATE = CROUCING";

                v_coll.size = new Vector2(o_coll.x, 5);
                v_coll.offset = Vector2.up * -2.5f;
                resetVelocity();

                if (Input.GetKeyUp(KeyCode.LeftControl))
                {
                    v_coll.size = o_coll;
                    v_coll.offset = Vector2.zero;
                    activeState = State.Normal;
                }
                break;
                #endregion
        }
    }
}