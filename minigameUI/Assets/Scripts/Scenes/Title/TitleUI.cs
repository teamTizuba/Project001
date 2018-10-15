﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class TitleUI
{
	MonoBehaviour _monoBehaviour;
	GameObject _nextObj;

	List<DropChara> _frontDropCharaList = new List<DropChara>();
	List<DropChara> _backDropCharaList = new List<DropChara>();

	List<int> _dropIndexList = new List<int>();

	bool _isEndAnim = false;
	public void Init( MonoBehaviour monoBehaviour )
	{
		_monoBehaviour = monoBehaviour;
		var gameObject = monoBehaviour.gameObject;
		var canvasObj = gameObject.transform.Find( "Canvas" ).gameObject;
		_nextObj = canvasObj.transform.Find( "nextText" ).gameObject;
		_nextObj.SetActive( false );

		var charaParentObj = canvasObj.transform.Find( "charaParent" ).gameObject;
		{
			var frontObj = charaParentObj.transform.Find( "front" ).gameObject;
			for( int i = 0 ; i < 4 ; i++ )
			{
				var dropChara = new DropChara();
				dropChara.Init( frontObj.transform.Find( "enemy_" +(i+1) ).gameObject );

				dropChara.Setup(
					CalcCharaSprite() ,
					CalcFrontPos() + new Vector3(0,500 * i,0),
					-8f
				);
				dropChara.SetCanUpdate( false );
				_frontDropCharaList.Add( dropChara );
			}
		}
		{
			var backObj = charaParentObj.transform.Find( "back" ).gameObject;
			for( int i = 0 ; i < 6 ; i++ )
			{
				var dropChara = new DropChara();
				dropChara.Init( backObj.transform.Find( "enemy_" + (i + 1) ).gameObject );
				dropChara.Setup(
					CalcCharaSprite() ,
					CalcBackPos() + new Vector3( 0 , 300 * i , 0 ) ,
					-6f
				);
				dropChara.SetCanUpdate( false );
				_backDropCharaList.Add( dropChara );
			}
		}

		_monoBehaviour.StartCoroutine( AnimCoroutine() );

	}

	public void Update()
	{
		foreach( var dropChara in _frontDropCharaList )
		{
			dropChara.Update();
			if( dropChara.GetPos().y < -1000 )
			{
				dropChara.Setup(
					CalcCharaSprite() ,
					CalcFrontPos() ,
					-8f
				);
			}
		}
		foreach( var dropChara in _backDropCharaList )
		{
			dropChara.Update();
			if( dropChara.GetPos().y < -1000 )
			{
				dropChara.Setup(
					CalcCharaSprite() ,
					CalcBackPos() ,
					-7f
				);
			}
		}
		if( _isEndAnim )
		{
			if( MyInput.GetInstance().IsTouchTrigger() )
			{
				SystemManager.GetInstance().LoadScene( "GameMainScene" );
			}
		}
	}

	IEnumerator AnimCoroutine()
	{
		var gameObject = _monoBehaviour.gameObject;
		var animation = gameObject.transform.Find( "Canvas" ).Find("logo").GetComponent<Animation>();

		while( animation.isPlaying )
		{
			if( MyInput.GetInstance().IsTouchTrigger() && animation[animation.clip.name].normalizedTime > 0.1f )
			{
				animation[ animation.clip.name ].normalizedTime = 1f;
				break;
			}
			yield return null;
		}

		foreach( var dropChara in _frontDropCharaList )
		{
			dropChara.SetCanUpdate( true );
		}
		foreach( var dropChara in _backDropCharaList )
		{
			dropChara.SetCanUpdate( true );
		}


		_nextObj.SetActive( true );
		_isEndAnim = true;
	}

	int CalcDropIndex()
	{
		if( _dropIndexList.Count == 0 )
		{
			_dropIndexList.Add( 0 );
			_dropIndexList.Add( 1 );
			_dropIndexList.Add( 2 );
			_dropIndexList.Add( 3 );
		}
		var id = UnityEngine.Random.Range( 0 , _dropIndexList.Count );
		var dropIndex = _dropIndexList[id];
		_dropIndexList.Remove( dropIndex );

		return dropIndex;
	}

	Vector3 CalcFrontPos()
	{
		float size = 150;
		float x = size * (CalcDropIndex() - 1.5f) + UnityEngine.Random.Range( -40f , 40f );
		float y = 850;
		return new Vector3( x , y , 0 );
	}

	Vector3 CalcBackPos()
	{
		float size = 150;
		float x = size * (CalcDropIndex() - 1.5f) + UnityEngine.Random.Range( -40f , 40f );
		float y = 850;
		return new Vector3( x , y , 0 );
	}

	Sprite CalcCharaSprite()
	{
		int id = UnityEngine.Random.Range( 0 , GameData.dataArray.Length );
		return Resources.Load<Sprite>( GameData.dataArray[id].resourceName );
	}

}
