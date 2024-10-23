using UnityEngine;

public class ExplosionPush : MonoBehaviour
{
	private void Start()
	{
		Invoke("Destroy", 0.1f);
	}

	private void Destroy()
	{
		Destroy(base.gameObject);
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player") && CombatManager.instance.player.isAlive)
		{
			col.GetComponent<PlayerController>().Stun(0.5f);
			col.GetComponent<PlayerHealth>().GetDamaged(1);
		}
	}
}
