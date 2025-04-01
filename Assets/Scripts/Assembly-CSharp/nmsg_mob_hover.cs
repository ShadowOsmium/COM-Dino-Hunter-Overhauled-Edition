using TNetSdk;
using UnityEngine;

public class nmsg_mob_hover : nmsg_struct
{
	public int m_nMobUID;

	public Vector3 m_v3HoverPoint;

	public override SFSObject Pack()
	{
		SFSObject sFSObject = new SFSObject();
		sFSObject.PutInt("uid", m_nMobUID);
		sFSObject.PutFloatArray("hover", new float[3] { m_v3HoverPoint.x, m_v3HoverPoint.y, m_v3HoverPoint.z });
		return sFSObject;
	}

	public override void UnPack(SFSObject data)
	{
		m_nMobUID = data.GetInt("uid");
		float[] floatArray = data.GetFloatArray("hover");
		m_v3HoverPoint = new Vector3(floatArray[0], floatArray[1], floatArray[2]);
	}
}
