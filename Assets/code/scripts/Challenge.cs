using UnityEngine;
using System.Collections;

public class Challenge : MonoBehaviour {

	//visualizacao
	public QuestionItem[] questions;			//pergunta
	public AnswerOptionController[] options;	//opcoes de resposta

	//Nao possuem script, alterracao diretamente no textmesh
	public tk2dTextMesh[] elements;				//elementos da equacao
	public tk2dTextMesh[] operations;			//operacoes
	public tk2dTextMesh equal;					//igualdade
	public tk2dTextMesh result;					//igualdade

	//dados
	public int correctAnswer;
	public int functionResult;
	public int[] functionOptions;
	public int[] functionElems;
	public string[] functionOp;



	// Use this for initialization
	void Start () {
		//Configura o desafio com as definicoes do usuario

		//Resposta da equacao
		for(int i = 0 ; i< questions.Length; i++)
		{
			questions[i].correctAnswer = correctAnswer;
			questions[i].Setup();//atualiza o textmesh
		}
		//Opcoes de resposta
		for(int i = 0 ; i< options.Length; i++)
		{
			options[i].answer = functionOptions[i];
			options[i].Setup();	//atualiza o textmesh
		}
		//Outros elementos da equacao
		for(int i = 0 ; i< elements.Length; i++)
		{
			elements[i].text = functionElems[i].ToString();
		}
		//Operacoes da equacao
		for(int i = 0 ; i< operations.Length; i++)
		{
			operations[i].text = functionOp[i];
		}
		//Sinal de igualdade
		equal.text = "=";

		//Lado direito da equacao
		result.text = functionResult.ToString();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Setup()
	{

		//Configura o desafio com as definicoes do usuario
		
		//Resposta da equacao
		for(int i = 0 ; i< questions.Length; i++)
		{
			questions[i].correctAnswer = correctAnswer;
			questions[i].Setup();//atualiza o textmesh
		}
		//Opcoes de resposta
		for(int i = 0 ; i< options.Length; i++)
		{
			options[i].answer = functionOptions[i];
			options[i].Setup();	//atualiza o textmesh
		}
		//Outros elementos da equacao
		for(int i = 0 ; i< elements.Length; i++)
		{
			elements[i].text = functionElems[i].ToString();
		}
		//Operacoes da equacao
		for(int i = 0 ; i< operations.Length; i++)
		{
			operations[i].text = functionOp[i];
		}
		//Sinal de igualdade
		equal.text = "=";
		
		//Lado direito da equacao
	}
}
