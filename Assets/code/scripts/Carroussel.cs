using UnityEngine;
using System.Collections;

public class Carroussel : MonoBehaviour {
	public Vector2 firstMousePosition;
	public bool buttonDown;
	// Use this for initialization
	void Start () {
	
	}
	void Update() {
		if(Input.GetMouseButtonDown(0))
		{
			buttonDown = true;
			firstMousePosition = Input.mousePosition;
		}
		else if(Input.GetMouseButtonUp(0))
		{
			buttonDown = false;
		}
		if(buttonDown && (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)))
		{
			Vector2 actualMousePosition = Input.mousePosition;
			Vector2 deltaMouse = actualMousePosition - firstMousePosition;
			deltaMouse.Normalize();
			transform.Translate(new Vector2(deltaMouse.x/4,0));
			firstMousePosition = Input.mousePosition;	//modo 1
			Debug.Log (deltaMouse);
			
			//Posicao do mouse enquanto o botao esquero esta clicado
			//Vector3 touchDeltaPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			
			//touchDeltaPosition.z = 0;	//zera o eixo z da posicao do mouse
			//Vector2 deltaMousePosition = touchDeltaPosition;
			//move o objeto no plano xy
			//transform.Translate(new Vector2(deltaMousePosition,0));

		}
	}
}
