using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class GameData{
	public class Data {
		/// <summary>
		/// 画像の名前.
		/// </summary>
		public string resourceName = string.Empty;
		/// <summary>
		/// 攻撃までの時間(最小)　秒.
		/// </summary>
		public float timeMin = 0f;
		/// <summary>
		/// 攻撃までの時間(最大)　秒.
		/// </summary>
		public float timeMax = 0f;

		public Data(string a, float b, float c)
		{
			resourceName = a;
			timeMin = b;
			timeMax = c;
		}
	}

	/// <summary>
	/// キャラデータ.
	/// </summary>
	public static Data[] dataArray = new Data[]
	{
		new Data(
			"GameMain/Enemy001",
			1f,
			2f
			),
		new Data(
			"GameMain/Enemy002",
			0.5f,
			1f
			),
		new Data(
			"GameMain/Enemy003",
			0.2f,
			0.4f
			)
	};

	// ビックリマークが出る前の時間.
	public static float actionMin = 4f;
	public static float actionMax = 8f;
}
