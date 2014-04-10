using UnityEngine;
using System.Collections;

public class ToMainMenu : MonoBehaviour {

	void OnClick()
	{
		Application.LoadLevel("MainMenu");
	}
}
