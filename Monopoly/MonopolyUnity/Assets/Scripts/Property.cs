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
	public bool owned;
	public UIButton buyPropButton;

	// Use this for initialization
	void Start () {
		rentValues = new List<int>();
	}

	public void OnLand()
	{
		if(GameManager.instance.GetPlayerOnSpace(id).money > price && price > 0)
		{
			buyPropButton.gameObject.SetActive(true);
		}

	}
}
