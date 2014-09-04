using UnityEngine;
using System.Collections;

public class JanelaResultado : MonoBehaviour {
	public GameObject window;
	public UIButton btNextLevel;
	public GameObject[] goldStars;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame

	public void Show(int rating)
	{
		window.SetActive (true);
		if(rating > 3) 
			rating = 3;
		StartCoroutine(_SetGoldStars (rating));
	}


	IEnumerator _SetGoldStars(int _rating)
	{
		yield return new WaitForSeconds (0.5f);
		for (int i =0; i<_rating;i++)
		{
			goldStars[i].SetActive(true);
		}
	}

	public void OnClickMenuButton()
	{
		Application.LoadLevel ("gameMenu");
	}
}
