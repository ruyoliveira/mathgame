using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;  
using System;

public class ChallengesManager : MonoBehaviour {

	public int numChallenges;

	public Challenge[] challenges;

	public GameObject challengePrefab;

	public Transform spawnPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void LoadChallenges()
	{
		Load ("Assets/Fase1.txt");
		/*
		this.numChallenges = 5;

		this.challenges = new Challenge[this.numChallenges];
		//0
		GameObject challenge = Instantiate(challengePrefab, spawnPoint.position,spawnPoint.rotation) as GameObject;	//cria um challenge e armazena
	
		SetupChallenge(challenge.GetComponent<Challenge>(),5,12,new int[2]{5,3}, new int[1]{7}, new string[1]{"+"});

		challenges[0] = challenge.GetComponent<Challenge>();
		//1
		challenge = Instantiate(challengePrefab, spawnPoint.position,spawnPoint.rotation) as GameObject;	//cria um challenge e armazena
		
		SetupChallenge(challenge.GetComponent<Challenge>(),9,21,new int[2]{7,9}, new int[1]{12}, new string[1]{"+"});
		
		challenges[1] = challenge.GetComponent<Challenge>();
		//2
		challenge = Instantiate(challengePrefab,spawnPoint.position,spawnPoint.rotation) as GameObject;	//cria um challenge e armazena
		
		SetupChallenge(challenge.GetComponent<Challenge>(),12,19,new int[2]{12,14}, new int[1]{7}, new string[1]{"+"});
		
		challenges[2] = challenge.GetComponent<Challenge>();
	
		//3
		challenge = Instantiate(challengePrefab,spawnPoint.position,spawnPoint.rotation) as GameObject;	//cria um challenge e armazena
		
		SetupChallenge(challenge.GetComponent<Challenge>(),8,17,new int[2]{8,6}, new int[1]{9}, new string[1]{"+"});
		
		challenges[3] = challenge.GetComponent<Challenge>();

		//4
		challenge = Instantiate(challengePrefab,spawnPoint.position,spawnPoint.rotation) as GameObject;	//cria um challenge e armazena
		
		SetupChallenge(challenge.GetComponent<Challenge>(),6,19,new int[2]{6,4}, new int[1]{13}, new string[1]{"+"});
		
		challenges[4] = challenge.GetComponent<Challenge>();
		*/
		/*
		for( int i = 0; i < numChallenges ; i++)
		{
			GameObject challenge = Instantiate(challengePrefab,transform.position,transform.rotation) as GameObject;	//cria um challenge e armazena

			SetupChallenge(challenge,5,12,, { 2 }, {"+"});
		}*/
	}
	public void SetupChallenge(Challenge c, int correctAnswer, int functionResult, int[] functionOptions, int[] functionElems, string[] functionOp)
	{
		c.correctAnswer = correctAnswer;
		c.functionResult = functionResult;
		c.functionOptions = functionOptions;
		c.functionElems = functionElems;
		c.functionOp = functionOp;
		c.Setup();
	}


	public void  DoStuff(string[] entries)
	{

	}
	//Carrega o numero de desafios do nivel
	public void LoadNumChallenge(string num)
	{
		this.numChallenges = int.Parse(num);
		this.challenges = new Challenge[numChallenges];
	}
	//Carrega cada desafio do nivel
	public void LoadChallenge(string[] challenge, int i)
	{
		GameObject c = Instantiate(challengePrefab, spawnPoint.position,spawnPoint.rotation) as GameObject;	//cria um challenge e armazena;
		int correct = int.Parse (challenge[0]);
		int resul = int.Parse (challenge[1]);
		int[] opts = new int[2];
		opts[0] = int.Parse (challenge[2]);
		opts[1] = int.Parse (challenge[3]);

		int[] elems = new int[1];
		elems[0] = int.Parse (challenge[4]);

		string[] op = new string[1];
		op[0] = challenge[5];

		SetupChallenge(c.GetComponent<Challenge>(), correct, resul, opts, elems, op);

		this.challenges[i] = c.GetComponent<Challenge>();	//armazena o desafio na lista de desafios
	}
	private bool Load(string fileName)
	{
		// Handle any problems that might arise when reading the text
		try
		{
			string line;
			// Create a new StreamReader, tell it which file to read and what encoding the file
			// was saved as
			StreamReader theReader = new StreamReader(fileName, Encoding.Default);
			
			// Immediately clean up the reader after this block of code is done.
			// You generally use the "using" statement for potentially memory-intensive objects
			// instead of relying on garbage collection.
			// (Do not confuse this with the using directive for namespace at the 
			// beginning of a class!)
			using (theReader)
			{
				int i = 0;	//contador de linhas nao vazias
				// While there's lines left in the text file, do this:
				do
				{
					line = theReader.ReadLine();
					
					if (line != null)
					{
						// Do whatever you need to do with the text line, it's a string now
						// In this example, I split it into arguments based on comma
						// deliniators, then send that array to DoStuff()
						string[] entries = line.Split(',');

						if (entries.Length == 1)	//desafio
						{
							LoadNumChallenge(entries[0]);
							++i;

						}
						else if(entries.Length > 1)//numero de desafios
						{
							Debug.Log("oi");
							LoadChallenge(entries, i-1);
							++i;


						}

					}

				}
				while (line != null);
				
				// Done reading, close the reader and return true to broadcast success    
				theReader.Close();
				return true;
			}
		}
		
		// If anything broke in the try block, we throw an exception with information
		// on what didn't work
		catch (Exception e)
		{
			Debug.Log("{0}\n"+ e.Message);
			return false;
		}
	}
}

