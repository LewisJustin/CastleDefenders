using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class DebugEditor : EditorWindow
{
	List<GameObject> enemies;
	public Transform[] prefabs = new Transform[2];

	int hSliderValue;

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
			prefabs[i] = (Transform)EditorGUILayout.ObjectField(prefabs[i], typeof(Transform), false);
		}

		GUILayout.Label("Enemies", EditorStyles.boldLabel);
		hSliderValue = (int)GUILayout.HorizontalSlider(hSliderValue, 0f, 2f);

		if(GUILayout.Button("Spawn Bow Man") && Application.isPlaying)
		{
			GameObject.Find("GameManager").GetComponent<WaveSpawner>().SpawnIndividualEnemy(hSliderValue, prefabs[0]);
		}
		if(GUILayout.Button("Spawn Swordsman") && Application.isPlaying)
		{
			GameObject.Find("GameManager").GetComponent<WaveSpawner>().SpawnIndividualEnemy(hSliderValue, prefabs[1]);
		}
		if(GUILayout.Button("Spawn Dragon") && Application.isPlaying)
		{
			Debug.Log("Spawn Dragon");
		}
	}
}