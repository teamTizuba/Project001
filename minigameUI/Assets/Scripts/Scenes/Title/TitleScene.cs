﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		SystemManager.GetInstance().Update();

		if( MyInput.GetInstance().IsTouchTrigger() )
		{
			SystemManager.GetInstance().LoadScene( "GameMainScene" );
		}
	}


}
