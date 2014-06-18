using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public bool isSelected;	//indica se alguma resposta esta sendo selecionada

	public bool isAnswered;	//indica se a resposta foi escolhida

	public int answer;		//armazena a resposta escolhida

	public bool solve;		//indica se a equacao deve ser solucionada

	public QuestionItem question;


	// Use this for initialization
	void Start () {
		//inicializa as variaveis
		isSelected = false;

		isAnswered = false;

		answer = 0;
	
	}

	void Update () {
		//Condicao existe para caso seja necessario utilizar um botao p inicar a solucao da pergunta
		if(isAnswered)
		{
			solve = true;
		}
		if(solve)
		{
			//Se a resposta estiver correta
			if(answer == question.correctAnswer)
			{
				question.isCorrect = true;	//indica a pergunta que a resposta escolhioda e correta
				Debug.Log ("Acertou");
			}
			//Se resposta estiver errada
			else
			{
				question.isCorrect = false;	//indica a pergunta que a resposta escolhioda e errada
				Debug.Log ("Errou");


			}
			question.solve = true;
			
		}
		else
		{
			if(isSelected)
			{
				//Move o personagem
				if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)) 
				{
					
					//Posicao do mouse enquanto o botao esquero esta clicado
					Vector3 touchDeltaPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

					touchDeltaPosition.z = 0;	//zera o eixo z da posicao do mouse

					//move o objeto no plano xy
					transform.position = Vector3.Lerp(transform.position,touchDeltaPosition,0.1f);

				}
			}
		}

	}
	//Define se o player foi selecionado
	public void SetSelected(bool state)
	{
		this.isSelected = state;	//seta flag selecionado

	}

	//eventos

	//Seleciona o objeto
	void OnMouseDown(){

		this.SetSelected(true);

	}  
	//Desseleciona o objeto
	void OnMouseUp(){

		this.SetSelected(false);

	} 

	//Detecta colisao com respostas
	void OnTriggerStay2D(Collider2D col)
	{
		//Se nao estiver selecionado
		if(!isSelected)
		{
			//Detecta se foi solto em cima de uma resposta
			if(col.CompareTag("Resposta"))
			{
				int colAnswer = col.gameObject.GetComponent<AnswerOptionController>().answer;	//adquire texto da resposta

				if( this.answer != colAnswer)
				{
					this.answer =colAnswer;	//armazena a resposta

					isAnswered = true;	//flag de resposta  escolhida

					Debug.Log (colAnswer);
				}

			}
		}
	}
}
