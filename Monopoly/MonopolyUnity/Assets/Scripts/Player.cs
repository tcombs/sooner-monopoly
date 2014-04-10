using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public int playerID;
	//public string playerName;
	public int spaceOn;
	public int money;
	public int numOfGOJCards;
	public int numOfRollsInJail;
	public UILabel playerIDLabel;
	public UILabel playerMoneyLabel;

	public void ValuesChanged(bool newGameChange)
	{
		if(newGameChange)
			playerIDLabel.text = "Player " + playerID.ToString();

		playerMoneyLabel.text = money.ToString();
	}
}
