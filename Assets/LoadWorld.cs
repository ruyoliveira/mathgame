using UnityEngine;
using System.Collections;

public class LoadWorld : MonoBehaviour {
	public string sceneName;
	public void LoadSelectedWorld(int selectedWorld)
	{
			Debug.Log (selectedWorld);
			PlayerPrefs.SetInt("CurrentWorld",selectedWorld);
			Application.LoadLevel(sceneName);
	}
	public void QuitGame()
	{
		Application.Quit();
	}
}
