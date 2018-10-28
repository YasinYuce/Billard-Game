using UnityEngine;

public class KarambolGameMode : MonoBehaviour, IGameMode 
{
	#region Variables

	[SerializeField]
	private Transform BallParent = null;

	[SerializeField]
	private GameEvent_Float ballCollisionEvent = null;


	[SerializeField]
	private GameModeInfo info = null;
	public GameModeInfo Info { get { return info; } }

	private volatile Transform player;
	private static readonly object playerLockObj = new object();
	public Transform Player {	get { 
			lock (playerLockObj) {
				return player; 
			}
		}
		set	{ 
			lock (playerLockObj) {
				player = value;
			}
		}
	}

	private Transform[] balls;
	public Transform[] Balls {	get { return balls; }	}

	private Vector3[] ballDefaultPositions;
	public Vector3[] BallDefaultPositions { get { return ballDefaultPositions; } }

	public bool IsNextTurnReady { 
		get { 
			for (int i = 0; i < balls.Length; i++)
				if (balls [i].GetComponent<Rigidbody> ().velocity != Vector3.zero)
					return false;
			return true;
		} 
	}

	public int PlayerGetPointsInTurn
	{
		get{
			int length = balls.Length;
			for (int i = 0; i < length; i++)
				if (Player != balls [i] && !balls [i].GetComponent<BallCollisionInfo> ().IsPlayerHitThisBall)
					return 0;
			return 1;
		}
	}

	private const float tableScaleX = 8f, tableScaleY = 4f;

	private TableState stateOfTable;
	public TableState StateOfTable { get { return stateOfTable; } }
	#endregion

	#region TableState
	public void SetTableState(Vector3 _cameraOffset, Vector3 _hitForce){
		Vector3[] _ballPositions = new Vector3[balls.Length];
		for (int i = 0; i < _ballPositions.Length; i++)
			_ballPositions [i] = balls [i].position;

		stateOfTable = new TableState (_ballPositions, _hitForce, _cameraOffset);
	}

	public void ResetTableState ()
	{
		stateOfTable = null;
	}
    #endregion

	#region Initialize Mode
	public void InitializeMode ()
	{
		if (!info)
			throw new System.Exception ("Game Mode Info is not set.");

		//Check for balls created before for this game mode.
		if (balls != null) {
			setBallsToDefaultPositions ();
		} 
		// if its not, then creates it.
		else { 
			balls = new Transform[info.BallInfos.Length];
			ballDefaultPositions = new Vector3[balls.Length];

			for (int i = 0; i < balls.Length; i++)
				createBall (i);
		}
	}

	/// <summary>
	/// Sets the balls to default positions.
	/// </summary>
	private void setBallsToDefaultPositions ()
	{
		for (int i = 0; i < balls.Length; i++)
			balls [i].position = ballDefaultPositions [i];
	}

	/// <summary>
	/// Creates the ball.
	/// </summary>
	/// <param name="i">The index.</param>
	private void createBall (int i)
	{
		ballDefaultPositions [i] = GetBallPosition (info.BallInfos [i].PositionRatio);

		balls [i] = Instantiate (info.BallPrefab, ballDefaultPositions [i], Quaternion.identity, BallParent);
		balls [i].GetComponent<Renderer> ().material.color = info.BallInfos [i].BallColor;

		AddComponentsToBallForThisMode (balls [i].gameObject, info.BallInfos [i].CanBePlayer);
	}

	/// <summary>
	/// Gets the ball position.
	/// </summary>
	/// <returns>The ball position.</returns>
	/// <param name="positionRatio">Position ratio.</param>
	private Vector3 GetBallPosition(Vector3 positionRatio){
		Vector3 pos = BallParent.position + new Vector3 (-tableScaleX / 2f, (info.BallPrefab.localScale.y + 0.1f) / 2f, -tableScaleY / 2f);
		pos.x += positionRatio.x * tableScaleX;
		pos.z += positionRatio.z * tableScaleY;
		return pos;
	}

	/// <summary>
	/// Adds the components to ball for this mode.
	/// </summary>
	/// <param name="ball">Ball.</param>
	/// <param name="isPlayer">If set to <c>true</c> is player.</param>
	public void AddComponentsToBallForThisMode(GameObject ball, bool isPlayer){
		ball.AddComponent<BallCollisionInfo> ();
		//Normally if more then 1, player needs to choose.
		if (!Player && isPlayer) {
			ball.AddComponent<PlayerBallCollision> ().Initialize (ballCollisionEvent);
			Player = ball.transform;
		}
		else
			ball.AddComponent<BallCollision> ().Initialize (ballCollisionEvent);
	}

	#endregion

	#region Initialize for next turn
	/// <summary>
	/// Initializes the balls for next turn.
	/// </summary>
	public void InitializeBallsForNextTurn ()
	{
		int length = balls.Length;
		for (int i = 0; i < length; i++)
			if (balls [i].GetComponent<BallCollisionInfo> ().IsPlayerHitThisBall)
				balls [i].GetComponent<BallCollisionInfo> ().InitializeForNextTurn ();
	}
	#endregion
}