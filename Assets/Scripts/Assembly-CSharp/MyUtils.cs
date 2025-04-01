using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using UnityEngine;

public class MyUtils
{
	protected static int m_nUIDCount;

	protected static PlatformEnum m_SimulatePlatform;

	public static string g_sWindowsAccount = "TESTACCOUNT_WINDOWS";

	public static int UIDCount
	{
		get
		{
			return m_nUIDCount;
		}
		set
		{
			m_nUIDCount = value;
		}
	}

	public static PlatformEnum SimulatePlatform
	{
		get
		{
			return m_SimulatePlatform;
		}
		set
		{
			m_SimulatePlatform = value;
		}
	}

	public static bool isWindows
	{
		get
		{
			return !Application.isMobilePlatform;
		}
	}

	public static bool isIOS
	{
		get
		{
			return m_SimulatePlatform == PlatformEnum.IOS;
		}
	}

	public static bool isAndroid
	{
		get
		{
			return m_SimulatePlatform == PlatformEnum.Android;
		}
	}

	public static bool isPad
	{
		get
		{
			if (Screen.width == 2048 || Screen.width == 1024)
			{
				return true;
			}
			return false;
		}
	}

	public static int GetUID()
	{
		if (++m_nUIDCount > 99999999)
		{
			m_nUIDCount = 1;
		}
		return m_nUIDCount;
	}

	public static void LimitEulerAngle(ref float value, float min, float max)
	{
		if (min == 0f && max == 0f)
		{
			return;
		}
		if (min < 0f)
		{
			min += 360f;
			float num = (min + max) / 2f;
			if (value < min && value > num)
			{
				value = min;
			}
			if (value > max && value < num)
			{
				value = max;
			}
		}
		else
		{
			float num2 = (360f + min + max) / 2f;
			if (value < min || value > num2)
			{
				value = min;
			}
			if (value > max && value < num2)
			{
				value = max;
			}
		}
	}

	public static bool LimitAngle(ref float value, float min, float max)
	{
		if (min == 0f && max == 0f)
		{
			if (value > 360f || value < -360f)
			{
				value %= 360f;
			}
			return true;
		}
		if (value < min)
		{
			value = min;
		}
		else if (value > max)
		{
			value = max;
		}
		return false;
	}

	public static float AngleAroundAxis(Vector3 dirA, Vector3 dirB, Vector3 axis)
	{
		dirA -= Vector3.Project(dirA, axis);
		dirB -= Vector3.Project(dirB, axis);
		float num = Vector3.Angle(dirA, dirB);
		return num * (float)((!(Vector3.Dot(axis, Vector3.Cross(dirA, dirB)) < 0f)) ? 1 : (-1));
	}

	public static float Lerp(float src, float dst, float rate)
	{
		if (rate >= 1f)
		{
			return dst;
		}
		if (rate <= 0f)
		{
			return src;
		}
		return src + (dst - src) * rate;
	}

	public static bool Compare(float cpvalue, int operate, float value, float maxvalue = 0f)
	{
		if (maxvalue == 0f)
		{
			switch (operate)
			{
			case 3:
				operate = 1;
				break;
			case 4:
				operate = 2;
				break;
			}
		}
		switch (operate)
		{
		case 1:
			if (value >= cpvalue)
			{
				return true;
			}
			break;
		case 3:
			if (value / maxvalue >= cpvalue / 100f)
			{
				return true;
			}
			break;
		case 2:
			if (value < cpvalue)
			{
				return true;
			}
			break;
		case 4:
			if (value / maxvalue < cpvalue / 100f)
			{
				return true;
			}
			break;
		}
		return false;
	}

	public static int High32(int nValue)
	{
		return nValue >> 16;
	}

	public static int Low32(int nValue)
	{
		return nValue & 0xFFFF;
	}

	public static int Make32(int nHigh, int nLow)
	{
		return ((nHigh & 0xFFFF) << 16) + (nLow & 0xFFFF);
	}

	public static float GetDistance(Vector3 p1, Vector3 p2)
	{
		return (p1 - p2).magnitude;
	}

	public static float GetDistanceSqr(Vector3 p1, Vector3 p2)
	{
		return (p1 - p2).sqrMagnitude;
	}

	public static Vector3 GetDir(Vector3 p1, Vector3 p2)
	{
		return (p2 - p1).normalized;
	}

	public static float K4S5(float value)
	{
		return Mathf.Floor(value + 0.5f);
	}

	public static float ReckonAnimSpeed(float fTime, float fAnimLen)
	{
		return fAnimLen / fTime;
	}

	public static GameObject LoadResources(string sPath)
	{
		UnityEngine.Object @object = Resources.Load(sPath);
		if (@object == null)
		{
			return null;
		}
		return (GameObject)UnityEngine.Object.Instantiate(@object);
	}

	public static bool GetAttribute(XmlNode node, string name, ref string value)
	{
		if (node == null || node.Attributes[name] == null)
		{
			return false;
		}
		value = node.Attributes[name].Value.Trim();
		if (value.Length < 1)
		{
			return false;
		}
		return true;
	}

	public static string TimeToString(float fTime, bool bIgnoreHour = false)
	{
		if (fTime == 0f)
		{
			if (bIgnoreHour)
			{
				return "--:--";
			}
			return "--:--:--";
		}
		int num = Mathf.FloorToInt(fTime);
		int num2 = num / 60;
		num %= 60;
		int num3 = num2 / 60;
		num2 %= 60;
		string text = string.Empty;
		if (!bIgnoreHour)
		{
			text = ((num3 >= 10) ? (text + num3) : (text + "0" + num3));
			text += ":";
		}
		text = ((num2 >= 10) ? (text + num2) : (text + "0" + num2));
		text += ":";
		if (num < 10)
		{
			return text + "0" + num;
		}
		return text + num;
	}

	public static string PriceToString(int nPrice)
	{
		if (nPrice == 0)
		{
			return nPrice.ToString();
		}
		if (nPrice % 1000000 == 0)
		{
			return nPrice / 1000000 + "M";
		}
		if (nPrice % 1000 == 0)
		{
			return nPrice / 1000 + "K";
		}
		return nPrice.ToString();
	}

	public static Vector3 GetControlPos(Transform transform)
	{
		Vector3 zero = Vector3.zero;
		while (transform.parent != null && transform.name != "TUIControls")
		{
			zero += transform.localPosition;
			transform = transform.parent;
		}
		return zero;
	}

	public static T GetControl<T>(Transform root, string path) where T : MonoBehaviour
	{
		Transform transform = root.Find(path);
		if (transform == null)
		{
			return (T)null;
		}
		return transform.GetComponent<T>();
	}

	public static float CalcShootLightTimeRatio(float min, float max)
	{
		return 1f / (max - min);
	}

	public static bool IsRetina()
	{
		if (Screen.width == 2048 || Screen.width == 960)
		{
			return true;
		}
		return false;
	}

	public static string CombinateStr(string[] arrValue)
	{
		string text = string.Empty;
		if (arrValue == null)
		{
			return text;
		}
		for (int i = 0; i < arrValue.Length; i++)
		{
			text = ((i != 0) ? (text + "," + arrValue[i]) : arrValue[i]);
		}
		return text;
	}

	public static int Formula_Gold2Crystal(int nGold)
	{
		int num = 0;
		num = Mathf.CeilToInt(0.01904f * Mathf.Pow(nGold * 10, 0.8f) - 3f);
		if (num < 1)
		{
			num = 1;
		}
		return num;
	}

	public static string GetMacAddr()
	{
		string text = MiscPlugin.GetMacAddr();
		if (text == null || text.Length <= 0)
		{
			text = "0000000000";
		}
		return text;
	}

	public static string GetUUID()
	{
		string uUID = DevicePlugin.GetUUID();
		if (uUID == null || uUID.Length <= 0)
		{
			uUID = g_sWindowsAccount;
		}
		return uUID;
	}

	public static string GetGCAccount()
	{
		string account = GameCenterPlugin.GetAccount();
		if (account != null && account.Length > 0)
		{
			return account.Substring(2);
		}
		return string.Empty;
	}

	public static string TranslateGCAccount(string account, bool illegal)
	{
		if (account == null || account.Length < 2)
		{
			return account;
		}
		if (illegal)
		{
			if (account[0] == 'G' && account[1] == ':')
			{
				return account;
			}
			return "G:" + account;
		}
		if (account[0] == 'G' && account[1] == ':')
		{
			return account.Substring(2);
		}
		return account;
	}

	public static string GetMD5(string content)
	{
		MD5 mD = new MD5CryptoServiceProvider();
		byte[] bytes = Encoding.Default.GetBytes(content);
		byte[] array = mD.ComputeHash(bytes);
		mD.Clear();
		string text = string.Empty;
		for (int i = 0; i < array.Length - 1; i++)
		{
			text += array[i].ToString("x").PadLeft(2, '0');
		}
		return text;
	}

	public static void SaveFile(string sPath, string sFileData)
	{
		if (sFileData != null && sFileData.Length > 0)
		{
			StreamWriter streamWriter = new StreamWriter(sPath, false);
			streamWriter.Write(sFileData);
			streamWriter.Flush();
			streamWriter.Close();
		}
	}

	public static void ZipString(string content, ref string zipedcontent)
	{
		if (content.Length >= 1)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(content);
			MemoryStream memoryStream = new MemoryStream();
			DeflaterOutputStream deflaterOutputStream = new DeflaterOutputStream(memoryStream);
			deflaterOutputStream.Write(bytes, 0, bytes.Length);
			deflaterOutputStream.Close();
			bytes = memoryStream.ToArray();
			zipedcontent = Convert.ToBase64String(bytes);
		}
	}

	public static void UnZipString(string content, ref string unzipedcontent)
	{
		if (content.Length >= 1)
		{
			byte[] array = Convert.FromBase64String(content);
			InflaterInputStream inflaterInputStream = new InflaterInputStream(new MemoryStream(array, 0, array.Length));
			MemoryStream memoryStream = new MemoryStream();
			int num = 0;
			byte[] array2 = new byte[4096];
			while ((num = inflaterInputStream.Read(array2, 0, array2.Length)) != 0)
			{
				memoryStream.Write(array2, 0, num);
			}
			unzipedcontent = Encoding.UTF8.GetString(memoryStream.ToArray());
		}
	}

	public static int formula_goldendragon(int nPlayerLevel)
	{
		if (nPlayerLevel < 6)
		{
			return 18 + (nPlayerLevel - 1) * 1;
		}
		if (nPlayerLevel < 13)
		{
			return 40 + (nPlayerLevel - 6) * 1;
		}
		if (nPlayerLevel < 26)
		{
			return 70 + (nPlayerLevel - 13) * 2;
		}
		if (nPlayerLevel < 38)
		{
			return 135 + (nPlayerLevel - 26) * 3;
		}
		return 226 + (nPlayerLevel - 38) * 5;
	}

	public static int formula_crystaldragon(int nPlayerLevel)
	{
		return 1 + Mathf.FloorToInt((float)(nPlayerLevel - 1) / 10f);
	}

	public static byte[] Pack(object structobj)
	{
		int num = Marshal.SizeOf(structobj);
		byte[] array = new byte[num];
		IntPtr intPtr = Marshal.AllocHGlobal(num);
		Marshal.StructureToPtr(structobj, intPtr, false);
		Marshal.Copy(intPtr, array, 0, num);
		Marshal.FreeHGlobal(intPtr);
		return array;
	}

	public static void UnPack(object structobj, byte[] data)
	{
		int num = Marshal.SizeOf(structobj);
		if (num <= data.Length)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			Marshal.Copy(data, 0, intPtr, num);
			Marshal.PtrToStructure(intPtr, structobj);
			Marshal.FreeHGlobal(intPtr);
		}
	}

	public static byte[] Serialize<T>(T obj)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
		using (MemoryStream memoryStream = new MemoryStream())
		{
			StreamWriter textWriter = new StreamWriter(memoryStream, Encoding.UTF8);
			XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
			xmlSerializerNamespaces.Add(string.Empty, string.Empty);
			xmlSerializer.Serialize(textWriter, obj, xmlSerializerNamespaces);
			return memoryStream.ToArray();
		}
	}

	public static T Deserialize<T>(byte[] data)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
		using (MemoryStream stream = new MemoryStream(data))
		{
			StreamReader textReader = new StreamReader(stream, Encoding.UTF8);
			return (T)xmlSerializer.Deserialize(textReader);
		}
	}

	public static T Copy<T>(T obj)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
		using (MemoryStream stream = new MemoryStream())
		{
			xmlSerializer.Serialize(stream, obj);
			return (T)xmlSerializer.Deserialize(stream);
		}
	}

	public static float formula_monsterlife(float fLife, int nLevel)
	{
		if (nLevel <= 60)
		{
			return fLife * iMacroDefine.m_fMonsterPower_Life;
		}
		return fLife * (1f + (float)(nLevel - 60) / iMacroDefine.m_fMonsterPower_Lvl) * iMacroDefine.m_fMonsterPower_Life;
	}

	public static float formula_monsterdamage(float fDmg, int nLevel)
	{
		if (nLevel <= 60)
		{
			return fDmg * iMacroDefine.m_fMonsterPower_Damage;
		}
		return fDmg * (1f + (float)(nLevel - 60) / iMacroDefine.m_fMonsterPower_Lvl) * iMacroDefine.m_fMonsterPower_Damage;
	}

	public static int formula_monstergold(int nGold, int nLevel)
	{
		if (nLevel <= 60)
		{
			return Mathf.FloorToInt((float)nGold * iMacroDefine.m_fMonsterReward_Gold);
		}
		return Mathf.FloorToInt((float)nGold * (1f + (float)(nLevel - 60) / iMacroDefine.m_fMonsterReward_Lvl) * iMacroDefine.m_fMonsterReward_Gold);
	}

	public static int formula_monsterexp(int nExp, int nLevel)
	{
		if (nLevel <= 60)
		{
			return Mathf.FloorToInt((float)nExp * iMacroDefine.m_fMonsterReward_Exp);
		}
		return Mathf.FloorToInt((float)nExp * (1f + (float)(nLevel - 60) / iMacroDefine.m_fMonsterReward_Lvl) * iMacroDefine.m_fMonsterReward_Exp);
	}

	public static int formula_stagegold(int nGold, int nLevel)
	{
		if (nLevel <= 60)
		{
			return Mathf.FloorToInt((float)nGold * iMacroDefine.m_fStageReward_Gold);
		}
		return Mathf.FloorToInt((float)nGold * (1f + (float)(nLevel - 60) / iMacroDefine.m_fStageReward_Lvl) * iMacroDefine.m_fStageReward_Gold);
	}

	public static int formula_stageexp(int nExp, int nLevel)
	{
		if (nLevel <= 60)
		{
			return Mathf.FloorToInt((float)nExp * iMacroDefine.m_fStageReward_Exp);
		}
		return Mathf.FloorToInt((float)nExp * (1f + (float)(nLevel - 60) / iMacroDefine.m_fStageReward_Lvl) * iMacroDefine.m_fStageReward_Exp);
	}

	public static float formula_armor2protect(float armor)
	{
		return armor / 10f;
	}

	public static bool FindStartDate(ref DateTime start, DateTime today, int nDay, int nHour, int nWeekDay)
	{
		DateTime dateTime = today;
		if (nDay > 0)
		{
			int num = today.Month;
			if (nDay > today.Day)
			{
				num--;
				if (num <= 0)
				{
					num = 12;
				}
			}
			dateTime = new DateTime(today.Year, num, nDay, 0, 0, 0);
		}
		if (nHour > 0)
		{
			int day = dateTime.Day;
			if (nHour > dateTime.Hour)
			{
				dateTime = dateTime.AddDays(-1.0);
			}
			dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, nHour, 0, 0);
		}
		if (nWeekDay > 0)
		{
			if (nDay > 0)
			{
				if (dateTime.DayOfWeek != (DayOfWeek)nWeekDay)
				{
					return false;
				}
			}
			else
			{
				int dayOfWeek = (int)dateTime.DayOfWeek;
			}
		}
		return true;
	}
}
