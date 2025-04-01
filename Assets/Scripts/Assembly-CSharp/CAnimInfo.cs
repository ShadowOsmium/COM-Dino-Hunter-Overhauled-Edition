public class CAnimInfo
{
	public kAnimEnum m_Type;

	public string m_sAnimName;

	public float m_fStep;

	public CAnimInfo(kAnimEnum type, string sAnimName)
	{
		m_Type = type;
		m_sAnimName = sAnimName;
		m_fStep = 0f;
	}

	public CAnimInfo(kAnimEnum type, string sAnimName, float step)
	{
		m_Type = type;
		m_sAnimName = sAnimName;
		m_fStep = step;
	}
}
