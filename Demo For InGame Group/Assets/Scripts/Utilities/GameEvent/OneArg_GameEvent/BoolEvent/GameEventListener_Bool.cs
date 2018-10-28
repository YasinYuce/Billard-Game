using UnityEngine;

public class GameEventListener_Bool : GameEventListener_OneArg <bool, GameEvent_Bool> 
{
	public BoolSerializableUnityEvent Response;
	public override SerializableUnityEvent response {
		get {
			return Response;
		}
	}

	[System.Serializable]
	public class BoolSerializableUnityEvent : SerializableUnityEvent {}
}