using UnityEngine;

public class GameEventListener_Float : GameEventListener_OneArg <float, GameEvent_Float> 
{
	public FloatSerializableUnityEvent Response;
	public override SerializableUnityEvent response {
		get {
			return Response;
		}
	}

	[System.Serializable]
	public class FloatSerializableUnityEvent : SerializableUnityEvent {}
}
