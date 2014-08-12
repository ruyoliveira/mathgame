using UnityEngine;
using System.Collections;

public class UiRootControl : MonoBehaviour {
	public UIButton pause;
	public UIButton gameMenu;
	public UIButton restartLevel;
	public UIButton resume;
	//public Shader shader;
	// Use this for initialization
	void Start () {
		//Camera.main.SetReplacementShader (shader, null);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClick_gameMenu()
	{
		print ("Clicou - GameMenu");
	}
	public void OnClick_restartLevel()
	{
		print ("Clicou - Restart");
		Time.timeScale = 1;
	}
	public void OnClick_resume()
	{
		print ("Clicou - Resume");
		pause.gameObject.GetComponent<UIPlayTween> ().Play (false);
		pause.isEnabled = true;
		Time.timeScale = 1;
	}
	public void OnClick_Pause()
	{
		print ("Clicou - Pause");
		pause.isEnabled = false;
		Time.timeScale = 0;
	}
	public void OnClick_NextLevel()
	{
		print ("Clicou - NextLevel");
	}
}
