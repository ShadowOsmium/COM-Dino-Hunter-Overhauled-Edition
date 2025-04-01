using TNetSdk;

public class nmsg_lobby_player_to_other : nmsg_struct
{
	public CGameNetManager.CNetUserInfoBase m_NetUserInfoBase = new CGameNetManager.CNetUserInfoBase();

	public override SFSObject Pack()
	{
		SFSObject sFSObject = new SFSObject();
		sFSObject.PutUtfString("deviceid", m_NetUserInfoBase.m_sDeivceId);
		sFSObject.PutUtfString("name", m_NetUserInfoBase.m_sName);
		sFSObject.PutInt("hunterlvl", m_NetUserInfoBase.m_nHunterLvl);
		sFSObject.PutInt("hunterexp", m_NetUserInfoBase.m_nHunterExp);
		sFSObject.PutInt("charid", m_NetUserInfoBase.m_nCharID);
		sFSObject.PutInt("charlvl", m_NetUserInfoBase.m_nCharLvl);
		sFSObject.PutInt("charexp", m_NetUserInfoBase.m_nCharExp);
		sFSObject.PutInt("weaponid", m_NetUserInfoBase.m_nWeaponID);
		sFSObject.PutInt("weaponlvl", m_NetUserInfoBase.m_nWeaponLvl);
		sFSObject.PutInt("titleid", m_NetUserInfoBase.m_nTitle);
		sFSObject.PutInt("combatpower", m_NetUserInfoBase.m_nCombatPower);
		sFSObject.PutUtfString("signature", m_NetUserInfoBase.m_sSignature);
		sFSObject.PutIntArray("weaponlist", m_NetUserInfoBase.m_arrWeapon);
		sFSObject.PutInt("avatarhead", m_NetUserInfoBase.m_nAvatarHead);
		sFSObject.PutInt("avatarupper", m_NetUserInfoBase.m_nAvatarUpper);
		sFSObject.PutInt("avatarlower", m_NetUserInfoBase.m_nAvatarLower);
		sFSObject.PutInt("avatarheadup", m_NetUserInfoBase.m_nAvatarHeadup);
		sFSObject.PutInt("avatarneck", m_NetUserInfoBase.m_nAvatarNeck);
		sFSObject.PutInt("avatarbracelet", m_NetUserInfoBase.m_nAvatarBracelet);
		sFSObject.PutInt("avatarbadge", m_NetUserInfoBase.m_nAvatarBadge);
		sFSObject.PutInt("avatarstone", m_NetUserInfoBase.m_nAvatarStone);
		return sFSObject;
	}

	public override void UnPack(SFSObject data)
	{
		m_NetUserInfoBase.m_sDeivceId = data.GetUtfString("deviceid");
		m_NetUserInfoBase.m_sName = data.GetUtfString("name");
		m_NetUserInfoBase.m_nHunterLvl = data.GetInt("hunterlvl");
		m_NetUserInfoBase.m_nHunterExp = data.GetInt("hunterexp");
		m_NetUserInfoBase.m_nCharID = data.GetInt("charid");
		m_NetUserInfoBase.m_nCharLvl = data.GetInt("charlvl");
		m_NetUserInfoBase.m_nCharExp = data.GetInt("charexp");
		m_NetUserInfoBase.m_nWeaponID = data.GetInt("weaponid");
		m_NetUserInfoBase.m_nWeaponLvl = data.GetInt("weaponlvl");
		m_NetUserInfoBase.m_nTitle = data.GetInt("titleid");
		m_NetUserInfoBase.m_nCombatPower = data.GetInt("combatpower");
		m_NetUserInfoBase.m_sSignature = data.GetUtfString("signature");
		m_NetUserInfoBase.m_arrWeapon = data.GetIntArray("weaponlist");
		m_NetUserInfoBase.m_nAvatarHead = data.GetInt("avatarhead");
		m_NetUserInfoBase.m_nAvatarUpper = data.GetInt("avatarupper");
		m_NetUserInfoBase.m_nAvatarLower = data.GetInt("avatarlower");
		m_NetUserInfoBase.m_nAvatarHeadup = data.GetInt("avatarheadup");
		m_NetUserInfoBase.m_nAvatarNeck = data.GetInt("avatarneck");
		m_NetUserInfoBase.m_nAvatarBracelet = data.GetInt("avatarbracelet");
		m_NetUserInfoBase.m_nAvatarBadge = data.GetInt("avatarbadge");
		m_NetUserInfoBase.m_nAvatarStone = data.GetInt("avatarstone");
	}
}
