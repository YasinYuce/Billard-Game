using UnityEngine;

public interface IGameMode 
{
	GameModeInfo Info { get; }

	Transform Player { get; }

	Transform[] Balls { get; }

	Vector3[] BallDefaultPositions { get; }

	bool IsNextTurnReady { get; }

	int PlayerGetPointsInTurn { get; }

	TableState StateOfTable { get; }

	void InitializeMode ();

	void AddComponentsToBallForThisMode (GameObject ball, bool isPlayer);

	void InitializeBallsForNextTurn ();

	void SetTableState (Vector3 _cameraOffset, Vector3 _hitForce);

	void ResetTableState ();
}
