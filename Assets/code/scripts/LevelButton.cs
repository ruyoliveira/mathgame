using UnityEngine;
using System.Collections;

public class LevelButton : MonoBehaviour {
	public UILabel label;
	public string mundo;
	public int level
	{
		get
		{
			return int.Parse(label.text);
		}
		set
		{
			label.text = value.ToString ("00");
		}
	}

	public void LoadScene()
	{
		//temporario
		if (level == 1) {
			Application.LoadLevel ("core");
			return;
		}
		//
		string LevelName = mundo + label.text; 
		if(Application.CanStreamedLevelBeLoaded(LevelName))
			Application.LoadLevel(LevelName);
		else
			print("Level " + LevelName + " dont exist!");
	}
}
