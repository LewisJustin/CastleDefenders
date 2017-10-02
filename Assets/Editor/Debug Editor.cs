using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class DebugEditor : EditorWindow
{
	private GameObject arrowPrefab;

	int hSliderValue;
	int healthSliderValue;
	public string coinsToAdd;

	bool foldoutCheating = true;
	bool foldoutEnemies = true;

	[MenuItem("Window/Debug")]
	public static void ShowWindow()
	{
		GetWindow<DebugEditor>("Debug");
	}
	Vector2 scrollPos;
	void OnGUI()
	{
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

		foldoutCheating = EditorGUILayout.Foldout(foldoutCheating, "Cheat mode");

		if(foldoutCheating)
		{
			GUILayout.Label("Put arrow here");
			arrowPrefab = (GameObject)EditorGUILayout.ObjectField(arrowPrefab, typeof(GameObject), false);
	
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
			
		}

		foldoutEnemies = EditorGUILayout.Foldout(foldoutEnemies, "Enemies");

		if(foldoutEnemies)
		{
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
				Debug.Log("Happyman is slow....");
			}
		}

		

		EditorGUILayout.EndScrollView();
	}
}