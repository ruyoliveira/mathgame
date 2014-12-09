using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Parse;
public class ParseControl: MonoBehaviour {

	public static ParseControl current;
	public static bool usingParse;
	public bool _usingParse;

	void Start()
	{
		usingParse = _usingParse;
		if(usingParse)
		{
			current = this;
			DontDestroyOnLoad (this);
		}
	}

	public void SaveScore(string levelName, int score, float time, IParseComunication sender)
	{
		print ("*********** Saving Score **************");
		StartCoroutine (_SaveScore (levelName, score, time, sender));

	}
	IEnumerator _SaveScore(string levelName, int score, float time, IParseComunication sender)
	{
		print ("searching by: " + levelName);
		bool processing = true;
		var query = ParseObject.GetQuery ("Desafio").WhereEqualTo("nome", levelName);
		//query.WhereEqualTo ("nome", nome.ToString());
		query.FirstAsync().ContinueWith(t =>
		                                {
			if(t.IsFaulted || t.IsCanceled)
			{
				print("Not found: " + levelName);
			}
			else
			{

				ParseObject desafio =  t.Result;
				print(desafio.Get<string>("nome"));
				var estatistica = ParseObject.Create("Estatistica");
				estatistica.Add("aluno", ParseUser.CurrentUser);
				estatistica.Add("desafio", desafio);
				estatistica.Add("pontuacao", score);
				estatistica.Add("tempo", time);

				Task task = estatistica.SaveAsync();
				while(!task.IsCompleted);
				processing = false;
			}
		});

		while(processing)
			yield return null;
		print ("----------Save Score Done----------");
		sender.ParseDone ();
	}
}
