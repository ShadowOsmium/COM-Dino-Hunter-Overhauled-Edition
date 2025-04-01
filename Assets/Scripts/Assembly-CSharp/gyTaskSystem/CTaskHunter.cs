using System.Collections.Generic;

namespace gyTaskSystem
{
	public class CTaskHunter : CTaskBase
	{
		public class CHunter
		{
			public int nMobID;

			public int nCurCount;

			public int nMaxCount;

			public CHunter(int nMobID, int nMaxCount)
			{
				this.nMobID = nMobID;
				this.nMaxCount = nMaxCount;
				nCurCount = 0;
			}

			public bool IsMax()
			{
				return nCurCount >= nMaxCount;
			}
		}

		public List<CHunter> m_ltHunter;

		public CTaskHunter()
		{
			m_ltHunter = new List<CHunter>();
		}

		public override void Initialize(CTaskInfo taskinfo)
		{
			base.Initialize(taskinfo);
			CTaskInfoHunter cTaskInfoHunter = m_curTaskInfo as CTaskInfoHunter;
			if (cTaskInfoHunter == null)
			{
				return;
			}
			foreach (CTaskInfoHunter.CHunterInfo item in cTaskInfoHunter.ltHunterInfo)
			{
				CHunter cHunter = new CHunter(item.nMobID, item.nMaxCount);
				if (cHunter != null)
				{
					m_ltHunter.Add(cHunter);
				}
			}
		}

		public override void Reset()
		{
			base.Reset();
			foreach (CHunter item in m_ltHunter)
			{
				item.nCurCount = 0;
			}
		}

		public override void OnKillMonster(int nMobID, int nCount = 1)
		{
			int num = 0;
			foreach (CHunter item in m_ltHunter)
			{
				if (item.nMobID == nMobID)
				{
					item.nCurCount += nCount;
					if (item.nCurCount > item.nMaxCount)
					{
						item.nCurCount = item.nMaxCount;
					}
					base.isUpdateData = true;
				}
				if (item.IsMax())
				{
					num++;
				}
			}
			if (num == m_ltHunter.Count)
			{
				TaskCompleted();
			}
		}
	}
}
