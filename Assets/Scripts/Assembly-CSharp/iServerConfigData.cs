using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class iServerConfigData : MonoBehaviour
{
	public class CConfigInfo
	{
		public enum kState
		{
			None,
			Normal,
			Fetch,
			Success,
			Failed
		}

		public kState m_State;

		public string m_sFileName = string.Empty;

		public string m_sMD5 = string.Empty;

		public UnPack m_UnPack;

		public bool IsSuccess()
		{
			return m_State == kState.Success;
		}

		public bool IsFailed()
		{
			return m_State == kState.Failed;
		}
	}

	public class CServerConfigInfo
	{
		public Dictionary<string, string> m_dictServerInfo;

		public CServerConfigInfo()
		{
			m_dictServerInfo = new Dictionary<string, string>();
			Clear();
		}

		public void Clear()
		{
			m_dictServerInfo.Clear();
		}

		public string GetFileMD5(string sFileName)
		{
			if (!m_dictServerInfo.ContainsKey(sFileName))
			{
				return string.Empty;
			}
			return m_dictServerInfo[sFileName];
		}

		public void LoadData(XmlDocument doc)
		{
			XmlNode documentElement = doc.DocumentElement;
			if (documentElement == null)
			{
				return;
			}
			string empty = string.Empty;
			foreach (XmlNode item in documentElement)
			{
				if (!(item.Name == "node"))
				{
					continue;
				}
				string value = string.Empty;
				string value2 = string.Empty;
				if (GetAttribute(item, "filename", ref value) && GetAttribute(item, "md5", ref value2))
				{
					if (!m_dictServerInfo.ContainsKey(value))
					{
						m_dictServerInfo.Add(value, value2);
					}
					else
					{
						m_dictServerInfo[value] = value2;
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

	protected class CExpireConfig
	{
		public enum State
		{
			None,
			Wait,
			Fetch,
			Success,
			Failed
		}

		public State m_State;

		public string m_sFileName;
	}

	public delegate void UnPack(string sData);

	public delegate void OnEvent();

	protected static iServerConfigData m_Instance;

	protected OnEvent m_OnSuccess;

	protected OnEvent m_OnFailed;

	protected OnEvent m_OnNetError;

	protected CServerConfigInfo m_ServerConfigInfo;

	protected string m_sUrl = iMacroDefine.CompanyAccountURL;

	protected string m_sUrl_MD5 = "CoMDH_ClientConfigMD5";

	protected string m_sServerInfoFilePathSrc = "D:\\development\\_DinoCapWorld\\src\\_DinoCapWorld\\Documents\\serverconfig";

	protected string m_sServerInfoFilePathDst = "D:\\development\\_DinoCapWorld\\src\\_DinoCapWorld\\Documents\\serverconfig";

	public string m_sServerInfoKey = "trinitigame_comdh";

	protected Dictionary<string, CConfigInfo> m_dictConfigInfo;

	protected List<CExpireConfig> m_ltExpireConfig;

	public static iServerConfigData GetInstance()
	{
		if (m_Instance == null)
		{
			GameObject gameObject = new GameObject("_ServerConfigData");
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			m_Instance = gameObject.AddComponent<iServerConfigData>();
		}
		return m_Instance;
	}

	private void Awake()
	{
		m_ServerConfigInfo = new CServerConfigInfo();
		m_dictConfigInfo = new Dictionary<string, CConfigInfo>();
		m_ltExpireConfig = new List<CExpireConfig>();
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (m_ltExpireConfig.Count > 0)
		{
			CExpireConfig cExpireConfig = m_ltExpireConfig[0];
			if (cExpireConfig.m_State == CExpireConfig.State.Wait)
			{
				cExpireConfig.m_State = CExpireConfig.State.Fetch;
				iServerFile.Instance.Visit(m_sUrl + "/CoMDH_ClientConfig_3.1.7a/" + cExpireConfig.m_sFileName + ".txt", OnFetchConfig_S, OnFetchConfig_F, -1f);
			}
		}
	}

	public void AddConfigInfo(string sFileName, string sMD5, UnPack onunpack)
	{
		if (!m_dictConfigInfo.ContainsKey(sFileName))
		{
			CConfigInfo cConfigInfo = new CConfigInfo();
			cConfigInfo.m_sFileName = sFileName;
			cConfigInfo.m_sMD5 = sMD5;
			cConfigInfo.m_UnPack = onunpack;
			cConfigInfo.m_State = CConfigInfo.kState.Normal;
			m_dictConfigInfo.Add(sFileName, cConfigInfo);
		}
	}

	public void SetConfigMD5(string sFileName, string sMD5)
	{
		if (m_dictConfigInfo.ContainsKey(sFileName))
		{
			m_dictConfigInfo[sFileName].m_sMD5 = sMD5;
		}
	}

	public void Fetch(OnEvent onsunccess, OnEvent onfailed)
	{
		m_OnSuccess = onsunccess;
		m_OnFailed = onfailed;
		iServerFile.Instance.Visit(m_sUrl + "/" + m_sUrl_MD5 + "_3.1.7a.txt", OnFetchMD5_S, OnFetchMD5_F, -1f);
	}

	public void GenerateMD5List()
	{
		XmlDocument xmlDocument = new XmlDocument();
		XmlNode newChild = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", "no");
		xmlDocument.AppendChild(newChild);
		XmlElement xmlElement = xmlDocument.CreateElement("root");
		xmlDocument.AppendChild(xmlElement);
		foreach (CConfigInfo value in m_dictConfigInfo.Values)
		{
			XmlElement xmlElement2 = xmlDocument.CreateElement("node");
			xmlElement.AppendChild(xmlElement2);
			xmlElement2.SetAttribute("filename", value.m_sFileName);
			xmlElement2.SetAttribute("md5", value.m_sMD5);
		}
		xmlDocument.Save(m_sServerInfoFilePathSrc + "\\" + m_sUrl_MD5 + ".xml");
	}

	protected CConfigInfo GetConfigInfo(string sFileName)
	{
		if (!m_dictConfigInfo.ContainsKey(sFileName))
		{
			return null;
		}
		return m_dictConfigInfo[sFileName];
	}

	protected void OnFetchMD5_S(string sFileData)
	{
		UnityEngine.Debug.Log("OnFetchMD5_S " + sFileData);
		m_ServerConfigInfo.Clear();
		string empty = string.Empty;
		try
		{
			empty = XXTEAUtils.Decrypt(sFileData, m_sServerInfoKey);
			MyUtils.UnZipString(empty, ref empty);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(empty);
			m_ServerConfigInfo.LoadData(xmlDocument);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError("LoadServerData Error " + ex);
		}
		m_ltExpireConfig.Clear();
		foreach (CConfigInfo value in m_dictConfigInfo.Values)
		{
			if (value.m_sFileName.Length >= 1)
			{
				string fileMD = m_ServerConfigInfo.GetFileMD5(value.m_sFileName);
				if (value.m_sMD5.Length < 1 || fileMD.Length < 1 || value.m_sMD5 != fileMD)
				{
					CExpireConfig cExpireConfig = new CExpireConfig();
					cExpireConfig.m_sFileName = value.m_sFileName;
					cExpireConfig.m_State = CExpireConfig.State.Wait;
					m_ltExpireConfig.Add(cExpireConfig);
					UnityEngine.Debug.Log("expireconfig " + value.m_sFileName + " " + value.m_sMD5);
				}
			}
		}
		if (m_ltExpireConfig.Count < 1 && m_OnSuccess != null)
		{
			m_OnSuccess();
		}
	}

	protected void OnFetchMD5_F()
	{
		UnityEngine.Debug.Log("OnFetchMD5_F ");
		if (m_OnFailed != null)
		{
			m_OnFailed();
		}
	}

	protected void OnFetchConfig_S(string sFileData)
	{
		if (m_ltExpireConfig.Count < 1)
		{
			if (m_OnSuccess != null)
			{
				m_OnSuccess();
			}
			return;
		}
		CExpireConfig cExpireConfig = m_ltExpireConfig[0];
		try
		{
			if (cExpireConfig.m_State == CExpireConfig.State.Fetch)
			{
				CConfigInfo configInfo = GetConfigInfo(cExpireConfig.m_sFileName);
				if (configInfo != null && configInfo.m_UnPack != null)
				{
					configInfo.m_UnPack(sFileData);
					UnityEngine.Debug.Log("fetch success " + cExpireConfig.m_sFileName);
				}
				m_ltExpireConfig.RemoveAt(0);
				if (m_ltExpireConfig.Count < 1 && m_OnSuccess != null)
				{
					m_OnSuccess();
				}
			}
		}
		catch
		{
			Debug.Log("fetch failed " + cExpireConfig.m_sFileName);
			m_ltExpireConfig.Clear();
			if (m_OnFailed != null)
			{
				m_OnFailed();
			}
		}
	}

	protected void OnFetchConfig_F()
	{
		if (m_ltExpireConfig.Count > 0)
		{
			CExpireConfig cExpireConfig = m_ltExpireConfig[0];
			if (cExpireConfig.m_State == CExpireConfig.State.Fetch)
			{
				UnityEngine.Debug.Log("fetch failed " + cExpireConfig.m_sFileName);
			}
		}
		m_ltExpireConfig.Clear();
		if (m_OnFailed != null)
		{
			m_OnFailed();
		}
	}

	protected string TransformXML2TXT(string srcpath, string dstpath, string key)
	{
		if (srcpath.Length < 1 || dstpath.Length < 1)
		{
			return string.Empty;
		}
		string zipedcontent = string.Empty;
		UnityEngine.Debug.Log(srcpath);
		if (File.Exists(srcpath))
		{
			StreamReader streamReader = null;
			try
			{
				streamReader = new StreamReader(srcpath);
				zipedcontent = streamReader.ReadToEnd();
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
		if (zipedcontent != null && zipedcontent.Length > 0)
		{
			MyUtils.ZipString(zipedcontent, ref zipedcontent);
			string text = XXTEAUtils.Encrypt(zipedcontent, key);
			StreamWriter streamWriter = new StreamWriter(dstpath, false);
			streamWriter.Write(text);
			streamWriter.Flush();
			streamWriter.Close();
			return text;
		}
		return string.Empty;
	}
}
