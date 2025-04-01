using UnityEngine;

public class DevicePlugin
{
	public static AndroidJavaClass androidplatform;

	public static void InitAndroidPlatform()
	{
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	androidplatform = new AndroidJavaClass("com.trinitigame.androidplatform.AndroidPlatformActivity");
		//}
	}

	public static void AndroidQuit()
	{
		//androidplatform.CallStatic("AndroidQuit");
	}

	public static string GetDeviceModel()
	{
		return string.Empty;
	}

	public static string GetDeviceModelDetail()
	{
		return "";
		//UnityEngine.Debug.Log("GetAndroidVersion : " + androidplatform.CallStatic<string>("GetAndroidVersion", new object[0]));
		//return androidplatform.CallStatic<string>("GetAndroidVersion", new object[0]);
	}

	public static string GetUUID()
	{
		return "";
		//UnityEngine.Debug.Log("GetUUID : " + androidplatform.CallStatic<string>("GetUUID", new object[0]));
		//return androidplatform.CallStatic<string>("GetUUID", new object[0]);
	}

	public static string GetCountryCode()
	{
		return "";
		//UnityEngine.Debug.Log("GetCountry : " + androidplatform.CallStatic<string>("GetCountry", new object[0]));
		//return androidplatform.CallStatic<string>("GetCountry", new object[0]);
	}

	public static string GetLanguageCode()
	{
		return "";
		//UnityEngine.Debug.Log("GetLanguageCode : " + Application.systemLanguage);
		//return Application.systemLanguage.ToString();
	}

	public static string GetSysVersion()
	{
		return "";
		//UnityEngine.Debug.Log("GetAndroidVersion : " + androidplatform.CallStatic<string>("GetAndroidVersion", new object[0]));
		//return androidplatform.CallStatic<string>("GetAndroidVersion", new object[0]);
	}

	public static string GetAppVersion()
	{
		return "";
		//UnityEngine.Debug.Log("GetAndroidAPPVersion : " + androidplatform.CallStatic<string>("GetAndroidAPPVersion", new object[0]));
		//return androidplatform.CallStatic<string>("GetAndroidAPPVersion", new object[0]);
	}

	public static string GetAppBundleId()
	{
		return "";
		//UnityEngine.Debug.Log("GetPackgeName : " + androidplatform.CallStatic<string>("GetPackgeName", new object[0]));
		//return androidplatform.CallStatic<string>("GetPackgeName", new object[0]);
	}
}
