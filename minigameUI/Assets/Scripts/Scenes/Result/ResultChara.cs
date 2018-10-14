using UnityEngine;
using UnityEngine.UI;

public class ResultChara
{
	GameObject _gameObject;
	Vector3 _endPos;

	bool _canUpdate = false;
	bool _isEndDrop = false;

	public void Init( GameObject gameObject , int charaId )
	{
		_gameObject = gameObject;
		var image = gameObject.GetComponent<Image>();
		image.sprite = Resources.Load<Sprite>( GameData.dataArray[charaId].resourceName );
	}

	public void Setup( Vector3 pos , Vector3 endPos )
	{
		var rectTransform = _gameObject.GetComponent<RectTransform>();
		rectTransform.localPosition = new Vector3( pos.x , pos.y , pos.z );
		_endPos = endPos;
	}

	public void SetCanUpdate( bool canUpdate )
	{
		_canUpdate = canUpdate;
	}

	public void Update()
	{
		if( ! _canUpdate )
		{
			return;
		}

		var rectTransform = _gameObject.GetComponent<RectTransform>();
		

		if( ! _isEndDrop )
		{
			float dropSpeed = -15f;
			rectTransform.localPosition = new Vector3(
				rectTransform.localPosition.x ,
				rectTransform.localPosition.y + dropSpeed ,
				rectTransform.localPosition.z
			);
			if( rectTransform.localPosition.y < _endPos.y )
			{
				EndDrop();
			}
		}

	}

	public void EndDrop()
	{
		var rectTransform = _gameObject.GetComponent<RectTransform>();
		rectTransform.localPosition = new Vector3(
			rectTransform.localPosition.x ,
			_endPos.y ,
			rectTransform.localPosition.z
		);

		_isEndDrop = true;
	}

	public bool IsEndDrop()
	{
		return _isEndDrop;
	}

}