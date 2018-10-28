using UnityEngine;
using UnityEngine.Events;

public abstract class GameEventListener_OneArg<T, GameEventt> : MonoBehaviour
	where GameEventt : GameEvent_OneArg<T>
{
	public GameEventt Event;
	public abstract SerializableUnityEvent response { get; }

	void OnEnable(){
		Event.RegisterListener (response.Invoke);
	}

	void OnDisable(){
		Event.UnRegisterListener (response.Invoke);
	}

	//i tried to serialize with direct refrence to base class in different ways, didnt worked.
	[System.Serializable]
	public abstract class SerializableUnityEvent : UnityEvent<T>{ }
}