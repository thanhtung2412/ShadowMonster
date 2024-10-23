using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	private Material whiteMat;

	private Material defaultMat;

	private SpriteRenderer sr;

	public int maximumHealth;

	public int currentHealth;

	public int numOfOrbs;

	public Image[] orbs;

	private void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		whiteMat = Resources.Load("WhiteFlash", typeof(Material)) as Material;
		defaultMat = sr.material;
		maximumHealth = 5;
		currentHealth = maximumHealth;
	}

	private void Update()
	{
		if (currentHealth > maximumHealth)
		{
			currentHealth = maximumHealth;
		}
		for (int i = 0; i < orbs.Length; i++)
		{
			if (i < currentHealth)
			{
				if (!orbs[i].enabled)
				{
					orbs[i].GetComponent<Animator>().ResetTrigger("die");
					orbs[i].GetComponent<Animator>().SetTrigger("enable");
				}
				orbs[i].enabled = true;
			}
			else
			{
				orbs[i].GetComponent<Animator>().SetTrigger("die");
			}
		}
	}

	public void GetDamaged(int amount)
	{
		CombatManager.instance.player.comboCount = 0;
		AudioManager.instance.Play("gethit");
		currentHealth -= amount;
		sr.material = whiteMat;
		if (currentHealth <= 0)
		{
			CombatManager.instance.player.canMove = false;
			CombatManager.instance.player.canJump = false;
			CombatManager.instance.player.isAlive = false;
			FindObjectOfType<Camera>().GetComponent<Animator>().SetTrigger("death");
			Debug.Log("You Died!");
			CombatManager.instance.player.anim.SetTrigger("die");
			CombatManager.instance.player.GetComponent<Rigidbody2D>().isKinematic = true;
			AudioManager.instance.Play2("death");
		}
		else
		{
			Invoke("ResetMaterial", 0.1f);
		}
	}

	public void Heal(int amount)
	{
		currentHealth += amount;
		if (currentHealth > maximumHealth)
		{
			currentHealth = maximumHealth;
		}
	}

	public void ResetMaterial()
	{
		sr.material = defaultMat;
	}

	public void Died()
	{
		GameManager.instance.OnDeath();
	}
}
