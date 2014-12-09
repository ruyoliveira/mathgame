using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelButtonData : MonoBehaviour {
	public Image[] stars;
	public int levelNumber;
	public Text buttonText; 
	// Use this for initialization
	void Start () {
		buttonText.text = levelNumber.ToString();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
