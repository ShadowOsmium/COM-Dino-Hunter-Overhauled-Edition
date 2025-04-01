using TNetSdk;

public class nmsg_mob_dead : nmsg_struct
{
	public int m_nMobUID;

	public override SFSObject Pack()
	{
		SFSObject sFSObject = new SFSObject();
		sFSObject.PutInt("uid", m_nMobUID);
		return sFSObject;
	}

	public override void UnPack(SFSObject data)
	{
		m_nMobUID = data.GetInt("uid");
	}
}
