﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class RollDice : MonoBehaviour {
	
	void OnClick()
	{
		Debug.Log("Worked");
		Debug.Log("The current player's turn is: " +GameManager.instance.currentTurnPlayerID);
	
		int randDie1 = Random.Range(1,7);
		int randDie2 = Random.Range(1,7);
		Player playerRolled = GameManager.instance.GetPlayerWithID(GameManager.instance.currentTurnPlayerID);
	
		string[] args = {playerRolled.playerID.ToString()+" " ,randDie1.ToString()+" ", randDie2.ToString()};
		StartCoroutine(ACL2Manager.instance.RunACL2(ACL2Manager.ACL2Options.RollDice, args));

		GameManager.instance.spaces[playerRolled.spaceOn+randDie1+randDie2].gameObject.GetComponentInChildren<MoveTokenToSpace>().TriggerMove();
		GameManager.instance.spaces[playerRolled.spaceOn+randDie1+randDie2].OnLand();

		//In place of calling the roll module()
		GameManager.instance.UpdatePlayer(playerRolled.playerID, false);
		//this.gameObject.GetComponent<UIButton>().isEnabled = false;
	}
}
