using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlayerBallCollision : BallCollision 
{

	/// <summary>
	/// Raises the collision enter event.
	/// Checks For ball collision and marks the other ball.
	/// </summary>
	/// <param name="collision">Collision.</param>
	protected override void OnCollisionEnter (Collision collision)
	{
		base.OnCollisionEnter (collision);
		if (collision.transform.CompareTag ("Balls")) {
			BallCollisionInfo ballColInfo = collision.transform.GetComponent<BallCollisionInfo> ();
			if (ballColInfo != null && !ballColInfo.IsPlayerHitThisBall)
				ballColInfo.HitByPlayer ();
		}
	}
}
