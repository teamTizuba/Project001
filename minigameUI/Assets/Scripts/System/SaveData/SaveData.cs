using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataUtility : Singleton<SaveDataUtility>
{
	public class SaveData
	{
		public bool _isEndTutorial = false;
		public void SetupByStr( string str )
		{
			if( string.IsNullOrEmpty( str ) )
			{
				return;
			}
			var sep_1 = '\t';
			var saveDataStrAry = str.Split( sep_1 );
			_isEndTutorial = int.Parse( saveDataStrAry[0] ) > 0;
		}

		public string CalcStr()
		{
			string str = "";
			str += _isEndTutorial ? 1:0;
			return str;
		}
	}

	SaveData _saveData = new SaveData();

	protected override bool IsAddManager()
	{
		return false;
	}

	protected override void InitSub()
	{
		base.InitSub();
		Load();
	}

	public void Load()
	{
		string str = "";
		str = PlayerPrefs.GetString( "SaveData" );
		_saveData.SetupByStr( str );
	}

	public void Save()
	{
		PlayerPrefs.SetString( "SaveData" , _saveData.CalcStr() );
	}

	public SaveData GetSaveData()
	{
		return _saveData;
	}
}
