using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class DebugEditor : EditorWindow
{
	private GameObject arrowPrefab;

	int hSliderValue;
	int healthSliderValue;
	public string coinsToAdd;
	private string statToAdd;

	List<GameObject> enemies;

	bool foldoutCheating = true;
	bool foldoutEnemies = true;
	bool foldoutHardChangeEnemies = true;
	bool foldoutSoftChangeEnemies = true;

	private Transform archer;
	private Transform swordsman;

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

			foldoutEnemies = EditorGUILayout.Foldout(foldoutEnemies, "Spawn Enemies");

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
					GameObject.Find("GameManager").GetComponent<WaveSpawner>().SpawnIndividualEnemy(hSliderValue, GameObject.Find("GameManager").GetComponent<WaveSpawner>().dragon);
				}
			}

			foldoutHardChangeEnemies = EditorGUILayout.Foldout(foldoutHardChangeEnemies, "Change Enemy Stats Permanently! Don't touch unless you know what you are doing!!!!");

			if(foldoutHardChangeEnemies)
			{
				EditorGUILayout.LabelField("Archer, then knight");
				archer = (Transform)EditorGUILayout.ObjectField(archer, typeof(Transform), false);
				swordsman = (Transform)EditorGUILayout.ObjectField(swordsman, typeof(Transform), false);

				statToAdd = EditorGUILayout.TextField(statToAdd, GUILayout.Width(30), GUILayout.Height(20));

				int toAdd;
				bool isNumeric = int.TryParse(statToAdd, out toAdd);

				EditorGUILayout.BeginHorizontal();
					if (GUILayout.Button("More archer damage", GUILayout.Height(20)) && isNumeric)
					{
						archer.GetComponent<EnemyLogic>().damage += toAdd;
					}
					
					if (GUILayout.Button("Less archer damage", GUILayout.Height(20)))
					{
						archer.GetComponent<EnemyLogic>().damage -= toAdd;
					}
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal();
					if (GUILayout.Button("More swordsman damage", GUILayout.Height(20)) && isNumeric)
					{
						swordsman.GetComponent<EnemyLogic>().damage += toAdd;
					}
					
					if (GUILayout.Button("Less swordsman damage", GUILayout.Height(20)))
					{
						swordsman.GetComponent<EnemyLogic>().damage -= toAdd;
					}
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal();
					if (GUILayout.Button("More archer speed", GUILayout.Height(20)) && isNumeric)
					{
						archer.GetComponent<EnemyLogic>().speed += toAdd;
					}
					
					if (GUILayout.Button("Less archer speed", GUILayout.Height(20)))
					{
						archer.GetComponent<EnemyLogic>().speed -= toAdd;
					}
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal();
					if (GUILayout.Button("More swordsman speed", GUILayout.Height(20)) && isNumeric)
					{
						swordsman.GetComponent<EnemyLogic>().speed += toAdd;
					}
					
					if (GUILayout.Button("Less swordsman speed", GUILayout.Height(20)))
					{
						swordsman.GetComponent<EnemyLogic>().speed -= toAdd;
					}
				EditorGUILayout.EndHorizontal();
			}

			foldoutSoftChangeEnemies = EditorGUILayout.Foldout(foldoutSoftChangeEnemies, "change current enemies");

			if(foldoutSoftChangeEnemies)
			{
				if(GUILayout.Button("refresh enemies"))
				{
					enemies.Clear();
					if(GameObject.Find("EnemyParent").transform.childCount >= 1)
					{
						Debug.Log("Yay");
					}
					else
					{
						Debug.Log("Boo");
					}
				}
			}
		EditorGUILayout.EndScrollView();
	}
}