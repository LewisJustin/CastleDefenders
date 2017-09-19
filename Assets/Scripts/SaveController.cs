using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveController : MonoBehaviour {

	public static SaveController saveController;

	//public int gold; // game logic
	//public int waveNumber; // game logic
	//public int bowDrawSpeed; // bow logic
	//public int castleArmor; //game logic
	//public int bowDamage; // arrow logic

	[SerializeField] private GameObject gameManager;
	[SerializeField] private GameObject bow;
	[SerializeField] private GameObject arrowPrefab;
	[SerializeField] private GameObject audioManager;

	//Singleton manager
	private void Awake()
	{
		if(saveController == null)
		{
			DontDestroyOnLoad(gameObject);
			saveController = this;
		}
		else if (saveController != this)
		{
			Destroy(gameObject);
		}
	}

	public void Save()
	{
		// creates a binary formatter &  a file;
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

		// creates a object to save the data to
		PlayerData data = new PlayerData();
		data.gold = gameManager.GetComponent<GameLogic>().currency;
		data.waveNumber = gameManager.GetComponent<GameLogic>().waveNumber;
		data.bowDrawSpeed = bow.GetComponent<BowLogic>().bowDrawSpeed;
		data.castleArmor = gameManager.GetComponent<GameLogic>().castleArmour;
		data.bowDamage = arrowPrefab.GetComponent<ArrowLogic>().damage;
		data.reverseAiming = bow.GetComponent<Rotate>().aimingReversed;
		data.volume = audioManager.GetComponent<AudioManager>().volume;

		// writes the object to the file and closes it
		bf.Serialize(file, data);
		file.Close();
	}

	public void Load()
	{
		if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();

			gameManager.GetComponent<GameLogic>().currency = data.gold;
			gameManager.GetComponent<GameLogic>().waveNumber = data.waveNumber;
			bow.GetComponent<BowLogic>().bowDrawSpeed = data.bowDrawSpeed;
			gameManager.GetComponent<GameLogic>().castleArmour = data.castleArmor;
			arrowPrefab.GetComponent<ArrowLogic>().damage = data.bowDamage;
			bow.GetComponent<Rotate>().aimingReversed = data.reverseAiming;

			audioManager.GetComponent<AudioManager>().OnVolumeChanged(data.volume);
		}
	}

	public void Delete()
	{
		if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
		{
			File.Delete(Application.persistentDataPath + "/playerInfo.dat");
		}
	}

}


[Serializable]
class PlayerData
{
	public int gold;
	public int waveNumber;
	public float bowDrawSpeed;
	public int castleArmor;
	public int bowDamage;
	public float volume;
	public bool reverseAiming;
}