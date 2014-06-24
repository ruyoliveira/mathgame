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
	public float buttonWight;
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
		float distanceX = (pointMax.x - pointMin.x) / (float)(Coluns-1);
		float distanceY = (pointMax.y - pointMin.y) / (float)Lines;
		Vector3 newPosition = new Vector3(pointMin.x, pointMin.y, transform.localPosition.z);
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
			bt.transform.localPosition = newPosition;
			bt.GetComponent<LevelButton>().level = i+1;
			print("posi: " +bt.transform.position.ToString());
			newPosition.x += distanceX;
		}
	}
	private void GetPositions()
	{

		pointMin = _UICamera.ViewportToScreenPoint(new Vector3(0.1f,0.9f,0));
		pointMax = _UICamera.ViewportToScreenPoint(new Vector3(0.9f,0.1f,0));
		pointMin = _UICamera.ScreenToWorldPoint (pointMin);
		pointMax = _UICamera.ScreenToWorldPoint (pointMax);
		pointMin = transform.InverseTransformPoint (pointMin);
		pointMax = transform.InverseTransformPoint (pointMax);
		pointMax.x -= buttonWight;
		//pointMax.x -= buttonWight;
		print ("min: " + pointMin.ToString () + "   max: " + pointMax.ToString ());
	}
}
