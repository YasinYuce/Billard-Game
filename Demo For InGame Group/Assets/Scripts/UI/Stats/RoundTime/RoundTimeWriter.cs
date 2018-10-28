using System.Text;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class RoundTimeWriter : MonoBehaviour 
{
	private Text roundTimeText;

	private float time;
	public float RoundTime { get { return time; } }

	private StringBuilder stringBuilder;

	private void Awake(){
		roundTimeText = GetComponent<Text> ();
		stringBuilder = new StringBuilder ("aaaaa",5);
		this.enabled = false;
	}

	public void ResetInstance(){
		time = 0f;
		InitializeStringBuilder (0f);
	}

	private void FixedUpdate(){
		time += Time.fixedDeltaTime;
		InitializeStringBuilder (time);
	}

	/// <summary>
	/// Initializes the string builder.
	/// </summary>
	/// <param name="totalTime">Total time.</param>
	public void InitializeStringBuilder(float totalTime){
		int sec = ((int)totalTime).GetSecond (), min = ((int)totalTime).GetMinute ();

		stringBuilder [0] = CharHelper.Numbers0To9 [min / 10];
		stringBuilder [1] = CharHelper.Numbers0To9 [min % 10];

		stringBuilder [2] = CharHelper.colon;

		stringBuilder [3] = CharHelper.Numbers0To9 [sec / 10];
		stringBuilder [4] = CharHelper.Numbers0To9 [sec % 10];

		//var internalValue = _stringBuilder_str_info.GetValue (stringBuilder) as string;
		writeToText();
	}

	private void writeToText(){
		roundTimeText.text = stringBuilder.ToString ();
	}
}
