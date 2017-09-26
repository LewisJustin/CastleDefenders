using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class DebugEditor : EditorWindow
{
	List<GameObject> enemies;
	public Object[] prefabs = new Object[2];


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

		
		for (int i = 0; i < 2; i++)
		{
			prefabs[i] = EditorGUILayout.ObjectField(prefabs[i], typeof(Object), false);
		}

		GUILayout.Label("Enemies", EditorStyles.boldLabel);
		int direction = (int)GUILayout.HorizontalSlider(1, 0, 2);

		if(GUILayout.Button("Spawn Bow Man") && Application.isPlaying)
		{
			GameObject.Find("GameManager").GetComponent<WaveSpawner>().SpawnIndividualEnemy(direction, prefabs[1]);
		}
		if(GUILayout.Button("Spawn Swordsman") && Application.isPlaying)
		{
			Debug.Log("Spawn Swordsman");
		}
		if(GUILayout.Button("Spawn Dragon") && Application.isPlaying)
		{
			Debug.Log("Spawn Dragon");
		}
	}
}