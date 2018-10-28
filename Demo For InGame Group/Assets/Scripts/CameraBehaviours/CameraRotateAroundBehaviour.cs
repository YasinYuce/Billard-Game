using UnityEngine;

public class CameraRotateAroundBehaviour : CameraBehaviour 
{
	private const string Axis = "Mouse X";

	private float turnSpeed;
	public float TurnSpeed { get { return turnSpeed; } }

	/// <summary>
	/// Iterates this instance.
	/// </summary>
	public override void Tick ()
	{
		if (Input.GetKeyDown (KeyCode.Mouse1)) {
			startTime = Time.time - 0.01f;
			focusPos = focusTransform.position;
			cameraPos = cameraTransform.position;
		} else if (Input.GetKey (KeyCode.Mouse1)) {
			offset = Quaternion.AngleAxis (Input.GetAxis (Axis) * turnSpeed, Vector3.up) * offset;
			distance = Vector3.Distance (cameraTransform.position, focusTransform.position + offset);
			lookRotation = Quaternion.LookRotation (-offset);
			targetPos = focusPos + offset;
		}

		base.Tick ();
	}

	/// <summary>
	/// Initialize this instance.
	/// </summary>
	public override void Initialize ()
	{
		base.Initialize ();
		lookRotation = Quaternion.LookRotation (-offset);
	}

	public CameraRotateAroundBehaviour (Transform _cameraTransform, Transform _focusTransform, Vector3 _offset, float _turnSpeed) : base(_cameraTransform, _focusTransform, _offset){
		turnSpeed = _turnSpeed;
		lookRotation = Quaternion.LookRotation (-_offset);
	}
}