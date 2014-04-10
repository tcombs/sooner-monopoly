using UnityEngine;
using System.Collections;

public class PropertyUpgrade : MonoBehaviour {

	void OnClick()
	{
		Property parent = transform.parent.GetComponent<Property>();

		if(GameManager.instance.upgradingProperty == true && parent.upgradeLevel != 6 && GameManager.instance.currentTurnPlayerID == parent.playerIDWhoOwns
		   && GameManager.instance.GetPlayerWithID(GameManager.instance.currentTurnPlayerID).money > parent.upgradePrice)
		{
			string[] args = {parent.id.ToString()};
			string[] payArgs = {GameManager.instance.currentTurnPlayerID.ToString() + " ", parent.upgradePrice.ToString()};

			StartCoroutine(ACL2Manager.instance.RunACL2(ACL2Manager.ACL2Options.UpgradeProperty, args));
			StartCoroutine(ACL2Manager.instance.RunACL2(ACL2Manager.ACL2Options.PayPlayer, payArgs));
			GameManager.instance.UpdatePlayer(GameManager.instance.currentTurnPlayerID, false);
			GameManager.instance.UpdatePropertyState();

			GameManager.instance.upgradingProperty = false;
		}
	}
}
