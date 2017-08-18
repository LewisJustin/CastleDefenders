using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveLogic : MonoBehaviour {

	#region Variables
	private float castleMaxHealth = 100;
	public float castleHealth;
	public int currency = 100;
	private int enemyCount;
	private int waveReward;
	#endregion

	#region GameObjectReferences

	[SerializeField] RectTransform castleHealthFill;
	#endregion

	private void Start()
	{
		castleHealth = castleMaxHealth;
	}

	private void Update()
	{
		SetCastleHealthFill(castleHealth/castleMaxHealth);
	}

	private void SetCastleHealthFill(float _amount)
	{
		castleHealthFill.localScale = new Vector3(_amount, 1f);
	}
}
