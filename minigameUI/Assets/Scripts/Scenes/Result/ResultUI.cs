using UnityEngine;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;

public class ResultUI
{
	TextMeshProUGUI _killText;
	MonoBehaviour _monoBehaviour;

	ResultScene.ReusltData _reusltData;

	bool _isEndAnim = false;

	public void Init( MonoBehaviour monoBehaviour , ResultScene.ReusltData reusltData )
	{
		_monoBehaviour = monoBehaviour;
		var gameObject = monoBehaviour.gameObject;
		var canvasObj = gameObject.transform.Find( "Canvas" );

		_reusltData = reusltData;


		_killText = canvasObj.transform.Find("Kill" ).Find( "numText" ).GetComponent<TextMeshProUGUI>();

		_monoBehaviour.StartCoroutine( AnimCoroutine() );
	}

	public void Update()
	{
		if( _isEndAnim )
		{
			if( MyInput.GetInstance().IsTouchTrigger() )
			{
				SystemManager.GetInstance().LoadScene( "TitleScene" );
			}
		}
	}

	IEnumerator AnimCoroutine()
	{
		int frame = 120;
		int nowFrame = 0;
		while( frame > nowFrame )
		{
			if( MyInput.GetInstance().IsTouchTrigger() )
			{
				break;
			}
			nowFrame++;
			{
				int num1 = UnityEngine.Random.Range( 1 , 999 );
				int num2 = UnityEngine.Random.Range( 1 , 999 );
				int num3 = UnityEngine.Random.Range( 1 , 999 );
				_killText.text = num1.ToString() + "," + num2.ToString() + "," + num3.ToString();
			}
			yield return null;
		}
		_killText.text = _reusltData.m_killList.Count.ToString("#,0");




		_isEndAnim = true;
	}

}