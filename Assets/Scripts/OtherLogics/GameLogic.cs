using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{

    #region Variables
    public int castleMaxHealth = 100;
    public float castleHealth;
    public int currency;
    private int waveReward;
    public int waveNumber;
    public int castleArmour;
    public bool Dead = false;

    private float trueArmor;
    #endregion

    #region GameObjectReferences
    [SerializeField] private Text waveNumberText;
    [SerializeField] private Text currencyText;
    [SerializeField] private RectTransform castleHealthFill;
    [SerializeField] private GameObject audioManager;
    #endregion

    private void Awake()
    {
        castleHealth = castleMaxHealth;
        currency = 0;
    }

    private void Update()
    {
        castleHealth = Mathf.Clamp(castleHealth, 0f, castleMaxHealth);

        if (castleHealth <= 0 && Dead == false)
        {
            die(waveNumber);
        }

        SetCastleHealthFill(castleHealth / castleMaxHealth);

        currencyText.text = "Gold: " + currency.ToString();

        waveNumberText.text = "Wave: " + waveNumber.ToString();
    }

    private void SetCastleHealthFill(float _amount)
    {
        castleHealthFill.localScale = new Vector3(_amount, 1f);
    }

    public void castleTakeDamage(int damage)
    {
        trueArmor = ((castleArmour * 10 * 0.3f)/100);

        if (trueArmor > .85f)
        {
            trueArmor = .85f;
        }

        castleHealth -= (damage * trueArmor);
    }
    private void die(int _waveNumber)
    {
        Dead = true;
        audioManager.GetComponent<AudioManager>().Play("CastleDestroy");
        Debug.Log("Died on wave " + _waveNumber);
    }
}

