using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

public class SetLayer : MonoBehaviour {
	public string layerName;
	public int layerOrder;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.renderer.sortingLayerName = layerName;
		gameObject.renderer.sortingOrder = layerOrder;
	}
}
