using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	//Temporario
	public tk2dTextMesh Text;
	//

	public Challenge[] desafios;

	public ResultWindow window;
	public bool questionSolved;
	public PlayerController _player;
	public QuestionItem _currentQuestion;

	public int correctAnswer;	//n de respostas corretas
	public int wrongAnswer;		//n de respostas erradas
	public float totalTime;		//tempo total transcorrido
	public int minimumCorrect;	//n minimo de resostas corretas do nivel
	public int soldeAnswers;	//saldo final de respostas corretas/erradas
	public int weightAnswer;	//peso do saldo total de respostas na pontuacao final
	public int weightTime;		//peso do tempo na pontuacao fiunal

	public int numChallenges; 	// numero de perguntas do nivel
	public int countAnswer;		//numero de perguntas respondidas
	public bool hasCountedAnswer;	//detecta se ja contou a resposta

	float totalTime;
	float timeOut;
	bool game;
	// Use this for initialization
	void Start () {
		//inicializa o primeiro desafio
		desafios[0].gameObject.SetActive(true);	
		
		//inicializa o relogio contador

		//inicializa o numero de desafios
		numChallenges = desafios.Length;

		_player =  GameObject.FindWithTag("Player").GetComponent<PlayerController>();	//busca o player e armazena a referencia no inicio da partida

		_currentQuestion =  GameObject.FindWithTag("Pergunta").GetComponent<QuestionItem>();	//busca a pergunta na cena e armazena a referencia no inicio da partida

		countAnswer = 0;	//inicializa o numero de perguntas respondidas

		correctAnswer = 0;	//inicializa o numero respostas corretas

		wrongAnswer = 0;	//inicializa o numero de respostas erradas

		hasCountedAnswer = false;




		totalTime = 0;
		timeOut = 15;//tempo inicial
		game = true;
	}
	
	// Update is called once per frame
	void Update () {
		//Jogador respondeu a pergunta
		if(_player.solved && !hasCountedAnswer)
		{
			hasCountedAnswer = true;	//indica que ja contou esta resposta
			++countAnswer;//incrementa o numero de perguntas respondidas

			//Incrementa o n de respostas corretas se o jogador acertou
			if(_player.isCorrect)
			{
				++correctAnswer;
			}
			//Incrementa o n de respostas erradas se o jogador acertou
			else
			{
				++wrongAnswer;
			}
			//Caso o jogador tenha respondido a todas as perguntas do nivel
			if(countAnswer == numChallenges)
			{
				window.correctAnswer = this.correctAnswer;	//atribui a janela respostas erradas
					
				window.wrongAnswer = this.wrongAnswer;	//atribui a janela respostas corretas

				window.minimumCorrect = 1;	//numero minimo de respostas corretas do level

				//Exibe a janela de resultado
				window.gameObject.SetActive(true);
			}
			//Caso contrario, lanca a nova pergunta
			else
			{
				//DESABILITA ANTIGA PERGUNTA
				desafios[countAnswer-1].gameObject.SetActive(false);

				//RESETA O PLAYER
				_player.ResetPlayer();
				_player.question = desafios[countAnswer].questions[0];

				//INSTANCIA A NOVA PERGUNTA
				desafios[countAnswer].gameObject.SetActive(true);
				//HABILITA O JOGADOR P RESPONDER DENOVO
				hasCountedAnswer = false;
			}
		}
	
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

	public void RestartLevel()
	{

		//DESABILITA ANTIGA PERGUNTA
		desafios[countAnswer-1].gameObject.SetActive(false);

		//REINICIA OS CONTADORES
		countAnswer = 0;	//perguntas respoindidas

		correctAnswer = 0;	//respostas corretas
		
		wrongAnswer = 0;	//respostas erradas

		//RESETA O PLAYER
		_player.ResetPlayer();
		_player.question = desafios[countAnswer].questions[0];

		//INSTANCIA A NOVA PERGUNTA
		desafios[countAnswer].gameObject.SetActive(true);

		//HABILITA O JOGADOR P RESPONDER DENOVO
		hasCountedAnswer = false;

		window.gameObject.SetActive(false);

	}
	public void QuitLevel()
	{
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
