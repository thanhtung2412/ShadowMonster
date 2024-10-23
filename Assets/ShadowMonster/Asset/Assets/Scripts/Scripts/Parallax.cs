using UnityEngine;

public class Parallax : MonoBehaviour
{
	public Camera cam;

	public Transform subject;

	private Vector2 startPos;

	private float startZ;

	private float distanceFromSubject => base.transform.position.z - subject.position.z;

	private Vector2 travel => (Vector2)cam.transform.position - startPos;

	private float clippingPlane => cam.transform.position.z + ((distanceFromSubject > 0f) ? cam.farClipPlane : cam.nearClipPlane);

	private float parallaxFactor => Mathf.Abs(distanceFromSubject) / clippingPlane;

	private void Start()
	{
		startPos = base.transform.position;
		startZ = base.transform.position.z;
	}

	private void Update()
	{
		Vector2 vector = startPos + travel * parallaxFactor;
		base.transform.position = new Vector3(vector.x, vector.y, startZ);
	}
}
