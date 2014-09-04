using UnityEngine;
using System.Collections;

public class ObjectsManager : MonoBehaviour {
	//public Sprite[] objSprites;
	public GameObject[] objPrefabs;
	public int dragMode;
	//public GameObject objectMeta;
	public Transform respawn;
	public int nOfkinds;
	public int[] amounts;
	public int[] weights;
	// Use this for initialization
	void Start () 
	{
		amounts= new int[nOfkinds];
		weights= new int[nOfkinds];

	}
	public void Init()
	{
		//Instancia e Configura os objetos
		for(int i=0; i<nOfkinds; i++)
		{
			//Color kindColor = new Color(Random.value,Random.value,Random.value);
			for(int j = 0; j<amounts[i];j++)
			{
				GameObject tempObject = (GameObject)Instantiate(objPrefabs[i],new Vector2(respawn.position.x + Random.value,respawn.position.y),respawn.rotation);
				tempObject.transform.parent = this.gameObject.transform;
				DraggableObject tempDraggable = tempObject.GetComponent<DraggableObject>();
				tempDraggable.gravityScale = 1;
				tempDraggable.dragSpeed = 8;
				tempDraggable.weight = weights[i];
				//tempDraggable.renderer.material.color = kindColor;
				tempDraggable.dragMode = dragMode;

			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
