using UnityEngine;
using System.Collections;

public class UiRootControl : MonoBehaviour {
	public UIButton pause;
	public UIButton gameMenu;
	public UIButton restartLevel;
	public UIButton resume;
	public TweenScale confirmacaoMenuTween;

	private bool isPaused;
	private Animator animator;
	private CounterGameMode gameManager;
	//public Shader shader;
	// Use this for initialization
	void Start () {
		isPaused = false;
		animator = GetComponent<Animator> ();
		gameManager = GameObject.Find ("GameManager").GetComponent<CounterGameMode> ();
		//Camera.main.SetReplacementShader (shader, null);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClick_gameMenu()
	{
		print ("Clicou - GameMenu");
		animator.SetBool ("ConfirmacaoMenu", true);

	}
	public void OnClick_restartLevel()
	{
		print ("Clicou - Restart");
		Time.timeScale = 1;
		Application.LoadLevel (Application.loadedLevel);
	}
	public void OnClick_Pause()
	{
		isPaused = !isPaused;
		animator.SetBool("pause", isPaused);
		if(isPaused)
		{
			Time.timeScale = 0;
			print ("Clicou - Pause");
		}
		else
		{
			print ("Clicou - Resume");
			Time.timeScale = 1;
		}

	}
	public void OnClick_NextLevel()
	{
		print ("Clicou - NextLevel");
		int currentLevel = PlayerPrefs.GetInt ("CurrentLevel", 1);
		if(currentLevel!=9)
		{
			PlayerPrefs.SetInt("CurrentLevel", currentLevel + 1);
			Application.LoadLevel(Application.loadedLevel);
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
		animator.SetBool ("ConfirmacaoMenu", false);
	}
	public void OnClick_Confirm()
	{
		print ("Confirm");
		gameManager.Finished ();
	}

}
