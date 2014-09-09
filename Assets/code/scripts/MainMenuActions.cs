using UnityEngine;
using System.Collections;

public class MainMenuActions : MonoBehaviour {
	public static AudioSource themeMusic;	//musica tema
	// Use this for initialization
	void Start () {
		themeMusic = gameObject.GetComponent<AudioSource>();	//armazena a musica tema
		DontDestroyOnLoad(themeMusic);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void LoadScene(string sceneName)
	{
		if(sceneName != "Quit" && sceneName != "")
		{
			Application.LoadLevel(sceneName);
		}
		else
		{
			Application.Quit();
			Debug.Log (sceneName);
		}
	}
}
