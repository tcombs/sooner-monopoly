using UnityEngine;
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
		int playerIDRolled = GameManager.instance.currentTurnPlayerID;
	
		string[] args = {playerIDRolled.ToString()+" " ,randDie1.ToString()+" ", randDie2.ToString()};
		StartCoroutine(ACL2Manager.instance.RunACL2(ACL2Manager.ACL2Options.RollDice, args));

		//In place of calling the roll module()
		GameManager.instance.spaces[(randDie1+randDie2+GameManager.instance.player1.spaceOn)%40].gameObject.GetComponentInChildren<MoveTokenToSpace>().TriggerMove();
		GameManager.instance.UpdatePlayer(playerIDRolled, false);
		Player playerRolled = GameManager.instance.GetPlayerWithID(playerIDRolled);

		GameManager.instance.spaces[playerRolled.spaceOn].OnLand();
		GameManager.instance.ProgressToNextTurn();
	}
}
