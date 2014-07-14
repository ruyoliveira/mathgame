using UnityEngine;
using System.Collections;

public class Recipient : MonoBehaviour {
	public float counter;
	// Use this for initialization
	void Start () {
		counter  = 0;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D colision)
	{
		if(colision.gameObject.CompareTag("Objeto"))
		{
			DraggableObject dragObj = colision.gameObject.GetComponent<DraggableObject>();
			counter += dragObj.weight;
			dragObj.gravityScale = 0.2f;//deixa o obj sem gravidade quando entra no recipiente
		}
	}
	void OnTriggerExit2D(Collider2D colision)
	{
		if(colision.gameObject.CompareTag("Objeto"))
		{
			DraggableObject dragObj = colision.gameObject.GetComponent<DraggableObject>();
			counter -= dragObj.weight;
			dragObj.gravityScale = 1;//deixa o obj sem gravidade quando entra no recipiente
		}
	}
}
