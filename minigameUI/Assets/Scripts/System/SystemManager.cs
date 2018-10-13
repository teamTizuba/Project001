﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemManager : Singleton<SystemManager>
{
	protected override bool IsAddManager()
	{
		return false;
	}

	/// <summary>
	/// 全シーンのUodateの先頭で呼び出してほしいやつ
	/// </summary>
	public void Update()
	{
		MyInput.GetInstance().Update();
	}

	public void LoadScene( string sceneName )
	{
		SceneManager.LoadScene( sceneName );
		SingletonManager.GetInstance().ReleaseAll();
	}
}