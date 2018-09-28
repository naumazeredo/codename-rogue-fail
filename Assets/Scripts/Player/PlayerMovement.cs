using UnityEngine;

public class PlayerMovement : PhysicsObject {
  // XXX: Refactor? Create Movement(Vector2 currentGravity, Vector2 targetVelocity)?
  public float jumpHeightMin = 5f;
  public float jumpHeightMax = 5f;
  public float jumpSpeed = 5f;
  public float fallGravity = 5f;
  public float moveSpeed = 5f;
  public float dragX = 10f;
  public float airSpeed = 1f;
  public float airDragX = 1f;
  public float maxV = 10f;

  public LedgeGrabber LedgeGrabber;

  public float wallJumpImpulse = 1f;
  private float dragY;

  Rigidbody2D rb;

  private float inputX;

  private MoveState state = MoveState.Normal;

  private float ledgeDist = 0.5f;
  //private float input_y;

  void Start () {
    SetupPhysics();
    rb = GetComponent<Rigidbody2D>();
  }

  void Update() {
    inputX = Input.GetAxis("Horizontal");
    //input_y = Input.GetAxis("Vertical");
  }

  void FixedUpdate() {
    ContactInfo ground = GetBestContactInfo(Vector2.up);
    Debug.DrawLine(ground.position, ground.position + ground.normal, Color.cyan);
    dragY = fallGravity / maxV;
    // Gravity force
    if (rb.velocity.y <= 0f) {
      Vector2 dragForceY = -Vector2.Dot(rb.velocity, Vector2.up) * Vector2.up * dragY;
      rb.AddForce(rb.mass * fallGravity * Vector2.down);
      rb.AddForce(dragForceY);
    }
    else if (Input.GetButton("Jump")) {
      float jumpGravityMax = jumpSpeed * jumpSpeed / (2 * jumpHeightMax);
      rb.AddForce(rb.mass * jumpGravityMax * Vector2.down);
    }
    else {
      float jumpGravityMin = jumpSpeed * jumpSpeed / (2 * jumpHeightMin);
      rb.AddForce(rb.mass * jumpGravityMin * Vector2.down);
    }

    if (GetContactCount() > 0) {
      // Horizontal movement
      Vector2 inputForceX = inputX * moveSpeed * Vector2.right;
      rb.AddForce(inputForceX, ForceMode2D.Impulse);

      // Horizontal drag
      Vector2 dragForceX = Vector2.Dot(ground.relativeVelocity, Vector2.right) * Vector2.right * dragX;
      rb.AddForce(dragForceX);

      // Jump logic
      // FIXME: Collision with normal pointing down should not allow jumping
      if (Input.GetButtonDown("Jump")) {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed * rb.mass, ForceMode2D.Impulse);
        // Wall jump
        rb.AddForce(Vector2.Dot(ground.normal, Vector2.right) * Vector2.right * wallJumpImpulse * rb.mass,
        ForceMode2D.Impulse);
      }
    }
    else {
      // Mid-air horizontal movement
      Vector2 inputForceX = inputX * airSpeed * Vector2.right * rb.mass;
      rb.AddForce(inputForceX, ForceMode2D.Impulse);

      // Mid-air horizontal drag
      // TODO: air drag == drag always. Movements are ugly if they don't match
      Vector2 airDragForceX = -Vector2.Dot(rb.velocity, Vector2.right) * Vector2.right * airDragX;
      rb.AddForce(airDragForceX);
    }
    LedgeGrabber.CanGrab(true);
    if (Input.GetButton("Jump")) {
      LedgeGrabber.ReleaseLedge();
    }
  }
/*
  private void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("ledge") && Mathf.Abs(inputX) > 0) {
      //var dist = Vector2.Distance(other.transform.position,transform.position);
      var dist = other.bounds.max.y - other.transform.position.y;
      state = MoveState.LedgeGrab;
      var ledge = gameObject.GetComponent<DistanceJoint2D>();
      ledge.enabled = true;
      ledge.connectedAnchor = other.transform.position;
      ledge.distance = dist;
    }
  }*/
  
  public void ForceBreakLedge() { state = MoveState.Normal; }

  private enum MoveState
  {
      Normal,
      LedgeGrab
  }
}

