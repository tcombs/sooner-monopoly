using UnityEngine;
using System.Collections;

public class Upgradeproperty : MonoBehaviour {

	private string text = "Pick a property you own to upgrade and click";

	private string currentText = "";

	private GUIStyle textFrame;

	void OnClick()
	{
		textFrame = new GUIStyle();
		textFrame.normal.textColor = Color.white;
		textFrame.fontSize = 12;
		textFrame.alignment = TextAnchor.UpperCenter;

		GameManager.instance.upgradingProperty = true;

		currentText = text;
	}	


	void OnGUI()
	{
		if(!GameManager.instance.upgradingProperty)
			currentText = "";

		if(currentText != "")
		{
			int locationX = Screen.height/4;
			int locationY = Screen.width/15;

			GUI.Label(new Rect(locationX,locationY,150,200), currentText, textFrame);
		}
	}

}
