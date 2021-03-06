﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelsParameters : MonoBehaviour {
	public int Word;
	public string[] Questions;
	public int[] ChallengeTimeLimits;
	public int[] Answers;
	public string[] GenerateObjects;

	private int CurrentLevel;
	// Use this for initialization
	void Start () 
	{
		CounterGameMode counterGameMode = GetComponent<CounterGameMode> ();
		CurrentLevel = PlayerPrefs.GetInt ("CurrentLevel", 1);
		print (CurrentLevel);
		if(Word ==3 ) CurrentLevel = 1;


		GameObject.Find("Problema").GetComponent<Text>().text = Questions[CurrentLevel-1]; // set question
		counterGameMode.challengeTimeLimit = ChallengeTimeLimits[CurrentLevel-1];
		counterGameMode.correctAmount = Answers[CurrentLevel-1];


		if (Word == 1)
		{

			var parameters = GenerateObjects[CurrentLevel-1].Split(';');
			var objectsManager = GameObject.Find("GeradorDeObjetos").GetComponent<ObjectsManager>();
			for(int i = 0; i < objectsManager.objPrefabs.Length; i++)
			{
				objectsManager.amounts[i] = int.Parse(parameters[i]);
			}
			objectsManager.Init();
		}
		else if(Word == 3)
		{

			//print(CurrentLevel);
			var parameters = GenerateObjects[CurrentLevel-1].Split(';');

			var objectsManager = GameObject.Find("GeradorDeObjetos").GetComponent<ObjectsGeneratorMatriz>();
			for(int i = 0; i < objectsManager.prefabs.Length; i++)
			{
				objectsManager.amounts[i] = int.Parse(parameters[i]);
			}
			objectsManager.Init();
		}
		counterGameMode.Init();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
