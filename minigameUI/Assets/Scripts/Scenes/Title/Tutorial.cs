using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Tutorial
{
	GameObject _gameObject;
	Button _endBtn;
	Action _endAction;
	public void SetEndAction( Action action )
	{
		_endAction = action;
	}

	public void Init( GameObject gameObject )
	{
		gameObject.SetActive( false );
		_gameObject = gameObject;
		_endBtn = gameObject.transform.Find( "endBtn" ).GetComponent<Button>();
		_endBtn.onClick.AddListener( () => {
			if( _endAction != null )
			{
				_endAction();
			}
			else
			{
				Close();
			}
		} );
	}

	public void Open()
	{
		_gameObject.SetActive( true );
	}

	public void Close()
	{
		_gameObject.SetActive( false );
	}
}