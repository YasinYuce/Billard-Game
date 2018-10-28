using System.Text;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HitCountWriter : MonoBehaviour 
{
	private Text hitCountText;

	private int hitCount;
	public int Count { get { return hitCount; } }

	private StringBuilder stringBuilder;

	private void Awake(){
		hitCountText = GetComponent<Text> ();
		stringBuilder = new StringBuilder ();
	}

	public void ResetInstance(){
		hitCount = 0;
		InitializeStringBuilder (hitCount);
	}

	/// <summary>
	/// Player hits the ball.
	/// </summary>
	public void BallHitByPlayer(){
		hitCount += 1;
		InitializeStringBuilder (hitCount);
	}

	/// <summary>
	/// Initializes the string builder.
	/// </summary>
	/// <param name="_value">Value.</param>
	public void InitializeStringBuilder(int _value){
		int digitCount = _value == 0 ? 1 : (int)Mathf.Log10 (_value) + 1;
		initializeStringBuilderSize (digitCount);
			
		for (int i = digitCount - 1; i >= 0; i--)
			stringBuilder [i] = CharHelper.Numbers0To9 [_value.GetDigit(digitCount - 1 - i)];

		writeToText ();
	}

	/// <summary>
	/// İnitializes the size of the string builder.
	/// </summary>
	/// <param name="digitCount">Digit count.</param>
	private void initializeStringBuilderSize (int digitCount){
		if (stringBuilder.Length != digitCount) {
			stringBuilder.Length = 0;
			stringBuilder.Append (' ', digitCount);
		}
	}

	private void writeToText(){
		hitCountText.text = stringBuilder.ToString ();
	}
}
