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
	
	private readonly string BUY_PROPERTY_EXE = "C:\\Users\\Colton\\Documents\\sooner-monopoly\\Monopoly\\MonopolyUnity\\Resources\\ACL2Modules\\BuyProperty.exe"; 
	private readonly string ROLL_DICE_EXE = @"C:\Users\Colton\Documents\sooner-monopoly\Monopoly\MonopolyUnity\Resources\ACL2Modules\Roll";
	private readonly string MOVE_PLAYER_EXE = "C:\\Users\\Colton\\Documents\\sooner-monopoly\\Monopoly\\MonopolyUnity\\Resources\\ACL2Modules\\MovePlayerTo.exe";
	private readonly string PAY_PLAYER_EXE = "C:\\Users\\Colton\\Documents\\sooner-monopoly\\Monopoly\\MonopolyUnity\\Resources\\ACL2Modules\\PayToPlayer.exe";
	private readonly string UPGRADE_PROPERTY_EXE = "C:\\Users\\Colton\\Documents\\sooner-monopoly\\Monopoly\\MonopolyUnity\\Resources\\ACL2Modules\\UpgradeProperty.exe";

	public void Awake()
	{
		_instance = this;
	}

	public IEnumerator RunACL2(ACL2Options choice, string[] args)
	{
		asset = Resources.Load("/ACL2Modules/"+operativeFileName+".txt") as TextAsset;
		writer = new StreamWriter("Resources/ACL2Modules/"+operativeFileName+".txt");
		foreach(string arg in args)
		{
			writer.Write(arg);
		}
		writer.Close ();

		ProcessStartInfo pInfo = new ProcessStartInfo();
		pInfo.CreateNoWindow = false;
		pInfo.UseShellExecute = false;
		pInfo.RedirectStandardError = true;
		pInfo.RedirectStandardOutput = true;

		Process p = new Process();

		//Do file io to write args and then call exe
		switch(choice)
		{
		case ACL2Options.RollDice:
		{
		try{
				pInfo.FileName = ROLL_DICE_EXE;
				p.StartInfo = pInfo;
				
				p.Start();
				string errorMessage = p.StandardError.ReadToEnd();
				if(errorMessage != "")
					UnityEngine.Debug.LogError(errorMessage);
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
		}


		yield return 0;
	}
}
