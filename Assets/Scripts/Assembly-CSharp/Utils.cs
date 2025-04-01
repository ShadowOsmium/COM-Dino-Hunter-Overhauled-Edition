using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class Utils
{
	private static string m_SavePath;

	static Utils()
	{
		string dataPath = Application.dataPath;
		dataPath = Application.persistentDataPath + "/../Documents";
		if (!Directory.Exists(dataPath))
		{
			Directory.CreateDirectory(dataPath);
		}
		m_SavePath = dataPath;
	}

	public static bool CreateDocumentSubDir(string dirname)
	{
		string path = m_SavePath + "/" + dirname;
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
			return true;
		}
		return false;
	}

	public static void DeleteDocumentDir(string dirname)
	{
		string path = m_SavePath + "/" + dirname;
		if (Directory.Exists(path))
		{
			Directory.Delete(path, true);
		}
	}

	public static string SavePath()
	{
		return m_SavePath;
	}

	public static string GetTextAsset(string txt_name)
	{
		TextAsset textAsset = Resources.Load(txt_name) as TextAsset;
		if (null != textAsset)
		{
			return textAsset.text;
		}
		return string.Empty;
	}

	public static void FileSaveString(string name, string content)
	{
		string text = SavePath() + "/" + name;
		try
		{
			FileStream fileStream = new FileStream(text, FileMode.Create);
			StreamWriter streamWriter = new StreamWriter(fileStream);
			streamWriter.Write(content);
			streamWriter.Close();
			fileStream.Close();
		}
		catch
		{
			Debug.Log("Save" + text + " error");
		}
	}

	public static bool FileGetString(string name, ref string content)
	{
		string text = SavePath() + "/" + name;
		if (!File.Exists(text))
		{
			return false;
		}
		try
		{
			FileStream fileStream = new FileStream(text, FileMode.Open);
			StreamReader streamReader = new StreamReader(fileStream);
			content = streamReader.ReadToEnd();
			streamReader.Close();
			fileStream.Close();
			return true;
		}
		catch
		{
			Debug.Log("Load" + text + " error");
			return false;
		}
	}

	public static void FileGetStringByPath(string sPath, ref string content)
	{
		string text = Application.dataPath + "/" + sPath;
		if (!File.Exists(text))
		{
			Debug.Log("Load" + text + " error");
			return;
		}
		try
		{
			FileStream fileStream = new FileStream(text, FileMode.Open);
			StreamReader streamReader = new StreamReader(fileStream);
			content = streamReader.ReadToEnd();
			streamReader.Close();
			fileStream.Close();
		}
		catch
		{
			Debug.Log("Load" + text + " error");
		}
	}

	public static bool IsRetina()
	{
		if ((Screen.width >= 960 && Screen.height >= 640) || (Screen.width >= 640 && Screen.height >= 960))
		{
			return true;
		}
		return false;
	}

	public static bool IsPad()
	{
		if ((Screen.width > 960 && Screen.height > 640) || (Screen.width > 640 && Screen.height > 960))
		{
			return true;
		}
		return false;
	}

	public static bool ProbabilityIsRandomHit(float rate)
	{
		float num = UnityEngine.Random.Range(0f, 1f);
		float num2 = rate / 2f;
		float num3 = UnityEngine.Random.Range(num2, 1f - num2);
		if (num3 - num2 < num && num < num3 + num2)
		{
			return true;
		}
		return false;
	}

	public static bool IsChineseLetter(string input)
	{
		for (int i = 0; i < input.Length; i++)
		{
			int num = Convert.ToInt32(Convert.ToChar(input.Substring(i, 1)));
			if (num >= 128)
			{
				return true;
			}
		}
		return false;
	}

	public static bool IsCollide(Vector3 p1, float r1, Vector3 p2, float r2)
	{
		return (p1 - p2).sqrMagnitude <= (r1 + r2) * (r1 + r2);
	}

	public static bool PtInRect(Vector2 pt, Rect rect)
	{
		return pt.x >= rect.xMin && pt.x < rect.xMax && pt.y >= rect.yMin && pt.y < rect.yMax;
	}

	public static string AutoMaterialName(string sMaterialName)
	{
		if (IsRetina())
		{
			sMaterialName += "_hd";
		}
		return sMaterialName;
	}

	public static string TimeToString(float fTime)
	{
		if (fTime == 0f)
		{
			return "--:--:--";
		}
		int num = Mathf.FloorToInt(fTime);
		int num2 = num / 60;
		num %= 60;
		int num3 = num2 / 60;
		num2 %= 60;
		string empty = string.Empty;
		empty = ((num3 >= 10) ? (empty + num3) : (empty + "0" + num3));
		empty += ":";
		empty = ((num2 >= 10) ? (empty + num2) : (empty + "0" + num2));
		empty += ":";
		if (num < 10)
		{
			return empty + "0" + num;
		}
		return empty + num;
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

	public static bool CollideWithRect(Vector2 v2Pos1, Vector2 v2Size1, Vector2 v2Pos2, Vector2 v2Size2)
	{
		float num = v2Pos1.x - v2Size1.x / 2f;
		float num2 = v2Pos1.x + v2Size1.x / 2f;
		float num3 = v2Pos1.y - v2Size1.y / 2f;
		float num4 = v2Pos1.y + v2Size1.y / 2f;
		float num5 = v2Pos2.x - v2Size2.x / 2f;
		float num6 = v2Pos2.x + v2Size2.x / 2f;
		float num7 = v2Pos2.y - v2Size2.y / 2f;
		float num8 = v2Pos2.y + v2Size2.y / 2f;
		return num < num6 && num2 > num5 && num3 < num8 && num4 > num7;
	}

	public static bool CollideWithRect(Rect rect1, Rect rect2)
	{
		return rect1.xMin < rect2.xMax && rect1.xMax > rect2.xMin && rect1.yMin < rect2.yMax && rect1.yMax > rect2.yMin;
	}

	public static float AlignToEveness(float num)
	{
		int num2 = (int)num;
		if (num2 % 2 != 0)
		{
			return (float)num2 + 1f;
		}
		return num2;
	}

	public static string GetMD5Code(string str)
	{
		MD5 mD = new MD5CryptoServiceProvider();
		byte[] array = mD.ComputeHash(Encoding.Default.GetBytes(str), 0, str.Length);
		char[] array2 = new char[array.Length];
		Array.Copy(array, array2, array.Length);
		return new string(array2);
	}

	public static Vector2 AutoPosition(Vector2 v2Pos, Vector2 v2Size, Vector2 v2NewSize)
	{
		float num = 480f;
		float num2 = 320f;
		Vector2 zero = Vector2.zero;
		if (v2Pos.x <= 0f)
		{
			float num3 = num / 2f - (v2Pos.x + v2Size.x / 2f);
			if (num3 >= v2NewSize.x)
			{
				zero.x = v2Pos.x + v2Size.x / 2f + v2NewSize.x / 2f;
			}
			else
			{
				zero.x = num / 2f - v2NewSize.x / 2f;
			}
		}
		else
		{
			float num4 = v2Pos.x - v2Size.x / 2f - (0f - num) / 2f;
			if (num4 >= v2NewSize.x)
			{
				zero.x = v2Pos.x - v2Size.x / 2f - v2NewSize.x / 2f;
			}
			else
			{
				zero.x = (0f - num) / 2f + v2NewSize.x / 2f;
			}
		}
		if (v2Pos.y >= 0f)
		{
			float num5 = v2Pos.y - v2Size.y / 2f - (0f - num2) / 2f;
			if (num5 >= v2NewSize.y)
			{
				zero.y = v2Pos.y - v2Size.y / 2f - v2NewSize.y / 2f;
			}
			else
			{
				zero.y = (0f - num2) / 2f + v2NewSize.y / 2f;
			}
		}
		else
		{
			float num6 = num2 / 2f - v2Pos.y + v2Size.y / 2f;
			if (num6 >= v2NewSize.y)
			{
				zero.y = v2Pos.y + v2Size.y / 2f + v2NewSize.y / 2f;
			}
			else
			{
				zero.y = num2 / 2f - v2NewSize.y / 2f;
			}
		}
		return zero;
	}
}
