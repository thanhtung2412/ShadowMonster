using UnityEngine;

public class Egg : Enemy
{
	public GameObject monster;

	private new void Start()
	{
		name = "Egg";
		atkDamage = 0;
		maxHealth = 60;
		moveSpeed = 0f;
		atkRange = 0f;
		facingLeft = true;
		currentHealth = maxHealth;
		Invoke("Die", 2.5f);
		Invoke("SpawnEnemy", 2.4f);
		healAmount = 0;
		extraJumps = 0;
	}

	private void SpawnEnemy()
	{
		AudioManager.instance.Play2("eggcrack");
		GameObject item = Object.Instantiate(monster, new Vector2(base.transform.position.x, 0.5f), Quaternion.identity);
		spawner.GetComponent<WaveSpawner>().enemies.Add(item);
	}
}
