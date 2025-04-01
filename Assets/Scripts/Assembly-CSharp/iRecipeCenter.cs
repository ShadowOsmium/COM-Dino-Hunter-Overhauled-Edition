using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class iRecipeCenter
{
	protected Dictionary<int, CRecipeInfo> m_dictRecipeInfo;

	public iRecipeCenter()
	{
		m_dictRecipeInfo = new Dictionary<int, CRecipeInfo>();
	}

	public bool Load()
	{
		string content = string.Empty;
		if (MyUtils.isWindows)
		{
			if (!Utils.FileGetString("recipe.xml", ref content))
			{
				return false;
			}
		}
		else if (MyUtils.isIOS || MyUtils.isAndroid)
		{
			TextAsset textAsset = (TextAsset)Resources.Load(PrefabManager.GetPath(3010), typeof(TextAsset));
			if (textAsset == null)
			{
				return false;
			}
			content = textAsset.ToString();
		}
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(content);
		string value = string.Empty;
		XmlNode documentElement = xmlDocument.DocumentElement;
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			if (childNode.Name == "recipe" || !MyUtils.GetAttribute(childNode, "weaponid", ref value))
			{
				continue;
			}
			CRecipeInfo cRecipeInfo = new CRecipeInfo();
			cRecipeInfo.nWeaponID = int.Parse(value);
			if (MyUtils.GetAttribute(childNode, "weaponidnext", ref value))
			{
				cRecipeInfo.nWeaponIDNext = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "needgold", ref value))
			{
				cRecipeInfo.nNeedGold = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "materials", ref value))
			{
				string[] array = value.Split(',');
				for (int i = 0; i < array.Length; i++)
				{
					cRecipeInfo.ltMaterial.Add(new CMaterialInfo(int.Parse(array[i]), 1));
				}
			}
			if (MyUtils.GetAttribute(childNode, "materialscount", ref value))
			{
				string[] array = value.Split(',');
				for (int j = 0; j < array.Length && j < cRecipeInfo.ltMaterial.Count; j++)
				{
					cRecipeInfo.ltMaterial[j].nItemCount = int.Parse(array[j]);
				}
			}
			m_dictRecipeInfo.Add(cRecipeInfo.nWeaponID, cRecipeInfo);
		}
		return true;
	}

	public CRecipeInfo Get(int nID)
	{
		if (!m_dictRecipeInfo.ContainsKey(nID))
		{
			return null;
		}
		return m_dictRecipeInfo[nID];
	}
}
