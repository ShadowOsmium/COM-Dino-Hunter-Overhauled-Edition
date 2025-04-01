namespace MSDataUnit
{
	public class CMSDataBase
	{
		public float m_fTimeBegin;

		public float m_fTimeEnd;

		public kMSDataType m_Type;

		public virtual string GetDesc()
		{
			return m_Type.ToString();
		}
	}
}
