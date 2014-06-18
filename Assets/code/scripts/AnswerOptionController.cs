using UnityEngine;
using System.Collections;

/// <summary>
/// Script da resposta
/// Gerencia a exibicao e controle da opcao de resposta
/// </summary>
public class AnswerOptionController : MonoBehaviour {

	public int answer;

	public tk2dTextMesh _textMesh;

	// Use this for initialization
	void Start () {

		//Coleta o texto no text mesh
		_textMesh.text = answer.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
