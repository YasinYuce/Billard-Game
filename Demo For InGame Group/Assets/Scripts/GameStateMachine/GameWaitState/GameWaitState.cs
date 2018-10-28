using UnityEngine;

public class GameWaitState : IGameState 
{
	private CameraTransitionBehaviour cameraTransition;

	private void Awake(){
		this.enabled = false;
	}

	/// <summary>
	/// Initialize this state.
	/// </summary>
	public void Initialize(){
		if (cameraTransition == null) {
			Transform cameraTransform = GameManager.Instance.MainCamera;
			Vector3 offset = new Vector3 (-5f, 4f, 5f);
			cameraTransition = new CameraTransitionBehaviour (cameraTransform, GameManager.Instance.gameMode.Player, offset, Quaternion.LookRotation(-offset));
		}
	}

	public override void Update ()
	{
		cameraTransition.Tick ();
		if (cameraTransition.IsReached)
			SetNextState ();
	}

	/// <summary>
	/// Starts this state.
	/// </summary>
	public override void StartThisState ()
	{
		cameraTransition.Initialize ();
		base.StartThisState ();
	}

	/// <summary>
	/// Sets the next state.
	/// </summary>
	public override void SetNextState ()
	{
		StopThisState ();
		GetComponent<GameInsideState> ().InitializeAndStartThisState ();
	}
}
