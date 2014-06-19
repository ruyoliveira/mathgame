using UnityEngine;
using System.Collections;

public class ResultWindow : MonoBehaviour {
	//Pontuacoes a ser exibidas
	public int correctAnswer;	//n de respostas corretas
	public int wrongAnswer;		//n de respostas erradas
	public float totalTime;		//tempo total transcorrido
	public float remainingTime;		//tempo total transcorrido
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

		this.totalPoints = (this.correctAnswer *100) + (this.remainingTime * 10);
		
		//Numero de acertos maior que o minimo
		if(this.soldeAnswers > this.minimumCorrect)
		{
			this._textMesh.text += "            sucesso!            \n";
			this._textMesh.text += "acertos: ";
			this._textMesh.text += correctAnswer.ToString() + "\n";	//n de respostas corretas
			this._textMesh.text += "tempo usado: ";
			this._textMesh.text += totalTime.ToString("0.0") + "\n";	//n de respostas corretas
			this._textMesh.text += "tempo restante: ";
			this._textMesh.text += remainingTime.ToString("0.0") + "\n";	//n de respostas corretas
			this._textMesh.text += "Pontos: ";
			this._textMesh.text +=  totalPoints.ToString("0") + "\n";	//pontuacao

			
			
		}
		
		//Numero de acertos maior que o minimo
		else
		{
			this._textMesh.text += "       tente outra vez        \n";
			this._textMesh.text += "acertos: ";
			this._textMesh.text += correctAnswer.ToString() + "\n";	//n de respostas corretas
			this._textMesh.text += "tempo usado: ";
			this._textMesh.text += totalTime.ToString("0.0") + "\n";	//n de respostas corretas
			this._textMesh.text += "tempo restante: ";
			this._textMesh.text += remainingTime.ToString("0.0") + "\n";	//n de respostas corretas
			this._textMesh.text += "Pontos: ";
			this._textMesh.text +=  totalPoints.ToString("0") + "\n";	//pontuacao
		}
	}
	// Update is called once per frame
	void Update () {

	}
}
