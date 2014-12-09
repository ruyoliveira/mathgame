using UnityEngine;
using System.Collections;

public class CounterGameMode : MonoBehaviour, IParseComunication {
	//Desafio
	public Recipient recipient;
	public int correctAmount;
	public bool readyToAnswer;
//	public UILabel resultText;
	public int challengeTimeLimit;	//tempo limite do desafio


	//Variaveis do Pavio
	public GameObject pavio;
	private float tamPavio;
	public GameObject bomb;
	public Transform foguinho;

	//Temporizador
	private TimeControl timeControl;
	//Janelas
	public JanelaResultado janelaResultado;
	public GameObject curiosityPopup;

	public GameObject pnSaving;

	void Start () {
		//Busca recipiente
		recipient = GameObject.FindWithTag("Recipiente").GetComponent<Recipient>();
		//Busca temporizador
		timeControl = GetComponent<TimeControl> ();
		//Busca Janela
	}
	public void Init()
	{
		//Inicializa o temporizador
		timeControl.ClearTimer (challengeTimeLimit);
		timeControl.Resume ();
		//Busca pavio
		tamPavio = pavio.transform.localScale.x;
	}

	// Update is called once per frame
	void Update () 
	{
		//Fecha janela de curiosidade
//		if(Input.GetMouseButtonUp(0))
//		{
//			CloseCuriosityPopup();
//		}

		//Atualiza pavio
		UpdatePavio ();
	}

	public void Finished()
	{
		//Avalia a resposta
		timeControl.Pause();//pausa o tempo

		if(recipient.counter == correctAmount)
		{
			CalculatePoints(timeControl.GetProgressTime(), "Acertou");//Calcula pontuação baseado na pct do tempo max utilizado

		}
		else
		{
			CalculatePoints(1000, "Errou");//Calcula pontuação baseado na pct do tempo max utilizado
			//Explode a bomba somente se a resposta for errada
			if(!timeControl.IsCounting())
				bomb.GetComponent<Animator> ().SetTrigger ("TimesOver");
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

	//Anima pavio
	void UpdatePavio()
	{
		float xScale = (1f - timeControl.GetProgressTime ()) * tamPavio;
		pavio.transform.localScale = new Vector3 (xScale, pavio.transform.localScale.y,pavio.transform.localScale.z);

		//foguinho
		float x = pavio.GetComponent<SpriteRenderer>().bounds.size.x + pavio.transform.localPosition.x;
		foguinho.localPosition = new Vector3(x,foguinho.localPosition.y, foguinho.localPosition.z);
	}
	private void CalculatePoints(float param, string Resultado)
	{
		if(param == 1000)
		{
			janelaResultado.Show (0, Resultado);
			SaveRating(0);
		}
		else if(param < 0.5f)
		{
			janelaResultado.Show (3, Resultado);
			SaveRating(3);
		}
		else if(param< 0.75f)
		{
			janelaResultado.Show (2, Resultado);
			SaveRating(2);
		}
		else
		{
			janelaResultado.Show (1, Resultado);
			SaveRating(1);
		}

	}
	void SaveRating(int newRating)
	{
		int currentLevel = PlayerPrefs.GetInt ("CurrentLevel", 1);
		//print ("1");
		int currentRating = PlayerPrefs.GetInt (Application.loadedLevelName + "s" + currentLevel.ToString (), -1);
		//print ("2");
		string completeLevelName = Application.loadedLevelName + "s" + currentLevel.ToString ();
		//print ("3");
		if (newRating > currentRating)
			PlayerPrefs.SetInt (completeLevelName, newRating);
		//print (completeLevelName + " - " + newRating + " " + timeControl.GetRunningTime().ToString());
		if(ParseControl.usingParse)
			StartCoroutine (_SaveInParse (completeLevelName, newRating, timeControl.GetRunningTime ()));
		//print ("5");
	}
	IEnumerator _SaveInParse(string completLevelName, int score, float time)
	{
		pnSaving.SetActive (true);
		//int complet = 0;
		ParseControl.current.SaveScore(completLevelName, score, time, this);
		//while (complet == 0)
		//	yield return null;
		//painel.SetActive (false);
		yield return null;;

	}
	public void ParseDone()
	{
		pnSaving.SetActive(false);
	}

}
