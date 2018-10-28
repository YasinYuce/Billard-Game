using UnityEngine;

public abstract class CameraBehaviour 
{
	#region Variables
	protected Transform cameraTransform;

	protected Transform focusTransform;
	public Transform FocusTransform { get { return focusTransform; } }

	protected Vector3 offset, defaultOffset;
	public Vector3 Offset { get { return offset; } }

	protected Quaternion cameraRot;
	public Quaternion lookRotation { get; protected set; }

	protected Vector3 focusPos,	cameraPos, targetPos; 

	protected float startTime, distance, perc;
	protected const float lerpSpeed = 6f;
	#endregion

	/// <summary>
	/// Iterates this instance.
	/// </summary>
	public virtual void Tick(){
		perc = (Time.time - startTime) / distance * lerpSpeed;

		cameraTransform.position = Vector3.Lerp (cameraPos, targetPos, perc);
		cameraTransform.rotation = Quaternion.Lerp (cameraRot, lookRotation, perc);
	}

	/// <summary>
	/// Reset the Camera Behaviour.
	/// </summary>
	/// <param name="_offset">Offset.</param>
	/// <param name="_lookRotation">Look rotation.</param>
	public virtual void Reset(Vector3? _offset = null, Quaternion? _lookRotation = null){
		lookRotation = _lookRotation.HasValue ? _lookRotation.Value : lookRotation;
		offset = _offset.HasValue ? _offset.Value : offset;
		startTime = Time.time - 0.01f;
		distance = _offset.HasValue ? 6f : Vector3.Distance (cameraTransform.position, focusTransform.position + offset);
		focusPos = focusTransform.position;
		cameraPos = cameraTransform.position;
		targetPos = focusPos + offset;
		cameraRot = cameraTransform.rotation;
	}

	/// <summary>
	/// Initialize this instance.
	/// </summary>
	public virtual void Initialize(){
		if (offset != defaultOffset)
			offset = defaultOffset;
		Reset ();
	}

	public CameraBehaviour(Transform _cameraTransform, Transform _focusTransform, Vector3 _offset){
		cameraTransform = _cameraTransform;
		focusTransform = _focusTransform;
		defaultOffset = _offset;
	}
}
