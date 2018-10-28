using UnityEngine;

public class GameHittingState : IGameState 
{
	[SerializeField]
	private GameEvent BallHitByPlayerEvent = null;

	[SerializeField]
	private GameEvent_Bool timerStartStopEvent = null;

	[SerializeField]
	private GameEvent_Float ballCollisionEvent = null;

	[SerializeField]
	private HitStrengthBar strengthBar = null;

	[SerializeField]
	private KeyCode hitKeyCode = KeyCode.Space;

	private Vector3 offset;

	private readonly static float MaxHitLength = 100f;

	private IGameMode gameMode;

	private void Awake(){
		this.enabled = false;
		gameMode = GameManager.Instance.gameMode;
	}

	public override void Update ()
	{
		if (Input.GetKeyUp (hitKeyCode)) {
			Vector3 force = (GameManager.Instance.gameMode.Player.position - GameManager.Instance.MainCamera.position);
			force.y = 0f;
			float barValue = strengthBar.Value;
			force = force.normalized * MaxHitLength * barValue;

			GameManager.Instance.gameMode.Player.GetComponent<Rigidbody> ().AddForce (force);

			BallHitByPlayerEvent.Raise ();
			ballCollisionEvent.Raise (barValue);

			gameMode.SetTableState (offset, force);
			SetNextState ();
			return;
		}

		strengthBar.Tick ();
	}

	/// <summary>
	/// Starts this state.
	/// </summary>
	public override void StartThisState ()
	{
		strengthBar.gameObject.SetActive (true);
		strengthBar.Reset (null);
		timerStartStopEvent.Raise (true);
		base.StartThisState ();
	}

	/// <summary>
	/// Stops this state.
	/// </summary>
	public override void StopThisState ()
	{
		base.StopThisState ();
		strengthBar.gameObject.SetActive (false);
		timerStartStopEvent.Raise (false);
	}

	/// <summary>
	/// Sets the next state.
	/// </summary>
	public override void SetNextState ()
	{
		StopThisState ();
		GetComponent<GameWatchState> ().InitializeAndStartThisState (offset);
	}

	/// <summary>
	/// Initializes and starts this state.
	/// </summary>
	/// <param name="_offset">Offset.</param>
	public void InitializeAndStartThisState (Vector3 _offset)
	{
		offset = _offset;
		StartThisState ();
	}
}
