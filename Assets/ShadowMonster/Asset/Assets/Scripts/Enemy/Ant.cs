using UnityEngine;

public class Ant : Enemy
{
	public GameObject trigger;

	public GameObject body;

	private new void Start()
	{
		trigger.SetActive(value: false);
		body.SetActive(value: false);
	}

	private void Update()
	{
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
	}

	public void Jump()
	{
		if (facingLeft)
		{
			enemyRb.AddForce(Vector2.left * 16000f, ForceMode2D.Force);
		}
		else
		{
			enemyRb.AddForce(Vector2.right * 16000f, ForceMode2D.Force);
		}
	}

	public void TriggerSwitch()
	{
		if (!trigger.activeSelf)
		{
			trigger.SetActive(value: true);
			body.SetActive(value: true);
		}
		else
		{
			trigger.SetActive(value: false);
			body.SetActive(value: false);
		}
	}

	public void AtkSfx()
	{
		AudioManager.instance.Play2("bullatk");
	}
}
