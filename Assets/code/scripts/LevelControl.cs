using UnityEngine;
using System.Collections;

public class LevelControl : MonoBehaviour {
	public GameObject[] stars;
	public bool isLocked;
	public int nStars;
	public string levelName;
	public int levelNumber;
	public TextMesh text;
	public ButtonEventSprite buttonSprite;

	// Use this for initialization
	void Start () {
		if(isLocked)
		{
			buttonSprite.gameObject.SetActive(true);//ativa botao do level
			buttonSprite.SetLocked();//bloqueia botao
			text.text = "";
		}
		else
		{
			for(int i=0; i< nStars ;i++)
			{
				stars[i].SetActive(true);	//exibe as estrelas conquistadas no level
			}
			buttonSprite.gameObject.SetActive(true);//ativa botao do level
			buttonSprite.SetUnlocked();//desbloqueia botao
			text.text = levelNumber.ToString();

		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
