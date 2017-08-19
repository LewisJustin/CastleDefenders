using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class WaveLogic : MonoBehaviour {

	#region Variables
	private int castleMaxHealth = 100;
	public float castleHealth;
	public int currency = 100;
	private int enemyCount;
	private int waveReward;
	private int waveNumber;
    public int roundTimer = 0;
    private bool AllowNewTimer = true;
    [SerializeField]private int StartTime;
    #endregion


    #region GameObjectReferences
    [SerializeField] private Text waveNumberText;
	[SerializeField] private Text currencyText;
	[SerializeField] private RectTransform castleHealthFill;
	[SerializeField] private GameObject enemy01Prefab;
	[SerializeField] private GameObject enemySpawnPoint;
    [SerializeField] private AudioSource WaveEndTune;
    #endregion


    private void Start()
    {
        WaveEndTune = GetComponent<AudioSource>();
        WaveEndTune.mute = true;
    }
    private void Awake()
	{

        //Makes sure the CastleHealth is always max when you start the game
        castleHealth = castleMaxHealth;
                
        
    }


    private void Update()
	{
        #region CastleHealth
        castleHealth = Mathf.Clamp(castleHealth, 0f, castleMaxHealth);
        SetCastleHealthFill(castleHealth/castleMaxHealth);
        #endregion
        currencyText.text = currency.ToString();
        waveNumberText.text = waveNumber.ToString();


        //checks if we can start new timer
        if (roundTimer > 1)
        {
            //Timer and wave is still going
            AllowNewTimer = false;

            WaveEndTune.mute = false;
        }
        else if(roundTimer == 0)
        {
            //Timer and wave has ended
            AllowNewTimer = true;

            //Plays the WaveEndTune
            
            WaveEndTune.Play();
            
        }
	}

	private void SetCastleHealthFill(float _amount)
	{
		castleHealthFill.localScale = new Vector3(_amount, 1f);
	}

	public void StartWave()
	{
        
        if (AllowNewTimer == true)
        {
            roundTimer = StartTime;
            StartCoroutine(Timer());
        }
        else if(AllowNewTimer == false)
        {
            roundTimer = StartTime;
        }

        //Makes sure the timer resets everytime we start new wave
        roundTimer = StartTime;


       
        waveNumber++;


        //Gives the castle max health when we start new wave
		castleHealth = castleMaxHealth;

		StartCoroutine(enemySpawnTime(waveNumber));
	}



	IEnumerator enemySpawnTime(int _waveNumber)
	{

        for (int i = 0; i < roundTimer;) {
            SpawnEnemy();

            //waitTime
            float waitTime = (Random.Range(0.1f, 10 / waveNumber));
            #region Debug
            Debug.Log("Wavenumber =" + " " + waveNumber + "  " + "waitTime = " + " " + (waitTime.ToString("F2")));

            #endregion
            yield return new WaitForSeconds(waitTime);
        }
        
	}

    IEnumerator Timer()
    {

        for (int i = 0; i < roundTimer;)
        {
            roundTimer--;
            yield return new WaitForSeconds(1);
        }
    }

    private void SpawnEnemy()
    {
        //ENEMY SPAWN SCRIPT GOES HERE
        Debug.Log("Spawn Enemy");

    }
}