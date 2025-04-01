using TNetSdk;
using UnityEngine;

public class nmsg_player_aim : nmsg_struct
{
	public Vector3 m_v3AimPoint;

	public override SFSObject Pack()
	{
		SFSObject sFSObject = new SFSObject();
		sFSObject.PutFloatArray("aimpoint", new float[3] { m_v3AimPoint.x, m_v3AimPoint.y, m_v3AimPoint.z });
		return sFSObject;
	}

	public override void UnPack(SFSObject data)
	{
		float[] floatArray = data.GetFloatArray("aimpoint");
		m_v3AimPoint = new Vector3(floatArray[0], floatArray[1], floatArray[2]);
	}
}
