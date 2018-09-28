using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LedgeGrabber : MonoBehaviour {
	private LedgeBehaviour _attachedLedge;
	public bool _canGrab;
	private Joint _joint;
	public UnityEvent OnLedgeGrab;
	public UnityEvent OnLedgeRelease;
	public UnityEvent OnLedgeBreak;

	private void OnJointBreak(float breakForce) {
		OnLedgeBreak.Invoke();
	}

	private void GrabLedge(LedgeBehaviour ledge) {
		Debug.Log("Buceta");
		OnLedgeGrab.Invoke();
    var l = gameObject.AddComponent<DistanceJoint2D>();
    l.autoConfigureDistance = false;
    l.enableCollision = true;
		l.distance = ledge.GrabDistance;
	}

	public void ReleaseLedge() {
		_attachedLedge = null;
		Destroy(_joint);
	}

	public void OnTriggerEnter2D(Collider2D other) {
		LedgeBehaviour otherLedge = other.GetComponent<LedgeBehaviour>();
		if (otherLedge!=null && _canGrab && !IsAttached()) {
			_attachedLedge = otherLedge;
			GrabLedge(otherLedge);
		}
	}
	

	public void OnTriggerStay2D(Collider2D other) {
		LedgeBehaviour otherLedge = other.GetComponent<LedgeBehaviour>();
		if (otherLedge!=null && _canGrab && !IsAttached()) {
			_attachedLedge = otherLedge;
			GrabLedge(otherLedge);
		}
	}
	

	public bool IsAttached() {
		return _attachedLedge != null;
	}

	public void CanGrab(bool canGrab) {
		_canGrab = canGrab;
	}
	
}
