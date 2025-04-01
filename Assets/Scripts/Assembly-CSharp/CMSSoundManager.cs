public class CMSSoundManager
{
	protected static CMSSoundManager m_Instance;

	public static CMSSoundManager GetInstance()
	{
		if (m_Instance == null)
		{
			m_Instance = new CMSSoundManager();
		}
		return m_Instance;
	}
}
