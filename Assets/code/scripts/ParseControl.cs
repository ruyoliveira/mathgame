using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using Parse;
public class ParseControl: MonoBehaviour {
	public static ParseControl current;
	void Start()
	{
		current = this;
		DontDestroyOnLoad (this);
	}

	public void SavePontuation(string nome, int pontuacao, float tempo, MonoBehaviour mono)
	{
		StartCoroutine (_SavePontuation (nome, pontuacao, tempo, mono));

	}
	IEnumerator _SavePontuation(string nome, int pontuacao, float tempo, MonoBehaviour mono)
	{
		var query = ParseObject.GetQuery ("Desafio");
		query.WhereEqualTo ("nome", nome);
		bool processing = true;
		query.FirstAsync().ContinueWith(t =>
		                                {
			if(t.IsFaulted || t.IsCanceled)
			{
				
			}
			else
			{
				var desafio =(ParseObject) t.Result;
				var estatistica = ParseObject.Create("Estatistica");
				estatistica.Add("aluno", ParseUser.CurrentUser);
				estatistica.Add("desafio", desafio);
				estatistica.Add("pontuacao", pontuacao);
				estatistica.Add("tempo", tempo);
				Task task = estatistica.SaveAsync();
				while(!task.IsCompleted);
				processing = false;
			}
		});
		while(processing)
			yield return null;
		mono.SendMessage("ParseDone");
	}
}
