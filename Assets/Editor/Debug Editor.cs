using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class DebugEditor : EditorWindow
{
	List<GameObject> enemies;
	private GameObject arrowPrefab;

	int hSliderValue;
	int healthSliderValue;
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

		GUILayout.Label("Health");
		healthSliderValue = (int)GUILayout.HorizontalSlider(healthSliderValue, 0f, GameObject.Find("GameManager").GetComponent<GameLogic>().castleMaxHealth);

		GameObject.Find("GameManager").GetComponent<GameLogic>().castleHealth = healthSliderValue;
		
		GUILayout.Label("Enemies", EditorStyles.boldLabel);
		hSliderValue = (int)GUILayout.HorizontalSlider(hSliderValue, 0f, 2f);

		if(GUILayout.Button("Spawn Archer") && Application.isPlaying)
		{
			GameObject.Find("GameManager").GetComponent<WaveSpawner>().SpawnIndividualEnemy(hSliderValue, GameObject.Find("GameManager").GetComponent<WaveSpawner>().archer);
		}
		if(GUILayout.Button("Spawn Swordsman") && Application.isPlaying)
		{
			GameObject.Find("GameManager").GetComponent<WaveSpawner>().SpawnIndividualEnemy(hSliderValue, GameObject.Find("GameManager").GetComponent<WaveSpawner>().swordsman);
		}
		if(GUILayout.Button("Spawn Dragon"))
		{
			Debug.Log("Happyman gave up :(");
		}

		EditorGUILayout.EndScrollView();
	}
}