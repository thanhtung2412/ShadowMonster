using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public new string name;

	public int atkDamage;

	public int maxHealth;

	public int currentHealth;

	public float moveSpeed;

	public GameObject deathAnim;

	public GameObject stun;

	public Transform stunLoc;

	public Transform deathLoc;

	public float atkRange;

	public float atkRate;

	public float atkStunTime;

	public int healAmount;

	public int extraJumps;

	public string hitSound;

	public bool facingLeft;

	public bool retreating;

	public bool isMoving;

	public bool canMove;

	public bool isAttacking;

	public Transform player;

	public Transform spawner;

	public Rigidbody2D enemyRb;

	public Animator anim;

	private Material whiteMat;

	private Material defaultMat;

	private SpriteRenderer sr;

	public void Awake()
	{
		spawner = GameObject.FindGameObjectWithTag("spawner").transform;	
		healAmount = 1;
		player = GameObject.FindGameObjectWithTag("Player").transform;
		enemyRb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		sr = GetComponent<SpriteRenderer>();
		whiteMat = Resources.Load("WhiteFlash", typeof(Material)) as Material;
		defaultMat = sr.material;
	}

	public void OnEnable()
	{
		currentHealth = maxHealth;
		canMove = true;
	}

	public void Start()
	{
		currentHealth = maxHealth;
	}

	public void ReceiveDamage(int damage)
	{
		AudioManager.instance.Play("slashhit");
		currentHealth -= damage;
		sr.material = whiteMat;
		if (currentHealth <= 0)
		{
			Die();
		}
		else
		{
			Invoke("ResetMaterial", 0.1f);
		}
	}

	public void ResetMaterial()
	{
		sr.material = defaultMat;
	}

	public IEnumerator ChangeMaterial()
	{
		sr.material = whiteMat;
		yield return new WaitForSeconds(0.2f);
		sr.material = defaultMat;
		yield return new WaitForSeconds(0.2f);
		sr.material = whiteMat;
		yield return new WaitForSeconds(0.1f);
		sr.material = defaultMat;
		yield return new WaitForSeconds(0.1f);
		sr.material = whiteMat;
		yield return new WaitForSeconds(0.1f);
		sr.material = defaultMat;
		yield return new WaitForSeconds(0.1f);
		sr.material = whiteMat;
		yield return new WaitForSeconds(0.1f);
		sr.material = defaultMat;
	}

	public void Move()
	{
		if (CombatManager.instance.player.isAlive && !retreating && canMove)
		{
			Vector3 vector = player.position - base.transform.position;
			vector.y = 0f;
			if (vector.magnitude > atkRange)
			{
				isMoving = true;
				Vector2 position = Vector2.MoveTowards(target: new Vector2(player.position.x, enemyRb.position.y), current: enemyRb.position, maxDistanceDelta: moveSpeed * Time.fixedDeltaTime);
				enemyRb.MovePosition(position);
			}
			else
			{
				isMoving = false;
			}
		}
		if (player.position.x > enemyRb.position.x && facingLeft && canMove)
		{
			Flip();
		}
		else if (player.position.x < enemyRb.position.x && !facingLeft && canMove)
		{
			Flip();
		}
	}

	public void Retreat(int retreatDistance, float retreatSpeed)
	{
		Vector3 vector = player.position - base.transform.position;
		vector.y = 0f;
		if (vector.magnitude < (float)retreatDistance)
		{
			retreating = true;
			if (player.position.x < enemyRb.position.x)
			{
				enemyRb.velocity = new Vector2(1f * retreatSpeed, enemyRb.velocity.y);
				if (facingLeft)
				{
					Flip();
				}
			}
			else
			{
				enemyRb.velocity = new Vector2(-1f * retreatSpeed, enemyRb.velocity.y);
				if (!facingLeft)
				{
					Flip();
				}
			}
		}
		else
		{
			retreating = false;
		}
	}

	public void Attack()
	{
		if (CombatManager.instance.player.isAlive && canMove && !isAttacking && (player.position - base.transform.position).magnitude <= atkRange)
		{
			StartCoroutine(StartAttack(atkRate));
		}
	}

	public IEnumerator StartAttack(float atkRate)
	{
		isMoving = false;
		isAttacking = true;
		BlockMove();
		anim.SetTrigger("attack");
		yield return new WaitForSeconds(atkRate);
		StartMove();
		DestroyStun();
		yield return new WaitForSeconds(0.2f);
		isAttacking = false;
	}

	public void Flip()
	{
		facingLeft = !facingLeft;
		base.transform.Rotate(0f, 180f, 0f);
	}

	public void Die()
	{
		Instantiate(deathAnim, new Vector2(deathLoc.position.x, deathLoc.position.y), Quaternion.identity);
		Destroy(base.gameObject);
		player.GetComponent<PlayerHealth>().Heal(healAmount);
		player.GetComponent<PlayerController>().GetExtraJump(extraJumps);
		spawner.GetComponent<WaveSpawner>().RemoveFromList(base.gameObject);
	}

	public void Stun()
	{
		Instantiate(stun, new Vector2(stunLoc.position.x, stunLoc.position.y), Quaternion.identity).transform.SetParent(stunLoc);
	}

	public void DestroyStun()
	{
		if (stunLoc != null)
		{			
			Destroy(stunLoc.transform.GetChild(0).gameObject);
		}
	}

	public void BlockMove()
	{
		canMove = false;
	}

	public void StartMove()
	{
		canMove = true;
	}
}
