using UnityEngine;

public class eggdemon : Enemy
{
	private float timeAlive;

	private bool expl;

	private new void Start()
	{
		if (Random.Range(1, 3) == 1)
		{
			AudioManager.instance.Play2("laugh");
		}
		else
		{
			AudioManager.instance.Play2("laugh2");
		}
		healAmount = 0;
		extraJumps = 0;
		timeAlive = 3f;
	}

	private void Update()
	{
		timeAlive -= Time.deltaTime;
		Attack();
		Move();
		if (isMoving)
		{
			anim.SetBool("run", value: true);
		}
		else
		{
			anim.SetBool("run", value: false);
		}
		if (currentHealth <= 20 || timeAlive <= 0f)
		{
			StartCoroutine(StartAttack(atkRate));
		}
		if (timeAlive <= 1f && !expl)
		{
			expl = true;
			StartCoroutine(ChangeMaterial());
		}
	}

	public void AtkSfx()
	{
		AudioManager.instance.Play2("breathin");
	}
}
