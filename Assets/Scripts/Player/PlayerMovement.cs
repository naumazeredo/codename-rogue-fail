using UnityEngine;

public class PlayerMovement : PhysicsObject {
  InputManager inputManager;

  public int playerId = 0;

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

  public float wallJumpImpulse = 1f;
  private float dragY;

  Rigidbody2D rb;

  private float inputX;
  //private float input_y;

  private MoveState state = MoveState.Normal;

  //private float ledgeDist = 0.5f;

  void Start () {
    SetupPhysics();

    rb = GetComponent<Rigidbody2D>();
    inputManager = GameObject.FindWithTag("Input Manager").GetComponent<InputManager>();
  }

  void Update() {
    inputX = 0;
    if (inputManager.GetKey(playerId, InputKey.Right)) inputX += 1;
    if (inputManager.GetKey(playerId, InputKey.Left )) inputX -= 1;
  }

  void FixedUpdate() {
    ContactInfo ground = GetBestContactInfo(Vector2.up);
    Debug.DrawLine(ground.position, ground.position + ground.normal, Color.cyan);
    dragY = fallGravity / maxV;

    if (state == MoveState.Normal) {
      // Gravity force
      if (rb.velocity.y <= 0f) {
        Vector2 dragForceY = -Vector2.Dot(rb.velocity, Vector2.up) * Vector2.up * dragY;
        rb.AddForce(rb.mass * fallGravity * Vector2.down);
        rb.AddForce(dragForceY);
      } else if (inputManager.GetKey(playerId, InputKey.Jump)) {
        float jumpGravityMax = jumpSpeed * jumpSpeed / (2 * jumpHeightMax);
        rb.AddForce(rb.mass * jumpGravityMax * Vector2.down);
      } else {
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
        if (inputManager.GetKeyDown(playerId, InputKey.Jump)) {
          rb.velocity = new Vector2(rb.velocity.x, 0);
          rb.AddForce(Vector2.up * jumpSpeed * rb.mass, ForceMode2D.Impulse);

          // Wall jump
          rb.AddForce(Vector2.Dot(ground.normal, Vector2.right) * Vector2.right * wallJumpImpulse * rb.mass,
            ForceMode2D.Impulse);
        }
      } else {
        // Mid-air horizontal movement
        Vector2 inputForceX = inputX * airSpeed * Vector2.right * rb.mass;
        rb.AddForce(inputForceX, ForceMode2D.Impulse);

        // Mid-air horizontal drag
        // TODO: air drag == drag always. Movements are ugly if they don't match
        Vector2 airDragForceX = -Vector2.Dot(rb.velocity, Vector2.right) * Vector2.right * airDragX;
        rb.AddForce(airDragForceX);
      }
    //} else { //Ledge grab state
    }


    //Debug.Log(state);
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("ledge") && other.transform.position.y - transform.position.y > 0 && Mathf.Abs(inputX) > 0) {
      var dist = Vector2.Distance(other.transform.position,transform.position);
      state = MoveState.LedgeGrab;
      DistanceJoint2D ledge = gameObject.AddComponent<DistanceJoint2D>();
      ledge.connectedAnchor = other.transform.position;
      ledge.distance = dist;
      ledge.autoConfigureDistance = false;
      ledge.enableCollision = true;
    }
  }

  private enum MoveState
  {
      Normal,
      LedgeGrab
  }
}

// TODO: For ledge grab, detect using CollisionFetcher if is close to edge, then pass to LedgeGrab state,
// on LedgeGrab state, zero player input, and set an opposite force equal to gravity, or simply disable it, until
// the player inputs to drop the ledge or get up. On get up, force player on top of ledge, by calculating the collision
// boundaries relative to player position.
