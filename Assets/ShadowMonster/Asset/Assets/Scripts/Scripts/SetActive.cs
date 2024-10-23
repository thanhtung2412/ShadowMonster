using UnityEngine;
using UnityEngine.UI;

public class SetActive : MonoBehaviour
{
	public void DisableThis()
	{
		GetComponent<Image>().enabled = false;
		GetComponent<Animator>().ResetTrigger("die");
	}
}
