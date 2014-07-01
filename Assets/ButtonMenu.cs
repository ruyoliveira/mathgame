using UnityEngine;
using System.Collections;

public class ButtonMenu : MonoBehaviour {
	private TweenRotation tRotation;
	private TweenScale tScale;
	// Use this for initialization
	void Start () 
	{
		tRotation = GetComponent<TweenRotation> ();
		tScale = GetComponent<TweenScale> ();
	}

	void OnHover(bool over)
	{
		if (over)
		{
			tRotation.ResetToBeginning();
			tRotation.PlayForward();

			tScale.ResetToBeginning();
			tScale.Play();
		}
		else
		{
			tScale.enabled = false;
		}
	}

}
