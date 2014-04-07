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
	
		string[] args = {playerIDRolled.ToString() ,randDie1.ToString(), randDie2.ToString()};

		if(File.Exists(Application.dataPath+"/ALC2Modules/next_move.txt"))
		{
			Debug.Log("It exists");
		}
		else
		{
			StreamWriter sr = File.CreateText(Application.dataPath+"/ALC2Modules/next_move.txt");
			Debug.Log("Opened as new one");
		}


		//StreamWriter sw;

		//sw = new StreamWriter(Application.dataPath+"/ALC2Modules/next_move.txt");

		//sw.WriteLine(playerIDRolled + " " + randDie1 + " " + randDie2);
		//sw.Close();
		//StartCoroutine(ACL2Manager.instance.RunACL2(ACL2Manager.ACL2Options.RollDice));

		GameManager.instance.spaces[(randDie1+randDie2+GameManager.instance.player1.spaceOn)%41].gameObject.GetComponent<MoveTokenToSpace>().TriggerMove();
		GameManager.instance.player1.spaceOn = (randDie1+randDie2+GameManager.instance.player1.spaceOn)%41;
	}
}
