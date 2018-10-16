using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour {

	TitleUI _titleUI = new TitleUI();

	static bool existBgm = false;
	// Use this for initialization
	void Start () {
		SystemManager.GetInstance().Update();
		Application.targetFrameRate = 60;
		if (existBgm == false) {
			var bgmPrefab = Resources.Load<GameObject>("GameMain/Sound/BGM");
			var bgmObj = Instantiate(bgmPrefab);
			DontDestroyOnLoad(bgmObj);
			existBgm = true;
		}
		SystemManager.GetInstance().Update();

		_titleUI.Init( this );

	}
	
	// Update is called once per frame
	void Update () {
		SystemManager.GetInstance().Update();
		_titleUI.Update();
	}


}
