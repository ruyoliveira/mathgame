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
	public int dragMode;//3 - carnes;
	public  Vector3 mousePosition;	//posicao do mouse
	public  float dragSpeed;	//velocidade de carregar o objeto
	public float gravityScale;	//gravidade
	public Transform respawn; //posicao de respawn dos objetos
	public bool isCounted;
	public bool stopMovimentOnDrop;
	public bool useRespaw;

	// Use this for initialization
	void Start () {
		if(useRespaw)
			respawn = GameObject.FindWithTag("Respawn").transform;
		else
		{
			respawn = (new GameObject("Respawn " + name)).transform;
			respawn.parent = transform.parent;
			respawn.position = transform.position;
		}
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
			_rigidbody.angularVelocity = 0f;
			_rigidbody.angularDrag = 0f;
			_rigidbody.velocity = Vector2.zero;
			if(useRespaw)
				_transform.position = new Vector2(respawn.position.x + Random.value,respawn.position.y + Random.value);
			else
			{
				_transform.position = respawn.position;
				_transform.eulerAngles = Vector3.zero;
			}
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
				//Debug.Log("Desselecionado");
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
			if(dragMode==3)
			{
				if(isCounted)
				{
					_rigidbody.isKinematic = false;
					_rigidbody.gravityScale = gravityScale;
				}
				else
					if(stopMovimentOnDrop)
					{
						_rigidbody.velocity = new Vector2(0f,0f);
						_rigidbody.isKinematic = true;
					}
			}
			else
			{
				_rigidbody.gravityScale = gravityScale;
				if(stopMovimentOnDrop)
					_rigidbody.velocity = new Vector2(0f,0f);
			}
		}
	}
	//Seleciona o objeto
	void OnMouseDown(){
		
		isSelected = true;
		
	}  

	//Getter e Setter


}
