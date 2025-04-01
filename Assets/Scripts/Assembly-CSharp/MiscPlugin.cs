using UnityEngine;

public class MiscPlugin
{
	public static string GetMacAddr()
	{
		string deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
		UnityEngine.Debug.Log("GetMacAddr id : " + deviceUniqueIdentifier);
		return deviceUniqueIdentifier;
	}

	public static bool IsIAPCrack()
	{
		return false;
	}

	public static bool IsJailbreak()
	{
		return false;
	}
}
