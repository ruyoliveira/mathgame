using UnityEngine;
using System.Collections;

public class ResultWindow : MonoBehaviour {
	//Pontuacoes a ser exibidas
	public int correctAnswer;	//n de respostas corretas
	public int wrongAnswer;		//n de respostas erradas
	public float totalTime;		//tempo total transcorrido
	public float totalPoints;		//pontuacao total
	public int minimumCorrect;	//n minimo de resostas corretas do nivel
	public int soldeAnswers;	//saldo final de respostas corretas/erradas

	public tk2dTextMesh _textMesh;
	// Use this for initialization
	void Start () {
	
		this.correctAnswer = 0;
		this.wrongAnswer = 0;
		this.totalTime = 0;
		this.minimumCorrect = 0;
	}
	//Procedimentos executados quando a janela e ativada
	void OnEnable()
	{
		this._textMesh.text = "";	//limpa o texto da janela

		this.soldeAnswers = this.correctAnswer - this.wrongAnswer;
		
		//Numero de acertos maior que o minimo
		if(this.soldeAnswers > this.minimumCorrect)
		{
			this._textMesh.text += "Sucesso! \n";
			this._textMesh.text += "Saldo final: ";
			this._textMesh.text += soldeAnswers.ToString() + " ";	//n de respostas corretas
			
			
		}
		
		//Numero de acertos maior que o minimo
		else
		{
			this._textMesh.text += "Tente outra vez \n";
			this._textMesh.text += "Saldo final: ";
			this._textMesh.text += soldeAnswers.ToString() + " ";	//n de respostas corretas
		}
	}
	// Update is called once per frame
	void Update () {

	}
}
