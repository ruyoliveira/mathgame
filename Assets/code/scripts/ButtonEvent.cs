using UnityEngine;
using System.Collections;

public class ButtonEvent : MonoBehaviour {
	
	public Color colorOver = new Color(1f,0.88f,0.55f);
	public Color colorPushed  = new Color(0.66f,0.66f,0.48f);  
	public EventDelegate scaleUp;
	public EventDelegate scaleDown;

	public string level; //If empty quit the application
	
	private Color originalColor;
	
	void Start(){      
		originalColor = gameObject.renderer.material.color;    
	}
	
	void OnMouseEnter()    
	{
		gameObject.renderer.material.color= colorOver;
		scaleUp.Execute();
	}  
	
	void OnMouseExit()
	{
		gameObject.renderer.material.color= originalColor;
		scaleDown.Execute();

	}
	
	void OnMouseDown()
	{
		gameObject.renderer.material.color= colorPushed;
	}
	
	void OnMouseUpAsButton()       
	{      
		if(level.Length>0)
			Application.LoadLevel(level);
		else
			Application.Quit();
	}
}
