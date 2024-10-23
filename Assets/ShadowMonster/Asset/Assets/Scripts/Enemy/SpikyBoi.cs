using UnityEngine;

public class SpikyBoi : Enemy
{
	public GameObject trigger;

	private new void Start()
	{
		trigger.SetActive(value: false);
	}

	private void Update()
	{
		Move();
		Attack();
		if (isMoving)
		{
			anim.SetBool("run", value: true);
		}
		else
		{
			anim.SetBool("run", value: false);
		}
	}

	public void TriggerSwitch()
	{
		if (!trigger.activeSelf)
		{
			trigger.SetActive(value: true);
		}
		else
		{
			trigger.SetActive(value: false);
		}
	}

	public void SwingSfx()
	{
		AudioManager.instance.Play("spikyboiswing");
	}
}
