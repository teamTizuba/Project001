using UnityEngine;

static public class MyInput
{
	static public bool IsTouch()
	{
		if( Input.touchCount > 0 )
		{
			return true;
		}
		return false;
	}

	static public bool GetTouchPos( out Vector2 pos )
	{
		if( Input.touchCount > 0 )
		{
			pos = Input.GetTouch( 0 ).position;
			return true;
		}

		pos = new Vector2();
		return false;
	}
}
