using UnityEngine;

public class gyUIPlayerHUD : MonoBehaviour
{
	public UILabel m_Label;

	protected bool m_bActive;

	protected bool m_bShow;

	protected CCharBase m_Target;

	protected iGameSceneBase m_GameScene
	{
		get
		{
			return iGameApp.GetInstance().m_GameScene;
		}
	}

	private void Awake()
	{
		m_bActive = false;
		m_bShow = false;
		m_Target = null;
	}

	private void Start()
	{
	}

	private void LateUpdate()
	{
		if (m_bActive)
		{
			UpdatePos();
		}
	}

	public void Initialize(CCharBase target)
	{
		m_bActive = false;
		m_bShow = false;
		m_Target = target;
	}

	public void Destroy()
	{
		Object.Destroy(base.gameObject);
	}

	public void SetName(string sName)
	{
		if (!(m_Label == null))
		{
			m_Label.text = sName;
		}
	}

	public void SetActive(bool bActive)
	{
		m_bActive = bActive;
		Show(bActive);
	}

	public void Show(bool bShow)
	{
		m_bShow = bShow;
		if (bShow)
		{
			base.gameObject.SetActiveRecursively(bShow);
		}
		if (m_Label != null)
		{
			m_Label.enabled = bShow;
		}
	}

	protected void UpdatePos()
	{
		CCharUser user = m_GameScene.GetUser();
		if (user == null)
		{
			return;
		}
		if (Vector3.Dot(user.Dir2D, (m_Target.Pos - user.Pos).normalized) < 0f)
		{
			Show(false);
			return;
		}
		if (!m_bShow)
		{
			Show(true);
		}
		Vector3 v3Screen = Vector3.zero;
		if (m_GameScene.WorldToScreenPointNGUI(m_Target.GetBone(0).position, ref v3Screen))
		{
			base.transform.localPosition = v3Screen;
		}
	}
}
