using UnityEngine;
using System.Collections;

public class QuestionItem : MonoBehaviour {

	public int correctAnswer;
	
	public tk2dTextMesh _textMesh;

	public bool solve; //indica que a equacao deve ser resolvida

	public bool isCorrect;	//indica se a resposta escolhida e a correta


	// Use this for initialization
	void Start () {

		solve = false;

		isCorrect = false;
		//se ainda nao tiver escolhido a resposta
		_textMesh.text = "?";

	
	}
	
	// Update is called once per frame
	void Update () {
		if(solve)
		{
			//Mostra resposta
			_textMesh.text = correctAnswer.ToString();

			if(isCorrect)
			{
				_textMesh.color = new Color(0, 255,0);
			}
			else
			{
				_textMesh.color = new Color(255,0,0);

			}
		}
	
	}


}
