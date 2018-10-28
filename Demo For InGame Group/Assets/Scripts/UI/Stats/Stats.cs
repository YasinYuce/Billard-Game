using UnityEngine;

public class Stats : MonoBehaviour
{
	[SerializeField]
	private ScoreWriter score = null;

	[SerializeField]
	private HitCountWriter hitCount = null;

	[SerializeField]
	private RoundTimeWriter roundTime = null;

	private const string lastStatsString = "LastStats";

	private void Awake(){
		WriteLastStats ();
	}

	public void WriteLastStats(){
		StatsInfo info = JsonUtility.FromJson<StatsInfo> (
			PlayerPrefs.GetString (lastStatsString, "{}")
		);

		score.InitializeStringBuilder (info.Score);
		hitCount.InitializeStringBuilder (info.HitCount);
		roundTime.InitializeStringBuilder (info.RoundTime);
	}

	private void OnApplicationQuit(){
		SaveLastStats ();
	}

	public void SaveLastStats(){
		PlayerPrefs.SetString (lastStatsString, JsonUtility.ToJson(new StatsInfo(score.Score, hitCount.Count, roundTime.RoundTime)));
	}

	#region Stats Info
	struct StatsInfo {
		
		public int Score;
		public int HitCount;

		public float RoundTime;

		public StatsInfo (int _score, int _hitCount, float _roundTime){
			Score =_score;
			HitCount =_hitCount;
			RoundTime = _roundTime;
		}
	}
	#endregion
}
