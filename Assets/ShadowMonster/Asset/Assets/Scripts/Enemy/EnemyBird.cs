using UnityEngine;

public class EnemyBird : Enemy
{
	private float timeBetweenEggs = 2.5f;

	public float countdown;

	public GameObject egg;

	public Transform spawnEgg;

	private new void Start()
	{
		name = "Bird";
		atkDamage = 3;
		maxHealth = 40;
		moveSpeed = 5f;
		atkRange = 10f;
		facingLeft = true;
		countdown = timeBetweenEggs;
		currentHealth = maxHealth;
	}

	private void Update()
	{
		countdown -= Time.deltaTime;
		countdown = Mathf.Clamp(countdown, 0f, float.PositiveInfinity);
		if (countdown <= 0f)
		{
			Attack();
			countdown = timeBetweenEggs;
		}
		else
		{
			Move();
			Retreat(8, 2f);
		}
	}

	private new void Attack()
	{
		anim.SetTrigger("Attack");
	}

	private void SpawnEgg()
	{
		GameObject item = Instantiate(egg, new Vector2(spawnEgg.position.x, spawnEgg.position.y), Quaternion.identity);
		spawner.GetComponent<WaveSpawner>().enemies.Add(item);
	}
}
