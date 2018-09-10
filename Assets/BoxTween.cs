using UnityEngine;

public class BoxTween : MonoBehaviour
{
    [SerializeField] private float frequency;
    [SerializeField] private float amplitude;
    private Rigidbody2D rb;
    private float time;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        time = 0;
    }

	void FixedUpdate ()
	{
	    float omega = 2 * Mathf.PI * frequency;
	    rb.velocity = amplitude * Mathf.Sin(omega * time) * Vector2.right;
	    time += Time.fixedDeltaTime;
	    if ( omega*time >= 2*Mathf.PI ) time = 0;
	}
}
