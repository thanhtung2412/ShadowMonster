using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
	public static SceneLoader instance;

	public Text highScore;

	public int score;

	public Button start;

	private AsyncOperation asyncLoad;

	private void Awake()
	{
		instance = this;
	}

	public void Start()
	{
		Scene activeScene = SceneManager.GetActiveScene();
		score = PlayerPrefs.GetInt("HighScore");
		asyncLoad = SceneManager.LoadSceneAsync(1);
		asyncLoad.allowSceneActivation = false;
		if (activeScene.buildIndex == 0)
		{
			highScore.text = score.ToString();
		}
	}

	public void OnPressedStart()
	{
		Invoke("StartGame", 1.2f);
	}

	public void Dissolve()
	{
		start.GetComponent<Button>().interactable = false;
	}

	public void StartGame()
	{
		asyncLoad.allowSceneActivation = true;
	}

	public void BackToMenu()
	{
		SceneManager.LoadScene(0);
		Time.timeScale = 1f;
	}

	public int ReturnSceneIndex()
	{
		if (SceneManager.GetActiveScene().buildIndex == 0)
		{
			return 0;
		}
		return 1;
	}
}
