﻿using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{
	public Player player1;
	public Player player2;
	public Player player3;
	public Player player4;

	private static readonly string PLAYER_STATE_PATH = "/ACL2Modules/player_state.txt";

	// Use this for initialization
	void Start () 
	{
		UpdatePlayer (0, true);
		UpdatePlayer (1, true);
		UpdatePlayer (2, true);
		UpdatePlayer (3, true);
	}

	public void UpdatePlayer(int playerID, bool import)
	{
		StreamReader sr = new StreamReader(Application.dataPath + PLAYER_STATE_PATH);
		string fileContents = sr.ReadToEnd();
		sr.Close();
		
		string[] lines = fileContents.Split("\n"[0]);

		switch (playerID)
		{
		case 0:
			string[] player1States = lines[0].Split(' ');
			player1.playerPosition = int.Parse(player1States[0]);
			player1.spaceOn = int.Parse(player1States[1]);
			player1.money = int.Parse(player1States[2]);
			player1.numOfGOJCards = int.Parse(player1States[3]);
			player1.numOfRollsInJail = int.Parse(player1States[4]);
			break;
		case 1:
			string[] player2States = lines[1].Split(' ');
			player2.playerPosition = int.Parse(player2States[0]);
			player2.spaceOn = int.Parse(player2States[1]);
			player2.money = int.Parse(player2States[2]);
			player2.numOfGOJCards = int.Parse(player2States[3]);
			player2.numOfRollsInJail = int.Parse(player2States[4]);
			break;
		case 2:
			string[] player3States = lines[2].Split(' ');
			player3.playerPosition = int.Parse(player3States[0]);
			player3.spaceOn = int.Parse(player3States[1]);
			player3.money = int.Parse(player3States[2]);
			player3.numOfGOJCards = int.Parse(player3States[3]);
			player3.numOfRollsInJail = int.Parse (player3States[4]);
			break;
		case 3:
			string[] player4States = lines[3].Split(' ');
			player4.playerPosition = int.Parse (player4States[0]);
			player4.spaceOn = int.Parse(player4States[1]);
			player4.money = int.Parse(player4States[2]);
			player4.numOfGOJCards = int.Parse(player4States[3]);
			player4.numOfRollsInJail = int.Parse(player4States[4]);
			break;
		default:
			Debug.LogError("Import error in GameManager");
			break;
		}
	}
}
