using UnityEngine;

public class ArrowController : MonoBehaviour
{
	public Transform player;

	public Material mat;
	private void FixedUpdate()
	{
		_ = player.transform.position;
		Vector3 vector = player.position - base.transform.position;
		float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		base.transform.rotation = Quaternion.Euler(0f, 0f, num + 90f);
	}
}
