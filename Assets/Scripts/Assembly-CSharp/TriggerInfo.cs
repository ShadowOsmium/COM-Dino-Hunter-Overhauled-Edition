using System.Collections.Generic;

public class TriggerInfo
{
	public int nEventType;

	public List<int> ltEventParam;

	public bool bEventLoop;

	public TriggerInfo()
	{
		ltEventParam = new List<int>();
	}
}
