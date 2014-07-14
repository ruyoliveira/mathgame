using UnityEngine;
using System.Collections;

public class DraggableObject : MonoBehaviour {
	private Rigidbody2D _rigidbody;	//armazenamento estatico do rigidbody
	private Transform _transform;


	public bool isSelected {//Flag objeto selecionado
		get;
		set;
	}

	public float weight;
	public int dragMode;
	public  Vector3 mousePosition;	//posicao do mouse
	public  float dragSpeed;	//velocidade de carregar o objeto
	public float gravityScale;	//gravidade
	public Transform respawn; //posicao de respawn dos objetos
	public bool isCounted;


	// Use this for initialization
	void Start () {
		respawn = GameObject.FindWithTag("Respawn").transform;
		_rigidbody = rigidbody2D;
		_transform = transform;
		isCounted = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Detecta se o objeto saiu da tela e coloca devolta a posicao inicial
		double  VerticalSeen    = Camera.main.orthographicSize * 2.0;
		double HorizontalSeen = VerticalSeen * Screen.width / Screen.height;
		if(transform.position.x >  HorizontalSeen / 2 
		   || transform.position.x < - HorizontalSeen / 2
		   || transform.position.y >  VerticalSeen *10	//deixa sair um pouco da parte superior da tela
		   || transform.position.y < - VerticalSeen / 2
		   )
		{
			_transform.position = new Vector2(respawn.position.x + Random.value,respawn.position.y + Random.value);
			_rigidbody.velocity = Vector2.zero;
			isSelected = false;//deseleciona
			if(dragMode == 1)
			{
				this.collider2D.isTrigger = false;
			}
		}



		//Objeto selecionado
		if(isSelected)
		{
			_rigidbody.gravityScale = 0;
			//Desseleciona
			if(Input.GetMouseButtonUp(0))
			{
				isSelected = false;
				if(dragMode == 1)
				{
					Physics2D.IgnoreCollision(this.collider2D,this.collider2D,false);
					this.collider2D.isTrigger = false;
				}
				Debug.Log("Desselecionado");
			}
			//Move o personagem
			else if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)) 
			{
				if(dragMode == 1)
				{
					this.collider2D.isTrigger = true;
				}
				//Posicao do mouse enquanto o botao esquero esta clicado
				Vector3 touchDeltaPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				
				touchDeltaPosition.z = 0;	//zera o eixo z da posicao do mouse
				mousePosition = touchDeltaPosition;
				//move o objeto no plano xy
				_rigidbody.velocity = new Vector2( - dragSpeed *  (transform.position.x - mousePosition.x), -dragSpeed * (transform.position.y - mousePosition.y));
				
			}
		}
		else
		{
			_rigidbody.gravityScale = gravityScale;
		}
	}
	//Seleciona o objeto
	void OnMouseDown(){
		
		isSelected = true;
		
	}  

	//Getter e Setter


}
