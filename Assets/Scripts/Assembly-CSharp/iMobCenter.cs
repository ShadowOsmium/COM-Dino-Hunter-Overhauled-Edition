using System.Collections.Generic;
using System.Xml;

public class iMobCenter : iBaseCenter
{
	protected Dictionary<int, CMobInfo> m_dictMobInfo;

	public iMobCenter()
	{
		m_dictMobInfo = new Dictionary<int, CMobInfo>();
	}

	public CMobInfo Get(int nID)
	{
		if (!m_dictMobInfo.ContainsKey(nID))
		{
			return null;
		}
		return m_dictMobInfo[nID];
	}

	public CMobInfoLevel Get(int nID, int nLevel)
	{
		CMobInfo cMobInfo = Get(nID);
		if (cMobInfo == null)
		{
			return null;
		}
		if (nLevel > 60)
		{
			nLevel = 60;
		}
		return cMobInfo.Get(nLevel);
	}

	protected override void LoadData(string content)
	{
		m_dictMobInfo.Clear();
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(content);
		string value = string.Empty;
		m_nReadIndex = 0;
		XmlNode documentElement = xmlDocument.DocumentElement;
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			m_nReadIndex++;
			if (childNode.Name != "mob" || !MyUtils.GetAttribute(childNode, "id", ref value))
			{
				continue;
			}
			int num = int.Parse(value);
			if (!MyUtils.GetAttribute(childNode, "lvl", ref value))
			{
				continue;
			}
			int nLevel = int.Parse(value);
			CMobInfoLevel cMobInfoLevel = Get(num, nLevel);
			if (cMobInfoLevel == null)
			{
				CMobInfo cMobInfo = Get(num);
				if (cMobInfo == null)
				{
					cMobInfo = new CMobInfo();
					cMobInfo.nID = num;
					m_dictMobInfo.Add(num, cMobInfo);
				}
				cMobInfoLevel = cMobInfo.Get(nLevel);
				if (cMobInfoLevel == null)
				{
					cMobInfoLevel = new CMobInfoLevel();
					cMobInfoLevel.nLevel = nLevel;
					cMobInfo.Add(nLevel, cMobInfoLevel);
				}
			}
			if (MyUtils.GetAttribute(childNode, "rare", ref value))
			{
				cMobInfoLevel.nRareType = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "type", ref value))
			{
				cMobInfoLevel.nType = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "model", ref value))
			{
				cMobInfoLevel.nModel = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "name", ref value))
			{
				cMobInfoLevel.sName = value;
			}
			if (MyUtils.GetAttribute(childNode, "desc", ref value))
			{
				cMobInfoLevel.sDesc = value;
			}
			if (MyUtils.GetAttribute(childNode, "icon", ref value))
			{
				cMobInfoLevel.sIcon = value;
			}
			if (MyUtils.GetAttribute(childNode, "life", ref value))
			{
				cMobInfoLevel.fLife = float.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "movespeed", ref value))
			{
				cMobInfoLevel.fMoveSpeed = float.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "meleerange", ref value))
			{
				cMobInfoLevel.fMeleeRange = float.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "damage", ref value))
			{
				cMobInfoLevel.fDamage = float.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "skill", ref value))
			{
				cMobInfoLevel.ltSkill.Clear();
				string[] array = value.Split(',');
				for (int i = 0; i < array.Length; i++)
				{
					SkillComboRateInfo item = new SkillComboRateInfo(int.Parse(array[i]), 100f);
					cMobInfoLevel.ltSkill.Add(item);
				}
			}
			if (cMobInfoLevel.ltSkill != null && MyUtils.GetAttribute(childNode, "skillrate", ref value))
			{
				string[] array = value.Split(',');
				for (int j = 0; j < array.Length && j < cMobInfoLevel.ltSkill.Count; j++)
				{
					cMobInfoLevel.ltSkill[j].m_fRate = float.Parse(array[j]);
				}
			}
			if (MyUtils.GetAttribute(childNode, "skillpassive", ref value))
			{
				cMobInfoLevel.ltSkillPassive.Clear();
				string[] array = value.Split(',');
				for (int k = 0; k < array.Length; k++)
				{
					cMobInfoLevel.ltSkillPassive.Add(int.Parse(array[k]));
				}
			}
			if (MyUtils.GetAttribute(childNode, "hardiness", ref value))
			{
				cMobInfoLevel.fHardiness = float.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "aimanager", ref value))
			{
				cMobInfoLevel.nAIManagerID = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "movespeedrate", ref value))
			{
				cMobInfoLevel.fMoveSpeedRate = float.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "rushspeedrate", ref value))
			{
				cMobInfoLevel.fRushSpeedRate = float.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "iswaitrot", ref value))
			{
				cMobInfoLevel.isWaitRot = bool.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "goldrate", ref value))
			{
				cMobInfoLevel.nGoldRate = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "gold", ref value))
			{
				cMobInfoLevel.nGold = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "exp", ref value))
			{
				cMobInfoLevel.nExp = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "dropgroup", ref value))
			{
				cMobInfoLevel.nDropGroup = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "dropcount", ref value))
			{
				string[] array = value.Split(',');
				for (int l = 0; l < array.Length && l < cMobInfoLevel.arrDropCount.Length; l++)
				{
					cMobInfoLevel.arrDropCount[l] = int.Parse(array[l]);
				}
			}
			if (MyUtils.GetAttribute(childNode, "dropcountrate", ref value))
			{
				string[] array = value.Split(',');
				for (int m = 0; m < array.Length && m < cMobInfoLevel.arrDropCountRate.Length; m++)
				{
					cMobInfoLevel.arrDropCountRate[m] = int.Parse(array[m]);
				}
			}
			cMobInfoLevel.ltHardinessInfo.Clear();
			for (int n = 1; n <= 5; n++)
			{
				if (MyUtils.GetAttribute(childNode, "bodypart" + n, ref value))
				{
					string[] array = value.Split(',');
					if (array.Length == 4)
					{
						CHardinessInfo cHardinessInfo = new CHardinessInfo();
						cHardinessInfo.nPartID = int.Parse(array[0]);
						cHardinessInfo.fHardiness = float.Parse(array[1]);
						cHardinessInfo.nAnimEnum = int.Parse(array[2]);
						cHardinessInfo.fDmgRate = float.Parse(array[3]);
						cMobInfoLevel.ltHardinessInfo.Add(cHardinessInfo);
					}
				}
			}
		}
	}
}
