using UnityEngine;

public class ShootPreview 
{
	private GameObject previewGo;
	private LineRenderer lineRenderer;

	private Transform focusTransform, cameraTransform;


	public void OpenPreview(){
		previewGo.SetActive (true);
	}

	public void ClosePreview(){
		previewGo.SetActive (false);
	}

	public void Tick(){
		rayCastTheBoard ();
	}

	/// <summary>
	/// Detirmines the balls destination.
	/// </summary>
	private void rayCastTheBoard ()
	{
		RaycastHit hit;
		float playerHalfScale = (focusTransform.localScale.x / 2f);
		Vector3 directionn = (focusTransform.position - new Vector3 (cameraTransform.position.x, focusTransform.position.y, cameraTransform.position.z)).normalized;
		if (Physics.SphereCast (new Ray (focusTransform.position, directionn), playerHalfScale, out hit)) {
			Vector3 pos = hit.point + hit.normal.normalized * playerHalfScale;
			previewGo.transform.position = pos;
			lineRenderer.SetPosition (0, hit.transform.CompareTag ("Balls") ? pos : pos + Vector3.Reflect (directionn, hit.normal) * 0.5f);
			lineRenderer.SetPosition (1, pos);
			lineRenderer.SetPosition (2, focusTransform.position + focusTransform.localScale.x / 2f * directionn);
		}
	}

	public ShootPreview(Transform _camera, Transform _focusTransform){
		cameraTransform = _camera;
		focusTransform = _focusTransform;
		previewGo = Resources.Load<GameObject> ("Prefabs/ShootPreview");
		previewGo = GameObject.Instantiate (previewGo);
		lineRenderer = previewGo.GetComponent<LineRenderer> ();
		ClosePreview ();
	}
}
