using UnityEngine;
using UnityEngine.Advertisements;
using System;
using System.Collections;
using System.Collections.Generic;

public class MyUnityAds : Singleton<MyUnityAds>
{

	public IEnumerator ShowCoroutine()
	{
		// Wait until Unity Ads is initialized,
		//  and the default ad placement is ready.
		int count = 0;
		while( !Advertisement.isInitialized || !Advertisement.IsReady() )
		{
			count++;
			if(count > 10)
			{
				yield break;
			}
			yield return new WaitForSeconds( 0.5f );
		}

		// Show the default ad placement.
		Advertisement.Show();

		while( Advertisement.isShowing )
		{
			yield return null;
		}

	}

}