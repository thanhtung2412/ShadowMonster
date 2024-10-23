using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	private CinemachineVirtualCamera cinemachineVirtualCamera;

	private float shakeTimer;

	private float startingIntensity;

	private float shakeTimerTotal;

	public static CameraShake Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
		cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
		Invoke("Follow", 1f);
	}

	public void ShakeCamera(float intensity, float time)
	{
		cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
		startingIntensity = intensity;
		shakeTimerTotal = time;
		shakeTimer = time;
	}

	private void Update()
	{
		if (shakeTimer > 0f)
		{
			shakeTimer -= Time.deltaTime;
			cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1f - shakeTimer / shakeTimerTotal);
		}
	}

	private void Follow()
	{
		cinemachineVirtualCamera.Follow = GameObject.FindGameObjectWithTag("Player").transform;
	}
}
