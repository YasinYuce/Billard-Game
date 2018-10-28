using System;
using UnityEngine;
using System.Collections.Generic;

public abstract class GameEvent_OneArg <T> : ScriptableObject 
{
	private List<Action<T>> listeners = new List<Action<T>>();

	public void Raise(T t){
		for (int i = listeners.Count - 1; i >= 0; i--)
			listeners [i].Invoke (t);
	}

	public void RegisterListener(Action<T> listenerAction){
		if (!listeners.Contains (listenerAction))
			listeners.Add (listenerAction);
	}

	public void UnRegisterListener(Action<T> listenerAction){
		if (listeners.Contains (listenerAction))
			listeners.Remove (listenerAction);
	}
}