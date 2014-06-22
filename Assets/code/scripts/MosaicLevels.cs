using UnityEngine;
using System.Collections;

public class MosaicLevels : MonoBehaviour {
	private Vector3 pointMin;
	private Vector3 pointMax;

	private int Lines;
	private int Coluns;
	public int numberOfButtons;
	public Camera _UICamera;
	public GameObject ButtonPrefab;
	// Use this for initialization
	void Start () 
	{
		Lines = 3;
		Coluns = 5;
		GetPositions ();
		CreatMosaic ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	private void CreatMosaic()
	{
		float distanceX = (pointMax.x - pointMin.x) / (float)Coluns;
		float distanceY = (pointMax.y - pointMin.y) / (float)Lines;
		Vector3 newPosition = new Vector3(pointMin.x, pointMin.y, transform.position.z);
		print ("dx: " + distanceX + "    dy: " + distanceY + "    new: "+newPosition.ToString());
		for(int i = 0;i<numberOfButtons;i++)
		{
			if(newPosition.x > pointMax.x)
			{
				newPosition.x = pointMin.x;
				newPosition.y += distanceY;
			}
			print("posi: " +newPosition.ToString());
			GameObject bt = NGUITools.AddChild(gameObject, ButtonPrefab);
			bt.transform.position = newPosition;
			print("posi: " +bt.transform.position.ToString());
			newPosition.x += distanceX;
		}
	}
	private void GetPositions()
	{

		pointMin = _UICamera.ViewportToScreenPoint(new Vector3(0.2f,0.8f,0));
		pointMax = _UICamera.ViewportToScreenPoint(new Vector3(0.9f,0.2f,0));
		pointMin = _UICamera.ScreenToWorldPoint (pointMin);
		pointMax = _UICamera.ScreenToWorldPoint (pointMax);
		print ("min: " + pointMin.ToString () + "   max: " + pointMax.ToString ());
	}
}
