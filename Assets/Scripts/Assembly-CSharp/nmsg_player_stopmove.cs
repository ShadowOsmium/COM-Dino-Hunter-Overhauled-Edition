using TNetSdk;
using UnityEngine;

public class nmsg_player_stopmove : nmsg_struct
{
	public Vector3 m_v3StopPoint;

	public override SFSObject Pack()
	{
		SFSObject sFSObject = new SFSObject();
		sFSObject.PutFloatArray("stoppoint", new float[3] { m_v3StopPoint.x, m_v3StopPoint.y, m_v3StopPoint.z });
		return sFSObject;
	}

	public override void UnPack(SFSObject data)
	{
		float[] floatArray = data.GetFloatArray("stoppoint");
		m_v3StopPoint = new Vector3(floatArray[0], floatArray[1], floatArray[2]);
	}
}
