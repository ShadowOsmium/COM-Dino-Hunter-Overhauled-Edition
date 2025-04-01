using TNetSdk;

public class nmsg_mob_action : nmsg_struct
{
	public int m_nMobUID;

	public int m_nAction;

	public override SFSObject Pack()
	{
		SFSObject sFSObject = new SFSObject();
		sFSObject.PutInt("uid", m_nMobUID);
		sFSObject.PutInt("action", m_nAction);
		return sFSObject;
	}

	public override void UnPack(SFSObject data)
	{
		m_nMobUID = data.GetInt("uid");
		m_nAction = data.GetInt("action");
	}
}
