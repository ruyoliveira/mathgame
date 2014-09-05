using UnityEngine;
using System.Collections;

public class ObjectsGeneratorMatriz : MonoBehaviour {
	public Transform origin;
	public int coluns;
	public Vector2 distance;
	public GameObject[] prefabs;
	public float[] weights;
	public int[] amounts;

	// Use this for initialization
	public void Init () {
		Generate ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Generate()
	{
		int _currentColun = 0;
		int _currentLine = 0;
		for(int i = 0; i < prefabs.Length; i++)
		{
			for(int count = 0; count < amounts[i] ; count++)
			{
				var obj = Instantiate(prefabs[i]) as GameObject;
				obj.transform.parent = transform;
				obj.rigidbody2D.isKinematic = true;
				obj.transform.localPosition = new Vector3(origin.localPosition.x + distance.x*_currentColun, origin.localPosition.y - distance.y*_currentLine, origin.localPosition.z);
				obj.GetComponent<DraggableObject>().weight = weights[i];
				obj.GetComponent<SpriteRenderer>().sortingOrder = _currentLine;


				_currentColun++;
				if(_currentColun >= coluns){
					_currentColun = 0;
					_currentLine ++;
				}
			}
		}
	}
}
