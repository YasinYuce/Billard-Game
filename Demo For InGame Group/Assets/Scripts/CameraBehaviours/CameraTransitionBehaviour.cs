using UnityEngine;

public class CameraTransitionBehaviour : CameraBehaviour 
{
	public bool IsReached { get { 
			if (Vector3.Distance (cameraTransform.position, targetPos) < 0.01f)
				return true;
			return false;
		} 
	}

	public CameraTransitionBehaviour (Transform _cameraTransform, Transform _focusTransform, Vector3 _offset, Quaternion _lookRotation) : base (_cameraTransform, _focusTransform, _offset){ 
		lookRotation = _lookRotation;
	}
}
