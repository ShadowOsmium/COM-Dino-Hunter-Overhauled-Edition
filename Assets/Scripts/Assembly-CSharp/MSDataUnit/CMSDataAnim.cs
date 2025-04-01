using UnityEngine;

namespace MSDataUnit
{
	public class CMSDataAnim : CMSDataBase
	{
		public string m_sAnim;

		public WrapMode m_WrapMode;

		public float m_fAnimSpeed;

		public float m_fAnimTime;

		public CMSDataAnim()
		{
			m_Type = kMSDataType.Anim;
			m_sAnim = string.Empty;
		}

		public override string GetDesc()
		{
			return "anim: " + m_sAnim;
		}
	}
}
