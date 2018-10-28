using UnityEngine;

[CreateAssetMenu(menuName = "Game Mode/New GameMode Info")]
public class GameModeInfo : ScriptableObject 
{
	[SerializeField]
	private Transform ballPrefab = null;
	public Transform BallPrefab { get {
			if(!ballPrefab)
				throw new System.Exception ("Ball prefab is not set in Game Mode Info :" + name);
			return ballPrefab; } }

	[SerializeField]
	private BallInitializeInfo[] ballInfos = null;
	public BallInitializeInfo[] BallInfos { get { return ballInfos; } }
	
}

[System.Serializable]
public class BallInitializeInfo{
	public Vector3 PositionRatio;
	public Color BallColor;
	public bool CanBePlayer;
}