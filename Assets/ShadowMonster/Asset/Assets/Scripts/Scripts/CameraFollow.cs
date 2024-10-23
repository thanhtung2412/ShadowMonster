using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;

	public Vector3 offset;

	[Range(1f, 100f)]
	public float smoothFactor;

	private void FixedUpdate()
	{
		Follow();
	}

	private void Follow()
	{
		_ = target.position + offset;
		float x = Mathf.Clamp(base.transform.position.x, -10f, 20f);
		Vector3 position = Vector3.Lerp(b: new Vector3(x, 9f, 0f), a: base.transform.position, t: smoothFactor * Time.fixedDeltaTime);
		base.transform.position = position;
	}
}
