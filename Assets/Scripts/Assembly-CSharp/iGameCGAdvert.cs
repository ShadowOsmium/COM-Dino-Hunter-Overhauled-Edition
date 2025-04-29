using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class iGameCGAdvert : MonoBehaviour
{
	public class CServerAdvertInfo
	{
		public string sVideo = string.Empty;

		public string sVideoUrl = string.Empty;

		public Dictionary<int, string> dictAdvertUrl;

		public CServerAdvertInfo()
		{
			dictAdvertUrl = new Dictionary<int, string>();
		}

		public void LoadData(XmlDocument doc)
		{
			try
			{
				XmlNode documentElement = doc.DocumentElement;
				if (documentElement == null)
				{
					return;
				}
				string value = string.Empty;
				foreach (XmlNode item in documentElement)
				{
					if (item.Name != "advert")
					{
						continue;
					}
					bool flag = false;
					if (GetAttribute(item, "video", ref value))
					{
						sVideo = value;
						if (sVideo.IndexOf(".mp4") == -1)
						{
							sVideo += ".mp4";
						}
						flag = true;
					}
					int key = 0;
					if (GetAttribute(item, "popularizetype", ref value))
					{
						key = int.Parse(value);
					}
					if (GetAttribute(item, "url", ref value))
					{
						if (!dictAdvertUrl.ContainsKey(key))
						{
							dictAdvertUrl.Add(key, value);
						}
						else
						{
							dictAdvertUrl[key] = value;
						}
						if (flag)
						{
							sVideoUrl = value;
						}
					}
				}
			}
			catch
			{
				sVideo = string.Empty;
				sVideoUrl = string.Empty;
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

	protected string m_sUrl = iMacroDefine.CompanyAccountURL;

	protected string m_sUrl_File = "CoMDH_AdvertConfig";

	protected string m_sServerInfoFilePathSrc = "D:\\development\\_DinoCapWorld\\src\\_DinoCapWorld\\Documents\\serverconfig";

	protected string m_sServerInfoFilePathDst = "D:\\development\\_DinoCapWorld\\src\\_DinoCapWorld\\Documents\\serverconfig";

	public string m_sServerInfoKey = "trinitigame_comdh";

	private void Awake()
	{
		Application.targetFrameRate = 240;
	}

	private void Start()
	{
		iServerFile.Instance.Visit(m_sUrl + "/" + m_sUrl_File + "_3.1.7a.txt", OnSuccess, OnFailed, 5f);
	}

	private void Update()
	{
	}

	protected void OnResult(string filename, string url = "")
	{
//		Debug.Log(filename + " " + url);
//		if (filename.Length < 1 || url.Length < 1)
//		{
//			Application.LoadLevelAsync("Scene_Main");
//			return;
//		}
		try
		{
			XAdManagerWrapper.SetVideoAdFile(filename);
			XAdManagerWrapper.SetVideoAdUrl(url);
			XAdManagerWrapper.ShowVideoAdLocal();
		}
		catch
		{
			UnityEngine.Debug.Log("filename = " + filename);
			UnityEngine.Debug.Log("url = " + url);
			UnityEngine.Debug.Log("server advert config error!");
		}
		Application.LoadLevelAsync("Scene_Main");
	}

	protected void OnSuccess(string sFileData)
	{
		UnityEngine.Debug.Log("OnSuccess " + sFileData);
		iGameState gameState = iGameApp.GetInstance().m_GameState;
		if (gameState == null)
		{
			return;
		}
		string empty = string.Empty;
		try
		{
			empty = XXTEAUtils.Decrypt(sFileData, m_sServerInfoKey);
			MyUtils.UnZipString(empty, ref empty);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(empty);
			if (gameState.m_ServerAdvertInfo == null)
			{
				gameState.m_ServerAdvertInfo = new CServerAdvertInfo();
			}
			gameState.m_ServerAdvertInfo.LoadData(xmlDocument);
			OnResult(gameState.m_ServerAdvertInfo.sVideo, gameState.m_ServerAdvertInfo.sVideoUrl);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError("OnSuccess Error " + ex);
			OnResult(string.Empty, string.Empty);
		}
	}

	protected void OnFailed()
	{
		OnResult(string.Empty, string.Empty);
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
