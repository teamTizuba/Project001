using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMainScene : MonoBehaviour
{

	enum eState
	{
		Ready,
		Buttle,
		End
	}
	eState m_state = eState.Ready;
	float m_timer = 0.0f;
	float m_timeLimit = 0;
	Image m_mychara;
	Image m_enemy;
	Image m_action;
	Image m_win;
	Image m_lose;
	bool m_isWin = false;

	// Use this for initialization
	void Start()
	{
		m_timeLimit = Random.RandomRange(4.0f, 8.0f);
		var canvas = GameObject.Find("Canvas");
		m_mychara = canvas.transform.FindChild("MyChara").GetComponent<Image>();
		m_enemy = canvas.transform.FindChild("Enemy").GetComponent<Image>();
		m_action = canvas.transform.FindChild("Action").GetComponent<Image>();
		m_win = canvas.transform.FindChild("Win").GetComponent<Image>();
		m_lose = canvas.transform.FindChild("Lose").GetComponent<Image>();
	}

	// Update is called once per frame
	void Update()
	{
		SystemManager.GetInstance().Update();
		switch (m_state) {
		case eState.Ready:
			UpdateReady();
			break;
		case eState.Buttle:
			UpdateButtle();
			break;
		case eState.End:
			UpdateEnd();
			break;
		}
	}

	void UpdateReady()
	{
		m_timer += Time.deltaTime;
		if (m_timer >= m_timeLimit) {
			m_action.gameObject.SetActive(true);
			m_state = eState.Buttle;
			m_timeLimit = Random.RandomRange(0.2f, 0.8f);
			m_timer = 0f;
		} else {
			if (MyInput.GetInstance().IsTouchTrigger()) {
				m_lose.gameObject.SetActive(true);
				m_state = eState.End;
			}
		}
	}

	void UpdateButtle()
	{
		bool isEnd = false;
		m_timer += Time.deltaTime;
		if (MyInput.GetInstance().IsTouchTrigger()) {
			m_isWin = true;
			isEnd = true;
		} else if (m_timer >= m_timeLimit) {
			isEnd = true;
		}

		if (isEnd) {
			m_mychara.transform.localPosition = new Vector3(m_mychara.transform.localPosition.x, 200f, m_mychara.transform.localPosition.z);
			m_enemy.transform.localPosition = new Vector3(m_mychara.transform.localPosition.x, -150f, m_mychara.transform.localPosition.z);
			m_timeLimit = 5.0f;
			if (m_isWin) {
				m_win.gameObject.SetActive(true);
			} else {
				m_lose.gameObject.SetActive(true);
			}
			m_action.gameObject.SetActive(false);
			m_state = eState.End;
		}
	}
	void UpdateEnd()
	{
		m_timer += Time.deltaTime;
		if (m_timer >= m_timeLimit) {
		}
	}
}
