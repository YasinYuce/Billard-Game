using UnityEngine;

public static class IntExtend 
{
	/// <summary>
	/// Gets the digit from integer.
	/// </summary>
	/// <returns>The digit.</returns>
	/// <param name="number">Number.</param>
	/// <param name="digitIndex">Digit ındex.</param>
	public static int GetDigit(this int number, int digitIndex){
		number = number / (int)Mathf.Pow (10, digitIndex);
		return number % 10;
	}

	/// <summary>
	/// Removes the minutes, gets the second from given total time.
	/// </summary>
	/// <returns>The second.</returns>
	/// <param name="totalTime">Total time.</param>
	public static int GetSecond(this int totalTime){
		return totalTime % 60;
	}

	/// <summary>
	/// Gives total minute passed from given total time.
	/// </summary>
	/// <returns>The minute.</returns>
	/// <param name="totalTime">Total time.</param>
	public static int GetMinute(this int totalTime){
		return totalTime / 60;
	}
}
