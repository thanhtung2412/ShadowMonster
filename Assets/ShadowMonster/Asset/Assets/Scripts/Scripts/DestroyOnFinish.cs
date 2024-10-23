using UnityEngine;

public class DestroyOnFinish : MonoBehaviour
{
	public void Destroy()
	{
		Destroy(base.gameObject);
	}
}
