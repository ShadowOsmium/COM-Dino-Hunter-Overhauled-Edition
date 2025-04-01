using TNetSdk;

public class nmsg_mob_skill : nmsg_struct
{
	public int m_nMobUID;

	public int m_nComboSkillID;

	public int m_nComboSkillIndex;

	public int m_nTargetUID;

	public override SFSObject Pack()
	{
		SFSObject sFSObject = new SFSObject();
		sFSObject.PutInt("uid", m_nMobUID);
		sFSObject.PutInt("comboskillid", m_nComboSkillID);
		sFSObject.PutInt("comboskillindex", m_nComboSkillIndex);
		sFSObject.PutInt("target", m_nTargetUID);
		return sFSObject;
	}

	public override void UnPack(SFSObject data)
	{
		m_nMobUID = data.GetInt("uid");
		m_nComboSkillID = data.GetInt("comboskillid");
		m_nComboSkillIndex = data.GetInt("comboskillindex");
		m_nTargetUID = data.GetInt("target");
	}
}
