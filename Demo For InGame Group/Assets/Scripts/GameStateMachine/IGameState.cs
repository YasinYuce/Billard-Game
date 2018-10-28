using UnityEngine;

public abstract class IGameState : MonoBehaviour
{
	/// <summary>
	/// Update this state.
	/// </summary>
	public abstract void Update ();

	/// <summary>
	/// Sets the next state.
	/// </summary>
	public abstract void SetNextState ();

	/// <summary>
	/// Starts this state.
	/// </summary>
	public virtual void StartThisState(){
		this.enabled = true;
	}

	/// <summary>
	/// Stops this state.
	/// </summary>
	public virtual void StopThisState(){
		this.enabled = false;
	}
}
