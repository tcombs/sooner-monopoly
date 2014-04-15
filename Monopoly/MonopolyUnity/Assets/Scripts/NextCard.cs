using UnityEngine;
using System.Collections;

public class NextCard : MonoBehaviour {

	public PopupInfo infoCard;

	void OnClick()
	{
		infoCard.DisplayNext();
	}
}
