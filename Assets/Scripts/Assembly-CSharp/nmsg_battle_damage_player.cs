using TNetSdk;

public class nmsg_battle_damage_player : nmsg_struct
{
	public int m_nPlayerUID;

	public float m_fCurHP;

	public float m_fMaxHP;

	public override SFSObject Pack()
	{
		SFSObject sFSObject = new SFSObject();
		sFSObject.PutInt("uid", m_nPlayerUID);
		sFSObject.PutFloat("curhp", m_fCurHP);
		sFSObject.PutFloat("maxhp", m_fMaxHP);
		return sFSObject;
	}

	public override void UnPack(SFSObject data)
	{
		m_nPlayerUID = data.GetInt("uid");
		m_fCurHP = data.GetFloat("curhp");
		m_fMaxHP = data.GetFloat("maxhp");
	}
}
