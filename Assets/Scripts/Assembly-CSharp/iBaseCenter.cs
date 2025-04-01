using System;
using UnityEngine;

public class iBaseCenter
{
	public string sFileName = string.Empty;

	public string sMD5 = string.Empty;

	protected int m_nReadIndex;

	public virtual bool Load(string sFileName)
	{
		//this.sFileName = sFileName;
		//string content = string.Empty;
		//if (!Utils.FileGetString(sFileName + iMacroDefine.SaveExpandedName, ref content))
		//{
		//	return false;
		//}
		//sMD5 = MyUtils.GetMD5(content);
		LoadDataDecrypt(/*content*/SpoofedData.LoadSpoof(sFileName));
		return true;
	}

	public virtual void OnFetch(string content)
	{
		//sMD5 = MyUtils.GetMD5(content);
		LoadDataDecrypt(/*content*/SpoofedData.LoadSpoof(sFileName));
		//MyUtils.SaveFile(Utils.SavePath() + "/" + sFileName + iMacroDefine.SaveExpandedName, content);
	}

	protected void LoadDataDecrypt(string content)
	{
		//content = XXTEAUtils.Decrypt(content, iServerConfigData.GetInstance().m_sServerInfoKey);
		//MyUtils.UnZipString(content, ref content);
		//try
		//{
			LoadData(content);
		//}
		//catch (Exception ex)
		//{
		//	iGameApp.GetInstance().ScreenLog("exception: readindex " + m_nReadIndex + " msg " + ex.StackTrace);
		//	Debug.LogWarning("exception: readindex " + m_nReadIndex + " msg " + ex.StackTrace);
		//}
	}

	protected virtual void LoadData(string content)
	{
	}
}
