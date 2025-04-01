using TNetSdk;

public class nmsg_social_addfriend : nmsg_struct
{
	public string m_sId;

	public override SFSObject Pack()
	{
		SFSObject sFSObject = new SFSObject();
		sFSObject.PutUtfString("id", m_sId);
		return sFSObject;
	}

	public override void UnPack(SFSObject data)
	{
		m_sId = data.GetUtfString("id");
	}
}
