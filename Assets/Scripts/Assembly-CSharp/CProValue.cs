public class CProValue
{
	public float m_fValue;

	public float m_fValueBase;

	public float m_fValueAffectFromSkill;

	public float m_fValueAffectFromBuff;

	public float m_fValueAffectFromEquip;

	public float Value
	{
		get
		{
			return m_fValue;
		}
		set
		{
			m_fValue = value;
		}
	}

	public float ValueBase
	{
		get
		{
			return m_fValueBase;
		}
		set
		{
			m_fValueBase = value;
		}
	}

	public CProValue()
	{
		m_fValue = 0f;
		m_fValueBase = 0f;
		m_fValueAffectFromSkill = 0f;
		m_fValueAffectFromBuff = 0f;
		m_fValueAffectFromEquip = 0f;
	}

	public void UpdateValue()
	{
		m_fValue = m_fValueBase + m_fValueAffectFromSkill + m_fValueAffectFromBuff + m_fValueAffectFromEquip;
		if (m_fValue < 0f)
		{
			m_fValue = 0f;
		}
	}
}
