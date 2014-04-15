using UnityEngine;
using System.Collections;

public class PreviousCard : MonoBehaviour {

	public PopupInfo infoCard;

	void OnClick()
	{
		infoCard.DisplayPrevious();
	}
}
