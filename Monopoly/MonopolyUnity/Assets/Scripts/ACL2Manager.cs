using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

public class ACL2Manager : MonoBehaviour {

	private static ACL2Manager _instance;
	public static ACL2Manager instance{ get {return _instance;}}
	
	public enum ACL2Options
	{
		BuyProperty,
		RollDice,
		MovePlayer,
		PayPlayer,
		UpgradeProperty
	}
	
	private readonly string BUY_PROPERTY_EXE = "C:\\Users\\Colton\\Documents\\sooner-monopoly\\Monopoly\\MonopolyUnity\\Assets\\ACL2Modules\\BuyProperty.exe"; 
	private readonly string ROLL_DICE_EXE = "C:\\Users\\Colton\\Documents\\sooner-monopoly\\Monopoly\\MonopolyUnity\\Assets\\ACL2Modules\\Roll.exe";
	private readonly string MOVE_PLAYER_EXE = "C:\\Users\\Colton\\Documents\\sooner-monopoly\\Monopoly\\MonopolyUnity\\Assets\\ACL2Modules\\MovePlayerTo.exe";
	private readonly string PAY_PLAYER_EXE = "C:\\Users\\Colton\\Documents\\sooner-monopoly\\Monopoly\\MonopolyUnity\\Assets\\ACL2Modules\\PayToPlayer.exe";
	private readonly string UPGRADE_PROPERTY_EXE = "C:\\Users\\Colton\\Documents\\sooner-monopoly\\Monopoly\\MonopolyUnity\\Assets\\ACL2Modules\\UpgradeProperty.exe";

	protected void Awake()
	{
		_instance = this;
	}

	public IEnumerator RunACL2(ACL2Options choice, string[] arguments)
	{
		//Do file io to write args and then call exe

		yield return 0;
	}
}
