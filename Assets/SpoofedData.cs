using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpoofedData 
{
	public static string LoadSpoof(string name)
	{
		TextAsset asset = Resources.Load<TextAsset>("spoofedconfigs/" + name);

		if (asset == null)
		{
			Debug.LogError("MISSING SPOOF: " + name);
			return "";
		}

		return asset.text;
	}
}
