using UnityEngine;
using System.Collections;

public class CounterGameMode : MonoBehaviour {
	public Recipient recipient;
	public int correctAmount;
	public bool readyToAnswer;
	public GameObject resultWindow;
	public UILabel resultText;


	public GameObject curiosityPopup;

	public GameObject pavio;
	private float tamPavio;
	public GameObject bomb;
	public Transform foguinho;
	// Use this for initialization
	void Start () {
		recipient = GameObject.FindWithTag("Recipiente").GetComponent<Recipient>();
		GetComponent<TimeControl> ().ClearTimer (5f);
		GetComponent<TimeControl> ().Resume ();

		tamPavio = pavio.transform.localScale.x;
	}
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonUp(0))
			CloseCuriosityPopup();

		UpdatePavio ();
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
			if(!GetComponent<TimeControl>().IsCounting())
				bomb.GetComponent<Animator> ().SetTrigger ("TimesOver");
			//ShowCuriosityPopup();
		}
		GetComponent<TimeControl>().Pause();
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

	
	void UpdatePavio()
	{
		float xScale = (1f - GetComponent<TimeControl> ().GetProgressTime ()) * tamPavio;
		pavio.transform.localScale = new Vector3 (xScale, pavio.transform.localScale.y,pavio.transform.localScale.z);

		//foguinho
		float x = pavio.GetComponent<SpriteRenderer>().bounds.size.x + pavio.transform.localPosition.x;
		foguinho.localPosition = new Vector3(x,foguinho.localPosition.y, foguinho.localPosition.z);
	}

}
