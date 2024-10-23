using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
	private Enemy enemy;

	private void Start()
	{
		enemy = base.transform.parent.GetComponent<Enemy>();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player") && CombatManager.instance.player.isAlive)
		{
			col.GetComponent<PlayerController>().Stun(enemy.atkStunTime);
			col.GetComponent<PlayerHealth>().GetDamaged(enemy.atkDamage);
			AudioManager.instance.Play(enemy.hitSound);
		}
	}
}
