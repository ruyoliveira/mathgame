using UnityEngine;
using System.Collections;

public class W2Manager : MonoBehaviour {
	public int correctAmount;
	public float time;
	TimeControl timeControl;
	// Use this for initialization
	void Start () {
		timeControl = GetComponent<TimeControl> ();
		timeControl.ClearTimer (time);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void Finish()
	{

	}
}
