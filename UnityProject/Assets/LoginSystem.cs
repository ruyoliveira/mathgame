using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Parse;

//Classe responsavel pelo sistema de login, genciando tanto a interface
//grafica quanto seu funcionamento interno.



public class LoginSystem : MonoBehaviour {
	//variaveis de controle
	bool loged; 
	bool tentandoEntrar;
	int currentWin;
	//---------------------------

	//variaveis referentes a janela de login
	public GameObject _1winLogin;
	public InputField nickInputLogin;
	public Button btEntrar;
	public Button btPrimeiroAcesso;
	public Text lbWarning;
	//----------------------

	//variaveis referentes a janela de cadastro
	public GameObject _2winCadastrar;
	public Button _2BtCadastrar;
	public InputField _2InputNome;
	public Toggle[] _2Toggles;
	private string _2OldString = "";
	public Text _2CenterLabel;
	string _2NickEscolhido = "";
	bool processing;
	//------------------------------

	//variaveis referentes a janela de Loading
	public GameObject _3winCarregar;
	public Text lbCarregando;
	public Button btVoltar_Carregando;
	//------------------------------------------------

	//variaveis referente a janela SingingUp
	public GameObject _4winSingingUp;
	public Text _4Label;
	public Button _4btvoltar;
	//-----------------------------------------

	// Use this for initialization
	void Start () {
		if(!ParseControl.usingParse)
			this.gameObject.SetActive(false);
		loged = false;
		tentandoEntrar = false;
		currentWin = 1;

	}
	
	// Update is called once per frame
	void Update () {
		if (!ParseControl.usingParse)
			return;


		if(currentWin==1 && !tentandoEntrar)
		{
			nickInputLogin.value = GetStringNoAccents(nickInputLogin.value.ToUpper());
			btEntrar.interactable = (nickInputLogin.value.Length == 5); 
		}
		else if (currentWin == 2)
		{
			_2BtCadastrar.interactable = (_2InputNome.value != "") && (_2InputNome.value.Length >= 3) && (_2NickEscolhido != "") && (!processing);
			if(_2InputNome.value != _2OldString)
			{
					_2InputNome.value = GetStringNoAccents(_2InputNome.value.ToUpper());
					_2OldString = _2InputNome.value;
			}
			else
			{

			}
			if(Input.GetKeyDown(KeyCode.Escape)) SetCurrentWindow(1);
		}
	}

	IEnumerator Entrar()
	{
		lbWarning.text = ""; // label de aviso de erro
		btPrimeiroAcesso.interactable = false; // desabilita botao Primeiro acesso
		btEntrar.interactable = false; // desabilita botao Entrar
		tentandoEntrar = true; // seta flag
		var lbButtonText = btEntrar.GetComponentInChildren<Text> (); // pega referencia da label do botao entrar
		lbButtonText.text = "Entrando..."; 
		bool waiting = true; // flags de controle
		bool sucesso = false; // flags de controle
		string warningText = "";

		//tenta login
		ParseUser.LogInAsync(nickInputLogin.value, nickInputLogin.value).ContinueWith(t => 
		{
			if(t.IsCanceled || t.IsFaulted)
			{
				var enumerator = t.Exception.InnerExceptions.GetEnumerator();
				if(enumerator.MoveNext())
				{
					ParseException ex = enumerator.Current as ParseException;

					print("error " + ex.Code + ": " + ex.Message);
					if(ex.Message.Contains("404 Not Found"))
					{
						warningText = "Usuario nao encontrado.\nSolicite primeiro acesso";
					}
					else
						warningText = "Falha ao conectar!";
				}
			}
			else
			{
				print("Secesso, login:" + ParseUser.CurrentUser.Username);
				sucesso = true;
			}
			waiting = false;

		});


		while(waiting) yield return null; // espera o parse processar o login

		if(sucesso) // em caso de sucesso inicia rotina de carregar dados
		{
			StartCoroutine(Carregar());
		}
		else
		{

		}


		lbWarning.text = warningText; //atualiza label de aviso
		btEntrar.interactable = true; // ativa o botao Entrar
		btPrimeiroAcesso.interactable = true; // ativa botao Primeiro acesso
		lbButtonText.text = "Entrar"; 
		tentandoEntrar = false;
	}
	IEnumerator Carregar()
	{

		_1winLogin.SetActive (false); // fecha janela de login
		_3winCarregar.SetActive (true); // abre janela de carregar dados
		btVoltar_Carregando.gameObject.SetActive(false); // desabilita botao Voltar enquanto estiver processando o carregamento
		bool carregando = true;// flags de controle
		bool sucesso = false; // flags de controle
		Dictionary<string, int> estatisticas = new Dictionary<string, int> (); // diconario que recebera as pontuaçoes.
		Dictionary<string, string> desafios = new Dictionary<string, string> ();
		PlayerPrefs.DeleteAll (); // limpar os dados do PlyerPrefs

		var queryDesafios = ParseObject.GetQuery ("Desafio");
		queryDesafios.FindAsync().ContinueWith(t =>
		{
			if(t.IsCanceled || t.IsFaulted)
			{

			}
			else
			{
				IEnumerable<ParseObject> resultados = t.Result;
				foreach(var resultado in resultados)
				{
					desafios.Add(resultado.ObjectId, resultado.Get<string>("nome"));
				}
				carregando = false;
			}
		});

		while (carregando)
			yield return null;

		carregando = true;
		var query = ParseObject.GetQuery ("Estatistica"); // cria a query
		query.WhereEqualTo ("aluno", ParseUser.CurrentUser); // filtra para pegara apenas as estatisticas do usuario logado
		query.FindAsync().ContinueWith(t=>
		{
			if(t.IsFaulted || t.IsCanceled)
			{
				print("faltou");
			}
			else
			{
				print("ok");
				IEnumerable<ParseObject> resultados = t.Result;
				int i = 0;
				foreach(ParseObject resultado in resultados)
				{
					print("iterando " + i);
					//print("iterando " + i);
					string idDesafio = resultado.Get<ParseObject>("desafio").ObjectId;
					print(idDesafio);
					//string nomeDesafio = resultado.Get<string>("nome");
					//print("iterando " + nomeDesafio);
					int pontuacao = resultado.Get<int>("pontuacao");
					//print(nomeDesafio + pontuacao);
					string nomeDesafio = desafios[idDesafio];
					if(!estatisticas.ContainsKey(nomeDesafio))
					{
						estatisticas.Add(nomeDesafio, pontuacao);
					}
					else
					{
						if(estatisticas[nomeDesafio] < pontuacao)
						{
							estatisticas[nomeDesafio] = pontuacao;
						}
					}
					i++;
				}
				sucesso = true;
			}
			carregando= false;

		});
		while (carregando)
						yield return null;

		
		foreach(KeyValuePair<string, int> item in estatisticas)
		{
			PlayerPrefs.SetInt(item.Key, item.Value);
		}

		while (carregando) {
			lbCarregando.text = "Carregando dados";
			yield return new WaitForSeconds(0.25f);
			lbCarregando.text = "Carregando dados.";
			yield return new WaitForSeconds(0.25f);
			lbCarregando.text = "Carregando dados..";
			yield return new WaitForSeconds(0.25f);
			lbCarregando.text = "Carregando dados...";
			yield return new WaitForSeconds(0.25f);
			yield return null;
		}
		if (sucesso)
		{
			gameObject.SetActive(false);
		}
		else
		{
			lbCarregando.text = "Erro!";
			btVoltar_Carregando.gameObject.SetActive(true);
		}
	}

	public void SetCurrentWindow(int window)
	{
		currentWin = window;
		if(currentWin == 1)
		{
			_1winLogin.SetActive(true);
			_2winCadastrar.SetActive(false);
			_3winCarregar.SetActive(false);
			_4winSingingUp.SetActive(false);
			lbWarning.text = "";
			StopCoroutine(GerarNicks());
		}
		if(currentWin == 2)
		{
			_1winLogin.SetActive(false);
			_2winCadastrar.SetActive(true);
			foreach(Toggle t in _2Toggles) 
			{
				t.transform.GetComponentInChildren<Text>().text = "-----";
				t.interactable = false;
			}
			StartCoroutine(GerarNicks());
		}
		if(currentWin == 4)
		{
			StopCoroutine(GerarNicks());
			_2winCadastrar.SetActive(false);
			_4winSingingUp.SetActive(true);
			StartCoroutine(CadastrarUsuario());

		}
	}
	IEnumerator GerarNicks()
	{
		processing = true;
		string lastString = "";
		while(true)
		{
			if(_2InputNome.value == _2OldString && _2OldString.Length >= 3 && _2OldString != lastString)
			{
				processing = true;
				lastString = _2OldString;
				_2CenterLabel.text = "Verificando...";
				foreach(Toggle t in _2Toggles) 
				{
					t.transform.GetComponentInChildren<Text>().text = "-----";
					t.interactable = false;
					
				}
				var query1 = ParseUser.Query.WhereEqualTo("nome", _2OldString);
				int count = -1;
				Task task1 = query1.CountAsync().ContinueWith(t=>
				{
					if(t.IsFaulted || t.IsCanceled)
						count = -1;
					else
					{
						count = t.Result;
					}
				
				
				});
				while(!task1.IsCompleted)
					yield return null;
				print(count);
				string subs = lastString.Substring(0, 3);
				if(count == 0)
				{
					string[] nicks = new string[3];
					_2CenterLabel.text = "Gerando Nicks...";
					do 
					{
						int[] num = new int[3];
						do{
							num[0] = Random.Range(10,100);
							num[1] = Random.Range(10,100);
							num[2] = Random.Range(10,100);
						}while(num[0] == num[1] || num[0] == num[2] || num[1] == num[2]);

						for(int i = 0; i<3; i++)
						{
							nicks[i] = subs + num[i].ToString();
						}
						var query21 = ParseUser.Query.WhereEqualTo("username",nicks[0]);
						var query22 = ParseUser.Query.WhereEqualTo("username", nicks[1]);
						var query23 = ParseUser.Query.WhereEqualTo("username", nicks[2]);
						var query2T = ParseUser.Query.Or(query21, query22, query23);
						Task task2 = query2T.CountAsync().ContinueWith(t=>
						                                              {
							if(t.IsFaulted || t.IsCanceled)
								count = -1;
							else
							{
								count = t.Result;
							}
						});
						while(!task2.IsCompleted)
							yield return null;
					}while(count != 0);
					if(_2OldString == lastString)
					{
						for(int i = 0; i< 3; i++)
						{
							_2Toggles[i].transform.GetComponentInChildren<Text>().text = nicks[i];
							_2Toggles[i].interactable = true;
							_2Toggles[i].isOn = false;
						}
						_2CenterLabel.text = "Escolha um Nick:";
						processing = false;
					}
				}
				else if(count == -1)
				{
					_2CenterLabel.text = "Falha ao conectar com o servidor!";
					_2CenterLabel.text = "  - - - - -";
					foreach(Toggle t in _2Toggles) 
					{
						t.transform.GetComponentInChildren<Text>().text = "-----";
						t.interactable = false;
						
					}
				}
				else
				{
					_2CenterLabel.text = "Erro: Ja existe um cadastro com esse nome!";
					//_2CenterLabel.text = "  - - - - -";
					foreach(Toggle t in _2Toggles) 
					{
						t.transform.GetComponentInChildren<Text>().text = "-----";
						t.interactable = false;
						
					}
				}

			}
			else if(_2OldString.Length < 3)
			{
				_2CenterLabel.text = "  - - - - -";
				foreach(Toggle t in _2Toggles) 
				{
					t.transform.GetComponentInChildren<Text>().text = "-----";
					t.interactable = false;

				}
			}
			yield return new WaitForSeconds (2f);
		}



	}
	IEnumerator CadastrarUsuario()
	{
		_4Label.text = "Cadastrando Usuario...";
		_4btvoltar.gameObject.SetActive (false);
		if(ParseUser.CurrentUser != null)
			ParseUser.LogOut();
		bool cadastrando = true;
		string warningString =  "";
		ParseUser newUser = new ParseUser (){Username = _2NickEscolhido, Password = _2NickEscolhido, Email = ""};
		newUser.Add ("nome", _2OldString);
		newUser.SignUpAsync ().ContinueWith(task=> {
		
			if(task.IsFaulted || task.IsCanceled)
			{
				warningString = "Falha ao Cadastrar.";

				print(task.Exception.Message);
				foreach(var ex in task.Exception.InnerExceptions){
					print(ex.Message);
				}
				//print(pex.HelpLink);
			}
			else
			{
				warningString = "Usuario cadastrado\ncom sucesso!";
			}
			cadastrando = false;
		});
		while(cadastrando)
			yield return null;
		_4Label.text = warningString;
		nickInputLogin.value = _2NickEscolhido;
		_4btvoltar.gameObject.SetActive(true);
	}
	public void OnChageNickCheckBox(){
		for(int i = 0;i < 3;i++)
		{
			if(_2Toggles[i].isOn){
				_2NickEscolhido = _2Toggles[i].transform.GetComponentInChildren<Text>().text;
				return;
			}
		}
		_2NickEscolhido = "";
	}
	public string GetStringNoAccents(string str)
		
	{
		/** Troca os caracteres acentuados por não acentuados **/
		string[] acentos = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
		string[] semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };
		for (int i = 0; i < acentos.Length; i++)
		{
			str = str.Replace(acentos[i], semAcento[i]);
		}
		/** Troca os caracteres especiais da string por "" **/
		string[] caracteresEspeciais = { "\\.", ",", "-", ":", "\\(", "\\)", "ª", "\\|", "\\\\", "°" };
		for (int i = 0; i < caracteresEspeciais.Length; i++)
		{
			str = str.Replace(caracteresEspeciais[i], "");
		}
		/** Troca os espaços no início por "" **/
		str = str.Replace("^\\s+", "");
		/** Troca os espaços no início por "" **/
		str = str.Replace("\\s+$", "");
		/** Troca os espaços duplicados, tabulações e etc por  " " **/
		str = str.Replace("\\s+", " ");
		return str;
	}
}
