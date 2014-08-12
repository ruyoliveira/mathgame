using UnityEngine;
using System.Collections;

public class TipsWindow : MonoBehaviour {
	public UILabel lbText;
	public GameObject painelWindow;
	// Use this for initialization
	public string tipsText
	{
		get{
			return lbText.text;
		}
		set{
			lbText.text = value;
		}
	}

	void Start()
	{
		painelWindow.SetActive (false);
	}

	public void ShowWindow()
	{
		painelWindow.SetActive (true);
	}
	public void CloseWindow()
	{
		painelWindow.SetActive (false);
	}
}
