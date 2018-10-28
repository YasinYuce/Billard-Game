using UnityEngine;

[RequireComponent(typeof(IGameMode))]
public class GameManager : MonoBehaviour 
{
	public static GameManager Instance { get; private set; }

	[SerializeField]
	private Transform table = null;
	public Transform Table { get { return table; } }

	[SerializeField]
	private Transform mainCamera = null;
	public Transform MainCamera { get { return mainCamera; }}

	public IGameMode gameMode { get; private set; }

	private void Awake(){
		if (Instance == null)
			Instance = this;
		else
			Destroy (this);

		gameMode = GetComponent<IGameMode> ();
	}
}
