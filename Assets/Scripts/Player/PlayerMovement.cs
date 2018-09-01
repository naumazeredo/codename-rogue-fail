using UnityEngine;

public class PlayerMovement : MonoBehaviour {
  public PlayerGrounded playerGrounded;

  public float moveSpeed = 2f;
  public float jumpSpeed = 5f;

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

  void FixedUpdate() {
    float velY = rb.velocity.y;

    if (inputJ && playerGrounded.IsGrounded()) {
      velY = jumpSpeed;
    }

    rb.velocity = new Vector2(inputX * moveSpeed, velY);
  }
}
