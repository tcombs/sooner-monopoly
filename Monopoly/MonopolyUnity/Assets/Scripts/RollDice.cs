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

		GameManager.instance.spaces[(randDie1+randDie2+GameManager.instance.player1.spaceOn)%41].GetComponentInChildren<MoveTokenToSpace>().TriggerMove();
		GameManager.instance.player1.spaceOn = (randDie1+randDie2+GameManager.instance.player1.spaceOn)%41;
	}
}
