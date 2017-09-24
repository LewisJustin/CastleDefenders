using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class DebugEditor : EditorWindow
{
	List<GameObject> enemies;

	[MenuItem("Window/Debug")]
	public static void ShowWindow()
	{
		GetWindow<DebugEditor>("Debug");
	}

	void OnGUI()
	{
		GUILayout.Label("Cheat Mode", EditorStyles.boldLabel);
		GUILayout.BeginHorizontal();
			if (GUILayout.Button("Coins"))
			{
				Debug.Log("Give Me Coins!!!");
			}

			if (GUILayout.Button("Bow Speed!"))
			{
				Debug.Log("Give Me Bow Speed!!!");
			}

			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();

			if(GUILayout.Button("Give Me Power!"))
			{
				Debug.Log("Add bow damage!!");
			}

			if (GUILayout.Button("Castle Armor!"))
			{
				Debug.Log("Give Me Castle Armor!!!");
			}
		GUILayout.EndHorizontal();
	
		GUILayout.Label("Enemies", EditorStyles.boldLabel);

		if(GUILayout.Button("Refresh Enemies"))
		{
			enemies.Clear();
			for (int i = 0; i < GameObject.Find("EnemyParent").transform.childCount; i++)
			{
				enemies.Add(GameObject.Find("EnemyParent").transform.GetChild(i).gameObject);
			}

			for (int i = 0; i < GameObject.Find("EnemyParent").transform.childCount; i++)
			{
				Debug.Log(enemies[i]);
				EditorGUILayout.TextArea(enemies[i] + "");
			}
		}
		
	}
}