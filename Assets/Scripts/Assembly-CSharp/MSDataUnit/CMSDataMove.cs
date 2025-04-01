using UnityEngine;

namespace MSDataUnit
{
	public class CMSDataMove : CMSDataBase
	{
		public Vector3 m_v3Dst;

		public string m_sAnim;

		public float m_fAnimRate;

		public CMSDataMove()
		{
			m_Type = kMSDataType.Move;
			m_sAnim = string.Empty;
		}

		public override string GetDesc()
		{
			return "dst: " + m_v3Dst;
		}
	}
}
