using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMainScene : MonoBehaviour
{

	enum eState
	{
		Load,		// データ読み込み.
		Appear,		// 登場演出.
		PreButtle,	// いつタッチ開始か…？.
		Buttle,		// 今だ！タッチ!.
		EnemyDown,	// 敵が倒れた.
	}
	eState m_state = eState.Load;
	float m_timer = 0;
	float m_timeLimit = 0f;
	float m_gameTime = 60f;
	Image m_mychara;
	Image m_enemy;
	Image m_action;
	Animation m_enemyAnimation;
	Text m_gameTimeText;
	int m_charaIndex = 0;
	ResultScene.ReusltData m_resultData = new ResultScene.ReusltData();

	// Use this for initialization
	void Start()
	{
		m_timeLimit = 3f;
		var canvas = GameObject.Find("Canvas");
		m_mychara = canvas.transform.Find("MyChara").GetComponent<Image>();
		m_enemy = canvas.transform.Find("Enemy").GetComponent<Image>();
		m_enemyAnimation = m_enemy.gameObject.GetComponent<Animation>();
		m_action = canvas.transform.Find("Action").GetComponent<Image>();
		m_gameTimeText = canvas.transform.Find("GameTime").GetComponent<Text>();
		m_enemy.sprite = Resources.Load<Sprite>(GameData.dataArray[m_charaIndex].resourceName);
	}

	// Update is called once per frame
	void Update()
	{
		SystemManager.GetInstance().Update();
		switch (m_state) {
		case eState.Load:
			UpdateLoad();
			break;
		case eState.Appear:
			UpdateAppear();
			break;
		case eState.PreButtle:
			UpdatePreButtle();
			break;
		case eState.Buttle:
			UpdateButtle();
			break;
		case eState.EnemyDown:
			UpdateEnemyDown();
			break;
		}
		m_gameTime -= Time.deltaTime;
		if (m_gameTime < 0f) {
			SystemManager.GetInstance().LoadScene("ResultScene");
		}
		m_gameTimeText.text = string.Format("{0:0.00}", m_gameTime);
		//m_gameTimeText.text = m_gameTime.ToString();
	}

	void UpdateLoad()
	{
		m_enemyAnimation.Play("EnemyAppear");
		m_state = eState.Appear;
	}
	void UpdateAppear()
	{
		if (m_enemyAnimation.isPlaying == false) {
			m_timeLimit = Random.RandomRange(GameData.actionMin, GameData.actionMax);
			m_timer = 0f;
			m_state = eState.PreButtle;
		}
	}
	void UpdatePreButtle()
	{
		m_timer += Time.deltaTime;
		if (m_timer >= m_timeLimit) {
			m_action.gameObject.SetActive(true);
			m_state = eState.Buttle;
			m_timer = 0f;
		} else {
			if (MyInput.GetInstance().IsTouchTrigger()) {
				m_gameTime -= 5f;
			}
		}
	}

	void UpdateButtle()
	{
		m_timer += Time.deltaTime;
		if (MyInput.GetInstance().IsTouchTrigger()) {
			m_enemyAnimation.Play("EnemyDown");
			m_action.gameObject.SetActive(false);
			m_charaIndex++;
			m_state = eState.EnemyDown;
		}
	}
	void UpdateEnemyDown()
	{
		if (m_enemyAnimation.isPlaying == false) {
			m_charaIndex = Random.RandomRange(0, GameData.dataArray.Length);
			m_enemy.sprite = Resources.Load<Sprite>(GameData.dataArray[m_charaIndex].resourceName);
			m_state = eState.Load;
		}
	}
}
