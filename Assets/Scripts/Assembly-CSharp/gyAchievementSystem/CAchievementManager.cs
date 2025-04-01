using System.Collections;
using System.Collections.Generic;

namespace gyAchievementSystem
{
	public class CAchievementManager
	{
		protected static CAchievementManager m_Instance;

		protected iGameData m_GameData;

		protected iDataCenter m_DataCenter;

		protected List<CAchievementTip> m_ltAchievementTip;

		protected bool m_bNeedSave;

		public static CAchievementManager GetInstance()
		{
			if (m_Instance == null)
			{
				m_Instance = new CAchievementManager();
				m_Instance.Initialize();
			}
			return m_Instance;
		}

		public CAchievementTip PopTip()
		{
			CAchievementTip result = m_ltAchievementTip[0];
			m_ltAchievementTip.RemoveAt(0);
			return result;
		}

		public int GetTipCount()
		{
			return m_ltAchievementTip.Count;
		}

		public void Initialize()
		{
			m_GameData = iGameApp.GetInstance().m_GameData;
			m_DataCenter = m_GameData.GetDataCenter();
			m_ltAchievementTip = new List<CAchievementTip>();
		}

		public void Update(float deltaTime)
		{
		}

		protected void SetAchievementCount(int nID, int nCount)
		{
			CAchievementInfo achievementInfo = m_GameData.GetAchievementInfo(nID);
			if (achievementInfo == null)
			{
				return;
			}
			CAchievementData achiData = m_DataCenter.GetAchiData(nID);
			if (achiData == null)
			{
				return;
			}
			int nCurValue = achiData.nCurValue;
			achiData.nCurValue = nCount;
			int stepCount = achievementInfo.GetStepCount();
			for (int i = 0; i < stepCount; i++)
			{
				CAchievementStep step = achievementInfo.GetStep(i);
				if (step == null || nCurValue >= step.nStepPurpose || achiData.nCurValue < step.nStepPurpose)
				{
					continue;
				}
				if (!achievementInfo.isDaily)
				{
					AddAchievementTip(achievementInfo.nID, achievementInfo.sName, i + 1);
					if (i == stepCount - 1)
					{
						achiData.nState = 2;
					}
				}
				m_bNeedSave = true;
				break;
			}
		}

		protected void AddAchievementTip(int nID, string sName, int nStep)
		{
			CAchievementTip cAchievementTip = new CAchievementTip();
			if (cAchievementTip != null)
			{
				cAchievementTip.nID = nID;
				cAchievementTip.sName = sName;
				cAchievementTip.nStep = nStep;
				m_ltAchievementTip.Add(cAchievementTip);
				iGameApp.GetInstance().Flurry_GainAchi(nID, nStep);
			}
		}

		protected IEnumerable GetAchievementData()
		{
			iAchievementCenter achicenter = m_GameData.GetAchievementCenter();
			Dictionary<int, CAchievementInfo> dictAchievementInfo = achicenter.GetData();
			if (dictAchievementInfo == null)
			{
				yield break;
			}
			foreach (CAchievementInfo info in dictAchievementInfo.Values)
			{
				CAchievementData data = m_DataCenter.GetAchiData(info.nID);
				if (data == null)
				{
					data = new CAchievementData
					{
						nID = info.nID,
						nState = 1,
						nCurValue = 0
					};
					m_DataCenter.AddAchiData(data.nID, data);
				}
				if (data.nState == 1)
				{
					yield return data;
				}
			}
		}

		public int GetAchiStar(int nAchiID)
		{
			iAchievementCenter achievementCenter = m_GameData.GetAchievementCenter();
			if (achievementCenter == null)
			{
				return 0;
			}
			CAchievementInfo achievementInfo = m_GameData.GetAchievementInfo(nAchiID);
			CAchievementData achiData = m_DataCenter.GetAchiData(nAchiID);
			if (achievementInfo == null || achiData == null)
			{
				return 0;
			}
			int stepCount = achievementInfo.GetStepCount();
			for (int i = 0; i < stepCount; i++)
			{
				CAchievementStep step = achievementInfo.GetStep(i);
				if (step != null && achiData.nCurValue < step.nStepPurpose)
				{
					return i;
				}
			}
			return stepCount;
		}

		public void AddAchievement(int nAchiType, object[] arrParam = null)
		{
			iAchievementCenter achievementCenter = m_GameData.GetAchievementCenter();
			if (achievementCenter == null)
			{
				return;
			}
			foreach (CAchievementData achievementDatum in GetAchievementData())
			{
				CAchievementInfo cAchievementInfo = achievementCenter.Get(achievementDatum.nID);
				if (cAchievementInfo == null || cAchievementInfo.nType != nAchiType)
				{
					continue;
				}
				switch (cAchievementInfo.nType)
				{
				case 6:
					if (arrParam != null && arrParam.Length == 2)
					{
						int num3 = (int)arrParam[0];
						int num4 = (int)arrParam[1];
						int nValue2 = -1;
						if (cAchievementInfo.GetParam(0, ref nValue2) && num3 == nValue2 && cAchievementInfo.GetParam(1, ref nValue2) && num4 == nValue2)
						{
							SetAchievementCount(cAchievementInfo.nID, achievementDatum.nCurValue + 1);
						}
					}
					break;
				case 7:
					if (arrParam != null && arrParam.Length == 1)
					{
						int num = (int)arrParam[0];
						int nValue = -1;
						if (cAchievementInfo.GetParam(0, ref nValue) && num == nValue)
						{
							SetAchievementCount(cAchievementInfo.nID, achievementDatum.nCurValue + 1);
						}
					}
					break;
				case 1:
					if (arrParam != null && arrParam.Length == 1)
					{
						int nCount3 = (int)arrParam[0];
						SetAchievementCount(cAchievementInfo.nID, nCount3);
					}
					break;
				case 2:
					if (arrParam != null && arrParam.Length == 1)
					{
						int nCount4 = (int)arrParam[0];
						SetAchievementCount(cAchievementInfo.nID, nCount4);
					}
					break;
				case 9:
					if (arrParam != null && arrParam.Length == 1)
					{
						int num2 = (int)arrParam[0];
						SetAchievementCount(cAchievementInfo.nID, achievementDatum.nCurValue + num2);
					}
					break;
				case 14:
					if (arrParam != null && arrParam.Length == 1)
					{
						int num5 = (int)arrParam[0];
						SetAchievementCount(cAchievementInfo.nID, achievementDatum.nCurValue + num5);
					}
					break;
				case 3:
					if (arrParam != null && arrParam.Length == 1)
					{
						int nCount2 = (int)arrParam[0];
						SetAchievementCount(cAchievementInfo.nID, nCount2);
					}
					break;
				case 4:
					if (arrParam != null && arrParam.Length == 1)
					{
						int nCount = (int)arrParam[0];
						SetAchievementCount(cAchievementInfo.nID, nCount);
					}
					break;
				default:
					SetAchievementCount(cAchievementInfo.nID, achievementDatum.nCurValue + 1);
					break;
				}
			}
			if (m_bNeedSave)
			{
				m_bNeedSave = false;
				m_DataCenter.Save();
			}
		}
	}
}
