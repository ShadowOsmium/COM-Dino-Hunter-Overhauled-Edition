using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace gyDataCenter
{
	public class XMLManager
	{
		public static bool Load(XmlDocument doc, string xpath, Dictionary<int, FieldInfo> dictFieldInfo, Dictionary<int, CData> dictData)
		{
			XmlNodeList xmlNodeList = doc.SelectNodes(xpath);
			if (xmlNodeList.Count < 1)
			{
				Debug.Log("cant find node for xpath:" + xpath);
				return false;
			}
			foreach (XmlNode item in xmlNodeList)
			{
				XmlElement element = (XmlElement)item;
				LoadAttribute(element, dictFieldInfo, dictData);
			}
			return true;
		}

		public static void Save()
		{
		}

		public static void LoadAttribute(XmlElement element, Dictionary<int, FieldInfo> dictField, Dictionary<int, CData> dictData)
		{
			if (dictField == null)
			{
				return;
			}
			CData cData = new CData();
			foreach (KeyValuePair<int, FieldInfo> item in dictField)
			{
				string text = element.GetAttribute(item.Value.sFieldName).Trim();
				if (text.Length < 1)
				{
					continue;
				}
				switch (item.Value.type)
				{
				case kDataType.INT:
					cData.SetData(item.Key, new CDataBaseInt(int.Parse(text)));
					if (item.Value.isKey)
					{
						int key = int.Parse(text);
						if (!dictData.ContainsKey(key))
						{
							dictData.Add(key, cData);
						}
					}
					break;
				case kDataType.FLOAT:
					cData.SetData(item.Key, new CDataBaseFloat(float.Parse(text)));
					break;
				case kDataType.BOOL:
					cData.SetData(item.Key, new CDataBaseBool(bool.Parse(text)));
					break;
				case kDataType.STRING:
					cData.SetData(item.Key, new CDataBaseString(text));
					break;
				case kDataType.ARR_INT:
				{
					string[] array = text.Split(',');
					if (array.Length > 0)
					{
						CDataBaseArrInt cDataBaseArrInt = new CDataBaseArrInt(array.Length);
						for (int l = 0; l < array.Length; l++)
						{
							cDataBaseArrInt.SetValue(l, int.Parse(array[l]));
						}
						cData.SetData(item.Key, cDataBaseArrInt);
					}
					break;
				}
				case kDataType.ARR_FLOAT:
				{
					string[] array = text.Split(',');
					if (array.Length > 0)
					{
						CDataBaseArrFloat cDataBaseArrFloat = new CDataBaseArrFloat(array.Length);
						for (int j = 0; j < array.Length; j++)
						{
							cDataBaseArrFloat.SetValue(j, float.Parse(array[j]));
						}
						cData.SetData(item.Key, cDataBaseArrFloat);
					}
					break;
				}
				case kDataType.ARR_BOOL:
				{
					string[] array = text.Split(',');
					if (array.Length > 0)
					{
						CDataBaseArrBool cDataBaseArrBool = new CDataBaseArrBool(array.Length);
						for (int k = 0; k < array.Length; k++)
						{
							cDataBaseArrBool.SetValue(k, bool.Parse(array[k]));
						}
						cData.SetData(item.Key, cDataBaseArrBool);
					}
					break;
				}
				case kDataType.ARR_STRING:
				{
					string[] array = text.Split(',');
					if (array.Length > 0)
					{
						CDataBaseArrString cDataBaseArrString = new CDataBaseArrString(array.Length);
						for (int i = 0; i < array.Length; i++)
						{
							cDataBaseArrString.SetValue(i, array[i]);
						}
						cData.SetData(item.Key, cDataBaseArrString);
					}
					break;
				}
				}
			}
		}
	}
}
