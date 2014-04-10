using UnityEngine;
using System.Collections;

public class EndTurn : MonoBehaviour {

	void OnClick()
	{
		GameManager.instance.ProgressToNextTurn();
	}
}
