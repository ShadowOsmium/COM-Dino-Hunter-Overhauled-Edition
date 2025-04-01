using TNetSdk;
using UnityEngine;

public class nmsg_player_beatback : nmsg_struct
{
	public Vector3 m_v3BeatBackPoint;

	public override SFSObject Pack()
	{
		SFSObject sFSObject = new SFSObject();
		sFSObject.PutFloatArray("beatback", new float[3] { m_v3BeatBackPoint.x, m_v3BeatBackPoint.y, m_v3BeatBackPoint.z });
		return sFSObject;
	}

	public override void UnPack(SFSObject data)
	{
		float[] floatArray = data.GetFloatArray("beatback");
		if (floatArray.Length == 3)
		{
			m_v3BeatBackPoint = new Vector3(floatArray[0], floatArray[1], floatArray[2]);
		}
	}
}
