using System.Collections.Generic;
using TNetSdk;

public class nmsg_lobby_other_to_player : nmsg_struct
{
	public Dictionary<int, CGameNetManager.CNetUserInfoBase> m_dictNetUserInfoBase = new Dictionary<int, CGameNetManager.CNetUserInfoBase>();

	public override SFSObject Pack()
	{
		SFSObject sFSObject = new SFSObject();
		sFSObject.PutInt("count", m_dictNetUserInfoBase.Count);
		int num = 0;
		foreach (KeyValuePair<int, CGameNetManager.CNetUserInfoBase> item in m_dictNetUserInfoBase)
		{
			int key = item.Key;
			CGameNetManager.CNetUserInfoBase value = item.Value;
			sFSObject.PutInt("id" + num, key);
			sFSObject.PutUtfString("deviceid" + num, value.m_sDeivceId);
			sFSObject.PutUtfString("name" + num, value.m_sName);
			sFSObject.PutInt("hunterlvl" + num, value.m_nHunterLvl);
			sFSObject.PutInt("hunterexp" + num, value.m_nHunterExp);
			sFSObject.PutInt("charid" + num, value.m_nCharID);
			sFSObject.PutInt("charlvl" + num, value.m_nCharLvl);
			sFSObject.PutInt("charexp" + num, value.m_nCharExp);
			sFSObject.PutInt("weaponid" + num, value.m_nWeaponID);
			sFSObject.PutInt("weaponlvl" + num, value.m_nWeaponLvl);
			sFSObject.PutInt("titleid" + num, value.m_nTitle);
			sFSObject.PutInt("combatpower" + num, value.m_nCombatPower);
			sFSObject.PutUtfString("signature" + num, value.m_sSignature);
			sFSObject.PutIntArray("weaponlist" + num, value.m_arrWeapon);
			sFSObject.PutInt("avatarhead" + num, value.m_nAvatarHead);
			sFSObject.PutInt("avatarupper" + num, value.m_nAvatarUpper);
			sFSObject.PutInt("avatarlower" + num, value.m_nAvatarLower);
			sFSObject.PutInt("avatarheadup" + num, value.m_nAvatarHeadup);
			sFSObject.PutInt("avatarneck" + num, value.m_nAvatarNeck);
			sFSObject.PutInt("avatarbracelet" + num, value.m_nAvatarBracelet);
			sFSObject.PutInt("avatarbadge" + num, value.m_nAvatarBadge);
			sFSObject.PutInt("avatarstone" + num, value.m_nAvatarStone);
			num++;
		}
		return sFSObject;
	}

	public override void UnPack(SFSObject data)
	{
		int @int = data.GetInt("count");
		for (int i = 0; i < @int; i++)
		{
			int int2 = data.GetInt("id" + i);
			if (!m_dictNetUserInfoBase.ContainsKey(int2))
			{
				CGameNetManager.CNetUserInfoBase cNetUserInfoBase = new CGameNetManager.CNetUserInfoBase();
				cNetUserInfoBase.m_sDeivceId = data.GetUtfString("deviceid" + i);
				cNetUserInfoBase.m_sName = data.GetUtfString("name" + i);
				cNetUserInfoBase.m_nHunterLvl = data.GetInt("hunterlvl" + i);
				cNetUserInfoBase.m_nHunterExp = data.GetInt("hunterexp" + i);
				cNetUserInfoBase.m_nCharID = data.GetInt("charid" + i);
				cNetUserInfoBase.m_nCharLvl = data.GetInt("charlvl" + i);
				cNetUserInfoBase.m_nCharExp = data.GetInt("charexp" + i);
				cNetUserInfoBase.m_nWeaponID = data.GetInt("weaponid" + i);
				cNetUserInfoBase.m_nWeaponLvl = data.GetInt("weaponlvl" + i);
				cNetUserInfoBase.m_nTitle = data.GetInt("titleid" + i);
				cNetUserInfoBase.m_nCombatPower = data.GetInt("combatpower" + i);
				cNetUserInfoBase.m_sSignature = data.GetUtfString("signature" + i);
				cNetUserInfoBase.m_arrWeapon = data.GetIntArray("weaponlist" + i);
				cNetUserInfoBase.m_nAvatarHead = data.GetInt("avatarhead" + i);
				cNetUserInfoBase.m_nAvatarUpper = data.GetInt("avatarupper" + i);
				cNetUserInfoBase.m_nAvatarLower = data.GetInt("avatarlower" + i);
				cNetUserInfoBase.m_nAvatarHeadup = data.GetInt("avatarheadup" + i);
				cNetUserInfoBase.m_nAvatarNeck = data.GetInt("avatarneck" + i);
				cNetUserInfoBase.m_nAvatarBracelet = data.GetInt("avatarbracelet" + i);
				cNetUserInfoBase.m_nAvatarBadge = data.GetInt("avatarbadge" + i);
				cNetUserInfoBase.m_nAvatarStone = data.GetInt("avatarstone" + i);
				m_dictNetUserInfoBase.Add(int2, cNetUserInfoBase);
			}
		}
	}
}
