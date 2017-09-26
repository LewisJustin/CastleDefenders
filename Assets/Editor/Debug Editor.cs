using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class DebugEditor : EditorWindow
{
	List<GameObject> enemies;
	public Transform[] prefabs = new Transform[2];
	private GameObject arrowPrefab;

	int hSliderValue;
	public string coinsToAdd;

	[MenuItem("Window/Debug")]
	public static void ShowWindow()
	{
		GetWindow<DebugEditor>("Debug");
	}
	Vector2 scrollPos;
	void OnGUI()
	{
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

		GUILayout.Label("Cheat Mode", EditorStyles.boldLabel);
		GUILayout.BeginHorizontal();

			coinsToAdd = EditorGUILayout.TextField(coinsToAdd, GUILayout.Width(30), GUILayout.Height(20));

			int n;
			bool isNumeric = int.TryParse(coinsToAdd, out n);

			if (GUILayout.Button("+", GUILayout.Width(20), GUILayout.Height(20)) && isNumeric && Application.isPlaying)
			{
				GameObject.Find("GameManager").GetComponent<GameLogic>().currency += n;
			}
			
			if (GUILayout.Button("-", GUILayout.Width(20), GUILayout.Height(20)))
			{
				GameObject.Find("GameManager").GetComponent<GameLogic>().currency -= n;
			}
		GUILayout.EndHorizontal();

			GUILayout.Label("Put arrow here");
			arrowPrefab = (GameObject)EditorGUILayout.ObjectField(arrowPrefab, typeof(GameObject), false);


			if (GUILayout.Button("Bow Speed! (lowest speed)"))
			{
				GameObject.Find("Bow").GetComponent<BowLogic>().bowDrawSpeed = .25f;
			}

			if(GUILayout.Button("Give Me Power!"))
			{
				arrowPrefab.GetComponent<ArrowLogic>().damage += 75;
			}

			if (GUILayout.Button("Castle Armor!"))
			{
				GameObject.Find("GameManager").GetComponent<GameLogic>().castleArmour += 5;
			}

		GUILayout.Label("Archer, then Swordsman");
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

		EditorGUILayout.EndScrollView();
	}
}