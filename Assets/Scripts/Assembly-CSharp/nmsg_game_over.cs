using TNetSdk;

public class nmsg_game_over : nmsg_struct
{
	public bool m_bWin;

	public override SFSObject Pack()
	{
		SFSObject sFSObject = new SFSObject();
		sFSObject.PutBool("win", m_bWin);
		return sFSObject;
	}

	public override void UnPack(SFSObject data)
	{
		m_bWin = data.GetBool("win");
	}
}
