using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour {

	TitleUI _titleUI = new TitleUI();

	// Use this for initialization
	void Start () {
		SystemManager.GetInstance().Update();
		Application.targetFrameRate = 60;
		DontDestroyOnLoad(GameObject.Find("BGM"));
		SystemManager.GetInstance().Update();

		_titleUI.Init( this );

	}
	
	// Update is called once per frame
	void Update () {
		SystemManager.GetInstance().Update();
		_titleUI.Update();
	}


}
