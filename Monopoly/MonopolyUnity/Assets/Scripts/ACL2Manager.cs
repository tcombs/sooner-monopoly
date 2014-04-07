using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System;

public class ACL2Manager : MonoBehaviour {

	public string operativeFileName;

	private TextAsset asset;
	private StreamWriter writer;

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
	
	private readonly string BUY_PROPERTY_EXE = "C:\\Users\\Colton\\Documents\\sooner-monopoly\\Monopoly\\MonopolyUnity\\Assets\\Resources\\ACL2Modules\\BuyProperty.exe"; 
	private readonly string ROLL_DICE_EXE = "C:\\Users\\Colton\\Documents\\sooner-monopoly\\Monopoly\\MonopolyUnity\\Assets\\Resources\\ACL2Modules\\Roll.exe";
	private readonly string MOVE_PLAYER_EXE = "C:\\Users\\Colton\\Documents\\sooner-monopoly\\Monopoly\\MonopolyUnity\\Assets\\Resources\\ACL2Modules\\MovePlayerTo.exe";
	private readonly string PAY_PLAYER_EXE = "C:\\Users\\Colton\\Documents\\sooner-monopoly\\Monopoly\\MonopolyUnity\\Assets\\Resources\\ACL2Modules\\PayToPlayer.exe";
	private readonly string UPGRADE_PROPERTY_EXE = "C:\\Users\\Colton\\Documents\\sooner-monopoly\\Monopoly\\MonopolyUnity\\Assets\\Resources\\ACL2Modules\\UpgradeProperty.exe";

	protected void Awake()
	{
		_instance = this;
	}

	public IEnumerator RunACL2(ACL2Options choice, string[] args)
	{
		asset = Resources.Load(operativeFileName+".txt") as TextAsset;
		writer = new StreamWriter("Resources/"+operativeFileName+".txt");
		writer.WriteLine(args);

		/*Process p = new Process();

		//Do file io to write args and then call exe
		switch(choice)
		{
		case ACL2Options.RollDice:
		{
		try{
			p.StartInfo.FileName = "C:\\Users\\Colton\\Documents\\sooner-monopoly\\Monopoly\\MonopolyUnity\\Assets\\ACL2Modules\\BuyProperty.exe";

			p.Start();
			//myProcess.WaitForExit();
		}
		catch(Exception e)
		{
			UnityEngine.Debug.LogError (e);
		}
			break;
		}
		default:
			break;
		}*/


		yield return 0;
	}
}
