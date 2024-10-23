using UnityEngine;

public class Lazer : MonoBehaviour
{
	public LineRenderer lr;

	public Transform target;

	public bool targetAcquired;

	public Vector3 lockedTarget;

	private void Awake()
	{
		lr = GetComponent<LineRenderer>();
		lr.positionCount = 2;
	}

	private void Update()
	{
		if (!targetAcquired)
		{
			lr.SetPosition(1, target.position);
		}
		else
		{
			lr.SetPosition(1, lockedTarget);
		}
	}

	public void AcquireTarget()
	{
		targetAcquired = true;
		lockedTarget = target.position;
	}

	public void AssignTarget(Vector3 startPos, Transform newTarget)
	{
		lr.SetPosition(0, startPos);
		target = newTarget;
	}
}
