using UnityEngine;

[RequireComponent(typeof(gyUILifeBar))]
public class gyLifeBarHUD : MonoBehaviour
{
	public enum State
	{
		Hold,
		Fadeout
	}

	protected iGameSceneBase m_GameScene;

	protected gyUILifeBar m_UILifeBar;

	protected State m_State;

	protected float m_fTimeCount;

	protected float m_fFadeTime;

	protected float m_fHoldTime;

	protected bool m_bActive;

	protected bool m_bShow;

	protected CCharBase m_Target;

	protected TweenScale m_TweenScale;

	private void Awake()
	{
		m_GameScene = iGameApp.GetInstance().m_GameScene;
		m_UILifeBar = GetComponent<gyUILifeBar>();
		m_bActive = false;
		m_bShow = false;
		m_Target = null;
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (!m_bActive)
		{
			return;
		}
		UpdatePos();
		switch (m_State)
		{
		case State.Hold:
			m_fTimeCount += Time.deltaTime;
			if (!(m_fTimeCount < m_fHoldTime))
			{
				m_fTimeCount = 0f;
				m_State = State.Fadeout;
				m_TweenScale = TweenScale.Begin(base.gameObject, m_fFadeTime, Vector3.zero);
			}
			break;
		case State.Fadeout:
			m_fTimeCount += Time.deltaTime;
			if (!(m_fTimeCount < m_fFadeTime))
			{
				m_TweenScale.enabled = false;
				m_fTimeCount = 0f;
				SetActive(false);
			}
			break;
		}
	}

	public void Initialize(CCharBase target)
	{
		m_bActive = false;
		m_bShow = false;
		m_Target = target;
		m_UILifeBar.InitValue(1f);
	}

	public void SetTime(float fHold, float fFade)
	{
		m_fHoldTime = fHold;
		m_fFadeTime = fFade;
	}

	public void SetColor(Color color)
	{
		if (!(m_UILifeBar == null))
		{
			m_UILifeBar.SetColor(color);
		}
	}

	public void SetColorShallow(Color color)
	{
		if (!(m_UILifeBar == null))
		{
			m_UILifeBar.SetColorShallow(color);
		}
	}

	public void InitLife(float fRate)
	{
		if (!(m_UILifeBar == null))
		{
			m_UILifeBar.InitValue(fRate);
		}
	}

	public void SetLife(float fRate)
	{
		SetActive(true);
		m_UILifeBar.SetValue(fRate);
		base.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		m_State = State.Hold;
		m_fTimeCount = 0f;
	}

	public void SetActive(bool bActive)
	{
		m_bActive = bActive;
		Show(bActive);
	}

	public void Show(bool bShow)
	{
		m_bShow = bShow;
		base.gameObject.SetActiveRecursively(bShow);
	}

	protected void UpdatePos()
	{
		CCharUser user = m_GameScene.GetUser();
		if (user == null)
		{
			return;
		}
		Vector3 vector = m_Target.Pos - user.Pos;
		if (Vector3.Dot(user.Dir2D, vector.normalized) < 0f)
		{
			Show(false);
			return;
		}
		if (!m_bShow)
		{
			Show(true);
		}
		Vector3 position = m_Target.GetBone(4).position;
		Vector3 position2 = m_Target.GetBone(0).position;
		Vector3 v3World = Vector3.Lerp(position2, position, 1f - Mathf.Clamp01(vector.magnitude / 10f));
		Vector3 v3Screen = Vector3.zero;
		if (m_GameScene.WorldToScreenPointNGUI(v3World, ref v3Screen))
		{
			base.transform.localPosition = v3Screen;
		}
	}
}
