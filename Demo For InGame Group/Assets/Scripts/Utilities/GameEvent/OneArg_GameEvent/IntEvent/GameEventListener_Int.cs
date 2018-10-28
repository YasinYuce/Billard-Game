using UnityEngine;

public class GameEventListener_Int : GameEventListener_OneArg <int, GameEvent_Int> 
{
	public IntSerializableUnityEvent Response;
	public override SerializableUnityEvent response {
		get {
			return Response;
		}
	}

	[System.Serializable]
	public class IntSerializableUnityEvent : SerializableUnityEvent {}
}
