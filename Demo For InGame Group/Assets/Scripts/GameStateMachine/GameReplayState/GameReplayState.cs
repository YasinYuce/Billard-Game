using UnityEngine;
using System.Collections;

public class GameReplayState : IGameState 
{
	private IGameMode gameMode;
	private TableState stateOfTable;

	private CameraTransitionBehaviour cameraTransition;

	private float startTime;

	private void Awake(){
		gameMode = GameManager.Instance.gameMode;
		this.enabled = false;
	}

	/// <summary>
	/// Initialize this state.
	/// </summary>
	public void Initialize(){
		if (cameraTransition == null) {
			cameraTransition = new CameraTransitionBehaviour (GameManager.Instance.MainCamera, 
				gameMode.Player, 
				Vector3.zero, 
				Quaternion.identity);
		}
	}

	/// <summary>
	/// Starts this state.
	/// </summary>
	public override void StartThisState ()
	{
		base.StartThisState ();

		startTime = Time.time + 2f;

		stateOfTable = gameMode.StateOfTable;

		// gets the last state balls and positions
		Vector3[] ballsPositions = stateOfTable.BallPositions;
		Transform[] balls = gameMode.Balls;

		for (int i = 0; i < balls.Length; i++)
			balls [i].position = ballsPositions [i];

		Vector3 offset = stateOfTable.Cameraoffset;
		cameraTransition.Reset (offset, Quaternion.LookRotation (-offset));

		StartCoroutine (delayedShoot ());
  	}

	/// <summary>
	/// Delayeds the shoot for replay.
	/// </summary>
	/// <returns>The shoot.</returns>
	private IEnumerator delayedShoot(){
		yield return new WaitForSeconds (1.5f);
		gameMode.Player.GetComponent<Rigidbody> ().AddForce (stateOfTable.HitForce);
	}

	public override void Update ()
	{
		if(!cameraTransition.IsReached)
			cameraTransition.Tick ();

		if (startTime > Time.time)
			return;

		if (gameMode.IsNextTurnReady) {
			SetNextState ();
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
}
