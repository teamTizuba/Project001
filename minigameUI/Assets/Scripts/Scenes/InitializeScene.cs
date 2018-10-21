using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeScene : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		var adMob = GameObject.Find("AdMob");
		DontDestroyOnLoad(adMob);
		SystemManager.GetInstance().LoadScene("TitleScene");
	}
}
	
