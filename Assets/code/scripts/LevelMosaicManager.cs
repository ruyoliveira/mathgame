using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelMosaicManager : MonoBehaviour {
	private Button[] levelButtons;
	private int worldNumber;
	public int maxLevel;
	// Use this for initialization
	void Start () {
		//salvando para teste
		PlayerPrefs.SetInt("w"+worldNumber+"s"+1,1);
		PlayerPrefs.SetInt("w"+worldNumber+"s"+2,3);
		PlayerPrefs.SetInt("w"+worldNumber+"s"+3,2);
		PlayerPrefs.SetInt("w"+worldNumber+"s"+4,1);
		PlayerPrefs.SetInt("w"+worldNumber+"s"+5,2);
		PlayerPrefs.SetInt("w"+worldNumber+"s"+6,3);
		PlayerPrefs.SetInt("w"+worldNumber+"s"+7,-1);
		PlayerPrefs.SetInt("w"+worldNumber+"s"+8,-1);


		//Inicializa vetor de botoes
		levelButtons = new Button[maxLevel];

		//Busca os botoes
		for(int i=0;i<levelButtons.Length;i++)
		{
			levelButtons[i] = GameObject.Find("Button"+( ( i+1 ).ToString() )).GetComponent<Button>();
		}
		//Mostra N de estrelas nos botoes
		foreach(Button bt in levelButtons)
		{

			LevelButtonData levelBtData = bt.gameObject.GetComponentInChildren<LevelButtonData>();
			string btName = bt.gameObject.name;//armazena  o nome do botao, q contem o numero do level



			//Verifica se o level esta bloqueado
			if(PlayerPrefs.GetInt("w"+worldNumber+"s"+levelBtData.levelNumber) == -1)
			{
				bt.interactable = false;
			}
			else
			{
				//desbloqueia o proximo level
				if(levelBtData.levelNumber < maxLevel)//se nao for o ultimo level sendo avaliado
				{
					if(PlayerPrefs.GetInt("w"+worldNumber+"s"+(levelBtData.levelNumber+1)) == -1)//se o proximo estiver bloqueado
					{
						Debug.Log (levelBtData.levelNumber);
						if(PlayerPrefs.GetInt("w"+worldNumber+"s"+levelBtData.levelNumber) > 0)//se tiver score maior q 0
						{
							PlayerPrefs.SetInt("w"+worldNumber+"s"+(levelBtData.levelNumber+1),0);
						}
					}
				}

				//Ativa as estrelas que o jogador conquistou no level
				for(int i =0; i < PlayerPrefs.GetInt("w"+worldNumber+"s"+levelBtData.levelNumber); i++)
				{
					levelBtData.stars[i].enabled = true;
				}
			}
		}
	}
	
	//Executa quando o level é carregado
	void OnLevelWasLoaded(int sceneIndex) {
		worldNumber = PlayerPrefs.GetInt("CurrentWorld");
		Debug.Log (worldNumber);
	}
	public void LoadLevel(int levelNumber)
	{
		PlayerPrefs.SetInt ("CurrentLevel", levelNumber);
		Application.LoadLevel("w" + worldNumber.ToString());
	}
}
