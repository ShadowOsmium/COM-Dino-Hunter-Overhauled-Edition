using TNetSdk;

public class nmsg_player_switchweapon : nmsg_struct
{
	public int m_nWeaponID;

	public int m_nWeaponLvl;

	public override SFSObject Pack()
	{
		SFSObject sFSObject = new SFSObject();
		sFSObject.PutInt("weaponid", m_nWeaponID);
		sFSObject.PutInt("weaponlvl", m_nWeaponLvl);
		return sFSObject;
	}

	public override void UnPack(SFSObject data)
	{
		m_nWeaponID = data.GetInt("weaponid");
		m_nWeaponLvl = data.GetInt("weaponlvl");
	}
}
