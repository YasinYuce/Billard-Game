using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameEvent))]
public class GameEventCustomInspector : Editor 
{

	GameEvent targett;
	void OnEnable(){
		targett = (GameEvent)target;
	}

	public override void OnInspectorGUI ()
	{
		if (GUILayout.Button ("Raise Event", GUILayout.Height(30f))) {
			targett.Raise ();
		}
	}
}
