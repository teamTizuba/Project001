using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultScene : MonoBehaviour {

	public class ReusltData : SceneData
	{
		public List<int> m_killList = new List<int>();
	}

	ReusltData _reusltData;

	ResultUI _resultUI = new ResultUI();

	// Use this for initialization
	void Start () {
		SystemManager.GetInstance().Update();
		var reusltData = SystemManager.GetInstance().GetSceneData() as ReusltData;

		if( reusltData == null )
		{
			reusltData = new ReusltData();
			for( int i = 0 ; i < 20 ; i++ )
			{
				reusltData.m_killList.Add( i % GameData.dataArray.Length );
			}
		}

		_resultUI.Init( this , reusltData );
	}
	
	// Update is called once per frame
	void Update () {
		SystemManager.GetInstance().Update();
		_resultUI.Update();
	}
}
