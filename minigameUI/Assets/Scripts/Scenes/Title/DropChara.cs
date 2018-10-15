using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class DropChara
{
	GameObject _gameObject;
	Image _image;
	bool _canUpdate = false;
	float _speed;

	public void Init( GameObject gameObject )
	{
		_gameObject = gameObject;
		_image = gameObject.GetComponent<Image>();
	}

	public void Setup( Sprite sprite , Vector3 pos , float speed )
	{
		_image.sprite = sprite;

		var rectTransform = _gameObject.GetComponent<RectTransform>();
		rectTransform.localPosition = new Vector3( pos.x , pos.y , pos.z );
		_speed = speed;
		rectTransform.Rotate( 0 , 0 , UnityEngine.Random.Range(0,12) * 30 );
	}

	public void SetCanUpdate( bool canUpdate )
	{
		_canUpdate = canUpdate;
	}

	public void Update()
	{
		if( !_canUpdate )
		{
			return;
		}
		var rectTransform = _gameObject.GetComponent<RectTransform>();
		rectTransform.localPosition = new Vector3(
			rectTransform.localPosition.x ,
			rectTransform.localPosition.y + _speed ,
			rectTransform.localPosition.z
		);

		rectTransform.Rotate(0,0,2);
	}

	public Vector3 GetPos()
	{
		var rectTransform = _gameObject.GetComponent<RectTransform>();
		return rectTransform.localPosition;
	}
}