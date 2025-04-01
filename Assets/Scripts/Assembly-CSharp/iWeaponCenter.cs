using System.Collections.Generic;
using System.Xml;

public class iWeaponCenter : iBaseCenter
{
	protected Dictionary<int, CWeaponInfo> m_dictWeaponInfo;

	public iWeaponCenter()
	{
		m_dictWeaponInfo = new Dictionary<int, CWeaponInfo>();
	}

	public Dictionary<int, CWeaponInfo> GetData()
	{
		return m_dictWeaponInfo;
	}

	public CWeaponInfo Get(int nID)
	{
		if (!m_dictWeaponInfo.ContainsKey(nID))
		{
			return null;
		}
		return m_dictWeaponInfo[nID];
	}

	public CWeaponInfoLevel Get(int nID, int nLevel)
	{
		CWeaponInfo cWeaponInfo = Get(nID);
		if (cWeaponInfo == null)
		{
			return null;
		}
		return cWeaponInfo.Get(nLevel);
	}

	public int GetLvlCount(int nID)
	{
		CWeaponInfo cWeaponInfo = Get(nID);
		if (cWeaponInfo == null)
		{
			return 0;
		}
		return cWeaponInfo.GetLvlCount();
	}

	protected override void LoadData(string content)
	{
		m_dictWeaponInfo.Clear();
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(content);
		string value = string.Empty;
		XmlNode documentElement = xmlDocument.DocumentElement;
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			if (childNode.Name != "weapon" || !MyUtils.GetAttribute(childNode, "id", ref value))
			{
				continue;
			}
			int num = int.Parse(value);
			if (!MyUtils.GetAttribute(childNode, "lvl", ref value))
			{
				continue;
			}
			int nLevel = int.Parse(value);
			CWeaponInfo cWeaponInfo = Get(num);
			if (cWeaponInfo == null)
			{
				cWeaponInfo = new CWeaponInfo();
				cWeaponInfo.nID = num;
				m_dictWeaponInfo.Add(num, cWeaponInfo);
			}
			CWeaponInfoLevel cWeaponInfoLevel = cWeaponInfo.Get(nLevel);
			if (cWeaponInfoLevel == null)
			{
				cWeaponInfoLevel = new CWeaponInfoLevel();
				cWeaponInfoLevel.nLevel = nLevel;
				cWeaponInfo.Add(nLevel, cWeaponInfoLevel);
			}
			if (MyUtils.GetAttribute(childNode, "unlockstage", ref value))
			{
				cWeaponInfo.m_nUnlockStageID = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "unlockhunterlvl", ref value))
			{
				cWeaponInfo.m_nUnlockHunterLvl = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "type", ref value))
			{
				cWeaponInfoLevel.nType = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "elementtype", ref value))
			{
				cWeaponInfoLevel.nElementType = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "attackmode", ref value))
			{
				cWeaponInfoLevel.nAttackMode = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "attackmodevalue", ref value))
			{
				cWeaponInfoLevel.ltAttackModeValue.Clear();
				string[] array = value.Split(',');
				for (int i = 0; i < array.Length; i++)
				{
					cWeaponInfoLevel.ltAttackModeValue.Add(float.Parse(array[i]));
				}
			}
			if (MyUtils.GetAttribute(childNode, "actiontype", ref value))
			{
				cWeaponInfoLevel.nActionType = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "model", ref value))
			{
				cWeaponInfoLevel.nModel = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "audiofire", ref value))
			{
				cWeaponInfoLevel.sAudioFire = value;
			}
			if (MyUtils.GetAttribute(childNode, "eff_bullet", ref value))
			{
				cWeaponInfoLevel.nBullet = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "eff_fire", ref value))
			{
				cWeaponInfoLevel.nFire = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "eff_hit", ref value))
			{
				cWeaponInfoLevel.nHit = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "name", ref value))
			{
				cWeaponInfoLevel.sName = value;
			}
			else
			{
				cWeaponInfoLevel.sName = "WeaponID " + cWeaponInfo.nID;
			}
			if (MyUtils.GetAttribute(childNode, "desc", ref value))
			{
				cWeaponInfoLevel.sDesc = value;
			}
			else
			{
				cWeaponInfoLevel.sDesc = "This is Desc of WeaponID " + cWeaponInfo.nID;
			}
			if (MyUtils.GetAttribute(childNode, "icon", ref value))
			{
				cWeaponInfoLevel.sIcon = value;
				cWeaponInfoLevel.sAudioFire = value;
			}
			if (MyUtils.GetAttribute(childNode, "shootaudio", ref value))
			{
				cWeaponInfoLevel.sAudioFire = value;
			}
			if (MyUtils.GetAttribute(childNode, "damage", ref value))
			{
				cWeaponInfoLevel.fDamage = float.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "critical", ref value))
			{
				cWeaponInfoLevel.fCritical = float.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "criticaldmg", ref value))
			{
				cWeaponInfoLevel.fCriticalDmg = float.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "shootspeed", ref value))
			{
				cWeaponInfoLevel.fShootSpeed = float.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "msdownshoot", ref value))
			{
				cWeaponInfoLevel.fMSDownRateShoot = float.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "msdownequip", ref value))
			{
				cWeaponInfoLevel.fMSDownRateEquip = float.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "precise", ref value))
			{
				cWeaponInfoLevel.fPrecise = float.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "capacity", ref value))
			{
				cWeaponInfoLevel.nCapacity = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "func", ref value))
			{
				string[] array = value.Split(',');
				for (int j = 0; j < array.Length; j++)
				{
					cWeaponInfoLevel.arrFunc[j] = int.Parse(array[j]);
				}
			}
			if (MyUtils.GetAttribute(childNode, "valuex", ref value))
			{
				string[] array = value.Split(',');
				for (int k = 0; k < array.Length; k++)
				{
					cWeaponInfoLevel.arrValueX[k] = int.Parse(array[k]);
				}
			}
			if (MyUtils.GetAttribute(childNode, "valuey", ref value))
			{
				string[] array = value.Split(',');
				for (int l = 0; l < array.Length; l++)
				{
					cWeaponInfoLevel.arrValueY[l] = int.Parse(array[l]);
				}
			}
			if (MyUtils.GetAttribute(childNode, "elementup", ref value))
			{
				cWeaponInfoLevel.fElementUp = float.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "elementupmonster", ref value))
			{
				cWeaponInfoLevel.ltElementUpMonster.Clear();
				string[] array = value.Split(',');
				for (int m = 0; m < array.Length; m++)
				{
					cWeaponInfoLevel.ltElementUpMonster.Add(int.Parse(array[m]));
				}
			}
			if (MyUtils.GetAttribute(childNode, "elementdown", ref value))
			{
				cWeaponInfoLevel.fElementDown = float.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "elementdownmonster", ref value))
			{
				cWeaponInfoLevel.ltElementDownMonster.Clear();
				string[] array = value.Split(',');
				for (int n = 0; n < array.Length; n++)
				{
					cWeaponInfoLevel.ltElementDownMonster.Add(int.Parse(array[n]));
				}
			}
			if (MyUtils.GetAttribute(childNode, "materials", ref value))
			{
				cWeaponInfoLevel.ltMaterials.Clear();
				string[] array = value.Split(',');
				for (int num2 = 0; num2 < array.Length; num2++)
				{
					cWeaponInfoLevel.ltMaterials.Add(int.Parse(array[num2]));
				}
			}
			if (MyUtils.GetAttribute(childNode, "materialscount", ref value))
			{
				cWeaponInfoLevel.ltMaterialsCount.Clear();
				string[] array = value.Split(',');
				for (int num3 = 0; num3 < array.Length; num3++)
				{
					cWeaponInfoLevel.ltMaterialsCount.Add(int.Parse(array[num3]));
				}
			}
			if (MyUtils.GetAttribute(childNode, "iscrystalpurchase", ref value))
			{
				cWeaponInfoLevel.isCrystalPurchase = bool.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "purchaseprice", ref value))
			{
				cWeaponInfoLevel.nPurchasePrice = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "levelupdesc", ref value))
			{
				cWeaponInfoLevel.sLevelUpDesc = value;
			}
			else
			{
				cWeaponInfoLevel.sLevelUpDesc = "DMG: " + cWeaponInfoLevel.fDamage;
			}
		}
	}
}
