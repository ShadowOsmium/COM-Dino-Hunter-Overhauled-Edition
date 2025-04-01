using TNetSdk;

public class nmsg_player_useskill : nmsg_struct
{
	public int m_nSkillID;

	public int m_nSkillLvl;

	public override SFSObject Pack()
	{
		SFSObject sFSObject = new SFSObject();
		sFSObject.PutInt("skillid", m_nSkillID);
		sFSObject.PutInt("skilllvl", m_nSkillLvl);
		return sFSObject;
	}

	public override void UnPack(SFSObject data)
	{
		m_nSkillID = data.GetInt("skillid");
		m_nSkillLvl = data.GetInt("skilllvl");
	}
}
