using UnityEngine;
using System.Collections;

public class Carroussel : MonoBehaviour {
	public Vector2 firstMousePosition;
	public bool buttonDown;
	public Transform leftLimit;
	public Transform rightLimit;
	// Use this for initialization
	void Start () {
	
	}
	void Update() {
		//Verifica se pressionou o botao esquerdo do mouse
		if(Input.GetMouseButtonDown(0))
		{
			buttonDown = true;
			firstMousePosition = Input.mousePosition;
		}
		//Verifica se soltou o botao esquerdo do mouse
		else if(Input.GetMouseButtonUp(0))
		{
			buttonDown = false;
		}
		//Move os elementos com relação ao deslocamento do mouse clicado
		if(buttonDown && (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)))
		{
			Vector2 actualMousePosition = Input.mousePosition;	//converte posicao atual do mouse em vector2
			Vector2 deltaMouse = actualMousePosition - firstMousePosition;	//calcula deslocamento do mouse
			deltaMouse.Normalize();		//normaliza deslocamento
			//Rotaciona para a esquerda
			if(deltaMouse.x >0)
			{
				//Nao rotaciona se o limite direito ja tiver sido atingido
				if(transform.position.x < rightLimit.position.x)
				{
					transform.Translate(new Vector2(deltaMouse.x/4,0));
					firstMousePosition = Input.mousePosition;	//modo 1
				}
			}
			//Rotaciona para a direita
			else
			{
				//Nao rotaciona se o limite esquerdo tiver sido atingido
				if(transform.position.x > leftLimit.position.x)
				{
					transform.Translate(new Vector2(deltaMouse.x/4,0));
					firstMousePosition = Input.mousePosition;	//modo 1
				}

			}

			//Debug.Log (deltaMouse);
		}
	}
}
