using UnityEngine;
using System.Collections;

public class PlayerGrounded : MonoBehaviour {
  // Total time to wait before deactivating is_grounded
  public float disableDelay = 2.1f;

  bool grounded;

  Coroutine disableCoroutine;

  IEnumerator DisableGrounded() {
    yield return new WaitForSeconds(disableDelay);
    grounded = false;
  }

  public bool IsGrounded() {
    return grounded;
  }

  void OnTriggerStay2D() {
    if (!grounded) {
      grounded = true;
    }
  }


  void OnTriggerExit2D() {
    if (grounded) {
      if (disableCoroutine != null)
        StopCoroutine(disableCoroutine);
      disableCoroutine = StartCoroutine(DisableGrounded());
    }
  }
}
