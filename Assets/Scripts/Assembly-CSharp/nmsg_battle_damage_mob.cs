using TNetSdk;

public class nmsg_battle_damage_mob : nmsg_struct
{
	public int m_nMobUID;

	public bool m_bBlack;

	public float m_fDamage;

	public override SFSObject Pack()
	{
		SFSObject sFSObject = new SFSObject();
		sFSObject.PutInt("uid", m_nMobUID);
		sFSObject.PutBool("black", m_bBlack);
		sFSObject.PutFloat("damage", m_fDamage);
		return sFSObject;
	}

	public override void UnPack(SFSObject data)
	{
		m_nMobUID = data.GetInt("uid");
		m_bBlack = data.GetBool("black");
		m_fDamage = data.GetFloat("damage");
	}
}
