using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class BallCollision : MonoBehaviour
{
	private GameEvent_Float ballCollisionEvent;

	/// <summary>
	/// Initialize the BallCollision.
	/// </summary>
	/// <param name="_ballCollisionEvent">Ball collision event.</param>
	public void Initialize(GameEvent_Float _ballCollisionEvent){
		ballCollisionEvent = _ballCollisionEvent;
	}

	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name="collision">Collision.</param>
	protected virtual void OnCollisionEnter(Collision collision){
		if(!collision.transform.CompareTag("TableFloor"))
			ballCollisionEvent.Raise (collision.impulse.magnitude);
	}
}

