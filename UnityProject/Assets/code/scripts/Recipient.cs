﻿using UnityEngine;
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
			if(dragObj.isCounted == false)
			{
				counter += dragObj.weight;
				dragObj.gravityScale = 1.0f;//deixa o obj sem gravidade quando entra no recipiente
				dragObj.isCounted = true;
			}
		}
	}
	void OnTriggerExit2D(Collider2D colision)
	{
		if(colision.gameObject.CompareTag("Objeto"))
		{
			DraggableObject dragObj = colision.gameObject.GetComponent<DraggableObject>();
			if(dragObj.isCounted == true)
			{
				counter -= dragObj.weight;
				dragObj.gravityScale = 1;//deixa o obj sem gravidade quando entra no recipiente
				dragObj.isCounted = false;
			}
		}
	}
}
