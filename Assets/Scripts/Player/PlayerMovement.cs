using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public PlayerGrounded playerGrounded;

    public float jumpHeightMax = 5f;
    public float jumpHeightMin = 5f;
    public float jumpSpeed = 5f;
    public float fallGravity = 5f;
    public float moveSpeed = 5f;
    public float dragX = 10f;
    public float airSpeed = 1f;
    public float airDragX = 1f;
    public float maxV = 10f;

    public float wallJumpImpulse = 1f;
    private float dragY;
    public CollisionFetcher cf;

    Rigidbody2D rb;

    float inputX;
    //float inputY;
    bool inputJ;
    //float input_b;
    //float input_r;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update () {
        // TODO: Use another variable to get input to multiple players
        inputX = Input.GetAxis("Horizontal");
        //input_y = Input.GetAxis("Vertical");
        inputJ = Input.GetButtonDown("Jump");
    }

    void FixedUpdate()
    {
        ContactInfo x = bestContactInfo(cf, Vector2.up);
        Debug.DrawLine(x.position,x.position+x.normal,Color.cyan);
        dragY = fallGravity/maxV;
        float jumpGravityMax = jumpSpeed*jumpSpeed/(2*jumpHeightMax);
        float jumpGravityMin = jumpSpeed*jumpSpeed/(2*jumpHeightMin);

        if (cf.GetContactCount() > 0)
        {
            if (rb.velocity.y <= 0f)
            {
                Vector2 dragForceY = ( -Vector2.Dot(rb.velocity,Vector2.up)*Vector2.up ) * dragY;
                rb.AddForce( rb.mass * fallGravity * Vector2.down );
                rb.AddForce(dragForceY);
            }
            else if ( Input.GetButton("Jump") )
            {
                rb.AddForce( rb.mass * jumpGravityMax * Vector2.down );
            }
            else
            {
                rb.AddForce( rb.mass * jumpGravityMin * Vector2.down );
            }

            Vector2 inputForceX = inputX * moveSpeed * Vector2.right * rb.mass;
            rb.AddForce(inputForceX, ForceMode2D.Impulse);

            Vector2 dragForceX = Vector2.Dot(x.relativeVelocity,Vector2.right)*Vector2.right * dragX;
            rb.AddForce(dragForceX);

            if (inputJ)
            {
                rb.AddForce(Vector2.up*jumpSpeed*rb.mass, ForceMode2D.Impulse);
                rb.AddForce(Vector2.Dot(x.normal,Vector2.right)*Vector2.right*wallJumpImpulse*rb.mass, ForceMode2D.Impulse);
            }
        }
        else
        {
            if (rb.velocity.y <= 0f)
            {
                Vector2 dragForceY = ( -Vector2.Dot(rb.velocity,Vector2.up)*Vector2.up ) * dragY;
                rb.AddForce( rb.mass * fallGravity * Vector2.down );
                rb.AddForce(dragForceY);
            }
            else if ( Input.GetButton("Jump") )
            {
                rb.AddForce( rb.mass * jumpGravityMax * Vector2.down );
            }
            else
            {
                rb.AddForce( rb.mass * jumpGravityMin * Vector2.down );
            }

            Vector2 inputForceX = inputX * airSpeed * Vector2.right * rb.mass;
            rb.AddForce(inputForceX, ForceMode2D.Impulse);

            Vector2 airDragForceX = -Vector2.Dot(rb.velocity,Vector2.right)*Vector2.right * airDragX;
            rb.AddForce(airDragForceX);
        }

    }

    private ContactInfo bestContactInfo(CollisionFetcher cfet, Vector2 axis)
    {
        float bestAngle = -1.1f;
        ContactInfo best = new ContactInfo();
        foreach (ContactInfo x in cfet)
        {
            float angle = Vector2.Dot(x.normal, axis);
            if (angle > bestAngle)
            {
                best = x;
                bestAngle = angle;
            }
        }

        return best;
    }
}
