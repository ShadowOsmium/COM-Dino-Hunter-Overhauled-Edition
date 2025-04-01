using System.Collections.Generic;

public class CAIManagerInfo
{
	public int nID;

	public int nAI;

	public List<CAITriggerInfo> ltAITrigger;

	public CAIManagerInfo()
	{
		ltAITrigger = new List<CAITriggerInfo>();
	}
}
