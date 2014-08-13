using UnityEngine;
using System.Collections;

public class VisibilityEvent : MonoBehaviour {

	public EventDelegate becameInvisible;
	public EventDelegate becameVisible;
	void OnBecameInvisible()
	{
		becameInvisible.Execute();
	}
	void OnBecameVisible()
	{
		becameVisible.Execute();
	}
}
