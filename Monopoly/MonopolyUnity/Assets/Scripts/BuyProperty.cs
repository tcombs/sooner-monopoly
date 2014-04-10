using UnityEngine;
using System.Collections;

public class BuyProperty : MonoBehaviour {

	void OnClick()
	{
		int playerIDBuying = GameManager.instance.currentTurnPlayerID;
		int propertyBuying = GameManager.instance.GetPlayerWithID(playerIDBuying).spaceOn;
		int propertyPrice = GameManager.instance.spaces[propertyBuying].price;

		GameManager.instance.spaces[propertyBuying].owned = true;

		string[] buyArguments = {playerIDBuying.ToString() + " ",propertyBuying.ToString()};
		string[] payArguments = {playerIDBuying.ToString() + " -",propertyPrice.ToString()};

		StartCoroutine(ACL2Manager.instance.RunACL2(ACL2Manager.ACL2Options.BuyProperty, buyArguments));
		StartCoroutine(ACL2Manager.instance.RunACL2(ACL2Manager.ACL2Options.PayPlayer, payArguments));

		GameManager.instance.UpdatePlayer(playerIDBuying, false);
	}
}
