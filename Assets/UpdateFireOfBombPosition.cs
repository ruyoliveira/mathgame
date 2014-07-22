using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class UpdateFireOfBombPosition : MonoBehaviour {
	public Transform pavil;
	void Update () {
		if(pavil)
		{
			float x = pavil.GetComponent<SpriteRenderer>().bounds.size.x + pavil.transform.localPosition.x;
			transform.localPosition = new Vector3(x,transform.localPosition.y, transform.localPosition.z);
		}
	}
}
