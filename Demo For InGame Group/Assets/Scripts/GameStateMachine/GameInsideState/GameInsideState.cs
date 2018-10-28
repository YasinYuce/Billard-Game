using UnityEngine;

public class GameInsideState : IGameState 
{
	[SerializeField]
	private GameEvent_Bool timerStartStopEvent = null;

	[SerializeField]
	private GameObject ReplayButton = null;

	private CameraRotateAroundBehaviour cameraInGameState;

	private ShootPreview shootPreview;

	private void Awake(){
		this.enabled = false;
	}

	/// <summary>
	/// Initialize this state.
	/// </summary>
	public void Initialize(){
		if (cameraInGameState == null || shootPreview == null) {
			Transform cameraTransform = GameManager.Instance.MainCamera, _player = GameManager.Instance.gameMode.Player;

			if (shootPreview == null)
				shootPreview = new ShootPreview (cameraTransform, _player);
			if(cameraInGameState == null)
				cameraInGameState = new CameraRotateAroundBehaviour (cameraTransform, _player, new Vector3 (-5f, 4f, 5f), 2.5f);
		}
	}

	public override void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space))
			SetNextState ();
		
		cameraInGameState.Tick ();
		shootPreview.Tick ();
	}

	/// <summary>
	/// Starts this state.
	/// </summary>
	public override void StartThisState ()
	{
		base.StartThisState ();
		cameraInGameState.Reset ();
		shootPreview.OpenPreview ();
		timerStartStopEvent.Raise (true);

		if (GameManager.Instance.gameMode.StateOfTable != null)
			ReplayButton.SetActive (true);
	}

	/// <summary>
	/// Stops this state.
	/// </summary>
	public override void StopThisState ()
	{
		base.StopThisState ();
		ReplayButton.SetActive (false);
		timerStartStopEvent.Raise (false);
	}

	/// <summary>
	/// Initialize and start this state.
	/// </summary>
	public void InitializeAndStartThisState ()
	{
		cameraInGameState.Initialize ();
		StartThisState ();
	}

	/// <summary>
	/// Closes the shoot preview.
	/// </summary>
	public void CloseShootPreview(){
		shootPreview.ClosePreview ();
	}

	/// <summary>
	/// Sets the state of the replay.
	/// </summary>
	public void SetReplayState(){
		StopThisState ();
		GetComponent<GameReplayState> ().StartThisState ();
	}

	/// <summary>
	/// Sets the next state.
	/// </summary>
	public override void SetNextState ()
	{
		StopThisState ();
		GetComponent<GameHittingState> ().InitializeAndStartThisState (cameraInGameState.Offset);
	}
}

