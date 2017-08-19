using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

	#region Variables
	private int castleMaxHealth = 100;
	public float castleHealth;
	public int currency = 100;
	private int waveReward;
	private int waveNumber;
	#endregion

	#region GameObjectReferences
	[SerializeField] private Text waveNumberText;
	[SerializeField] private Text currencyText;
	[SerializeField] private RectTransform castleHealthFill;
	#endregion

	private void Awake()
	{
		castleHealth = castleMaxHealth;
	}

	private void Update()
	{
		castleHealth = Mathf.Clamp(castleHealth, 0f, castleMaxHealth);

		SetCastleHealthFill(castleHealth/castleMaxHealth);

		currencyText.text = "Gold: " + currency.ToString();

		waveNumberText.text = "Wave: " + waveNumber.ToString();
	}

	private void SetCastleHealthFill(float _amount)
	{
		castleHealthFill.localScale = new Vector3(_amount, 1f);
	}
}
