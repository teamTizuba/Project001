using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;

public class ResultUI
{
	GameObject _toTitleObj;
	TextMeshProUGUI _killText;
	MonoBehaviour _monoBehaviour;
	List<ResultChara> _resultCharaList = new List<ResultChara>();
	GameObject _charaParent;

	ResultScene.ReusltData _reusltData;

	bool _isEndAnim = false;

	public void Init( MonoBehaviour monoBehaviour , ResultScene.ReusltData reusltData )
	{
		_monoBehaviour = monoBehaviour;
		var gameObject = monoBehaviour.gameObject;
		var canvasObj = gameObject.transform.Find( "Canvas" );

		_reusltData = reusltData;
		_charaParent = canvasObj.transform.Find( "CharaParent" ).gameObject;

		_toTitleObj = canvasObj.transform.Find( "TouchToTitle" ).gameObject;
		_toTitleObj.SetActive( false );
		_killText = canvasObj.transform.Find( "Kill" ).Find( "numText" ).GetComponent<TextMeshProUGUI>();

		var resultCharaBase = Resources.Load<GameObject>( "Result/Chara" );
		var tempObjList = new List<GameObject>();
		foreach( var charaId in reusltData.m_killList )
		{
			var obj = GameObject.Instantiate<GameObject>( resultCharaBase );
			obj.transform.SetParent( _charaParent.transform , false );
			obj.transform.localPosition = new Vector3( 0 , 1000 , 0 );
			obj.transform.rotation = Quaternion.Euler( 0 , 0 , UnityEngine.Random.Range( 0 , 8 ) * 45 );
			tempObjList.Add( obj );
		}

		for( int i = 0 ; i < tempObjList.Count ; i++ )
		{
			var obj = tempObjList[tempObjList.Count - 1 - i];
			var resultChara = new ResultChara();
			resultChara.Init( obj , reusltData.m_killList[i] );
			_resultCharaList.Add( resultChara );
		}


		float startCharaY = 850;
		float endCharaY = -600;
		List<int> posXIndexList = new List<int>();
		posXIndexList.Add( 0 );
		posXIndexList.Add( 1 );
		posXIndexList.Add( 2 );
		posXIndexList.Add( 3 );
		float nowHight = 0;
		float addHight = 200;

		for( int i = 0 ; i < _resultCharaList.Count ; i++ )
		{
			var id = UnityEngine.Random.Range( 0 , posXIndexList.Count - 1 );
			var posXIndex = posXIndexList[id];
			posXIndexList.Remove( posXIndex );

			_resultCharaList[i].Setup(
				new Vector3( CalcPosX( posXIndex ) , startCharaY , 0 ) ,
				new Vector3( 0 , endCharaY + nowHight , 0 )
			);

			if( posXIndexList.Count == 0 )
			{
				posXIndexList.Add( 0 );
				posXIndexList.Add( 1 );
				posXIndexList.Add( 2 );
				posXIndexList.Add( 3 );

				nowHight += addHight;
			}
		}


		_monoBehaviour.StartCoroutine( AnimCoroutine() );
	}

	public void Update()
	{
		foreach( var resultChara in _resultCharaList )
		{
			resultChara.Update();
		}

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
		if( true )
		{
			var ienumerator = MyUnityAds.GetInstance().ShowCoroutine();
			while( ienumerator.MoveNext() )
			{
				yield return null;
			}
		}


		int nowDropNum = 0;
		int dropNumAddFrame = 30;
		int frame = 0;


		while( nowDropNum < _resultCharaList.Count )
		{
			if( MyInput.GetInstance().IsTouchTrigger() && nowDropNum  > 0)
			{
				break;
			}

			frame++;

			if( frame > dropNumAddFrame )
			{
				frame = 0;
				if( nowDropNum < _resultCharaList.Count )
				{
					_resultCharaList[nowDropNum].SetCanUpdate( true );
					nowDropNum++;
					_killText.text = nowDropNum.ToString( "#,0" );
				}
			}

			yield return null;
		}

		_killText.text = _resultCharaList.Count.ToString( "#,0" );

		bool isEndDrop = false;
		while( ! isEndDrop )
		{
			if( MyInput.GetInstance().IsTouchTrigger() )
			{
				foreach( var resultChara in _resultCharaList )
				{
					resultChara.EndDrop();
				}
				break;
			}

			isEndDrop = true;
			foreach( var resultChara in _resultCharaList )
			{
				if( resultChara.IsEndDrop() )
				{
					continue;
				}
				isEndDrop = false;
				break;
			}

			yield return null;
		}

		frame = 0;

		while( frame < 30 )
		{
			frame++;
			yield return null;
		}

		_toTitleObj.SetActive( true );
		_isEndAnim = true;
	}


	float CalcPosX( int index )
	{
		float size = 220;
		return size * ( index - 1.5f) + UnityEngine.Random.Range(-40f,40f);
	}

}