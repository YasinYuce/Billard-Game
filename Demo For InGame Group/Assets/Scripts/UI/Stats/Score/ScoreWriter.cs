using System.Text;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreWriter : MonoBehaviour 
{
	[SerializeField]
	private GameEvent gameOverEvent = null;

	private Text scoreText;

	private int score;
	public int Score { get { return score; } }

	private StringBuilder stringBuilder;

	private void Awake(){
		scoreText = GetComponent<Text> ();
		stringBuilder = new StringBuilder ("a", 1);
	}

	public void ResetInstance(){
		score = 0;
		InitializeStringBuilder (score);
	}

	/// <summary>
	/// Changes score by given change.
	/// </summary>
	/// <param name="changeValue">Change value.</param>
	public void ScoreChanged(int changeValue){
		if (changeValue == 0)
			return;
		
		score += changeValue;
		InitializeStringBuilder (score);

		if (score >= 5)
			gameOverEvent.Raise ();
	}

	/// <summary>
	/// Initializes the string builder.
	/// </summary>
	/// <param name="_value">Value.</param>
	public void InitializeStringBuilder(int _value){
		stringBuilder [0] = CharHelper.Numbers0To9 [_value];
		writeToText ();
	}

	private void writeToText(){
		scoreText.text = stringBuilder.ToString ();
	}
}
