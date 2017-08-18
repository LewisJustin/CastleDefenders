using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveLogic : MonoBehaviour {

	#region Variables
	private int castleMaxHealth = 100;
	public float castleHealth;
	public int currency = 100;
	private int enemyCount;
	private int waveReward;
	private int waveNumber;
	#endregion

	#region GameObjectReferences
	[SerializeField] private Text waveNumberText;
	[SerializeField] private Text currencyText;
	[SerializeField] private RectTransform castleHealthFill;
	[SerializeField] private GameObject enemy01Prefab;
	[SerializeField] private GameObject enemySpawnPoint;
	#endregion

	private void Awake()
	{
		castleHealth = castleMaxHealth;
        #region LogWarning
        Debug.LogWarning("Please fix the `waitTime` variable");
        #endregion
    }

    private void Update()
	{
		castleHealth = Mathf.Clamp(castleHealth, 0f, castleMaxHealth);

		SetCastleHealthFill(castleHealth/castleMaxHealth);

		currencyText.text = currency.ToString();

		waveNumberText.text = waveNumber.ToString();
	}

	private void SetCastleHealthFill(float _amount)
	{
		castleHealthFill.localScale = new Vector3(_amount, 1f);
	}

	public void StartWave()
	{
		waveNumber++;

		castleHealth = castleMaxHealth;

		StartCoroutine(enemySpawnTime(waveNumber));
	}

	IEnumerator enemySpawnTime(int _waveNumber)
	{
        //This does Not work
		float waitTime = (Random.Range(0, (Mathf.Pow(waveNumber, 1/2))));
        #region Debug
        Debug.Log("Wavenumber =" + " " + waveNumber + "  " + "waitTime = "+" "+ (waitTime.ToString("F2")));

        #endregion
        yield return new WaitForSeconds(waitTime);


	}
}