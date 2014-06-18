using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	//Temporario
	public tk2dTextMesh Text;
	//

	float totalTime;
	float timeOut;
	bool game;
	// Use this for initialization
	void Start () {
		totalTime = 0;
		timeOut = 15;//tempo inicial
		game = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(game){
			UpdateTime ();
			//temporario
				if(Input.GetKeyDown(KeyCode.Equals))//incrementa 3s se pressionar +
					IncreaseTimeOut(3f);
				else if(Input.GetKeyDown(KeyCode.Minus))//decrementa 3s se pressionar -
				        DecrementTimeOut(3f);
			//-------------------
		}
	}

	//atualiza o tempo limite e o tempo total
	private void UpdateTime(){
		totalTime += Time.deltaTime;
		timeOut -= Time.deltaTime;
		Text.text = "Tempo total: " + totalTime.ToString ("0.0") + " s\nTempo limite: " + timeOut.ToString ("0.0") + " s";//temporario
		if(timeOut <= 0){
			Text.text += "\n\n Fim de Jogo!";//temporario
			TimesUp();
		}
	}

	//
	//procedimento que incremena o tempo limite
	public void IncreaseTimeOut(float seconds){
		timeOut += seconds;
		print ("Tempo incrementado: " + seconds.ToString ());
	}

	//Procedimento que decrementa o tempo limite
	public void DecrementTimeOut(float seconds){
		timeOut -= seconds;
		print ("Tempo decrementado: " + seconds.ToString ());
	}
	//Funçao que retorna o tempo limite
	public float GetTimeOut(){
		return timeOut;
	}
	private void TimesUp()
	{
		timeOut = 0;
		game = false;
	}
}
