using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Property : MonoBehaviour {

	public int price;
	public int upgradePrice;
	public int mortgageValue;
	public int demortgageValue;
	public List<int> rentValues;
	public string propName;
	public int id;
	public bool owned = false;
	public int playerIDWhoOwns = 0;
	public UIButton buyPropButton;

	// Use this for initialization
	void Start () {
		rentValues = new List<int>();
	}

	public void OnLand()
	{
		Debug.Log("landed on prop " + propName);

		if(propName == "Chance")
		{
			ChanceActivated();
		}
		else if(propName == "CChest")
		{
			CChestActivated();
		}
		else if(GameManager.instance.GetPlayerOnSpace(id).money > price && price > 0 && !owned)
		{
			buyPropButton.isEnabled = true;
		}
	}

	public void PlaceHotel()
	{

	}

	public void ChanceActivated()
	{
		int choice = Random.Range(0,10);
		if(choice < 3)
		{
			int moneyGained = Random.Range(5,11);

			string[] args = {GameManager.instance.currentTurnPlayerID + " ", (moneyGained*10).ToString()};
			StartCoroutine(ACL2Manager.instance.RunACL2(ACL2Manager.ACL2Options.PayPlayer, args));
			
			GameManager.instance.UpdatePlayer(GameManager.instance.currentTurnPlayerID, false);

			Debug.Log("You got " + choice*10);

		}
		else if(choice < 7)
		{
			int moneyLost = Random.Range(5,11);

			string[] args = {GameManager.instance.currentTurnPlayerID + " -", (moneyLost*10).ToString()};
			StartCoroutine(ACL2Manager.instance.RunACL2(ACL2Manager.ACL2Options.PayPlayer, args));
			
			GameManager.instance.UpdatePlayer(GameManager.instance.currentTurnPlayerID, false);

			Debug.Log("You lost " + choice*10);
		}
		else
		{
			int spaceToMoveTo = Random.Range(0,40);
			while(spaceToMoveTo == 30)
			{
				spaceToMoveTo = Random.Range (0,40);
			}

			string[] args = {GameManager.instance.currentTurnPlayerID + " ", spaceToMoveTo.ToString()};
			StartCoroutine(ACL2Manager.instance.RunACL2(ACL2Manager.ACL2Options.MovePlayer, args));

			GameManager.instance.UpdatePlayer(GameManager.instance.currentTurnPlayerID, false);

			GameManager.instance.spaces[spaceToMoveTo].gameObject.GetComponentInChildren<MoveTokenToSpace>().TriggerMove();

			Debug.Log("Move to space " + spaceToMoveTo);
		}
	}

	public void CChestActivated()
	{
		int moneyValue = Random.Range(1,6);

		string[] args = {GameManager.instance.currentTurnPlayerID + " ", (moneyValue*100).ToString()};
		StartCoroutine(ACL2Manager.instance.RunACL2(ACL2Manager.ACL2Options.PayPlayer, args));

		GameManager.instance.UpdatePlayer(GameManager.instance.currentTurnPlayerID, false);

		Debug.Log("You got " + moneyValue*10);
	}

}
