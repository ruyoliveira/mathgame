using UnityEngine;
using System.Collections;

public class CounterGameMode : MonoBehaviour {
	public Recipient recipient;
	public int correctAmount;
	public bool readyToAnswer;
	public GameObject resultWindow;
	public tk2dTextMesh resultText;


	public GameObject curiosityPopup;
	// Use this for initialization
	void Start () {
		recipient = GameObject.FindWithTag("Recipiente").GetComponent<Recipient>();
		
	}
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonUp(0))
			CloseCuriosityPopup();
	}
	public void Finished()
	{
		//Avalia a resposta
		if(recipient.counter == correctAmount)
		{
			resultWindow.SetActive(true);
			resultText.text = "Acertou";
			Debug.Log ("Acertou");
		}
		else
		{
			Debug.Log ("Errou");
			resultWindow.SetActive(true);
			resultText.text = "Errou";
			//ShowCuriosityPopup();
		}
	}
	public void RestartLevel()
	{
		StartCoroutine("AsyncRestart");
	}
	IEnumerator AsyncRestart() {
		Application.LoadLevel(Application.loadedLevel);
		yield return new WaitForSeconds(5.0f);
	}
	public void ShowCuriosityPopup()
	{
		curiosityPopup.SetActive (true);
	}
	public void CloseCuriosityPopup()
	{
		curiosityPopup.SetActive (false);
	}
}
