using System.Collections.Generic;

public class CTaskInfoHunter : CTaskInfo
{
	public class CHunterInfo
	{
		public int nMobID;

		public int nMaxCount;

		public CHunterInfo(int nMobID, int nMaxCount)
		{
			this.nMobID = nMobID;
			this.nMaxCount = nMaxCount;
		}
	}

	public List<CHunterInfo> ltHunterInfo;

	public CTaskInfoHunter()
	{
		ltHunterInfo = new List<CHunterInfo>();
	}
}
