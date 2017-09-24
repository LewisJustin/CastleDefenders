using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class DebugEditor : EditorWindow
{
	List<GameObject> enemies;

	public void addEnemy(GameObject _enemy)
	{
		enemies.Add(_enemy);
	}

	[MenuItem("Window/Debug")]
	public static void ShowWindow()
	{
		GetWindow<DebugEditor>("Debug");
	}

	void OnGUI()
	{
		ScriptableObject target = this;
		SerializedObject so = new SerializedObject(target);
		SerializedProperty stringsProperty = so.FindProperty("enemies");
		EditorGUILayout.PropertyField(stringsProperty, true);

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
	
		if(GUILayout.Button("Refresh Enemies"))
		{
			for (int i = 0; i < GameObject.Find("EnemyParent").transform.childCount; i++)
			{
				enemies.Add(GameObject.Find("EnemyParent").transform.GetChild(i).gameObject);
			}
		}


		
	}
}
