using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public GameObject deathScreen;

	public GameObject pauseMenu;

	public GameObject exitMenu;

	public bool paused;

	public Text score;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{		
		if (SceneLoader.instance.ReturnSceneIndex() == 1)
		{
			Cursor.visible = false;			
			deathScreen.SetActive(value: false);
			pauseMenu.SetActive(value: false);
		}
		else
		{
			Cursor.visible = true;
			exitMenu.SetActive(value: false);
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!paused)
			{
				Escape();
			}
			else
			{
				Continue();
			}
		}
	}

	public void OnDeath()
	{
		Time.timeScale = 0f;
		Cursor.visible = true;
		deathScreen.SetActive(value: true);
		WaveSpawner waveSpawner = Object.FindObjectOfType<WaveSpawner>();
		score.text = waveSpawner.GetComponent<WaveSpawner>().score.ToString();
	}

	public void Escape()
	{
		if (SceneLoader.instance.ReturnSceneIndex() == 0)
		{
			OpenExitMenu();
		}
		else
		{
			OpenPauseMenu();
		}
	}

	private void OpenPauseMenu()
	{
		Cursor.visible = true;
		CombatManager.instance.player.GetComponent<PlayerController>().enabled = false;
		CombatManager.instance.GetComponent<CombatManager>().enabled = false;
		paused = true;
		Time.timeScale = 0f;
		pauseMenu.SetActive(value: true);
	}

	public void Continue()
	{
		Cursor.visible = false;
		CombatManager.instance.GetComponent<CombatManager>().enabled = true;
		CombatManager.instance.player.GetComponent<PlayerController>().enabled = true;
		paused = false;
		Time.timeScale = 1f;
		pauseMenu.SetActive(value: false);
	}

	public void OpenExitMenu()
	{
		Cursor.visible = true;
		exitMenu.SetActive(value: true);
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void CloseExitMenu()
	{
		Cursor.visible = false;
		exitMenu.SetActive(value: false);
	}
}
