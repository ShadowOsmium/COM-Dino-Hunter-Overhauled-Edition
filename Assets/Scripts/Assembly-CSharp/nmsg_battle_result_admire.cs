using TNetSdk;

public class nmsg_battle_result_admire : nmsg_struct
{
	public int m_nId;

	public override SFSObject Pack()
	{
		SFSObject sFSObject = new SFSObject();
		sFSObject.PutInt("id", m_nId);
		return sFSObject;
	}

	public override void UnPack(SFSObject data)
	{
		m_nId = data.GetInt("id");
	}
}
