using UnityEngine;

public class iSceneMainMutiply : MonoBehaviour
{
	protected iGameData m_GameData
	{
		get
		{
			return iGameApp.GetInstance().m_GameData;
		}
	}

	protected iDataCenter m_DataCenter
	{
		get
		{
			if (m_GameData == null)
			{
				return null;
			}
			return m_GameData.GetDataCenter();
		}
	}

	private void Start()
	{
		if (m_DataCenter != null)
		{
			GameObject gameObject = new GameObject("windowinputname");
			iWindowInputName iWindowInputName2 = gameObject.AddComponent<iWindowInputName>();
			if (m_DataCenter != null)
			{
				iWindowInputName2.m_sInput = m_DataCenter.NickName;
			}
			iWindowInputName2.Show(true, OnOK, OnCancel);
		}
	}

	private void Update()
	{
	}

	private void OnGUI()
	{
	}

	protected void OnOK(string sInput)
	{
		if (m_DataCenter != null)
		{
			m_DataCenter.NickName = sInput;
			if (m_DataCenter.NickName.Length > 0)
			{
				iGameApp.GetInstance().EnterScene(kGameSceneEnum.MutipyHome);
			}
			else
			{
				iGameApp.GetInstance().EnterScene(kGameSceneEnum.Home);
			}
		}
	}

	protected void OnCancel()
	{
		iGameApp.GetInstance().EnterScene(kGameSceneEnum.Home);
	}
}
