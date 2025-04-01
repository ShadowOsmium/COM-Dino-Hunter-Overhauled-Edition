using UnityEngine;

namespace MSDataUnit
{
	public class CMSDataTele : CMSDataBase
	{
		public Vector3 m_v3Dst;

		public CMSDataTele()
		{
			m_Type = kMSDataType.Tele;
		}

		public override string GetDesc()
		{
			return "dst: " + m_v3Dst;
		}
	}
}
