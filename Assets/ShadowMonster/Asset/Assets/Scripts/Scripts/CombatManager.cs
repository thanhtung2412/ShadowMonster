using UnityEngine;

public class CombatManager : MonoBehaviour
{
	public static CombatManager instance;

	public bool canReceiveInput;

	public bool inputReceived;

	public PlayerController player;

	private void Awake()
	{
		player = GetComponent<PlayerController>();
		canReceiveInput = true;
		instance = this;
	}

	private void Update()
	{
		Attack();
		AttackAir();
	}

	public void Attack()
	{
		if (Input.GetButtonDown("Fire1") && player.isGrounded && !player.stunned)
		{		
			if (canReceiveInput)
			{			
				inputReceived = true;
				canReceiveInput = false;
			}
		}
	}

	public void AttackAir()
	{
		if (Input.GetButtonDown("Fire1") && !player.stunned && canReceiveInput)
		{			
			inputReceived = true;
			canReceiveInput = false;
		}
	}

	public void InputManager()
	{
		if (!canReceiveInput)
		{
			canReceiveInput = true;
		}
		else
		{
			canReceiveInput = false;
		}
	}
}
