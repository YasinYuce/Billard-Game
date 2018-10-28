using UnityEngine;

public class GameWatchState : IGameState 
{
	[SerializeField]
	private GameEvent_Int scoreChangeEvent = null;
	
	private CameraTransitionBehaviour cameraTransition;

	private IGameMode gameMode;

	private float startTime;

	private void Awake(){
		cameraTransition = new CameraTransitionBehaviour (GameManager.Instance.MainCamera, GameManager.Instance.Table, Vector3.zero, Quaternion.identity);
		gameMode = GameManager.Instance.gameMode;

		this.enabled = false;
	}

	public override void Update ()
	{
		cameraTransition.Tick ();

		if (startTime > Time.time)
			return;

		if (gameMode.IsNextTurnReady) {
			SetNextState ();
			scoreChangeEvent.Raise (gameMode.PlayerGetPointsInTurn);
			gameMode.InitializeBallsForNextTurn ();
		}
	}

	/// <summary>
	/// Sets the next state.
	/// </summary>
	public override void SetNextState ()
	{
		StopThisState ();
		GetComponent<GameInsideState> ().StartThisState ();
	}

	/// <summary>
	/// Initializes and starts this state.
	/// </summary>
	/// <param name="_offset">Offset.</param>
	public void InitializeAndStartThisState (Vector3 _offset){
		Vector3 offset = GameManager.Instance.gameMode.Player.position - cameraTransition.FocusTransform.position + _offset;
		cameraTransition.Reset (offset, Quaternion.LookRotation (-offset));
		startTime = Time.time + 2f;
		StartThisState ();
	}
}
