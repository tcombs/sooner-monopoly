using UnityEngine;
using System.Collections;

public class CloseCard : MonoBehaviour {

	public GameObject propertyCardObject;

	// Use this for initialization
	void OnClick()
	{
		propertyCardObject.SetActive(false);
	}
}
