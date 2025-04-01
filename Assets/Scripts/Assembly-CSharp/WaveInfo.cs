using System.Collections.Generic;
using UnityEngine;

public class WaveInfo
{
	public int nID;

	public int nEventType;

	public List<int> ltEventParam;

	public bool bEventLoop;

	public bool m_bRandom;

	public int m_nMobCount;

	public float m_fDelayTime;

	public float m_fInterval;

	public int m_nNumAtOnce;

	public int m_nLoop;

	public List<WaveMobInfo> m_ltWaveMobInfo;

	public string sCutScene;

	public string sCutSceneContent;

	public string sCutSceneAmbience;

	public string sCutSceneBGM;

	public int nDefaultMobLevel;

	public WaveInfo()
	{
		m_bRandom = false;
		m_nMobCount = 0;
		m_fDelayTime = 0f;
		m_fInterval = 0.1f;
		m_nNumAtOnce = 1;
		m_nLoop = -1;
		m_ltWaveMobInfo = new List<WaveMobInfo>();
		sCutScene = string.Empty;
		sCutSceneContent = string.Empty;
		sCutSceneBGM = string.Empty;
		sCutSceneAmbience = string.Empty;
		nDefaultMobLevel = 1;
	}

	public WaveMobInfo GetWaveMobInfo(int nIndex)
	{
		if (m_ltWaveMobInfo == null || nIndex < 0 || nIndex >= m_ltWaveMobInfo.Count)
		{
			return null;
		}
		return m_ltWaveMobInfo[nIndex];
	}

	public WaveMobInfo GetWaveMobInfoRandom()
	{
		if (m_ltWaveMobInfo == null || m_ltWaveMobInfo.Count < 1)
		{
			return null;
		}
		if (m_ltWaveMobInfo.Count == 1)
		{
			return m_ltWaveMobInfo[0];
		}
		int[] array = new int[m_ltWaveMobInfo.Count];
		for (int i = 0; i < m_ltWaveMobInfo.Count; i++)
		{
			if (i == 0)
			{
				array[i] = m_ltWaveMobInfo[i].nRandom;
			}
			else
			{
				array[i] = array[i - 1] + m_ltWaveMobInfo[i].nRandom;
			}
		}
		int num = Random.Range(1, array[m_ltWaveMobInfo.Count - 1] + 1);
		for (int j = 0; j < m_ltWaveMobInfo.Count; j++)
		{
			if (num <= array[j])
			{
				return m_ltWaveMobInfo[j];
			}
		}
		return null;
	}

	public int GetWaveMobInfoCount()
	{
		if (m_ltWaveMobInfo == null)
		{
			return 0;
		}
		return m_ltWaveMobInfo.Count;
	}

	public int GetWaveMobCount()
	{
		if (m_bRandom)
		{
			return m_nMobCount;
		}
		if (m_ltWaveMobInfo == null)
		{
			return 0;
		}
		return m_ltWaveMobInfo.Count;
	}
}
