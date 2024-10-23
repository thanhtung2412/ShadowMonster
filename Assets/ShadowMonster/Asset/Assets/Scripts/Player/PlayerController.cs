using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public GameObject jumpDust;

	public GameObject stun;

	public Transform stunLoc;

	public Transform attackPoint;

	public float atkRange;

	public LayerMask enemyLayer;

	public LayerMask buttonLayer;

	public int comboCount;

	public Animator anim;

	public float moveSpeed = 5f;

	private float movementx;

	private float jumpForce = 35f;

	private int atkDamage;

	public bool stunned;

	public float timeStunned;

	private Rigidbody2D rb;

	private bool facingRight = true;

	public bool jumping;

	private int extraJumps;

	public bool isGrounded;

	public Transform groundCheck;

	public float checkRadius;

	public LayerMask whatIsGround;

	public bool canMove;

	public bool canJump;

	public bool jumpTrigger;

	public bool isAlive = true;

	private void Start()
	{
		isAlive = true;
		jumping = false;
		stunned = false;
		rb = base.gameObject.GetComponent<Rigidbody2D>();
		anim = base.gameObject.GetComponent<Animator>();
		canMove = true;
		canJump = true;
		jumpTrigger = false;
		atkDamage = 20;
	}

	private void Update()
	{
		if (stunned)
		{
			timeStunned -= Time.deltaTime;
			timeStunned = Mathf.Clamp(timeStunned, 0f, 2f);
			if (timeStunned == 0f)
			{
				DestroyStun();
			}
			canMove = false;
		}
		movementx = Input.GetAxisRaw("Horizontal");
		if (canMove)
		{
			if (movementx != 0f && isGrounded && canMove)
			{
				anim.SetBool("run", value: true);
			}
			else
			{
				anim.SetBool("run", value: false);
			}
			if (isGrounded && jumping)
			{
				AudioManager.instance.Play("land");
				jumping = false;
			}
			if ((movementx < 0f && facingRight) || (movementx > 0f && !facingRight))
			{
				Flip();
			}
			if (Input.GetButtonDown("Jump") && extraJumps > 0 && !isGrounded && canJump && !stunned)
			{
				
				anim.SetBool("jump", value: true);
				
				rb.velocity = Vector2.up * jumpForce;
				extraJumps--;
			}
			else if (Input.GetButtonDown("Jump") && isGrounded && canJump && !stunned)
			{
			
				Instantiate(jumpDust, new Vector2(groundCheck.position.x, groundCheck.position.y), Quaternion.identity);
				anim.SetBool("jump", value: true);
				rb.velocity = Vector2.up * jumpForce;
			}
			if (CombatManager.instance.inputReceived)
			{
				anim.SetBool("run", value: false);
			}
		}
	}

	private void FixedUpdate()
	{
		if (isGrounded)
		{
			anim.SetBool("jump", value: false);
			extraJumps = 2;
		}
		if (!canMove)
		{
			rb.velocity = new Vector2(0f, rb.velocity.y);
			if (Input.GetButton("Jump") && extraJumps > 0)
			{
				jumpTrigger = true;
			}
			if (jumpTrigger && canJump && !stunned)
			{
				anim.SetBool("jump", value: true);
				//anim.SetInteger("JumpState", 0);
				rb.velocity = Vector2.up * jumpForce;
				extraJumps--;
				jumpTrigger = false;
				if (isGrounded)
				{
					Instantiate(jumpDust, new Vector2(groundCheck.position.x, groundCheck.position.y), Quaternion.identity);
				}
			}
		}
		else
		{
			rb.velocity = new Vector2(movementx * moveSpeed, rb.velocity.y);
		}
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
		
		if (rb.velocity.y > 1f)
		{
			anim.SetInteger("JumpState", 1);
		}
		else if (rb.velocity.y < -40f)
		{
			anim.SetInteger("JumpState", 2);
		}
		if (!isGrounded)
		{
			anim.SetBool("jump", value: true);
		}
	}

	private void Flip()
	{
		facingRight = !facingRight;
		base.transform.Rotate(0f, 180f, 0f);
	}

	public void StopMovement()
	{
		canMove = false;
	}

	public void StartMovement()
	{
		canMove = true;
	}

	public void StopJumping()
	{
		canJump = !canJump;
	}

	public void MoveOnAttack()
	{
		if (facingRight)
		{
			rb.AddForce(Vector2.right * 2000f, ForceMode2D.Force);
		}
		else
		{
			rb.AddForce(Vector2.left * 2000f, ForceMode2D.Force);
		}
	}

	public void MoveOnAttackFar()
	{
		if (facingRight)
		{
			rb.AddForce(Vector2.right * 4000f, ForceMode2D.Force);
		}
		else
		{
			rb.AddForce(Vector2.left * 4000f, ForceMode2D.Force);
		}
	}

	public void LiftOnAttack()
	{
		rb.velocity = Vector2.up * 13f;
	}

	public void GetExtraJump(int amount)
	{
		extraJumps += amount;
	}

	public void DoDamage()
	{
		Collider2D[] array = Physics2D.OverlapCircleAll(attackPoint.position, atkRange, enemyLayer);
		Collider2D[] array2 = array;
		foreach (Collider2D collider2D in array2)
		{
			comboCount++;			
			collider2D.GetComponent<Enemy>().ReceiveDamage(atkDamage);
			if (!facingRight)
			{
				collider2D.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 2000f, ForceMode2D.Force);
			}
			else
			{
				collider2D.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 2000f, ForceMode2D.Force);
			}
		}
		if (array.Length != 0)
		{
			AudioManager.instance.Play("slashhit");
		}
	
		
	}

	public void Stun(float stunTime)
	{
		timeStunned = stunTime;
		if (timeStunned > 0f && !stunned)
		{
			anim.SetBool("run", value: false);
			stunned = true;
			Instantiate(stun, new Vector2(stunLoc.position.x, stunLoc.position.y), Quaternion.identity).transform.SetParent(stunLoc);
		}
	}

	private void DestroyStun()
	{
		if (stunned)
		{
			GameObject obj = base.transform.GetChild(2).gameObject;
			stunned = false;			
			Destroy(obj.transform.GetChild(0).gameObject);
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (!(attackPoint == null))
		{
			Gizmos.DrawWireSphere(attackPoint.position, atkRange);
		}
	}

	public void SlashSound()
	{
		if (Random.Range(0, 2) == 0)
		{
			AudioManager.instance.Play("slash1");
		}
		else
		{
			AudioManager.instance.Play("slash2");
		}
	}

	public void StepSound()
	{
		int num = Random.Range(1, 4);
		AudioManager.instance.Play("step" + num);
	}

	public void YouDied()
	{
		SceneLoader.instance.BackToMenu();
	}
}
