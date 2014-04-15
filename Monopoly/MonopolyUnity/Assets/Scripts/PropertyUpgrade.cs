using UnityEngine;
using System.Collections;

public class PropertyUpgrade : MonoBehaviour {

	public PopupInfo currentPropertyInfo;
	private Property currentPropertyUpgrading;
	private Player player;

	public void InitializeSequence()
	{
		currentPropertyUpgrading = GameManager.instance.spaces[currentPropertyInfo.playerProps[currentPropertyInfo.currentProperty].id];
		player = GameManager.instance.GetPlayerWithID(GameManager.instance.currentTurnPlayerID);
	}

	void OnClick()
	{
		Debug.Log("Clicked");

		if(player.money > currentPropertyUpgrading.upgradePrice && currentPropertyUpgrading.upgradeLevel < 6)
			this.gameObject.GetComponent<UIButton>().isEnabled = true;
		else
			this.gameObject.GetComponent<UIButton>().isEnabled = false;

	}
}
