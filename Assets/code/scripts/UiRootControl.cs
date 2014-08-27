using UnityEngine;
using System.Collections;

public class UiRootControl : MonoBehaviour {
	public UIButton pause;
	public UIButton gameMenu;
	public UIButton restartLevel;
	public UIButton resume;

	public TweenScale confirmacaoMenuTween;
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
		gameMenu.isEnabled = false;
		restartLevel.isEnabled = false;
		resume.isEnabled = false;
		confirmacaoMenuTween.PlayForward ();

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
		if(!Application.loadedLevelName.Contains ("s9"))
		{
			Application.LoadLevel(Application.loadedLevel + 1);
		}
		else
			Application.LoadLevel("gameMenu");
	}

	public void PopupMenuSim()
	{
		Application.LoadLevel ("gameMenu");
	}
	public void PopupMenuNao()
	{
		gameMenu.isEnabled = true;
		restartLevel.isEnabled = true;
		resume.isEnabled = true;
		confirmacaoMenuTween.PlayReverse ();
	}

}
