using System.Collections.Generic;
using System.Xml;

public class iCharacterCenter : iBaseCenter
{
	protected Dictionary<int, CCharacterInfo> m_dictCharacterInfo;

	public iCharacterCenter()
	{
		m_dictCharacterInfo = new Dictionary<int, CCharacterInfo>();
	}

	public Dictionary<int, CCharacterInfo> GetData()
	{
		return m_dictCharacterInfo;
	}

	public CCharacterInfo Get(int nID)
	{
		if (!m_dictCharacterInfo.ContainsKey(nID))
		{
			return null;
		}
		return m_dictCharacterInfo[nID];
	}

	public CCharacterInfoLevel Get(int nID, int nLevel)
	{
		CCharacterInfo cCharacterInfo = Get(nID);
		if (cCharacterInfo == null)
		{
			return null;
		}
		return cCharacterInfo.Get(nLevel);
	}

	public CCharacterInfo GetInfoBySkillID(int nSkillID)
	{
		foreach (CCharacterInfo value in m_dictCharacterInfo.Values)
		{
			CCharacterInfoLevel cCharacterInfoLevel = value.Get(1);
			if (cCharacterInfoLevel == null || cCharacterInfoLevel.ltSkillPassive == null)
			{
				continue;
			}
			for (int i = 0; i < cCharacterInfoLevel.ltSkillPassive.Count; i++)
			{
				if (cCharacterInfoLevel.ltSkillPassive[i] == nSkillID)
				{
					return value;
				}
			}
		}
		return null;
	}

	protected override void LoadData(string content)
	{
		m_dictCharacterInfo.Clear();
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(content);
		string value = string.Empty;
		XmlNode documentElement = xmlDocument.DocumentElement;
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			if (childNode.Name != "character" || !MyUtils.GetAttribute(childNode, "id", ref value))
			{
				continue;
			}
			int num = int.Parse(value);
			if (!MyUtils.GetAttribute(childNode, "lvl", ref value))
			{
				continue;
			}
			int nLevel = int.Parse(value);
			CCharacterInfo cCharacterInfo = Get(num);
			if (cCharacterInfo == null)
			{
				cCharacterInfo = new CCharacterInfo();
				cCharacterInfo.nID = num;
				m_dictCharacterInfo.Add(num, cCharacterInfo);
			}
			CCharacterInfoLevel cCharacterInfoLevel = cCharacterInfo.Get(nLevel);
			if (cCharacterInfoLevel == null)
			{
				cCharacterInfoLevel = new CCharacterInfoLevel();
				cCharacterInfo.Add(nLevel, cCharacterInfoLevel);
			}
			if (MyUtils.GetAttribute(childNode, "unlocklevel", ref value))
			{
				cCharacterInfo.nUnLockLevel = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "iscrystalunlock", ref value))
			{
				cCharacterInfo.isCrystalUnLock = bool.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "unlockprice", ref value))
			{
				cCharacterInfo.nUnLockPrice = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "iscrystalpurchase", ref value))
			{
				cCharacterInfo.isCrystalPurchase = bool.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "purchaseprice", ref value))
			{
				cCharacterInfo.nPurchasePrice = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "characterpassiveskill", ref value))
			{
				cCharacterInfo.ltCharacterPassiveSkill.Clear();
				string[] array = value.Split(',');
				for (int i = 0; i < array.Length; i++)
				{
					cCharacterInfo.ltCharacterPassiveSkill.Add(int.Parse(array[i]));
				}
			}
			if (MyUtils.GetAttribute(childNode, "male", ref value))
			{
				cCharacterInfoLevel.isMale = bool.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "name", ref value))
			{
				cCharacterInfoLevel.sName = value;
			}
			else
			{
				cCharacterInfoLevel.sName = "Character " + cCharacterInfo.nID;
			}
			if (MyUtils.GetAttribute(childNode, "desc", ref value))
			{
				cCharacterInfoLevel.sDesc = value;
			}
			else
			{
				cCharacterInfoLevel.sDesc = "This is desc of Character " + cCharacterInfo.nID;
			}
			if (MyUtils.GetAttribute(childNode, "icon", ref value))
			{
				cCharacterInfoLevel.sIcon = value;
			}
			if (MyUtils.GetAttribute(childNode, "model", ref value))
			{
				cCharacterInfoLevel.nModel = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "life", ref value))
			{
				cCharacterInfoLevel.fLifeBase = float.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "skill", ref value))
			{
				cCharacterInfoLevel.nSkill = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "skillcd", ref value))
			{
				cCharacterInfoLevel.fSkillCD = float.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "skillpassive", ref value))
			{
				cCharacterInfoLevel.ltSkillPassive.Clear();
				string[] array = value.Split(',');
				for (int j = 0; j < array.Length; j++)
				{
					cCharacterInfoLevel.ltSkillPassive.Add(int.Parse(array[j]));
				}
			}
			if (MyUtils.GetAttribute(childNode, "exp", ref value))
			{
				cCharacterInfoLevel.nExp = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "avatarhead", ref value))
			{
				cCharacterInfoLevel.nAvatarHead = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "avatarupper", ref value))
			{
				cCharacterInfoLevel.nAvatarUpper = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "avatarlower", ref value))
			{
				cCharacterInfoLevel.nAvatarLower = int.Parse(value);
			}
		}
	}
}
