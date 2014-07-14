using UnityEngine;
using System.Collections;

public class CounterGameMode : MonoBehaviour {
	public Recipient recipient;
	public int correctAmount;
	public bool readyToAnswer;
	// Use this for initialization
	void Start () {
		recipient = GameObject.FindWithTag("Recipiente").GetComponent<Recipient>();

		//Configura o nivel
		correctAmount = 5;
	}
	// Update is called once per frame
	void Update () 
	{
		//Avalia a resposta
		if(readyToAnswer)
		{
			if(recipient.counter == correctAmount)
			{
				Debug.Log ("Acertou");
			}
			else
			{
				Debug.Log ("Errou");
			}
		}
	}
}
