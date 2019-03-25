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
			),
		new Data(
			"GameMain/Enemy004"
			),
		new Data(
			"GameMain/Enemy005"
			),
		new Data(
			"GameMain/Enemy006"
			),
		new Data(
			"GameMain/Enemy007"
			),
		new Data(
			"GameMain/Enemy008"
			),
		new Data(
			"GameMain/Enemy009"
			),
		new Data(
			"GameMain/Enemy010"
			),
		new Data(
			"GameMain/Enemy011"
			),
		new Data(
			"GameMain/Enemy012"
			),
		new Data(
			"GameMain/Enemy013"
			),
		new Data(
			"GameMain/Enemy014"
			),
		new Data(
			"GameMain/Enemy015"
			),
		new Data(
			"GameMain/Enemy016"
			)
	};

	// ビックリマークが出る前の時間.
	public static float actionMin = 0.1f;
	public static float actionMax = 0.5f;
}
