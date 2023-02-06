using UnityEngine;

/// <summary>
/// Moves character like a platformer character
/// </summary>
public class Move_Platformer : MonoBehaviour
{
    public float speed = 5f;
    public float jumpPower = 15f;

    private Rigidbody2D rb;
    private Animator ani;
    private SpriteRenderer spr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        rb.gravityScale = 5f;
        ani = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        #region Move
        float m = Input.GetAxis("Horizontal") * speed / 50f;
        if (m != 0f) spr.flipX = m < 0f;
        rb.transform.Translate(m, 0f, 0f);
        #endregion Move

        #region Jump
        if (Input.GetKey(KeyCode.Z))
        {
            if (!lastJumpPressed) wantToJump = 5;// set grace jump timer
            lastJumpPressed = true;
        }
        else lastJumpPressed = false;

        if (wantToJump > 0)
        {
            wantToJump--;
            bool canJump = false;
            if (grounded) canJump = true; // grounded: can jump
            else if (doubleJump) { doubleJump = false; canJump = true; } // use doublejump if available
            if (canJump) // if jump pressed and can jump
            {
                wantToJump = 0;
                rb.velocity = new Vector2(rb.velocity.x, jumpPower); // apply jump force
            }
        }
        #endregion Jump

        float s = Mathf.Max(Mathf.Abs(Input.GetAxis("Horizontal")), rb.velocity.y / jumpPower);
        s = Mathf.Clamp01(s * 0.8f + 0.2f);
        ani.speed = s;

    }

    private int wantToJump = 0;
    private bool lastJumpPressed = false;
    private bool grounded = false;
    private bool doubleJump = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            grounded = true; doubleJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block")
            && rb.velocity.y > 0f) // if this is moving up
        {
            grounded = false;
        }
    }
}
