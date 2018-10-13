using UnityEngine;

public class ResultUI
{
	public void Init( ResultScene.ReusltData reusltData)
	{
	}

	public void Update()
	{
		if( MyInput.GetInstance().IsTouchRelease() )
		{
			SystemManager.GetInstance().LoadScene( "TitleScene" );
		}
	}
}