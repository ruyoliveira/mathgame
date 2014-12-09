using UnityEngine;
using System.Collections;

public class TimeControl : MonoBehaviour {

	public float TotalTime;
	private float timeOut;

	private bool paused;

	private CounterGameMode manager;
	private float runningTime;

	void Start()
	{
		manager = GetComponent<CounterGameMode> ();
	}

	public void ClearTimer(float TotalTime)
	{	
		this.TotalTime = TotalTime;
		timeOut = TotalTime;
		paused = true;
		runningTime = 0;
		
	}
	//atualiza o tempo limite e o tempo total
	void Update()
	{
		if(!paused)
			UpdateTime();
	}

	private void UpdateTime()
	{
		runningTime += Time.deltaTime;
		timeOut -= Time.deltaTime;
		if(timeOut <= 0)
		{
			TimesUp();
		}
		
	}
	
	//
	//Funçao que retorna o tempo limite
	public float GetTimeOut(){
		return timeOut;
	}

	public void Pause(){
		paused = true;
	}
	public void Resume(){
		paused = false;
	}
	public float GetProgressTime(){
		return (TotalTime - timeOut) / TotalTime;
	}
	
	private void TimesUp()
	{
		timeOut = 0;
		paused = true;
		manager.SendMessage ("Finished");
	}
	public bool IsCounting()
	{
		return timeOut > 0;
	}
	public float GetRunningTime()
	{
		return runningTime;
	}
}
