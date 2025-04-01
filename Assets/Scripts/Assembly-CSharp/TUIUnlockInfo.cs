public class TUIUnlockInfo
{
	public UnlockType unlock_type;

	public int item_id;

	public string m_sPath = string.Empty;

	public TUIUnlockInfo(UnlockType m_unlock_type, int m_item_id = 0, string sPath = "")
	{
		unlock_type = m_unlock_type;
		item_id = m_item_id;
		m_sPath = sPath;
	}
}
