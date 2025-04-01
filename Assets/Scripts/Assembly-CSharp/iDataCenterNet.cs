using System;
using System.Collections.Generic;
using System.Xml;

public class iDataCenterNet
{
	protected CRankInfo[] m_arrRankInfo;

	protected CRankInfo[] m_arrRankInfoFriends;

	protected Dictionary<string, CNameCardInfo> m_dictNameCardInfo;

	protected float m_fRankInfo_RefreshTime;

	protected float m_fRankInfo_RefreshTimeCount;

	protected float m_fRankInfoFriends_RefreshTime;

	protected float m_fRankInfoFriends_RefreshTimeCount;

	public string m_sGameCenterCache { get; set; }

	public CRankInfo[] arrRankInfo
	{
		get
		{
			return m_arrRankInfo;
		}
		set
		{
			m_arrRankInfo = value;
		}
	}

	public CRankInfo[] arrRankInfoFriends
	{
		get
		{
			return m_arrRankInfoFriends;
		}
		set
		{
			m_arrRankInfoFriends = value;
		}
	}

	public iDataCenterNet()
	{
		m_dictNameCardInfo = new Dictionary<string, CNameCardInfo>();
		m_fRankInfo_RefreshTime = iMacroDefine.RankInfo_RefreshTime;
		m_fRankInfo_RefreshTimeCount = m_fRankInfo_RefreshTime;
		m_fRankInfoFriends_RefreshTime = iMacroDefine.RankInfoFriends_RefreshTime;
		m_fRankInfoFriends_RefreshTimeCount = m_fRankInfoFriends_RefreshTime;
		m_sGameCenterCache = string.Empty;
	}

	public void Update(float deltaTime)
	{
		foreach (CNameCardInfo value in m_dictNameCardInfo.Values)
		{
			value.Update(deltaTime);
		}
		if (m_fRankInfo_RefreshTimeCount < m_fRankInfo_RefreshTime)
		{
			m_fRankInfo_RefreshTimeCount += deltaTime;
		}
		if (m_fRankInfoFriends_RefreshTimeCount < m_fRankInfoFriends_RefreshTime)
		{
			m_fRankInfoFriends_RefreshTimeCount += deltaTime;
		}
	}

	public bool IsRankInfoExpired()
	{
		return m_fRankInfo_RefreshTimeCount >= m_fRankInfo_RefreshTime;
	}

	public bool IsRankInfoFriendsExpired()
	{
		return m_fRankInfoFriends_RefreshTimeCount >= m_fRankInfoFriends_RefreshTime;
	}

	public void ResetRankInfoTime()
	{
		m_fRankInfo_RefreshTimeCount = 0f;
	}

	public void ResetRankInfoFriendsTime()
	{
		m_fRankInfoFriends_RefreshTimeCount = 0f;
	}

	public CNameCardInfo GetNameCardInfo(string sID)
	{
		if (sID == null || sID.Length < 1)
		{
			return null;
		}
		if (!m_dictNameCardInfo.ContainsKey(sID))
		{
			return null;
		}
		return m_dictNameCardInfo[sID];
	}

	public void SetNameCardInfo(string sID, CNameCardInfo namecardinfo)
	{
		if (sID != null && sID.Length >= 1)
		{
			if (m_dictNameCardInfo.ContainsKey(sID))
			{
				m_dictNameCardInfo[sID] = namecardinfo;
			}
			else
			{
				m_dictNameCardInfo.Add(sID, namecardinfo);
			}
		}
	}

	public bool Load()
	{
		string content = string.Empty;
		if (!Utils.FileGetString("gamedatanet.xml", ref content))
		{
			return false;
		}
		LoadData(content);
		return true;
	}

	public void Save()
	{
		XmlDocument xmlDocument = new XmlDocument();
		SaveData(xmlDocument);
		string filename = Utils.SavePath() + "/gamedatanet.xml";
		xmlDocument.Save(filename);
	}

	protected void LoadData(string content)
	{
		if (content.Length < 1)
		{
			return;
		}
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(content);
		string value = string.Empty;
		XmlNode documentElement = xmlDocument.DocumentElement;
		foreach (XmlNode item in documentElement)
		{
			if (item.Name == "info")
			{
				if (MyUtils.GetAttribute(item, "gamecentercache", ref value))
				{
					m_sGameCenterCache = value;
				}
			}
			else if (item.Name == "namecard")
			{
				foreach (XmlNode item2 in item)
				{
					if (MyUtils.GetAttribute(item2, "id", ref value))
					{
						string text = value;
						if (!m_dictNameCardInfo.ContainsKey(text))
						{
							m_dictNameCardInfo.Add(text, new CNameCardInfo());
						}
						CNameCardInfo cNameCardInfo = m_dictNameCardInfo[text];
						cNameCardInfo.m_sID = text;
						if (MyUtils.GetAttribute(item2, "nickname", ref value))
						{
							cNameCardInfo.m_sNickName = value;
						}
						if (MyUtils.GetAttribute(item2, "photo", ref value))
						{
							cNameCardInfo.SetPhoto(Convert.FromBase64String(value));
						}
						if (MyUtils.GetAttribute(item2, "hunterlvl", ref value))
						{
							cNameCardInfo.m_nHunterLvl = int.Parse(value);
						}
						if (MyUtils.GetAttribute(item2, "hunterexp", ref value))
						{
							cNameCardInfo.m_nHunterExp = int.Parse(value);
						}
						if (MyUtils.GetAttribute(item2, "combatpower", ref value))
						{
							cNameCardInfo.m_nCombatPower = int.Parse(value);
						}
						if (MyUtils.GetAttribute(item2, "rank", ref value))
						{
							cNameCardInfo.m_nRank = int.Parse(value);
						}
						if (MyUtils.GetAttribute(item2, "beadmired", ref value))
						{
							cNameCardInfo.m_nBeAdmired = int.Parse(value);
						}
						if (MyUtils.GetAttribute(item2, "title", ref value))
						{
							cNameCardInfo.m_nTitle = int.Parse(value);
						}
						if (MyUtils.GetAttribute(item2, "gold", ref value))
						{
							cNameCardInfo.m_nGold = int.Parse(value);
						}
						if (MyUtils.GetAttribute(item2, "crystal", ref value))
						{
							cNameCardInfo.m_nCrystal = int.Parse(value);
						}
						if (MyUtils.GetAttribute(item2, "sceneproccess", ref value))
						{
							cNameCardInfo.m_fSceneProccess = float.Parse(value);
						}
						if (MyUtils.GetAttribute(item2, "signature", ref value))
						{
							cNameCardInfo.m_sSignature = value;
						}
					}
				}
			}
			else if (item.Name == "rank")
			{
				List<CRankInfo> list = new List<CRankInfo>();
				foreach (XmlNode item3 in item)
				{
					if (MyUtils.GetAttribute(item3, "id", ref value))
					{
						string sID = value;
						CRankInfo cRankInfo = new CRankInfo();
						cRankInfo.m_sID = sID;
						if (MyUtils.GetAttribute(item3, "nickname", ref value))
						{
							cRankInfo.m_sNickName = value;
						}
						if (MyUtils.GetAttribute(item3, "hunterlvl", ref value))
						{
							cRankInfo.m_nHunterLevel = int.Parse(value);
						}
						if (MyUtils.GetAttribute(item3, "combatpower", ref value))
						{
							cRankInfo.m_nCombatPower = int.Parse(value);
						}
						if (MyUtils.GetAttribute(item3, "beadbired", ref value))
						{
							cRankInfo.m_nBeAdmired = int.Parse(value);
						}
						if (MyUtils.GetAttribute(item3, "lastrank", ref value))
						{
							cRankInfo.m_nLastRank = int.Parse(value);
						}
						list.Add(cRankInfo);
					}
				}
				if (list.Count > 0)
				{
					m_arrRankInfo = list.ToArray();
				}
			}
			else
			{
				if (!(item.Name == "rankfriends"))
				{
					continue;
				}
				List<CRankInfo> list2 = new List<CRankInfo>();
				foreach (XmlNode item4 in item)
				{
					if (MyUtils.GetAttribute(item4, "id", ref value))
					{
						string sID2 = value;
						CRankInfo cRankInfo2 = new CRankInfo();
						cRankInfo2.m_sID = sID2;
						if (MyUtils.GetAttribute(item4, "nickname", ref value))
						{
							cRankInfo2.m_sNickName = value;
						}
						if (MyUtils.GetAttribute(item4, "hunterlvl", ref value))
						{
							cRankInfo2.m_nHunterLevel = int.Parse(value);
						}
						if (MyUtils.GetAttribute(item4, "combatpower", ref value))
						{
							cRankInfo2.m_nCombatPower = int.Parse(value);
						}
						if (MyUtils.GetAttribute(item4, "beadbired", ref value))
						{
							cRankInfo2.m_nBeAdmired = int.Parse(value);
						}
						if (MyUtils.GetAttribute(item4, "lastrank", ref value))
						{
							cRankInfo2.m_nLastRank = int.Parse(value);
						}
						list2.Add(cRankInfo2);
					}
				}
				if (list2.Count > 0)
				{
					m_arrRankInfoFriends = list2.ToArray();
				}
			}
		}
	}

	protected void SaveData(XmlDocument doc)
	{
		XmlNode newChild = doc.CreateXmlDeclaration("1.0", "UTF-8", "no");
		doc.AppendChild(newChild);
		XmlElement xmlElement = doc.CreateElement("root");
		doc.AppendChild(xmlElement);
		XmlElement xmlElement2 = doc.CreateElement("info");
		xmlElement.AppendChild(xmlElement2);
		xmlElement2.SetAttribute("gamecentercache", m_sGameCenterCache);
		if (m_arrRankInfo != null)
		{
			XmlElement xmlElement3 = doc.CreateElement("rank");
			xmlElement.AppendChild(xmlElement3);
			for (int i = 0; i < m_arrRankInfo.Length; i++)
			{
				CRankInfo cRankInfo = m_arrRankInfo[i];
				if (cRankInfo == null)
				{
					cRankInfo = new CRankInfo();
				}
				XmlElement xmlElement4 = doc.CreateElement("node");
				xmlElement3.AppendChild(xmlElement4);
				xmlElement4.SetAttribute("id", cRankInfo.m_sID);
				xmlElement4.SetAttribute("nickname", cRankInfo.m_sNickName);
				xmlElement4.SetAttribute("hunterlvl", cRankInfo.m_nHunterLevel.ToString());
				xmlElement4.SetAttribute("combatpower", cRankInfo.m_nCombatPower.ToString());
				xmlElement4.SetAttribute("beadmired", cRankInfo.m_nBeAdmired.ToString());
				xmlElement4.SetAttribute("lastrank", cRankInfo.m_nLastRank.ToString());
			}
		}
		if (m_arrRankInfoFriends != null)
		{
			XmlElement xmlElement5 = doc.CreateElement("rankfriends");
			xmlElement.AppendChild(xmlElement5);
			for (int j = 0; j < m_arrRankInfoFriends.Length; j++)
			{
				CRankInfo cRankInfo2 = m_arrRankInfoFriends[j];
				if (cRankInfo2 == null)
				{
					cRankInfo2 = new CRankInfo();
				}
				XmlElement xmlElement6 = doc.CreateElement("node");
				xmlElement5.AppendChild(xmlElement6);
				xmlElement6.SetAttribute("id", cRankInfo2.m_sID);
				xmlElement6.SetAttribute("nickname", cRankInfo2.m_sNickName);
				xmlElement6.SetAttribute("hunterlvl", cRankInfo2.m_nHunterLevel.ToString());
				xmlElement6.SetAttribute("combatpower", cRankInfo2.m_nCombatPower.ToString());
				xmlElement6.SetAttribute("beadmired", cRankInfo2.m_nBeAdmired.ToString());
				xmlElement6.SetAttribute("lastrank", cRankInfo2.m_nLastRank.ToString());
			}
		}
		if (m_dictNameCardInfo == null)
		{
			return;
		}
		XmlElement xmlElement7 = doc.CreateElement("namecard");
		xmlElement.AppendChild(xmlElement7);
		foreach (CNameCardInfo value in m_dictNameCardInfo.Values)
		{
			XmlElement xmlElement8 = doc.CreateElement("node");
			xmlElement7.AppendChild(xmlElement8);
			xmlElement8.SetAttribute("id", value.m_sID);
			xmlElement8.SetAttribute("nickname", value.m_sNickName);
			if (value.m_NCPack.photo != null)
			{
				xmlElement8.SetAttribute("photo", Convert.ToBase64String(value.m_NCPack.photo));
			}
			xmlElement8.SetAttribute("hunterlvl", value.m_nHunterLvl.ToString());
			xmlElement8.SetAttribute("hunterexp", value.m_nHunterExp.ToString());
			xmlElement8.SetAttribute("combatpower", value.m_nCombatPower.ToString());
			xmlElement8.SetAttribute("rank", value.m_nRank.ToString());
			xmlElement8.SetAttribute("beadmired", value.m_nBeAdmired.ToString());
			xmlElement8.SetAttribute("title", value.m_nTitle.ToString());
			xmlElement8.SetAttribute("gold", value.m_nGold.ToString());
			xmlElement8.SetAttribute("crystal", value.m_nCrystal.ToString());
			xmlElement8.SetAttribute("sceneproccess", value.m_fSceneProccess.ToString());
			xmlElement8.SetAttribute("signature", value.m_sSignature);
		}
	}
}
