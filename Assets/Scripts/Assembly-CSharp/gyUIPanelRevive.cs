using UnityEngine;

public class gyUIPanelRevive : MonoBehaviour
{
	public GameObject mTitleText;

	public GameObject mStatistics;

	public UILabel mGoldLabel;

	public UILabel mCrystalLabel;

	public gyUILifeBar mLifeBar;

	public GameObject mReviveButton;

	protected bool m_bShow;

	protected float m_fTime;

	protected float m_fTimeCount;

	protected bool m_bPause;

	private void Awake()
	{
		m_bShow = false;
		base.gameObject.SetActiveRecursively(false);
		m_bPause = false;
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (!m_bShow || m_bPause)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		if (!(m_fTimeCount < m_fTime))
		{
			return;
		}
		m_fTimeCount += deltaTime;
		if (mLifeBar != null)
		{
			mLifeBar.SetValue(1f - m_fTimeCount / m_fTime);
		}
		if (m_fTimeCount >= m_fTime)
		{
			mLifeBar.gameObject.SetActiveRecursively(false);
			if (mReviveButton != null)
			{
				mReviveButton.SetActiveRecursively(false);
			}
		}
	}

	public void Show(bool bShow)
	{
		m_bShow = bShow;
		base.gameObject.active = bShow;
		if (bShow)
		{
			base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
			return;
		}
		ShowStatistcs(bShow);
		base.transform.localPosition = new Vector3(10000f, 10000f, base.transform.localPosition.z);
	}

	public void ShowStatistcs(bool bShow)
	{
		mTitleText.SetActiveRecursively(bShow);
		mStatistics.SetActiveRecursively(bShow);
	}

	public void SetLostGold(int nGold)
	{
		if (!(mGoldLabel == null))
		{
			mGoldLabel.text = nGold.ToString();
		}
	}

	public void SetLostCrystal(int nCrystal)
	{
		if (!(mCrystalLabel == null))
		{
			mCrystalLabel.text = nCrystal.ToString();
		}
	}

	public void SetReviveTime(float fTime)
	{
		if (!(mLifeBar == null) && !(mReviveButton == null))
		{
			m_fTime = fTime;
			m_fTimeCount = 0f;
			mLifeBar.gameObject.SetActiveRecursively(true);
			mLifeBar.InitValue(1f);
			mReviveButton.SetActiveRecursively(true);
			PauseReviveTime(false);
		}
	}

	public void PauseReviveTime(bool bPause)
	{
		m_bPause = bPause;
	}

	public void ResetReviveTime()
	{
		SetReviveTime(m_fTime);
	}
}
