using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopupInfo : MonoBehaviour {
	[HideInInspector]
	public List<Property> playerProps;

	public UISprite propertyColor;
	public UILabel rent0price;
	public UILabel rent1price;
	public UILabel rent2price;
	public UILabel rent3price;
	public UILabel rent4price;
	public UILabel rent5price;
	public UILabel rent6price;
	public UILabel propertyName;
	public UILabel upgradeCost;

	public int currentProperty;

	private readonly string rentText = "$ rent with ";
	private readonly string upgradeText = "Cost to upgrade = ";

	public UIButton nextCard;
	public UIButton previousCard;

	public PropertyUpgrade pUpgrade;

	public void InitializeCardDisplay(List<Property> propsPassed)
	{
		playerProps = propsPassed;
		propertyColor.color = playerProps[0].propertyColor;
		propertyColor.alpha = 255.0f;
		propertyName.text = playerProps[0].propName;
		rent0price.text = playerProps[0].rentValues[0] + rentText + "0 hotels";
		rent1price.text = playerProps[0].rentValues[1] + rentText + "1 hotels";
		rent2price.text = playerProps[0].rentValues[2] + rentText + "2 hotels";
		rent3price.text = playerProps[0].rentValues[3] + rentText + "3 hotels";
		rent4price.text = playerProps[0].rentValues[4] + rentText + "4 hotels";
		rent5price.text = playerProps[0].rentValues[5] + rentText + "5 hotels";
		rent6price.text = playerProps[0].rentValues[6] + rentText + "6 hotels";

		currentProperty = 0;

		pUpgrade.InitializeSequence();
	}

	public void DisplayNext()
	{
		if(currentProperty < playerProps.Count-1)
		{
			currentProperty++;
			DisplayCard(currentProperty);
		}
	}

	public void DisplayPrevious()
	{
		if(currentProperty != 0)
		{
			currentProperty--;
			DisplayCard(currentProperty);
		}
	}

	private void DisplayCard(int listNumber)
	{
		propertyColor.color = playerProps[listNumber].propertyColor;
		propertyColor.alpha = 255.0f;
		propertyName.text = playerProps[listNumber].propName;
		rent0price.text = playerProps[listNumber].rentValues[0] + rentText + "0 hotels";
		rent1price.text = playerProps[listNumber].rentValues[1] + rentText + "1 hotels";
		rent2price.text = playerProps[listNumber].rentValues[2] + rentText + "2 hotels";
		rent3price.text = playerProps[listNumber].rentValues[3] + rentText + "3 hotels";
		rent4price.text = playerProps[listNumber].rentValues[4] + rentText + "4 hotels";
		rent5price.text = playerProps[listNumber].rentValues[5] + rentText + "5 hotels";
		rent6price.text = playerProps[listNumber].rentValues[6] + rentText + "6 hotels";
	}
}
