using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
	public enum SpawnState
	{
		Spawning = 0,
		Waiting = 1,
		Counting = 2
	}
	[Serializable]
	public class Wave
	{
		public string name;
		public int count;
		public float rate;
	}
	public int score;
	private int highScore;
	public Text scoreText;
	public Text waveCompleted;
	public GameObject[] landEnemies;
	public GameObject[] airEnemies;
	public Transform[] spawnPoints;
	public Transform[] airSpawnPoints;
	public List<GameObject> enemies = new List<GameObject>();
	public Wave[] waves;
	private int nextWave;
	public float timeBetweenWaves = 3f;
	public float waveCountdown;
	public float searchCountdown;
	private SpawnState state = SpawnState.Counting;

	private void Start()
	{
		highScore = PlayerPrefs.GetInt("HighScore");
		waveCompleted.enabled = false;
		waveCountdown = timeBetweenWaves;
	}

	private void Update()
	{
		if (state == SpawnState.Waiting)
		{
			if (EnemyIsAlive())
			{
				return;
			}
			WaveCompleted();
		}
		if (waveCountdown <= 0f)
		{			
            if (state != 0)
			{			
				waveCompleted.enabled = false;
				StartCoroutine(SpawnWave(waves[nextWave]));
			}
		}
		else
		{
			waveCountdown -= Time.deltaTime;	
		}
	}

	private bool EnemyIsAlive()
	{
		searchCountdown -= Time.deltaTime;
		if (searchCountdown <= 0f)
		{
			searchCountdown = 2f;
			if (enemies.Count == 0)
			{
				return false;
			}
		}
		return true;
	}

	private void WaveCompleted()
	{
		waveCompleted.text = "Waves Completed: " + waves[nextWave].name;
		waveCompleted.enabled = true;
		state = SpawnState.Counting;
		waveCountdown = timeBetweenWaves;
		if (nextWave + 1 > waves.Length - 1)
		{
			waveCompleted.text = "You have completed all the waves";
			waveCompleted.enabled = true;
			Debug.Log("Endless Run");
			StartCoroutine(EndlessRun());
		}
		else
		{
			nextWave++;
		}
	}

	private IEnumerator SpawnWave(Wave wave)
	{
		Debug.Log("GET READY!");
		state = SpawnState.Spawning;
		waveCompleted.enabled = true;
		waveCompleted.text = "Monsters are coming!";
		AudioManager.instance.Play("gong");
		yield return new WaitForSeconds(2f);
		waveCompleted.enabled = false;
		for (int i = 0; i < wave.count; i++)
		{
			if (UnityEngine.Random.Range(0, 2) == 0)
			{
				SpawnLandEnemy(landEnemies[UnityEngine.Random.Range(0, 2)]);
			}
			else
			{
				SpawnAirEnemy(airEnemies[UnityEngine.Random.Range(0, 2)]);
			}
			yield return new WaitForSeconds(1f / wave.rate);
		}
		state = SpawnState.Waiting;
	}

	private IEnumerator EndlessRun()
	{
		state = SpawnState.Spawning;
		yield return new WaitForSeconds(2f);
		waveCompleted.enabled = true;
		waveCompleted.text = "COUNTLESS HORDES OF ENEMIES ARE APROACHING!";
		AudioManager.instance.Play("gong");
		yield return new WaitForSeconds(2f);
		waveCompleted.enabled = false;
		for (int i = 0; i < 100000; i++)
		{
			if (UnityEngine.Random.Range(0, 2) == 0)
			{
				SpawnLandEnemy(landEnemies[UnityEngine.Random.Range(0, 2)]);
			}
			else
			{
				SpawnAirEnemy(airEnemies[UnityEngine.Random.Range(0, 2)]);
			}
			yield return new WaitForSeconds(0.5f);
		}
		state = SpawnState.Waiting;
	}

	private void SpawnLandEnemy(GameObject enemy)
	{
		Transform transform = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
		Debug.Log("Spawning:" + enemy.name);
		GameObject item = UnityEngine.Object.Instantiate(enemy, transform.position, Quaternion.identity);
		enemies.Add(item);
	}

	private void SpawnAirEnemy(GameObject enemy)
	{
		Transform transform = airSpawnPoints[UnityEngine.Random.Range(0, airSpawnPoints.Length)];
		Debug.Log("Spawning:" + enemy.name);
		GameObject item = Instantiate(enemy, transform.position, Quaternion.identity);
		enemies.Add(item);
	}

	private IEnumerator StopTime()
	{
		Camera camera = UnityEngine.Object.FindObjectOfType<Camera>();
		Animator camani = camera.GetComponent<Animator>();
		camani.SetTrigger("slow");
		AudioManager.instance.Play("slowmo");
		Time.timeScale = 0.3f;
		AudioManager.instance.SlowAudio();
		yield return new WaitForSecondsRealtime(0.5f);
		AudioManager.instance.Play("slowmofix");
		Time.timeScale = 0.4f;
		AudioManager.instance.SlowAudio();
		yield return new WaitForSecondsRealtime(0.3f);
		Time.timeScale = 0.5f;
		AudioManager.instance.SlowAudio();
		yield return new WaitForSecondsRealtime(0.2f);
		Time.timeScale = 0.6f;
		AudioManager.instance.SlowAudio();
		yield return new WaitForSecondsRealtime(0.2f);
		Time.timeScale = 0.7f;
		AudioManager.instance.SlowAudio();
		camani.SetTrigger("slowmoend");
		yield return new WaitForSecondsRealtime(0.1f);
		Time.timeScale = 0.8f;
		AudioManager.instance.SlowAudio();
		yield return new WaitForSecondsRealtime(0.1f);
		Time.timeScale = 0.9f;
		AudioManager.instance.SlowAudio();
		yield return new WaitForSecondsRealtime(0.1f);
		Time.timeScale = 1f;
		AudioManager.instance.SlowAudio();
	}

	public void RemoveFromList(GameObject enemy)
	{
		Camera.main.transform.Rotate(0f, 0f, 0f, Space.World);
		if (enemies.Count != 0)
		{ 		
			enemies.Remove(enemy);
			score++;
			scoreText.text = score.ToString();
			if (score > highScore)
			{
				PlayerPrefs.SetInt("HighScore", score);
			}
			if (enemies.Count == 0)
			{
				Debug.Log("stop time");
				StartCoroutine(StopTime());
			}
		}
	}
}
