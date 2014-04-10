using UnityEngine;
using System.Collections;

public class PopupInfo : MonoBehaviour {

	public string text = "";

	public Property parent;

	private string currentHoverText = "";
	private GUIStyle foreground;
	private GUIStyle background;
	
	// Use this for initialization
	void Start () {

		parent = this.transform.parent.GetComponent<Property>();

		foreground = new GUIStyle();
		foreground.normal.textColor = Color.white;
		foreground.alignment = TextAnchor.UpperCenter;
		foreground.wordWrap = true;
		
		background = new GUIStyle();
		background.normal.textColor = Color.black;
		background.alignment = TextAnchor.UpperCenter;
		background.wordWrap = true;
	}

	void OnMouseEnter()
	{
		text = parent.propName + " Price = " + parent.price + " Upgrade Cost = " + parent.upgradePrice + " Mortgage Value = " + parent.mortgageValue;
		if(parent.owned)
		{
			text = text + " Owned by: " + GameManager.instance.GetPlayerWithID(parent.playerIDWhoOwns).playerIDLabel.text;
		}
		currentHoverText = text;
	}

	void OnMouseExit()
	{
		currentHoverText = "";
	}

	void OnGUI()
	{
		if(currentHoverText != "")
		{
			float x = Event.current.mousePosition.x;
			float y = Event.current.mousePosition.y;

			GUI.Label(new Rect(x-149,y+40,150,200), currentHoverText, background);
			GUI.Label(new Rect(x-150,y+40,150,200), currentHoverText, foreground);
		}
	}
}
