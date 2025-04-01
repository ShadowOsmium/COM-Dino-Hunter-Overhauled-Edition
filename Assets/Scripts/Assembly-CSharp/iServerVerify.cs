using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class iServerVerify : MonoBehaviour
{
	public class CServerConfigInfo
	{
		public class CServerInfo
		{
			public string sName = string.Empty;

			public string sUrl = string.Empty;

			public string sKey = string.Empty;

			public float fTimeOut = -1f;
		}

		public class CGameServerInfo
		{
			public string sUrl = string.Empty;

			public int nPort;

			public int nGroup;
		}

		public class kDiscountType
		{
			public const int None = 0;

			public const int Weapon = 1;

			public const int Avatar = 2;

			public const int Character = 3;

			public const int Skill = 4;
		}

		public class CServerDiscount
		{
			public int nType;

			public int nID;

			public int nDiscount;
		}

		public class CPopularizeInfo
		{
			public int nType;

			public int nID;

			public string sBundleID;
		}

		public class CWorldMonsterInfo
		{
			public int m_nMobID;

			public float m_fRate;

			public int m_nDailyMax;

			public List<int> m_ltTaskTypeLimit;

			public float[] m_arrRefreshTime;

			public CWorldMonsterInfo()
			{
				m_ltTaskTypeLimit = new List<int>();
			}
		}

		public class CVersionIDChange
		{
			public int m_nSrcID;

			public int m_nDstID;
		}

		public string m_sVersion;

		public int m_ServerState;

		public Dictionary<int, string> m_dictServerStateStr;

		public string m_sServerTitle = string.Empty;

		public string m_sServerMessage = string.Empty;

		public Dictionary<string, CServerInfo> m_dictServerInfo;

		public List<CGameServerInfo> m_ltGameServerInfo;

		public List<CServerDiscount> m_ltServerDiscount;

		public List<int> m_ltGift;

		public List<CPopularizeInfo> m_ltServerPopularize;

		public List<CWorldMonsterInfo> m_ltWorldMonsterInfo;

		public List<CVersionIDChange> m_ltItemIDChange;

		public CServerConfigInfo()
		{
			m_dictServerStateStr = new Dictionary<int, string>();
			m_dictServerInfo = new Dictionary<string, CServerInfo>();
			m_ltGameServerInfo = new List<CGameServerInfo>();
			m_ltServerDiscount = new List<CServerDiscount>();
			m_ltGift = new List<int>();
			m_ltServerPopularize = new List<CPopularizeInfo>();
			m_ltWorldMonsterInfo = new List<CWorldMonsterInfo>();
			m_ltItemIDChange = new List<CVersionIDChange>();
			Clear();
		}

		public void Clear()
		{
			m_sVersion = string.Empty;
			m_ServerState = 0;
			m_dictServerStateStr.Clear();
			m_dictServerInfo.Clear();
			m_ltServerDiscount.Clear();
			m_ltGift.Clear();
			m_ltServerPopularize.Clear();
			m_ltWorldMonsterInfo.Clear();
			m_ltItemIDChange.Clear();
		}

		public CServerInfo GetServerInfo(string servername)
		{
			if (!m_dictServerInfo.ContainsKey(servername))
			{
				return null;
			}
			return m_dictServerInfo[servername];
		}

		public CGameServerInfo GetGameServerInfo(int nIndex)
		{
			if (nIndex < 0 || nIndex >= m_ltGameServerInfo.Count)
			{
				return null;
			}
			return m_ltGameServerInfo[nIndex];
		}

		public CGameServerInfo GetGameServerInfoRandom()
		{
			if (m_ltGameServerInfo.Count < 1)
			{
				return null;
			}
			if (m_ltGameServerInfo.Count == 1)
			{
				return m_ltGameServerInfo[0];
			}
			return m_ltGameServerInfo[UnityEngine.Random.Range(0, m_ltGameServerInfo.Count)];
		}

		public List<CServerDiscount> GetServerDiscount()
		{
			return m_ltServerDiscount;
		}

		public List<CVersionIDChange> GetItemIDChange()
		{
			return m_ltItemIDChange;
		}

		public bool IsPriceOff(int nType, int nID, ref int nDiscount)
		{
			if (m_ltServerDiscount == null || m_ltServerDiscount.Count < 1)
			{
				return false;
			}
			foreach (CServerDiscount item in m_ltServerDiscount)
			{
				if (item.nType == nType && item.nID == nID)
				{
					nDiscount = item.nDiscount;
					return true;
				}
			}
			return false;
		}

		public bool IsPopularize(int nType, int nID, ref string sBundleID)
		{
			if (m_ltServerPopularize == null || m_ltServerPopularize.Count < 1)
			{
				return false;
			}
			foreach (CPopularizeInfo item in m_ltServerPopularize)
			{
				if (item.nType == nType && item.nID == nID)
				{
					sBundleID = item.sBundleID;
					return true;
				}
			}
			return false;
		}

		public bool IsGift(int nWeaponID)
		{
			if (m_ltGift == null || !m_ltGift.Contains(nWeaponID))
			{
				return false;
			}
			return true;
		}

		public string GetServerStateStr()
		{
			if (m_ServerState == 0)
			{
				return string.Empty;
			}
			if (!m_dictServerStateStr.ContainsKey(m_ServerState))
			{
				return string.Empty;
			}
			return m_dictServerStateStr[m_ServerState];
		}

		public void LoadData(XmlDocument doc)
		{
			XmlNode documentElement = doc.DocumentElement;
			if (documentElement == null)
			{
				return;
			}
			string value = string.Empty;
			foreach (XmlNode item2 in documentElement.ChildNodes)
			{
				if (item2.Name == "common")
				{
					if (GetAttribute(item2, "version", ref value))
					{
						m_sVersion = value;
					}
					if (GetAttribute(item2, "serverstate", ref value))
					{
						m_ServerState = int.Parse(value);
					}
				}
				else if (item2.Name == "serverstatemessage")
				{
					foreach (XmlNode item3 in item2)
					{
						if (!(item3.Name != "node"))
						{
							int num = 0;
							string value2 = string.Empty;
							if (GetAttribute(item3, "serverstate", ref value))
							{
								num = int.Parse(value);
							}
							if (GetAttribute(item3, "message", ref value))
							{
								value2 = value;
							}
							if (num != 0 && !m_dictServerStateStr.ContainsKey(num))
							{
								m_dictServerStateStr.Add(num, value2);
							}
						}
					}
				}
				else if (item2.Name == "servermessage")
				{
					foreach (XmlNode item4 in item2)
					{
						if (item4.Name != "node")
						{
							continue;
						}
						foreach (XmlNode item5 in item4)
						{
							if (item5.Name == "title")
							{
								m_sServerTitle = item5.InnerText;
								break;
							}
						}
						foreach (XmlNode item6 in item4)
						{
							if (item6.Name == "message")
							{
								m_sServerMessage = item6.InnerText;
								UnityEngine.Debug.Log(m_sServerMessage);
								break;
							}
						}
					}
				}
				else if (item2.Name == "serverlist")
				{
					UnityEngine.Debug.Log(item2.Name);
					foreach (XmlNode item7 in item2)
					{
						if (!(item7.Name != "node") && GetAttribute(item7, "name", ref value))
						{
							string text = value;
							CServerInfo cServerInfo = null;
							if (!m_dictServerInfo.ContainsKey(text))
							{
								cServerInfo = new CServerInfo();
								cServerInfo.sName = text;
								m_dictServerInfo.Add(text, cServerInfo);
							}
							else
							{
								cServerInfo = m_dictServerInfo[text];
							}
							if (GetAttribute(item7, "url", ref value))
							{
								cServerInfo.sUrl = value;
							}
							if (GetAttribute(item7, "key", ref value))
							{
								cServerInfo.sKey = value;
							}
							if (GetAttribute(item7, "timeout", ref value))
							{
								cServerInfo.fTimeOut = float.Parse(value);
							}
						}
					}
				}
				else if (item2.Name == "gameserverlist")
				{
					m_ltGameServerInfo.Clear();
					foreach (XmlNode item8 in item2)
					{
						if (!(item8.Name != "node") && GetAttribute(item8, "url", ref value))
						{
							CGameServerInfo cGameServerInfo = new CGameServerInfo();
							cGameServerInfo.sUrl = value;
							if (GetAttribute(item8, "port", ref value))
							{
								cGameServerInfo.nPort = int.Parse(value);
							}
							if (GetAttribute(item8, "group", ref value))
							{
								cGameServerInfo.nGroup = int.Parse(value);
							}
							m_ltGameServerInfo.Add(cGameServerInfo);
						}
					}
				}
				else if (item2.Name == "discount")
				{
					UnityEngine.Debug.Log("discount");
					m_ltServerDiscount.Clear();
					foreach (XmlNode item9 in item2)
					{
						if (!(item9.Name != "node") && GetAttribute(item9, "type", ref value))
						{
							CServerDiscount cServerDiscount = new CServerDiscount();
							cServerDiscount.nType = int.Parse(value);
							if (GetAttribute(item9, "id", ref value))
							{
								cServerDiscount.nID = int.Parse(value);
							}
							if (GetAttribute(item9, "discount", ref value))
							{
								cServerDiscount.nDiscount = int.Parse(value);
							}
							m_ltServerDiscount.Add(cServerDiscount);
						}
					}
				}
				else if (item2.Name == "gift")
				{
					Debug.Log("gift");
					m_ltGift.Clear();
					foreach (XmlNode item10 in item2)
					{
						if (!(item10.Name != "node") && GetAttribute(item10, "weaponid", ref value))
						{
							int item = int.Parse(value);
							m_ltGift.Add(item);
						}
					}
				}
				else if (item2.Name == "popularize")
				{
					Debug.Log("popularize");
					m_ltServerPopularize.Clear();
					foreach (XmlNode item11 in item2)
					{
						if (!(item11.Name != "node") && GetAttribute(item11, "type", ref value))
						{
							CPopularizeInfo cPopularizeInfo = new CPopularizeInfo();
							cPopularizeInfo.nType = int.Parse(value);
							if (GetAttribute(item11, "id", ref value))
							{
								cPopularizeInfo.nID = int.Parse(value);
							}
							if (GetAttribute(item11, "bundleid", ref value))
							{
								cPopularizeInfo.sBundleID = value.Trim();
							}
							UnityEngine.Debug.Log(cPopularizeInfo.nType + " " + cPopularizeInfo.nID + " " + cPopularizeInfo.sBundleID);
							m_ltServerPopularize.Add(cPopularizeInfo);
						}
					}
				}
				else if (item2.Name == "worldmonster")
				{
					m_ltWorldMonsterInfo.Clear();
					foreach (XmlNode item12 in item2)
					{
						if (!GetAttribute(item12, "mobid", ref value))
						{
							continue;
						}
						CWorldMonsterInfo cWorldMonsterInfo = new CWorldMonsterInfo();
						cWorldMonsterInfo.m_nMobID = int.Parse(value);
						if (GetAttribute(item12, "rate", ref value))
						{
							cWorldMonsterInfo.m_fRate = float.Parse(value);
						}
						if (GetAttribute(item12, "dailymax", ref value))
						{
							cWorldMonsterInfo.m_nDailyMax = int.Parse(value);
						}
						if (GetAttribute(item12, "scenetypelimit", ref value))
						{
							cWorldMonsterInfo.m_ltTaskTypeLimit.Clear();
							string[] array = value.Split(',');
							if (array != null)
							{
								for (int i = 0; i < array.Length; i++)
								{
									cWorldMonsterInfo.m_ltTaskTypeLimit.Add(int.Parse(array[i]));
								}
							}
						}
						if (GetAttribute(item12, "refreshtime", ref value))
						{
							string[] array = value.Split(',');
							if (array != null && array.Length >= 2)
							{
								cWorldMonsterInfo.m_arrRefreshTime = new float[2];
								cWorldMonsterInfo.m_arrRefreshTime[0] = float.Parse(array[0]);
								cWorldMonsterInfo.m_arrRefreshTime[1] = float.Parse(array[1]);
							}
						}
						m_ltWorldMonsterInfo.Add(cWorldMonsterInfo);
					}
				}
				else if (item2.Name == "version_upgrade")
				{
					m_ltItemIDChange.Clear();
					foreach (XmlNode item13 in item2)
					{
						if (!(item13.Name == "item"))
						{
							continue;
						}
						foreach (XmlNode item14 in item13)
						{
							if (GetAttribute(item14, "src_id", ref value))
							{
								CVersionIDChange cVersionIDChange = new CVersionIDChange();
								cVersionIDChange.m_nSrcID = int.Parse(value);
								if (GetAttribute(item14, "dst_id", ref value))
								{
									cVersionIDChange.m_nDstID = int.Parse(value);
								}
								m_ltItemIDChange.Add(cVersionIDChange);
							}
						}
					}
				}
				else if (item2.Name == "coop_param")
				{
					if (GetAttribute(item2, "monster_life", ref value))
					{
						iMacroDefine.m_fMonsterPower_Life = float.Parse(value);
					}
					if (GetAttribute(item2, "monster_damage", ref value))
					{
						iMacroDefine.m_fMonsterPower_Damage = float.Parse(value);
					}
					if (GetAttribute(item2, "monster_level", ref value))
					{
						iMacroDefine.m_fMonsterPower_Lvl = float.Parse(value);
					}
					if (GetAttribute(item2, "monster_rewards_gold", ref value))
					{
						iMacroDefine.m_fMonsterReward_Gold = float.Parse(value);
					}
					if (GetAttribute(item2, "monster_rewards_exp", ref value))
					{
						iMacroDefine.m_fMonsterReward_Exp = float.Parse(value);
					}
					if (GetAttribute(item2, "monster_rewards_level", ref value))
					{
						iMacroDefine.m_fMonsterReward_Lvl = float.Parse(value);
					}
					if (GetAttribute(item2, "stage_rewards_gold", ref value))
					{
						iMacroDefine.m_fStageReward_Gold = float.Parse(value);
					}
					if (GetAttribute(item2, "stage_rewards_exp", ref value))
					{
						iMacroDefine.m_fStageReward_Exp = float.Parse(value);
					}
					if (GetAttribute(item2, "stage_rewards_level", ref value))
					{
						iMacroDefine.m_fStageReward_Lvl = float.Parse(value);
					}
				}
			}
		}

		protected bool GetAttribute(XmlNode node, string name, ref string value)
		{
			if (node == null || node.Attributes[name] == null)
			{
				return false;
			}
			value = node.Attributes[name].Value.Trim();
			if (value.Length < 1)
			{
				return false;
			}
			return true;
		}
	}

	protected enum kPingState
	{
		None,
		Delay,
		Pinging,
		Success,
		Fail
	}

	public delegate void OnEvent();

	protected static iServerVerify m_Instance;

	protected OnEvent m_OnSuccess;

	protected OnEvent m_OnFailed;

	protected OnEvent m_OnNetError;

	protected CServerConfigInfo m_ServerConfigInfo;

	protected kPingState m_PingState;

	protected float m_fTimeOut;

	protected float m_fTimeOutCount;

	protected float m_fTimeDelay;

	protected float m_fTimeDelayCount;

	protected string m_sUrl = iMacroDefine.CompanyAccountURL + "/CoMDH_ServerConfig_3.1.7a.txt";

	protected string m_sVersion = "1.0.1";

	protected string m_sServerInfoFilePathSrc = "D:\\development\\_DinoCapWorld\\src\\_DinoCapWorld\\Documents\\serverconfig\\CoMDH_ServerConfig.xml";

	protected string m_sServerInfoFilePathDst = "D:\\development\\_DinoCapWorld\\src\\_DinoCapWorld\\Documents\\serverconfig\\CoMDH_ServerConfig.txt";

	protected string m_sServerInfoKey = "trinitigame_comdh";

	public string Version
	{
		get
		{
			return m_sVersion;
		}
		set
		{
			m_sVersion = value;
		}
	}

	public static iServerVerify GetInstance()
	{
		if (m_Instance == null)
		{
			GameObject gameObject = new GameObject("_ServerVerify");
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			m_Instance = gameObject.AddComponent<iServerVerify>();
		}
		return m_Instance;
	}

	public bool IsSuccess()
	{
		return m_PingState == kPingState.Success;
	}

	public bool IsFailed()
	{
		return m_PingState == kPingState.Fail;
	}

	public CServerConfigInfo GetServerConfigInfo()
	{
		return m_ServerConfigInfo;
	}

	private void Awake()
	{
		m_ServerConfigInfo = new CServerConfigInfo();
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (m_PingState == kPingState.Delay)
		{
			m_fTimeDelayCount += Time.deltaTime;
			if (m_fTimeDelayCount >= m_fTimeDelay)
			{
				m_fTimeDelayCount = 0f;
				StartCoroutine(Connect());
			}
		}
		else
		{
			if (m_PingState != kPingState.Pinging)
			{
				return;
			}
			m_fTimeOutCount += Time.deltaTime;
			if (m_fTimeOutCount >= m_fTimeOut)
			{
				m_fTimeOutCount = 0f;
				UnityEngine.Debug.Log("test ping time out ");
				m_PingState = kPingState.Fail;
				if (m_OnNetError != null)
				{
					m_OnNetError();
				}
			}
		}
	}

	public void ConnectServer(string sVersion, OnEvent onsuccess, OnEvent onfailed, OnEvent onneterror, float timeout = 10f, float delaytime = 0f)
	{
		m_sVersion = sVersion;
		m_OnSuccess = onsuccess;
		m_OnFailed = onfailed;
		m_OnNetError = onneterror;
		m_fTimeOut = timeout;
		m_fTimeOutCount = 0f;
		if (delaytime <= 0f)
		{
			StartCoroutine(Connect());
			return;
		}
		m_PingState = kPingState.Delay;
		m_fTimeDelay = delaytime;
		m_fTimeDelayCount = 0f;
	}

	protected IEnumerator Connect()
	{
		//m_PingState = kPingState.Pinging;
		//WWW www = new WWW(m_sUrl + "?rand=" + UnityEngine.Random.Range(10, 99999));
		//Debug.Log(www.url);
		//yield return www;
		//if (m_PingState != kPingState.Pinging)
		//{
		//	yield break;
		//}
		//if (www.error != null)
		//{
		//	UnityEngine.Debug.Log("net error " + www.error);
		//	m_PingState = kPingState.Fail;
		//	if (m_OnNetError != null)
		//	{
		//		m_OnNetError();
		//	}
		//	yield break;
		//}
		//if (www.text == null || www.text.Length < 1)
		//{
		//	UnityEngine.Debug.Log("text is not exist ");
		//	m_PingState = kPingState.Fail;
		//	if (m_OnFailed != null)
		//	{
		//		m_OnFailed();
		//	}
		//	yield break;
		//}
		//LoadServerData(www.text);
		//if (m_sVersion != m_ServerConfigInfo.m_sVersion)
		//{
		//	UnityEngine.Debug.Log("version error " + m_ServerConfigInfo.m_sVersion);
		//	m_PingState = kPingState.Fail;
		//	if (m_OnFailed != null)
		//	{
		//		m_OnFailed();
		//	}
		//}
		//else
		//{
		//	UnityEngine.Debug.Log("serverconfig successed ");
		//	m_PingState = kPingState.Success;
		//	if (m_OnSuccess != null)
		//	{
				XmlDocument document = new XmlDocument();
				document.LoadXml(SpoofedData.LoadSpoof("serverspoof"));
				m_ServerConfigInfo.LoadData(document);
				m_OnSuccess();
				yield break;
			//}
		//}
	}

	protected void LoadServerData(string input)
	{
		UnityEngine.Debug.Log("LoadServerData " + input);
		m_ServerConfigInfo.Clear();
		string empty = string.Empty;
		try
		{
			empty = XXTEAUtils.Decrypt(input, m_sServerInfoKey);
			MyUtils.UnZipString(empty, ref empty);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(empty);
			m_ServerConfigInfo.LoadData(xmlDocument);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError("LoadServerData Error " + ex);
		}
	}

	protected void TransformXML2TXT(string srcpath, string dstpath, string key)
	{
		if (srcpath.Length < 1 || dstpath.Length < 1)
		{
			return;
		}
		string text = string.Empty;
		UnityEngine.Debug.Log(srcpath);
		if (File.Exists(srcpath))
		{
			StreamReader streamReader = null;
			try
			{
				streamReader = new StreamReader(srcpath);
				text = streamReader.ReadToEnd();
			}
			catch
			{
				Debug.Log("ERROR - Encrypt()!!!");
			}
			finally
			{
				if (streamReader != null)
				{
					streamReader.Close();
				}
			}
		}
		if (text != null && text.Length > 0)
		{
			string value = XXTEAUtils.Encrypt(text, key);
			StreamWriter streamWriter = new StreamWriter(dstpath, false);
			streamWriter.Write(value);
			streamWriter.Flush();
			streamWriter.Close();
		}
	}
}
