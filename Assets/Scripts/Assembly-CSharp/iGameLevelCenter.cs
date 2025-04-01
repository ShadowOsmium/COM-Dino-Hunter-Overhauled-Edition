using System.Collections.Generic;
using System.Xml;

public class iGameLevelCenter : iBaseCenter
{
	protected Dictionary<int, GameLevelInfo> m_dictGameLevelInfo;

	protected Dictionary<int, GameLevelGroupInfo> m_dictGameLevelGroupInfo;

	public iGameLevelCenter()
	{
		m_dictGameLevelInfo = new Dictionary<int, GameLevelInfo>();
		m_dictGameLevelGroupInfo = new Dictionary<int, GameLevelGroupInfo>();
	}

	protected override void LoadData(string content)
	{
		m_dictGameLevelInfo.Clear();
		m_dictGameLevelGroupInfo.Clear();
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(content);
		string value = string.Empty;
		XmlNode documentElement = xmlDocument.DocumentElement;
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			if (childNode.Name == "gamelevel")
			{
				if (!MyUtils.GetAttribute(childNode, "id", ref value))
				{
					continue;
				}
				GameLevelInfo gameLevelInfo = new GameLevelInfo();
				gameLevelInfo.nID = int.Parse(value);
				m_dictGameLevelInfo.Add(gameLevelInfo.nID, gameLevelInfo);
				if (MyUtils.GetAttribute(childNode, "scenename", ref value))
				{
					gameLevelInfo.sSceneName = value;
				}
				if (MyUtils.GetAttribute(childNode, "isskyscene", ref value))
				{
					gameLevelInfo.bIsSkyScene = bool.Parse(value);
				}
				if (MyUtils.GetAttribute(childNode, "name", ref value))
				{
					gameLevelInfo.sLevelName = value;
				}
				else
				{
					gameLevelInfo.sLevelName = "Level " + gameLevelInfo.nID;
				}
				if (MyUtils.GetAttribute(childNode, "desc", ref value))
				{
					gameLevelInfo.sLevelDesc = value;
				}
				else
				{
					gameLevelInfo.sLevelDesc = "This is desc of Level " + gameLevelInfo.nID;
				}
				if (MyUtils.GetAttribute(childNode, "icon", ref value))
				{
					gameLevelInfo.sIcon = value;
				}
				if (MyUtils.GetAttribute(childNode, "nav_plane", ref value))
				{
					gameLevelInfo.fNavPlane = float.Parse(value);
				}
				if (MyUtils.GetAttribute(childNode, "bp_cfg", ref value))
				{
					gameLevelInfo.nBirthPos = int.Parse(value);
				}
				if (MyUtils.GetAttribute(childNode, "gamewave", ref value))
				{
					string[] array = value.Split(',');
					for (int i = 0; i < array.Length; i++)
					{
						gameLevelInfo.ltGameWave.Add(int.Parse(array[i]));
					}
				}
				if (MyUtils.GetAttribute(childNode, "sp_cfg_sky", ref value))
				{
					gameLevelInfo.nDefaultSPSky = int.Parse(value);
				}
				if (MyUtils.GetAttribute(childNode, "sp_cfg_ground", ref value))
				{
					gameLevelInfo.nDefaultSPGround = int.Parse(value);
				}
				if (MyUtils.GetAttribute(childNode, "hp_cfg_def", ref value))
				{
					gameLevelInfo.nDefaultHoverPoint = int.Parse(value);
				}
				if (MyUtils.GetAttribute(childNode, "tpbegin_cfg", ref value))
				{
					gameLevelInfo.nTPBeginCfg = int.Parse(value);
				}
				if (MyUtils.GetAttribute(childNode, "tpend_cfg", ref value))
				{
					gameLevelInfo.nTPEndCfg = int.Parse(value);
				}
				if (MyUtils.GetAttribute(childNode, "task", ref value))
				{
					gameLevelInfo.nTaskID = int.Parse(value);
				}
				if (MyUtils.GetAttribute(childNode, "exp", ref value))
				{
					gameLevelInfo.nRewardExp = int.Parse(value);
				}
				if (MyUtils.GetAttribute(childNode, "gold", ref value))
				{
					gameLevelInfo.nRewardGold = int.Parse(value);
				}
				if (MyUtils.GetAttribute(childNode, "cutscene", ref value))
				{
					gameLevelInfo.sCutScene = value;
				}
				if (MyUtils.GetAttribute(childNode, "cutscenecontent", ref value))
				{
					gameLevelInfo.sCutSceneContent = value;
				}
				if (MyUtils.GetAttribute(childNode, "cutscene_ambience", ref value))
				{
					gameLevelInfo.sCutSceneAmbience = value;
				}
				if (MyUtils.GetAttribute(childNode, "BGM", ref value))
				{
					gameLevelInfo.sBGM = value;
				}
				if (MyUtils.GetAttribute(childNode, "BGM_ambience", ref value))
				{
					gameLevelInfo.sBGMAmbience = value;
				}
				if (MyUtils.GetAttribute(childNode, "proccess", ref value))
				{
					gameLevelInfo.m_nProccess = int.Parse(value);
				}
				if (MyUtils.GetAttribute(childNode, "limitmelee", ref value))
				{
					gameLevelInfo.m_bLimitMelee = bool.Parse(value);
				}
				foreach (XmlNode childNode2 in childNode.ChildNodes)
				{
					if (childNode2.Name == "MonsterNumLimit")
					{
						foreach (XmlNode childNode3 in childNode2.ChildNodes)
						{
							if (!(childNode3.Name != "limit"))
							{
								MonsterNumLimitInfo monsterNumLimitInfo = new MonsterNumLimitInfo();
								if (MyUtils.GetAttribute(childNode3, "type", ref value))
								{
									monsterNumLimitInfo.nLimitType = int.Parse(value);
								}
								if (MyUtils.GetAttribute(childNode3, "value", ref value))
								{
									monsterNumLimitInfo.nLimitValue = int.Parse(value);
								}
								if (MyUtils.GetAttribute(childNode3, "maxnumber", ref value))
								{
									monsterNumLimitInfo.nMax = int.Parse(value);
								}
								gameLevelInfo.ltMonsterNumLimit.Add(monsterNumLimitInfo);
							}
						}
					}
					else if (childNode2.Name == "trigger_sp")
					{
						if (!MyUtils.GetAttribute(childNode2, "trigger", ref value))
						{
							continue;
						}
						StartPointTrigger startPointTrigger = new StartPointTrigger();
						startPointTrigger.m_Trigger = new TriggerInfo();
						startPointTrigger.m_Trigger.nEventType = int.Parse(value);
						if (MyUtils.GetAttribute(childNode2, "triggervalue", ref value))
						{
							string[] array = value.Split(',');
							for (int j = 0; j < array.Length; j++)
							{
								startPointTrigger.m_Trigger.ltEventParam.Add(int.Parse(array[j]));
							}
						}
						if (MyUtils.GetAttribute(childNode2, "triggerloop", ref value))
						{
							startPointTrigger.m_Trigger.bEventLoop = bool.Parse(value);
						}
						if (MyUtils.GetAttribute(childNode2, "sp_cfg", ref value))
						{
							startPointTrigger.m_nStartPointCfg = int.Parse(value);
						}
					}
					else if (childNode2.Name == "trigger_hp")
					{
						if (!MyUtils.GetAttribute(childNode2, "trigger", ref value))
						{
							continue;
						}
						StartPointTrigger startPointTrigger2 = new StartPointTrigger();
						startPointTrigger2.m_Trigger = new TriggerInfo();
						startPointTrigger2.m_Trigger.nEventType = int.Parse(value);
						if (MyUtils.GetAttribute(childNode2, "triggervalue", ref value))
						{
							string[] array = value.Split(',');
							for (int k = 0; k < array.Length; k++)
							{
								startPointTrigger2.m_Trigger.ltEventParam.Add(int.Parse(array[k]));
							}
						}
						if (MyUtils.GetAttribute(childNode2, "triggerloop", ref value))
						{
							startPointTrigger2.m_Trigger.bEventLoop = bool.Parse(value);
						}
						if (MyUtils.GetAttribute(childNode2, "hp_cfg", ref value))
						{
							startPointTrigger2.m_nStartPointCfg = int.Parse(value);
						}
					}
					else if (childNode2.Name == "RewardMaterial")
					{
						gameLevelInfo.ltRewardMaterial.Clear();
						if (!MyUtils.GetAttribute(childNode2, "material", ref value))
						{
							continue;
						}
						string[] array = value.Split(',');
						if (array != null && array.Length > 0)
						{
							for (int l = 0; l < array.Length; l++)
							{
								CRewardMaterial cRewardMaterial = new CRewardMaterial();
								cRewardMaterial.nID = int.Parse(array[l]);
								gameLevelInfo.ltRewardMaterial.Add(cRewardMaterial);
							}
						}
						if (MyUtils.GetAttribute(childNode2, "mincount", ref value))
						{
							array = value.Split(',');
							if (array != null && array.Length > 0)
							{
								for (int m = 0; m < array.Length && m < gameLevelInfo.ltRewardMaterial.Count; m++)
								{
									gameLevelInfo.ltRewardMaterial[m].nMinCount = int.Parse(array[m]);
								}
							}
						}
						if (!MyUtils.GetAttribute(childNode2, "maxcount", ref value))
						{
							continue;
						}
						array = value.Split(',');
						if (array != null && array.Length > 0)
						{
							for (int n = 0; n < array.Length && n < gameLevelInfo.ltRewardMaterial.Count; n++)
							{
								gameLevelInfo.ltRewardMaterial[n].nMaxCount = int.Parse(array[n]);
							}
						}
					}
					else if (childNode2.Name == "recommand")
					{
						if (MyUtils.GetAttribute(childNode2, "recommandtype", ref value))
						{
							gameLevelInfo.m_nRecommandType = int.Parse(value);
						}
						if (MyUtils.GetAttribute(childNode2, "recommandid", ref value))
						{
							gameLevelInfo.m_nRecommandID = int.Parse(value);
						}
						if (MyUtils.GetAttribute(childNode2, "recommandlevel", ref value))
						{
							gameLevelInfo.m_nRecommandLevel = int.Parse(value);
						}
						if (MyUtils.GetAttribute(childNode2, "islimit", ref value))
						{
							gameLevelInfo.m_bRecommandLimit = bool.Parse(value);
						}
					}
				}
			}
			else
			{
				if (!(childNode.Name == "gamelevelgroup") || !MyUtils.GetAttribute(childNode, "id", ref value))
				{
					continue;
				}
				GameLevelGroupInfo gameLevelGroupInfo = new GameLevelGroupInfo();
				gameLevelGroupInfo.nID = int.Parse(value);
				m_dictGameLevelGroupInfo.Add(gameLevelGroupInfo.nID, gameLevelGroupInfo);
				if (MyUtils.GetAttribute(childNode, "name", ref value))
				{
					gameLevelGroupInfo.sName = value;
				}
				if (MyUtils.GetAttribute(childNode, "icon", ref value))
				{
					gameLevelGroupInfo.nIcon = int.Parse(value);
				}
				if (!MyUtils.GetAttribute(childNode, "levellist", ref value))
				{
					continue;
				}
				gameLevelGroupInfo.ltLevelList.Clear();
				string[] array = value.Split(',');
				if (array != null && array.Length > 0)
				{
					for (int num = 0; num < array.Length; num++)
					{
						gameLevelGroupInfo.ltLevelList.Add(int.Parse(array[num]));
					}
				}
			}
		}
	}

	public Dictionary<int, GameLevelInfo> GetData()
	{
		return m_dictGameLevelInfo;
	}

	public Dictionary<int, GameLevelGroupInfo> GetDataGroup()
	{
		if (m_dictGameLevelGroupInfo.Count == 0)
		{
			LoadData(SpoofedData.LoadSpoof("gamelevel"));
		}
		return m_dictGameLevelGroupInfo;
	}

	public GameLevelInfo Get(int nID)
	{
		if (!m_dictGameLevelInfo.ContainsKey(nID))
		{
			return null;
		}
		return m_dictGameLevelInfo[nID];
	}

	public GameLevelGroupInfo GetGroupInfo(int nID)
	{
		if (!m_dictGameLevelGroupInfo.ContainsKey(nID))
		{
			return null;
		}
		return m_dictGameLevelGroupInfo[nID];
	}

	public int GetGroupID(int nLevelID)
	{
		foreach (GameLevelGroupInfo value in m_dictGameLevelGroupInfo.Values)
		{
			if (value.ltLevelList == null)
			{
				continue;
			}
			foreach (int ltLevel in value.ltLevelList)
			{
				if (ltLevel == nLevelID)
				{
					return value.nID;
				}
			}
		}
		return -1;
	}

	public float GetProccess(int nLevelID)
	{
		int levelIndex = GetLevelIndex(nLevelID);
		if (levelIndex == -1)
		{
			return -1f;
		}
		int levelCount = GetLevelCount();
		if (levelCount < 1)
		{
			return -1f;
		}
		return (float)(levelIndex + 1) * 100f / (float)levelCount;
	}

	public int GetLevelIndex(int nLevelID)
	{
		int num = 0;
		foreach (GameLevelGroupInfo value in m_dictGameLevelGroupInfo.Values)
		{
			if (value.ltLevelList == null)
			{
				continue;
			}
			foreach (int ltLevel in value.ltLevelList)
			{
				if (ltLevel == nLevelID)
				{
					return num;
				}
				num++;
			}
		}
		return -1;
	}

	public int GetLevelBySeq(int nSeq)
	{
		int num = 0;
		foreach (GameLevelGroupInfo value in m_dictGameLevelGroupInfo.Values)
		{
			if (value.ltLevelList != null)
			{
				num += value.ltLevelList.Count;
				if (num >= nSeq)
				{
					return value.ltLevelList[value.ltLevelList.Count - 1 - (num - nSeq)];
				}
			}
		}
		return -1;
	}

	public List<int> GetLevelListBySeq(int nSeq)
	{
		List<int> list = new List<int>();
		int num = 0;
		foreach (GameLevelGroupInfo value in m_dictGameLevelGroupInfo.Values)
		{
			if (value.ltLevelList == null)
			{
				continue;
			}
			num += value.ltLevelList.Count;
			if (num < nSeq)
			{
				list.AddRange(value.ltLevelList);
				continue;
			}
			int num2 = value.ltLevelList.Count - 1 - (num - nSeq);
			for (int i = 0; i < num2; i++)
			{
				list.Add(value.ltLevelList[i]);
			}
			break;
		}
		return list;
	}

	public int GetNextLevel(int nLevelID)
	{
		bool flag = false;
		foreach (GameLevelGroupInfo value in m_dictGameLevelGroupInfo.Values)
		{
			if (value.ltLevelList == null)
			{
				continue;
			}
			foreach (int ltLevel in value.ltLevelList)
			{
				if (flag)
				{
					return ltLevel;
				}
				if (ltLevel == nLevelID)
				{
					flag = true;
				}
			}
		}
		return -1;
	}

	public int GetLevelCount()
	{
		int num = 0;
		foreach (GameLevelGroupInfo value in m_dictGameLevelGroupInfo.Values)
		{
			num += value.GetLevelCount();
		}
		return num;
	}
}
