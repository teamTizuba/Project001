using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultScene : MonoBehaviour {

	public class ReusltData : SceneData
	{
		public bool _IsComplete = false;
		public List<float> _secondList = new List<float>();
	}

	ReusltData _reusltData;

	ResultUI _resultUI = new ResultUI();

	// Use this for initialization
	void Start () {
		_reusltData = SystemManager.GetInstance().GetSceneData() as ReusltData;
	}
	
	// Update is called once per frame
	void Update () {
		SystemManager.GetInstance().Update();
		_resultUI.Update();
	}
}
