using UnityEngine;

public class GameMenuState : IGameState 
{
	private CameraTransitionBehaviour cameraTransition;

	[SerializeField]
	private GameObject MainMenu = null;

	void Awake(){
		this.enabled = false;
	}
		
	/// <summary>
	/// Initialize this state.
	/// </summary>
	public void Initialize(){
		if (cameraTransition == null) {
			Transform cameraTransform = GameManager.Instance.MainCamera;
			cameraTransition = new CameraTransitionBehaviour (cameraTransform, GameManager.Instance.Table, cameraTransform.position - Vector3.up * 2.45f, cameraTransform.rotation);
		}
	}
	
	public override void Update ()
	{
		cameraTransition.Tick ();

		if (cameraTransition.IsReached) {
			if(!MainMenu.activeSelf)
				MainMenu.SetActive (true);
			StopThisState ();
		}
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
		GetComponent<GameWaitState> ().StartThisState ();
	}
}
