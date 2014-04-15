﻿using UnityEngine;
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
	
	private readonly string BUY_PROPERTY_EXE = @"C:\ACLResources\BuyBat.bat"; 
	private readonly string ROLL_DICE_EXE = @"C:\ACLResources\RollBat.bat";
	private readonly string MOVE_PLAYER_EXE = @"C:\ACLResources\MoveBat.bat";
	private readonly string PAY_PLAYER_EXE = @"C:\ACLResources\PayBat.bat";
	private readonly string UPGRADE_PROPERTY_EXE = @"C:\ACLResources\UpgradeBat.bat";

	public void Awake()
	{
		_instance = this;
	}

	public void RunACL2(ACL2Options choice, string[] args)
	{
		//asset = Resources.Load("/ACL2Modules/"+operativeFileName+".txt") as TextAsset;
		writer = new StreamWriter(@"\ACLResources\"+operativeFileName+".txt");

		foreach(string arg in args)
		{
			writer.Write(arg);
		}
		writer.Close ();

		Process p = new Process();

		//Do file io to write args and then call exe
		switch(choice)
		{
		case ACL2Options.BuyProperty:
		{
			try{
				p.StartInfo.FileName = BUY_PROPERTY_EXE;
			}
			catch(Exception e)
			{
				UnityEngine.Debug.LogError (e);
			}
			break;
		}
		case ACL2Options.MovePlayer:
		{
			try{
				p.StartInfo.FileName = MOVE_PLAYER_EXE;
			}
			catch(Exception e)
			{
				UnityEngine.Debug.LogError (e);
			}
			break;
		}
		case ACL2Options.PayPlayer:
		{
			try{
				p.StartInfo.FileName = PAY_PLAYER_EXE;
			}
			catch(Exception e)
			{
				UnityEngine.Debug.LogError (e);
			}
			break;
		}
		case ACL2Options.UpgradeProperty:
		{
			try{
				p.StartInfo.FileName = UPGRADE_PROPERTY_EXE;
			}
			catch(Exception e)
			{
				UnityEngine.Debug.LogError (e);
			}
			break;
		}
		case ACL2Options.RollDice:
		{
		try{
				p.StartInfo.FileName = ROLL_DICE_EXE;
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

		p.Start();
		p.WaitForExit();
		p.Close();
	}
}
