using System.Collections.Generic;

public class CAnimData
{
	private Dictionary<kAnimEnum, CAnimInfo> m_dictAnimData;

	public CAnimData()
	{
		m_dictAnimData = new Dictionary<kAnimEnum, CAnimInfo>();
	}

	public Dictionary<kAnimEnum, CAnimInfo> GetData()
	{
		return m_dictAnimData;
	}

	public string GetName(kAnimEnum type)
	{
		if (!m_dictAnimData.ContainsKey(type))
		{
			return string.Empty;
		}
		return m_dictAnimData[type].m_sAnimName;
	}

	public float GetStep(kAnimEnum type)
	{
		if (!m_dictAnimData.ContainsKey(type))
		{
			return 0f;
		}
		return m_dictAnimData[type].m_fStep;
	}

	public void Cleanup()
	{
		if (m_dictAnimData != null)
		{
			m_dictAnimData.Clear();
		}
	}

	public void Add(CAnimInfo info)
	{
		if (!m_dictAnimData.ContainsKey(info.m_Type))
		{
			m_dictAnimData.Add(info.m_Type, info);
		}
	}
}
