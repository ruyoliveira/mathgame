﻿using UnityEngine;
using System.Collections;

public class GameMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	

	public void OnNewGameClick()
	{
		Application.LoadLevel ("WorldSelect");
	}

	public void OnContinueClick()
	{

	}
	public void OnConfigurationClick()
	{

	}
	public void OnExitClick()
	{
		Application.Quit ();
	}
}