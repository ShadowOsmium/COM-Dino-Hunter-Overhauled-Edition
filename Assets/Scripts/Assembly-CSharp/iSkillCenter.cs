using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class iSkillCenter
{
	public string sFileNamePlayer = string.Empty;

	public string sMD5Player = string.Empty;

	public string sFileNameMonster = string.Empty;

	public string sMD5Monster = string.Empty;

	protected Dictionary<int, CSkillInfo> m_dictSkillInfo;

	protected Dictionary<int, CSkillComboInfo> m_dictSkillComboInfo;

	public iSkillCenter()
	{
		m_dictSkillInfo = new Dictionary<int, CSkillInfo>();
		m_dictSkillComboInfo = new Dictionary<int, CSkillComboInfo>();
	}

	public void Clear()
	{
		m_dictSkillInfo.Clear();
		m_dictSkillComboInfo.Clear();
	}

	public Dictionary<int, CSkillInfo> GetDataSkillInfo()
	{
		return m_dictSkillInfo;
	}

	public CSkillInfo GetSkillInfo(int nID)
	{
		if (!m_dictSkillInfo.ContainsKey(nID))
		{
			return null;
		}
		return m_dictSkillInfo[nID];
	}

	public CSkillInfoLevel GetSkillInfo(int nID, int nLevel)
	{
		CSkillInfo skillInfo = GetSkillInfo(nID);
		if (skillInfo == null)
		{
			return null;
		}
		return skillInfo.Get(nLevel);
	}

	public CSkillComboInfo GetSkillComboInfo(int nComboID)
	{
		if (!m_dictSkillComboInfo.ContainsKey(nComboID))
		{
			return null;
		}
		return m_dictSkillComboInfo[nComboID];
	}

	public bool IsContainBlackSkill(int nSkillID)
	{
		CSkillInfoLevel skillInfo = GetSkillInfo(nSkillID, 1);
		if (skillInfo == null)
		{
			return false;
		}
		for (int i = 0; i < skillInfo.arrFunc.Length; i++)
		{
			if (skillInfo.arrFunc[i] == 13)
			{
				return true;
			}
		}
		return false;
	}

	public bool IsContainBlackComboSkill(int nComboSkillID)
	{
		CSkillComboInfo skillComboInfo = GetSkillComboInfo(nComboSkillID);
		if (skillComboInfo == null || skillComboInfo.ltSkill == null)
		{
			return false;
		}
		for (int i = 0; i < skillComboInfo.ltSkill.Count; i++)
		{
			if (IsContainBlackSkill(skillComboInfo.ltSkill[i]))
			{
				return true;
			}
		}
		return false;
	}

	public bool Load_Monster(string sFileName)
	{
		sFileNameMonster = sFileName;
		string content = string.Empty;
		//if (!Utils.FileGetString(sFileName + iMacroDefine.SaveExpandedName, ref content))
		//{
		//	return false;
		//}
		//sMD5Monster = ;//MyUtils.GetMD5(content);
		Load_MonsterData(SpoofedData.LoadSpoof(sFileName));
		return true;
	}

	public bool Load_Player(string sFileName)
	{
		sFileNamePlayer = sFileName;
		string content = string.Empty;
		//if (!Utils.FileGetString(sFileName + iMacroDefine.SaveExpandedName, ref content))
		//{
		//	return false;
		//}
		//sMD5Player = ;//MyUtils.GetMD5(content);
		Load_PlayerData(SpoofedData.LoadSpoof(sFileName));
		return true;
	}

	public void OnFetchMonsterData(string content)
	{
		try
		{
			Load_MonsterData(content);
			MyUtils.SaveFile(Utils.SavePath() + "/" + sFileNameMonster + iMacroDefine.SaveExpandedName, content);
		}
		catch
		{
			Debug.Log("fetch " + sFileNameMonster + " failed");
		}
	}

	protected void Load_MonsterData(string content)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(content);
		XmlNode documentElement = xmlDocument.DocumentElement;
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			if (childNode.Name == "skillinfo")
			{
				LoadSkillInfo(childNode);
			}
			else if (childNode.Name == "comboinfo")
			{
				LoadSkillComboInfo(childNode);
			}
		}
	}

	public void OnFetchPlayerData(string content)
	{
		try
		{
			Load_PlayerData(content);
			MyUtils.SaveFile(Utils.SavePath() + "/" + sFileNamePlayer + iMacroDefine.SaveExpandedName, content);
		}
		catch
		{
			Debug.Log("fetch " + sFileNamePlayer + " failed");
		}
	}

	protected void Load_PlayerData(string content)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(content);
		XmlNode documentElement = xmlDocument.DocumentElement;
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			if (childNode.Name == "skillinfo")
			{
				LoadSkillInfo(childNode);
			}
		}
	}

	protected void LoadSkillInfo(XmlNode root)
	{
		string value = string.Empty;
		foreach (XmlNode childNode in root.ChildNodes)
		{
			if (childNode.Name != "skill" || !MyUtils.GetAttribute(childNode, "id", ref value))
			{
				continue;
			}
			int num = int.Parse(value);
			if (!MyUtils.GetAttribute(childNode, "lvl", ref value))
			{
				continue;
			}
			int nLevel = int.Parse(value);
			CSkillInfo cSkillInfo = GetSkillInfo(num);
			if (cSkillInfo == null)
			{
				cSkillInfo = new CSkillInfo();
				cSkillInfo.nID = num;
				m_dictSkillInfo.Add(num, cSkillInfo);
			}
			CSkillInfoLevel cSkillInfoLevel = cSkillInfo.Get(nLevel);
			if (cSkillInfoLevel == null)
			{
				cSkillInfoLevel = new CSkillInfoLevel();
				cSkillInfoLevel.nID = num;
				cSkillInfoLevel.nLevel = nLevel;
				cSkillInfo.Add(nLevel, cSkillInfoLevel);
			}
			if (MyUtils.GetAttribute(childNode, "type", ref value))
			{
				cSkillInfoLevel.nType = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "rangetype", ref value))
			{
				cSkillInfoLevel.nRangeType = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "rangetypevalue", ref value))
			{
				cSkillInfoLevel.ltRangeValue.Clear();
				string[] array = value.Split(',');
				for (int i = 0; i < array.Length; i++)
				{
					cSkillInfoLevel.ltRangeValue.Add(float.Parse(array[i]));
				}
			}
			if (MyUtils.GetAttribute(childNode, "targetlimit", ref value))
			{
				cSkillInfoLevel.nTargetLimit = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "skillmode", ref value))
			{
				cSkillInfoLevel.nSkillMode = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "skillmodevalue", ref value))
			{
				cSkillInfoLevel.ltSkillModeValue.Clear();
				string[] array = value.Split(',');
				for (int j = 0; j < array.Length; j++)
				{
					cSkillInfoLevel.ltSkillModeValue.Add(float.Parse(array[j]));
				}
			}
			if (MyUtils.GetAttribute(childNode, "name", ref value))
			{
				cSkillInfoLevel.sName = value;
			}
			else
			{
				cSkillInfoLevel.sName = "Skill " + cSkillInfoLevel.nID;
			}
			if (MyUtils.GetAttribute(childNode, "desc", ref value))
			{
				cSkillInfoLevel.sDesc = value;
			}
			else
			{
				cSkillInfoLevel.sDesc = "This is desc of Skill " + cSkillInfoLevel.nID;
			}
			if (MyUtils.GetAttribute(childNode, "icon", ref value))
			{
				cSkillInfoLevel.sIcon = value;
			}
			if (MyUtils.GetAttribute(childNode, "targetmax", ref value))
			{
				cSkillInfoLevel.nTargetMax = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "func", ref value))
			{
				string[] array = value.Split(',');
				for (int k = 0; k < array.Length && k < cSkillInfoLevel.arrFunc.Length; k++)
				{
					cSkillInfoLevel.arrFunc[k] = int.Parse(array[k]);
				}
			}
			if (MyUtils.GetAttribute(childNode, "valuex", ref value))
			{
				string[] array = value.Split(',');
				for (int l = 0; l < array.Length && l < cSkillInfoLevel.arrValueX.Length; l++)
				{
					cSkillInfoLevel.arrValueX[l] = int.Parse(array[l]);
				}
			}
			if (MyUtils.GetAttribute(childNode, "valuey", ref value))
			{
				string[] array = value.Split(',');
				for (int m = 0; m < array.Length && m < cSkillInfoLevel.arrValueY.Length; m++)
				{
					cSkillInfoLevel.arrValueY[m] = int.Parse(array[m]);
				}
			}
			if (MyUtils.GetAttribute(childNode, "action", ref value))
			{
				cSkillInfoLevel.nAnim = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "actionspeed", ref value))
			{
				cSkillInfoLevel.fAnimSpeed = float.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "actionloop", ref value))
			{
				cSkillInfoLevel.nAnimLoop = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "actionbuff", ref value))
			{
				cSkillInfoLevel.nAnimBuffID = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "useaudio", ref value))
			{
				cSkillInfoLevel.sUseAudio = value;
			}
			if (MyUtils.GetAttribute(childNode, "unlocklevel", ref value))
			{
				cSkillInfo.nUnlockLevel = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "iscrystalunlock", ref value))
			{
				cSkillInfo.isCrystalUnlock = bool.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "unlockprice", ref value))
			{
				cSkillInfo.nUnlockPrice = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "iscrystalpurchase", ref value))
			{
				cSkillInfoLevel.isCrystalPurchase = bool.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "purchaseprice", ref value))
			{
				cSkillInfoLevel.nPurchasePrice = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "levelupdesc", ref value))
			{
				cSkillInfoLevel.sLevelUpDesc = value;
			}
			if (MyUtils.GetAttribute(childNode, "ismutiply", ref value))
			{
				cSkillInfoLevel.m_bMutiply = bool.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "mutiplyeff", ref value))
			{
				cSkillInfoLevel.m_fMutiplyEff = float.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "mutiplyefftime", ref value))
			{
				cSkillInfoLevel.m_fMutiplyEffTime = float.Parse(value);
			}
		}
	}

	protected void LoadSkillComboInfo(XmlNode root)
	{
		string value = string.Empty;
		foreach (XmlNode childNode in root.ChildNodes)
		{
			if (childNode.Name != "combo" || !MyUtils.GetAttribute(childNode, "id", ref value))
			{
				continue;
			}
			int num = int.Parse(value);
			CSkillComboInfo cSkillComboInfo = GetSkillComboInfo(num);
			if (cSkillComboInfo == null)
			{
				cSkillComboInfo = new CSkillComboInfo();
				cSkillComboInfo.nID = num;
				m_dictSkillComboInfo.Add(cSkillComboInfo.nID, cSkillComboInfo);
			}
			if (MyUtils.GetAttribute(childNode, "ignorecombolimit", ref value))
			{
				cSkillComboInfo.isIgnoreComboLimit = bool.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "coldown", ref value))
			{
				cSkillComboInfo.fCoolDown = float.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "skilllist", ref value))
			{
				cSkillComboInfo.ltSkill.Clear();
				string[] array = value.Split(',');
				for (int i = 0; i < array.Length; i++)
				{
					cSkillComboInfo.ltSkill.Add(int.Parse(array[i]));
				}
			}
			if (MyUtils.GetAttribute(childNode, "freezetime", ref value))
			{
				cSkillComboInfo.fFreezeTime = float.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "usecount", ref value))
			{
				cSkillComboInfo.nUseCount = int.Parse(value);
			}
		}
	}
}
