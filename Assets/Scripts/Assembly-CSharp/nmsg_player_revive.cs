using TNetSdk;

public class nmsg_player_revive : nmsg_struct
{
	public int m_nPlayerId;

	public override SFSObject Pack()
	{
		SFSObject sFSObject = new SFSObject();
		sFSObject.PutInt("netid", m_nPlayerId);
		return sFSObject;
	}

	public override void UnPack(SFSObject data)
	{
		m_nPlayerId = data.GetInt("netid");
	}
}
