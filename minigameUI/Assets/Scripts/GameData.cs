using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class GameData{
	public class Data {
		/// <summary>
		/// 画像の名前.
		/// </summary>
		public string resourceName = string.Empty;

		public Data(string a)
		{
			resourceName = a;
		}
	}

	/// <summary>
	/// キャラデータ.
	/// </summary>
	public static Data[] dataArray = new Data[]
	{
		new Data(
			"GameMain/Enemy001"
			),
		new Data(
			"GameMain/Enemy002"
			),
		new Data(
			"GameMain/Enemy003"
			)
	};

	// ビックリマークが出る前の時間.
	public static float actionMin = 0.2f;
	public static float actionMax = 1f;
}
