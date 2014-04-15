using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DisplayProperties : MonoBehaviour {

	public GameObject propertyDisplayTemplate;

	void OnGUI()
	{
		/*if(GameManager.instance.GetPlayerPropertyList(GameManager.instance.currentTurnPlayerID).Count > 0)
			this.gameObject.GetComponent<UIButton>().isEnabled = true;
		else
			this.gameObject.GetComponent<UIButton>().isEnabled = false;*/

	}

	void OnClick () {
		List<Property> currentProps = GameManager.instance.GetPlayerPropertyList(GameManager.instance.currentTurnPlayerID);
		propertyDisplayTemplate.SetActive(true);
		propertyDisplayTemplate.GetComponent<PopupInfo>().InitializeCardDisplay(currentProps);
	}
}
