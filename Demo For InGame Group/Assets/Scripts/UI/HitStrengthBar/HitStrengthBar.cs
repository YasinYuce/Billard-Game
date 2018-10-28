using UnityEngine;
using UnityEngine.UI;

public class HitStrengthBar : MonoBehaviour 
{
	[SerializeField]
	private Image imageToFill = null;

	private float currentVal = 0f;
	public float Value { get { return currentVal; } }

	private float plusVal = 0.02f;

	/// <summary>
	/// Iterates this instance.
	/// </summary>
	public void Tick(){
		currentVal += plusVal;
		imageToFill.fillAmount = currentVal;

		if (plusVal > 0f && currentVal >= 1f) {
			currentVal = 1f;
			plusVal = -plusVal;
		}else if (plusVal < 0f && currentVal <= 0f) {
			currentVal = 0f;
			plusVal = -plusVal;
		}
	}

	/// <summary>
	/// Resets this instance.
	/// </summary>
	/// <param name="speed">Speed.</param>
	public void Reset(float? speed){
		currentVal = 0f;
		plusVal = speed.HasValue ? Mathf.Abs (speed.Value) : 0.01f;
		imageToFill.fillAmount = currentVal;
	}
}
