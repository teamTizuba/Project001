using UnityEngine;

public class MyInput : Singleton<MyInput>
{
	bool _isPress = false;
	bool _isTrigger = false;
	bool _isRelease = false;

	protected override bool IsAddManager()
	{
		return false;
	}
	public void Update()
	{
		bool isPressNext = false;

		if( IsSmartPhone() )
		{
			if( Input.touchCount > 0 )
			{
				isPressNext = true;
			}
		} else
		{
			//PC
			isPressNext = Input.GetMouseButton( 0 );
		}

		_isTrigger = ((!_isPress) && isPressNext);
		_isRelease = (_isPress && ( ! isPressNext) );
		_isPress = isPressNext;
	}


	bool IsSmartPhone()
	{
#if UNITY_EDITOR
		return false;
#elif UNITY_IPHONE
		return true;
#elif UNITY_ANDROID
		return true;
#endif
		return false;
	}

	public bool IsTouch()
	{
		return _isPress;
	}

	public bool IsTouchTrigger()
	{
		return _isTrigger;
	}

	public bool IsTouchRelease()
	{
		return _isRelease;
	}


	public bool GetTouchPos( out Vector2 pos )
	{
		if( IsSmartPhone() )
		{
			if( Input.touchCount > 0 )
			{
				pos = Input.GetTouch( 0 ).position;
				return true;
			}
		} else
		{
			if( Input.GetMouseButton( 0 ) )
			{
				pos = Input.mousePosition;
				return true;
			}
		}

		pos = new Vector2();
		return false;
	}
}
