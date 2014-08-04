using UnityEngine;
using System.Collections;

public class TimeControl : MonoBehaviour {

	public float TotalTime;
	private float timeOut;

	private bool paused;

	public EventDelegate OnTimesUp;

	public void ClearTimer(float TotalTime)
	{	
		this.TotalTime = TotalTime;
		timeOut = TotalTime;
		paused = true;
		
	}
	//atualiza o tempo limite e o tempo total
	void Update()
	{
		if(!paused)
			UpdateTime();
	}

	private void UpdateTime()
	{
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
		OnTimesUp.Execute ();
	}
	public bool IsCounting()
	{
		return timeOut > 0;
	}
}
