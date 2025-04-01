using TNetSdk;
using UnityEngine;

public class nmsg_mob_move : nmsg_struct
{
	public int m_nMobUID;

	public Vector3 m_v3Src;

	public Vector3 m_v3Dst;

	public override SFSObject Pack()
	{
		SFSObject sFSObject = new SFSObject();
		sFSObject.PutInt("uid", m_nMobUID);
		sFSObject.PutFloatArray("src", new float[3] { m_v3Src.x, m_v3Src.y, m_v3Src.z });
		sFSObject.PutFloatArray("dst", new float[3] { m_v3Dst.x, m_v3Dst.y, m_v3Dst.z });
		return sFSObject;
	}

	public override void UnPack(SFSObject data)
	{
		m_nMobUID = data.GetInt("uid");
		float[] floatArray = data.GetFloatArray("src");
		m_v3Src = new Vector3(floatArray[0], floatArray[1], floatArray[2]);
		floatArray = data.GetFloatArray("dst");
		m_v3Dst = new Vector3(floatArray[0], floatArray[1], floatArray[2]);
	}
}
