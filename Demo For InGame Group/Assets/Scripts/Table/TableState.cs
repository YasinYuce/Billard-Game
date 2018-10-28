using UnityEngine;

public class TableState 
{
	private Vector3[] ballPositions;
	public Vector3[] BallPositions { get { return ballPositions; } }

	private Vector3 hitForce;
	public Vector3 HitForce { get { return hitForce; } }

	private Vector3 cameraOffset;
	public Vector3 Cameraoffset { get { return cameraOffset; } }

	public TableState(Vector3[] _ballPositions, Vector3 _hitForce, Vector3 _cameraOffset){
		cameraOffset = _cameraOffset;
		ballPositions = _ballPositions;
		hitForce = _hitForce;
	}
}
