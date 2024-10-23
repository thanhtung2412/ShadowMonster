using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
	private Enemy enemy;
	public GameObject arrow;

	public Transform target;

	public bool shooting;

	public bool spawning;

	private float startTimeBtwShots = 3f;

	private float timeBtwShots;

	private void Start()
	{
		enemy = transform.parent.GetComponent<Enemy>();		
		target = GameObject.FindGameObjectWithTag("body").transform;
		spawning = false;
		shooting = false;
		Instantiate(arrow, new Vector2(base.transform.position.x, base.transform.position.y - 1.8f), Quaternion.identity).transform.SetParent(base.gameObject.transform);
		timeBtwShots = startTimeBtwShots;
	}

	private void FixedUpdate()
	{
		if (CombatManager.instance.player.isAlive)
		{
			Rotate();
			if (timeBtwShots <= 0f)
			{
				AudioManager.instance.Play2("laser");
				base.transform.GetChild(0).GetComponent<Arrow>().GetTarget();
				base.transform.GetChild(0).SetParent(null);
				timeBtwShots = startTimeBtwShots;
			}
			else
			{
				timeBtwShots -= Time.deltaTime;
			}
			if (base.transform.childCount == 0 && !spawning)
			{
				spawning = true;
				Invoke("SpawnArrow", 2f);
			}
		}
	}

	private void SpawnArrow()
	{
		base.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		GameObject newArrow = Instantiate(arrow, new Vector2(base.transform.position.x, base.transform.position.y - 1.8f), Quaternion.identity);
		newArrow.transform.SetParent(gameObject.transform);		
		spawning = false;
	}

	private void Rotate()
	{
		Vector3 vector = target.position - base.transform.position;
		float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		base.transform.rotation = Quaternion.AngleAxis(num + 90f, Vector3.forward * Time.deltaTime * 2f);
	}
}
