using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleStoreLogic : MonoBehaviour
{
	[SerializeField] int CastleArmorCost;
	[SerializeField] int UpgradeBowDamageCost;
	[SerializeField] int UpgradeBowFireRateCost;

	void UpgradeCastleArmor()
	{
		if (GetComponent<GameLogic>().currency <= CastleArmorCost)
		{
			GetComponent<GameLogic>().currency -= CastleArmorCost;
			GetComponent<GameLogic>().castleArmour += 1;
		}
	}

	void UpgradeBowDamage()
	{
		if (GetComponent<GameLogic>().currency <= UpgradeBowDamageCost)
		{
			GetComponent<GameLogic>().currency -= UpgradeBowDamageCost;
			// make bow script
		}
	}

	void UpgradeBowFireRate()
	{
		if (GetComponent<GameLogic>().currency <= UpgradeBowFireRateCost)
		{
			GetComponent<GameLogic>().currency -= UpgradeBowFireRateCost;
			// make bow script
		}
	}


}