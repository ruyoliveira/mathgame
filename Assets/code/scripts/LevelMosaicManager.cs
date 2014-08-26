using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelMosaicManager : MonoBehaviour {
	public Button[] levelButtons;
	public int worldNumber;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void LoadLevel(int levelNumber)
	{
		Application.LoadLevelAsync("w"+worldNumber+"s"+levelNumber);
	}
}
