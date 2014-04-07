using UnityEngine;
using System.Collections;

public class BuyProperty : MonoBehaviour {

	public delegate void EnableButton(); 
	public static event EnableButton enable;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
