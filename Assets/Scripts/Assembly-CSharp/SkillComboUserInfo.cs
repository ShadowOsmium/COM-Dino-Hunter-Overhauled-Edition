public class SkillComboUserInfo : SkillComboRateInfo
{
	public float m_fCurCoolDown;

	public float m_fMaxCoolDown;

	public SkillComboUserInfo(int nID, float fRate, float fCoolDown)
		: base(nID, fRate)
	{
		m_fCurCoolDown = fCoolDown;
		m_fMaxCoolDown = fCoolDown;
	}

	public void Update(float deltaTime)
	{
		if (!IsCoolDown())
		{
			m_fCurCoolDown -= deltaTime;
		}
	}

	public bool IsCoolDown()
	{
		return m_fCurCoolDown < 0f;
	}

	public void ResetCoolDown()
	{
		m_fCurCoolDown = m_fMaxCoolDown;
	}
}
