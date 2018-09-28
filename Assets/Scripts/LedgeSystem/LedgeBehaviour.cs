using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeBehaviour : MonoBehaviour {

	public float GrabDistance = 1.0f;

	private void OnDrawGizmos() {
		Gizmos.DrawWireSphere(transform.position, GrabDistance);
	}
}
