using UnityEngine;
using System.Collections;

public class TipsWindow : MonoBehaviour {
	public UILabel lbText;

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

	public void ShowWindow()
	{

	}
	public void CloseWindow()
	{

	}
}
