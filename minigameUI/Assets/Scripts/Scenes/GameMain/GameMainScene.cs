using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMainScene : MonoBehaviour
{

	enum eState
	{
		Ready,		// タッチ受付前.
		PreButtle,	// いつタッチ開始か…？.
		Buttle,		// 今だ！タッチ!.
		End			// 終了～.
	}
	eState m_state = eState.Ready;
	float m_timer = 0.0f;
	float m_timeLimit = 0;
	Image m_mychara;
	Image m_enemy;
	Image m_action;
	Image m_win;
	Image m_lose;
	Image m_ready;
	int m_charaIndex = 0;
	ResultScene.ReusltData m_resultData = new ResultScene.ReusltData();

	// Use this for initialization
	void Start()
	{
		m_timeLimit = 3f;
		var canvas = GameObject.Find("Canvas");
		m_mychara = canvas.transform.Find("MyChara").GetComponent<Image>();
		m_enemy = canvas.transform.Find("Enemy").GetComponent<Image>();
		m_action = canvas.transform.Find("Action").GetComponent<Image>();
		m_win = canvas.transform.Find("Win").GetComponent<Image>();
		m_lose = canvas.transform.Find("Lose").GetComponent<Image>();
		m_ready = canvas.transform.Find("Ready").GetComponent<Image>();
		m_enemy.sprite = Resources.Load<Sprite>(GameData.dataArray[m_charaIndex].resourceName);
	}

	// Update is called once per frame
	void Update()
	{
		SystemManager.GetInstance().Update();
		switch (m_state) {
		case eState.Ready:
			UpdareReady();
			break;
		case eState.PreButtle:
			UpdatePreButtle();
			break;
		case eState.Buttle:
			UpdateButtle();
			break;
		case eState.End:
			UpdateEnd();
			break;
		}
	}

	void UpdareReady()
	{
		m_timer += Time.deltaTime;
		if (m_timer >= m_timeLimit) {
			m_timeLimit = Random.RandomRange(GameData.actionMin, GameData.actionMax);
			m_timer = 0f;
			m_state = eState.PreButtle;
			m_ready.gameObject.SetActive(false);
		}
	}
	void UpdatePreButtle()
	{
		m_timer += Time.deltaTime;
		if (m_timer >= m_timeLimit) {
			m_action.gameObject.SetActive(true);
			m_state = eState.Buttle;
			m_timeLimit = Random.RandomRange(GameData.dataArray[m_charaIndex].timeMin, GameData.dataArray[m_charaIndex].timeMax);
			m_timer = 0f;
		} else {
			if (MyInput.GetInstance().IsTouchTrigger()) {
				m_lose.gameObject.SetActive(true);
				m_timer = 0f;
				m_timeLimit = 5.0f;
				m_charaIndex++;
				m_state = eState.End;
			}
		}
	}

	void UpdateButtle()
	{
		bool isEnd = false;
		bool isWin = false;
		m_timer += Time.deltaTime;
		if (MyInput.GetInstance().IsTouchTrigger()) {
			isWin = true;
			isEnd = true;
		} else if (m_timer >= m_timeLimit) {
			isEnd = true;
		}

		if (isEnd) {
			m_mychara.transform.localPosition = new Vector3(m_mychara.transform.localPosition.x, 200f, m_mychara.transform.localPosition.z);
			m_enemy.transform.localPosition = new Vector3(m_mychara.transform.localPosition.x, -150f, m_mychara.transform.localPosition.z);
			if (isWin) {
				m_resultData._secondList.Add(m_timer);
				m_win.gameObject.SetActive(true);
			} else {
				m_lose.gameObject.SetActive(true);
			}
			m_action.gameObject.SetActive(false);
			m_charaIndex++;
			m_timer = 0f;
			m_timeLimit = 5.0f;
			m_state = eState.End;
		}
	}
	void UpdateEnd()
	{
		m_timer += Time.deltaTime;
		if (m_timer >= m_timeLimit) {
			if(m_charaIndex >= GameData.dataArray.Length || m_resultData._secondList.Count!=m_charaIndex) {
				m_resultData._IsComplete = m_resultData._secondList.Count == m_charaIndex;
				SystemManager.GetInstance().SetSceneData(m_resultData);
				SystemManager.GetInstance().LoadScene("ResultScene");
			} else {
				m_win.gameObject.SetActive(false);
				m_lose.gameObject.SetActive(false);
				m_action.gameObject.SetActive(false);
				m_ready.gameObject.SetActive(true);
				m_state = eState.Ready;
				m_timer = 0f;
				m_timeLimit = 3f;
				m_mychara.transform.localPosition = new Vector3(m_mychara.transform.localPosition.x, -200f, m_mychara.transform.localPosition.z);
				m_enemy.transform.localPosition = new Vector3(m_mychara.transform.localPosition.x, 250f, m_mychara.transform.localPosition.z);
				m_enemy.sprite = Resources.Load<Sprite>(GameData.dataArray[m_charaIndex].resourceName);
			}
		}
	}
}
