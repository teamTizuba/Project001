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
	GameObject m_downTimeObj = null;
	Animation m_downTimeAnim = null;
	Animation m_mycharaAnimation;
	Animation m_enemyAnimation;
	Text m_gameTimeText;
	int m_charaIndex = 0;
	ResultScene.ReusltData m_resultData = new ResultScene.ReusltData();
	AudioSource m_seAction = null;
	AudioSource m_seAttack = null;
	AudioSource m_seTimeDown = null;
	List<Sprite> m_animalSprites = new List<Sprite>();

	// Use this for initialization
	void Start()
	{
		m_timeLimit = 3f;
		for (int i = 0; i < GameData.dataArray.Length; ++i) {
			m_animalSprites.Add(Resources.Load<Sprite>(GameData.dataArray[i].resourceName));
		}
		m_charaIndex = Random.RandomRange(0, GameData.dataArray.Length);

		var canvas = GameObject.Find("Canvas");
		m_mychara = canvas.transform.Find("MyChara").GetComponent<Image>();
		m_mycharaAnimation = m_mychara.gameObject.GetComponent<Animation>();
		m_enemy = canvas.transform.Find("Enemy").GetComponent<Image>();
		m_enemyAnimation = m_enemy.gameObject.GetComponent<Animation>();
		m_action = canvas.transform.Find("Action").GetComponent<Image>();
		m_gameTimeText = canvas.transform.Find("GameTime").GetComponent<Text>();
		m_enemy.sprite = m_animalSprites[m_charaIndex];
		m_downTimeObj = canvas.transform.Find("DownTime").gameObject;
		m_downTimeAnim = m_downTimeObj.GetComponent<Animation>();
		var audio = GameObject.Find("Audio").transform;
		m_seAction = audio.Find("SEAction").GetComponent<AudioSource>();
		m_seAttack = audio.Find("SEAttack").GetComponent<AudioSource>();
		m_seTimeDown = audio.Find("SETimeDown").GetComponent<AudioSource>();

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
			SystemManager.GetInstance().SetSceneData( m_resultData );
			SystemManager.GetInstance().LoadScene("ResultScene");
			m_gameTimeText.text = 0.00f.ToString();
		} else {
			m_gameTimeText.text = string.Format("{0:0.00}", m_gameTime);
			if (m_downTimeObj.active) {
				if (m_downTimeAnim.isPlaying == false) {
					m_downTimeObj.active = false;
				}
			}
		}
	}

	void UpdateLoad()
	{
		m_enemyAnimation.Play("EnemyAppear");
		m_state = eState.Appear;
		if (MyInput.GetInstance().IsTouchTrigger()) {
			DownTime();
		}
	}
	void UpdateAppear()
	{
		if (m_enemyAnimation.isPlaying == false) {
			m_timeLimit = Random.RandomRange(GameData.actionMin, GameData.actionMax);
			m_timer = 0f;
			m_state = eState.PreButtle;
			return;
		}
		if (MyInput.GetInstance().IsTouchTrigger()) {
			DownTime();
		}
	}
	void UpdatePreButtle()
	{
		m_timer += Time.deltaTime;
		if (m_timer >= m_timeLimit) {
			m_seAction.Play();
			m_action.gameObject.SetActive(true);
			m_state = eState.Buttle;
			m_timer = 0f;
			return;
		}
		if (MyInput.GetInstance().IsTouchTrigger()) {
			DownTime();
		}
	}

	void UpdateButtle()
	{
		m_timer += Time.deltaTime;
		if (MyInput.GetInstance().IsTouchTrigger()) {
			m_seAttack.Play();
			m_mycharaAnimation.Play("MyCharaAttack");
			m_enemyAnimation.Play("EnemyDown");
			m_action.gameObject.SetActive(false);
			m_state = eState.EnemyDown;
			m_resultData.m_killList.Add(m_charaIndex);
		}
	}
	void UpdateEnemyDown()
	{
		if (MyInput.GetInstance().IsTouchTrigger()) {
			DownTime();
		}
		if (m_enemyAnimation.isPlaying == false) {
			int oldIndex = m_charaIndex;
			m_charaIndex = Random.RandomRange(0, GameData.dataArray.Length);
			if (oldIndex == m_charaIndex) {
				if(m_charaIndex < GameData.dataArray.Length - 1) {
					m_charaIndex++;
				} else {
					m_charaIndex = 0;
				}
			}
			m_enemy.sprite = m_animalSprites[m_charaIndex];
			m_state = eState.Load;
		}
	}
	void DownTime()
	{
		m_gameTime -= 5f;
		m_downTimeObj.active = true;
		m_downTimeAnim.Play("DownTime");
		m_seTimeDown.Play();
	}
}
