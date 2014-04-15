using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	public static GameManager instance{ get {return _instance;}}

	public Player player1;
	public Player player2;
	public Player player3;
	public Player player4;

	private List<Property> player1Props = new List<Property>();
	private List<Property> player2Props = new List<Property>();
	private List<Property> player3Props = new List<Property>();
	private List<Property> player4Props = new List<Property>();

	[HideInInspector]
	public bool upgradingProperty = false;

	[HideInInspector]
	public bool tradingProperty = false;

	public List<Property> spaces; 

	private static readonly string PLAYER_STATE_PATH = @"C:\ACLResources\player_state.txt";
	private static readonly string PROPERTY_STATE_PATH = @"C:\ACLResources\prop_state.txt";
	private static readonly string PROPERTY_DATA = @"C:\ACLResources\PROPERTIES.txt";
	private static readonly string PROPERTY_NAME_DATA = @"C:\ACLResources\PROPERTYNAMES.txt";

	[HideInInspector]
	public int currentTurnPlayerID;

	protected void Awake()
	{
		_instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		NewGame();
		SetupProperties();
		InitPropertyStates();
		UpdatePropertyState();
		UpdatePlayer (1, true);
		UpdatePlayer (2, true);
		UpdatePlayer (3, true);
		UpdatePlayer (4, true);

		currentTurnPlayerID = player1.playerID;
	}

	public void ProgressToNextTurn()
	{
		if(currentTurnPlayerID + 1 == 5)
			currentTurnPlayerID = 1;
		else
			currentTurnPlayerID++;

		GameObject.Find ("EndTurnButton").GetComponent<UIButton>().isEnabled = false;
		GameObject.Find("RollButton").GetComponent<UIButton>().isEnabled = true;
	}

	public void UpdatePlayer(int playerID, bool newGameChange)
	{
		StreamReader sr = new StreamReader(PLAYER_STATE_PATH);
		string fileContents = sr.ReadToEnd();
		sr.Close();
		
		string[] lines = fileContents.Split("\n"[0]);

		switch (playerID)
		{
		case 1:
			string[] player1States = lines[0].Split(' ');
			player1.playerID = int.Parse(player1States[0]);
			player1.spaceOn = int.Parse(player1States[1]);
			player1.money = int.Parse(player1States[2]);
			player1.numOfGOJCards = int.Parse(player1States[3]);
			player1.numOfRollsInJail = int.Parse(player1States[4]);
			player1.ValuesChanged(newGameChange);
			break;
		case 2:
			string[] player2States = lines[1].Split(' ');
			player2.playerID = int.Parse(player2States[0]);
			player2.spaceOn = int.Parse(player2States[1]);
			player2.money = int.Parse(player2States[2]);
			player2.numOfGOJCards = int.Parse(player2States[3]);
			player2.numOfRollsInJail = int.Parse(player2States[4]);
			player2.ValuesChanged(newGameChange);
			break;
		case 3:
			string[] player3States = lines[2].Split(' ');
			player3.playerID = int.Parse(player3States[0]);
			player3.spaceOn = int.Parse(player3States[1]);
			player3.money = int.Parse(player3States[2]);
			player3.numOfGOJCards = int.Parse(player3States[3]);
			player3.numOfRollsInJail = int.Parse (player3States[4]);
			player3.ValuesChanged(newGameChange);
			break;
		case 4:
			string[] player4States = lines[3].Split(' ');
			player4.playerID = int.Parse (player4States[0]);
			player4.spaceOn = int.Parse(player4States[1]);
			player4.money = int.Parse(player4States[2]);
			player4.numOfGOJCards = int.Parse(player4States[3]);
			player4.numOfRollsInJail = int.Parse(player4States[4]);
			player4.ValuesChanged(newGameChange);
			break;
		default:
			//Debug.LogError("Import error in GameManager");
			break;
		}
	}

	private void NewGame()
	{
		StreamWriter writer = new StreamWriter(PLAYER_STATE_PATH);
		for(int i = 1; i < 5; i++)
		{
			writer.WriteLine(i + " 0 1500 0 0");
		}
		writer.Close();
	}

	public Player GetPlayerOnSpace(int spaceID)
	{
		if(player1.spaceOn == spaceID)
			return player1;
		else if(player2.spaceOn == spaceID)
			return player2;
		else if(player3.spaceOn == spaceID)
			return player3;
		else
			return player4;
	}

	public Player GetPlayerWithID(int playerID)
	{
		if(player1.playerID == playerID)
			return player1;
		else if(player2.playerID == playerID)
			return player2;
		else if(player3.playerID == playerID)
			return player3;
		else
			return player4;
	}

	private void SetupProperties()
	{
		StreamReader sr = new StreamReader(PROPERTY_DATA);
		string fileContents = sr.ReadToEnd();
		sr.Close();

		string[] lines = fileContents.Split("\n"[0]);
		foreach(string line in lines)
		{
			string formattedLine = line.Replace("\r","");
			formattedLine = formattedLine.Replace("\n","");
			string[] lineContents = formattedLine.Split(' ');
			int idToModify = int.Parse(lineContents[0]);
			spaces[idToModify].id = idToModify;
			spaces[idToModify].price = int.Parse(lineContents[1]);
			spaces[idToModify].upgradePrice = int.Parse(lineContents[2]);
			spaces[idToModify].mortgageValue = int.Parse(lineContents[3]);
			spaces[idToModify].demortgageValue = int.Parse(lineContents[4]);
			for(int i = 5; i < 11; i++)
				spaces[idToModify].rentValues.Add(int.Parse(lineContents[i]));

		}


		StreamReader sr2 = new StreamReader(PROPERTY_NAME_DATA);
		string fileContents2 = sr2.ReadToEnd();
		sr2.Close();

		string[] lines2 = fileContents2.Split("\n"[0]);
		foreach(string line in lines2)
		{
			string[] lineData = line.Split(' ');
			int id  = int.Parse(lineData[0]);

			spaces[id].propName = lineData[1].Replace("_"," ").Replace("\r","");
		}
	}

	private void InitPropertyStates()
	{
		StreamWriter writer = new StreamWriter(PROPERTY_STATE_PATH);
		for(int i = 1; i < 40; i++)
		{
			if(spaces[i].propName != "CChest" && spaces[i].propName != "Chance" && spaces[i].propName != "Austin" && 
			   spaces[i].propName != "No Parking" && spaces[i].propName != "Go To Austin")
			{
				writer.WriteLine(i.ToString() + " -1 -1 0");
			}
		}

		writer.Close();
	}

	public void UpdatePropertyState()
	{
		StreamReader sr = new StreamReader(PROPERTY_STATE_PATH);
		string fileContents = sr.ReadToEnd();

		string[] lines = fileContents.Split("\n"[0]);
		foreach(string line in lines)
		{
			string formattedLine = line.Replace("\r","");
			formattedLine = formattedLine.Replace("\n","");

			if(formattedLine != "")
			{
				string[] lineContents = formattedLine.Split(' ');
				int idToModify = int.Parse(lineContents[0]);
				spaces[idToModify].playerIDWhoOwns = int.Parse(lineContents[1]);
				spaces[idToModify].upgradeLevel = int.Parse(lineContents[2]);
				spaces[idToModify].isMortgaged = int.Parse(lineContents[3]) == 0;
			}
		}
		sr.Close();
		UpdateActiveProperties();
	}

	public void UpdateActiveProperties()
	{
		foreach(Property p in spaces)
		{
			switch(p.playerIDWhoOwns)
			{
			case 1:
				player1Props.Add(p);
				break;
			case 2:
				player2Props.Add(p);
				break;
			case 3:
				player3Props.Add(p);
				break;
			case 4:
				player4Props.Add(p);
				break;
			default:
				if(p.playerIDWhoOwns > 0)
					Debug.LogError(p.playerIDWhoOwns + " is not a valid player in the game");
				break;
			}
		}
	}

	public List<Property> GetPlayerPropertyList(int playerID)
	{
		if(playerID == 1)
			return player1Props;
		else if(playerID == 2)
			return player2Props;
		else if(playerID == 3)
			return player3Props;
		else if(playerID == 4)
			return player4Props;
		else
		{
			Debug.LogError("The given player id is not a valid player: " + playerID);
			return new List<Property>();
		}


	}                               
}
