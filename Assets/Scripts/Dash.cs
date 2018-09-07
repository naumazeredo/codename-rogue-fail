using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Dash : MonoBehaviour
{

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _tapTime = 0f;
    [SerializeField] private float _dashTime = 0f;
    [SerializeField] private Vector2 _dir = Vector2.zero;

	[SerializeField] private bool _isDashing = false;
	[SerializeField] private DashInfo _dashInfo;

    private bool lastFramePressed = false;

	// Use this for initialization
	void Start ()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetButtonDown(_dashInfo.buttonName) && !_isDashing)
	    {
            _isDashing = true;
            _dir = _rb.velocity.normalized;
	    }
	}

    private void FixedUpdate()
    {
        //_dashTime += Time.fixedTime;

        var force = Vector2.down * _rb.mass * Physics.gravity;
        _rb.AddForce(force );
        Debug.Log( _rb.mass * Physics.gravity );
        Debug.Log( force );
        //_rb.velocity = _dir * _dashInfo.dashSpeed;
        if (_dashTime >= _dashInfo.dashTime)
        {
            _isDashing = false;
            _dashTime = 0;
        }
    }
}
