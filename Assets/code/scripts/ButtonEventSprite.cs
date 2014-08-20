using UnityEngine;
using System.Collections;

/// <summary>
/// Script de botao que carrega novo level ou sai do jogo ao clicar. Permite atribuir sprites para os estados de botao
/// locked, normal, hover e clicked.
/// </summary>
public class ButtonEventSprite : MonoBehaviour {
	//Sprites
	public Sprite normal;
	public Sprite hover;
	public Sprite clicked;
	public Sprite locked;

	//referencia do renderer
	private SpriteRenderer spriteRenderer;

	//Eventos de tween de eslaca
	public EventDelegate scaleUp;
	public EventDelegate scaleDown;

	//level a ser carregado
	public string level; //If empty quit the application

	//controle
	public bool isLocked = false;
	public bool isInitialized = false;
	
	void Start(){     
		spriteRenderer = gameObject.renderer as SpriteRenderer;	//armazena referencia, evita erro de execucao concorrente
	}
	
	void OnMouseEnter()    
	{
		if(!isLocked)
		{
			spriteRenderer.sprite = hover;	//muda sprite para hover
			scaleUp.Execute();	//ativa tween de escala
		}
	}  
	
	void OnMouseExit()
	{
		if(isInitialized)
		{
			if(!isLocked)
			{
				spriteRenderer.sprite = normal;	//muda sprite para normal
				scaleDown.Execute();	//ativa tween de escala
			}
		}

	}
	
	void OnMouseDown()
	{
		if(isInitialized)
		{
			if(!isLocked)
			{
				spriteRenderer.sprite = clicked;	//muda sprite para click
			}
		}
	}
	
	void OnMouseUpAsButton()       
	{     
		if(isInitialized)
		{
			if(!isLocked)
			{
				//Carrega level, se especificado, senao sai do jogo
				if(level.Length>0)
					Application.LoadLevel(level);	//carrega level
				else
					Application.Quit();	//sai do jogo
			}
		}
	}
	public void SetLocked()
	{
		isLocked = true;	//indica que o level ainda etsa bloqueado
		spriteRenderer = gameObject.renderer as SpriteRenderer;	//armazena referencia, evita erro de execucao concorrente
		spriteRenderer.sprite = locked;	//muda sprite
		isInitialized = true; //indica que este elemento foi inicializado
	}
	public void SetUnlocked()
	{
		isLocked = false;	//indica que o level esta desbloqueado
		spriteRenderer = gameObject.renderer as SpriteRenderer;	//armazena referencia, evita erro de execucao concorrente
		spriteRenderer.sprite = normal;	//muda sprite
		isInitialized = true; //indica que este elemento foi inicializado
	}
}
