using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class BallCollisionInfo : MonoBehaviour 
{
	public bool IsPlayerHitThisBall { get; private set; }

	/// <summary>
	/// Hits the by player.
	/// </summary>
	public void HitByPlayer(){
		IsPlayerHitThisBall = true;
	}

	/// <summary>
	/// Initializes for next turn.
	/// </summary>
	public void InitializeForNextTurn(){
		IsPlayerHitThisBall = false;
	}
}
