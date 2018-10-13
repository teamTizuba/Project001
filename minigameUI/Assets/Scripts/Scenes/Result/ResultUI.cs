using UnityEngine;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;

public class ResultUI
{
	GameObject _winObj;
	GameObject _loseObj;
	TextMeshProUGUI _killText;
	TextMeshProUGUI _scoreText;
	MonoBehaviour _monoBehaviour;

	ResultScene.ReusltData _reusltData;

	bool _isEndAnim = false;

	public void Init( MonoBehaviour monoBehaviour , ResultScene.ReusltData reusltData )
	{
		_monoBehaviour = monoBehaviour;
		var gameObject = monoBehaviour.gameObject;
		var canvasObj = gameObject.transform.Find( "Canvas" );

		_reusltData = reusltData;
		var winOrLoseObj = canvasObj.transform.Find("WinOrLose");
		_winObj = winOrLoseObj.Find( "win" ).gameObject;
		_winObj.SetActive( false );
		_loseObj = winOrLoseObj.Find( "lose" ).gameObject;
		_loseObj.SetActive( false );


		_killText = canvasObj.transform.Find("Kill" ).Find( "numText" ).GetComponent<TextMeshProUGUI>();
		_scoreText = canvasObj.transform.Find("Score").Find( "numText" ).GetComponent<TextMeshProUGUI>();

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
			{
				int num1 = UnityEngine.Random.Range( 1 , 999 );
				int num2 = UnityEngine.Random.Range( 1 , 999 );
				int num3 = UnityEngine.Random.Range( 1 , 999 );
				_scoreText.text = num1.ToString() + "," + num2.ToString() + "," + num3.ToString();
			}
			yield return null;
		}
		_killText.text = _reusltData._secondList.Count.ToString("#,0");

		yield return null;

		frame = 120;
		nowFrame = 0;
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
				_scoreText.text = num1.ToString() + "," + num2.ToString() + "," + num3.ToString();
			}
			yield return null;
		}

		int score = 0;
		foreach( var second in _reusltData._secondList )
		{
			float add = 100f / second;
			score += (int)add;
		}
		_scoreText.text = score.ToString( "#,0" );


		frame = 30;
		nowFrame = 0;
		while( frame > nowFrame )
		{
			nowFrame++;
			if( MyInput.GetInstance().IsTouchTrigger() )
			{
				break;
			}
			yield return null;
		}
		_winObj.SetActive( _reusltData._IsComplete );
		_loseObj.SetActive( !_reusltData._IsComplete );

		_isEndAnim = true;
	}

}