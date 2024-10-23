using UnityEngine;

public class Arrow : MonoBehaviour
{
	public int atkDamageArrow;

	private Vector2 target;

	private Transform player;

	private LineRenderer lr;

	public Collider2D trigger;

	public bool shooting;

	public bool gotTarget;

	public GameObject explosion;

	private Rigidbody2D rb;

	private void Start()
	{
		lr = GetComponent<LineRenderer>();
		trigger = GetComponent<Collider2D>();
		base.gameObject.SetActive(value: false);
		shooting = false;
		gotTarget = false;
		rb = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		Invoke("SetActive", 0.1f);
	}

	private void Update()
	{
		if (base.transform.parent == null && gotTarget && !shooting)
		{
			shooting = true;
			Invoke("Shoot", 1f);
		}
	}

	private void Shoot()
	{
		Vector2 vector = target - new Vector2(base.transform.position.x, base.transform.position.y);
		vector.Normalize();
		rb.AddForce(vector * 2000f, ForceMode2D.Force);
		trigger.enabled = !trigger.enabled;
		lr.enabled = !lr.enabled;
	}

	private void SetActive()
	{
		base.gameObject.SetActive(value: true);
	}

	public void GetTarget()
	{
		gotTarget = true;
		target = new Vector2(player.position.x, player.position.y);
		Vector2 vector = target - new Vector2(base.transform.position.x, base.transform.position.y);
		vector.Normalize();
		lr.SetPosition(0, base.transform.position);
		lr.SetPosition(1, vector * 1000f);
	}

	private void OnDrawGizmos()
	{
		_ = target;
		Gizmos.DrawLine(base.transform.position, target);
	}

	private void OnCollisionEnter2D(Collision2D col)
	{		
		if (col.gameObject.CompareTag("Player") && CombatManager.instance.player.isAlive)
		{
			col.gameObject.GetComponent<PlayerHealth>().GetDamaged(atkDamageArrow);
		}
		Destroy(base.gameObject);
		Instantiate(explosion, new Vector2(base.transform.position.x, base.transform.position.y - 0.3f), Quaternion.identity);
	}
}
