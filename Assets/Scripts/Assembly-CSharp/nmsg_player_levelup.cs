using TNetSdk;

public class nmsg_player_levelup : nmsg_struct
{
	public int m_nLevel;

	public override SFSObject Pack()
	{
		SFSObject sFSObject = new SFSObject();
		sFSObject.PutInt("level", m_nLevel);
		return sFSObject;
	}

	public override void UnPack(SFSObject data)
	{
		m_nLevel = data.GetInt("level");
	}
}
