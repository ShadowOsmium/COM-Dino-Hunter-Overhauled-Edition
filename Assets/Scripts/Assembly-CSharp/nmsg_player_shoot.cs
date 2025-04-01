using TNetSdk;

public class nmsg_player_shoot : nmsg_struct
{
	public bool m_bShoot;

	public override SFSObject Pack()
	{
		SFSObject sFSObject = new SFSObject();
		sFSObject.PutBool("isShoot", m_bShoot);
		return sFSObject;
	}

	public override void UnPack(SFSObject data)
	{
		m_bShoot = data.GetBool("isShoot");
	}
}
