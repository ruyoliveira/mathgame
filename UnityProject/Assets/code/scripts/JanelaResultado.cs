using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JanelaResultado : MonoBehaviour {
	public GameObject window;
	public Button btNextLevel;
	public Text lbResultado;
	public GameObject[] goldStars;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame

	public void Show(int rating, string Resultado)
	{
		window.SetActive (true);
		lbResultado.text = Resultado;
		if(rating > 3) 
			rating = 3;
		StartCoroutine(_SetGoldStars (rating));
	}


	IEnumerator _SetGoldStars(int _rating)
	{
		yield return new WaitForSeconds (0.5f);
		for (int i =0; i<_rating;i++)
		{
			goldStars[i].animation.Play("PutGoldStar");
			yield return new WaitForSeconds(0.4f);
		}
	}

	public void OnClickMenuButton()
	{
		Application.LoadLevel ("gameMenu");
	}
}
