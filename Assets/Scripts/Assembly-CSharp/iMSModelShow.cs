using System.Collections.Generic;
using System.IO;
using System.Xml;
using MSDataUnit;
using UnityEngine;

[ExecuteInEditMode]
public class iMSModelShow : MonoBehaviour
{
	public string filename;

	public iMSModel m_MSModel;

	public List<CMSDataBase> m_ltMSData;

	public bool isEditor;

	protected bool m_bActive;

	protected float m_fTimeCount;

	protected int m_nRunIndex;

	private void Awake()
	{
		m_ltMSData = new List<CMSDataBase>();
		m_bActive = false;
		if (isEditor && filename.Length > 0)
		{
			Load(filename);
			Start();
		}
	}

	private void Update()
	{
		if (isEditor && m_ltMSData == null)
		{
			m_ltMSData = new List<CMSDataBase>();
			Load(filename);
		}
		Update(Time.deltaTime);
	}

	protected void Update(float deltaTime)
	{
		if (!m_bActive)
		{
			return;
		}
		m_fTimeCount += deltaTime;
		while (m_nRunIndex >= 0 && m_nRunIndex < m_ltMSData.Count)
		{
			CMSDataBase cMSDataBase = m_ltMSData[m_nRunIndex];
			float fTimeBegin = cMSDataBase.m_fTimeBegin;
			if (m_fTimeCount < fTimeBegin)
			{
				break;
			}
			switch (cMSDataBase.m_Type)
			{
			case kMSDataType.Move:
			{
				CMSDataMove cMSDataMove = cMSDataBase as CMSDataMove;
				if (cMSDataMove != null)
				{
					m_MSModel.Move(cMSDataMove.m_v3Dst, cMSDataMove.m_sAnim, cMSDataMove.m_fAnimRate, cMSDataMove.m_fTimeEnd - cMSDataMove.m_fTimeBegin);
				}
				break;
			}
			case kMSDataType.Anim:
			{
				CMSDataAnim cMSDataAnim = cMSDataBase as CMSDataAnim;
				if (cMSDataAnim != null)
				{
					m_MSModel.CrossAnim(cMSDataAnim.m_sAnim, cMSDataAnim.m_WrapMode, cMSDataAnim.m_fAnimSpeed, cMSDataAnim.m_fAnimTime);
				}
				break;
			}
			}
			m_nRunIndex++;
		}
	}

	public void Start()
	{
		m_bActive = true;
		m_fTimeCount = 0f;
		m_nRunIndex = 0;
	}

	public void Stop()
	{
		m_bActive = false;
	}

	public bool Save(string m_sFileName)
	{
		if (m_ltMSData.Count < 1)
		{
			return false;
		}
		string text = Application.dataPath + "/Resources/_Config/MSModelShow";
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		string empty = string.Empty;
		XmlDocument xmlDocument = new XmlDocument();
		XmlNode newChild = xmlDocument.CreateXmlDeclaration("1.0", "gb2312", "yes");
		xmlDocument.AppendChild(newChild);
		XmlElement xmlElement = xmlDocument.CreateElement("Root");
		xmlElement.SetAttribute("name", m_sFileName);
		xmlDocument.AppendChild(xmlElement);
		for (int i = 0; i < m_ltMSData.Count; i++)
		{
			CMSDataBase cMSDataBase = m_ltMSData[i];
			XmlElement xmlElement2 = xmlDocument.CreateElement("MSNode");
			xmlElement2.SetAttribute("Begin", cMSDataBase.m_fTimeBegin.ToString());
			xmlElement2.SetAttribute("End", cMSDataBase.m_fTimeEnd.ToString());
			int type = (int)cMSDataBase.m_Type;
			xmlElement2.SetAttribute("Type", type.ToString());
			switch (cMSDataBase.m_Type)
			{
			case kMSDataType.Tele:
			{
				CMSDataTele cMSDataTele = cMSDataBase as CMSDataTele;
				empty = cMSDataTele.m_v3Dst.x + "," + cMSDataTele.m_v3Dst.y + "," + cMSDataTele.m_v3Dst.z;
				xmlElement2.SetAttribute("dst", empty);
				break;
			}
			case kMSDataType.Move:
			{
				CMSDataMove cMSDataMove = cMSDataBase as CMSDataMove;
				empty = cMSDataMove.m_v3Dst.x + "," + cMSDataMove.m_v3Dst.y + "," + cMSDataMove.m_v3Dst.z;
				xmlElement2.SetAttribute("dst", empty);
				xmlElement2.SetAttribute("moveanim", cMSDataMove.m_sAnim);
				xmlElement2.SetAttribute("moveanimrate", cMSDataMove.m_fAnimRate.ToString());
				break;
			}
			case kMSDataType.Anim:
			{
				CMSDataAnim cMSDataAnim = cMSDataBase as CMSDataAnim;
				xmlElement2.SetAttribute("animname", cMSDataAnim.m_sAnim);
				int wrapMode = (int)cMSDataAnim.m_WrapMode;
				xmlElement2.SetAttribute("wrapmode", wrapMode.ToString());
				xmlElement2.SetAttribute("animspeed", cMSDataAnim.m_fAnimSpeed.ToString());
				xmlElement2.SetAttribute("animtime", cMSDataAnim.m_fAnimTime.ToString());
				break;
			}
			}
			xmlElement.AppendChild(xmlElement2);
		}
		string text2 = text + "/" + m_sFileName + ".xml";
		xmlDocument.Save(text2);
		text = Utils.SavePath() + "/_Config/MSModelShow";
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		text2 = text + "/" + m_sFileName + ".xml";
		xmlDocument.Save(text2);
		return true;
	}

	public bool Load(string m_sFileName)
	{
		string content = string.Empty;
		if (MyUtils.isWindows)
		{
			if (!Utils.FileGetString("_Config/MSModelShow/" + m_sFileName + ".xml", ref content))
			{
				return false;
			}
		}
		else if (MyUtils.isIOS || MyUtils.isAndroid)
		{
			TextAsset textAsset = (TextAsset)Resources.Load("_Config/MSModelShow/" + m_sFileName, typeof(TextAsset));
			if (textAsset == null)
			{
				return false;
			}
			content = textAsset.ToString();
		}
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(content);
		string value = string.Empty;
		if (m_ltMSData != null)
		{
			m_ltMSData.Clear();
		}
		XmlNode documentElement = xmlDocument.DocumentElement;
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			if (childNode.Name != "MSNode")
			{
				continue;
			}
			kMSDataType kMSDataType = kMSDataType.None;
			if (!MyUtils.GetAttribute(childNode, "Type", ref value))
			{
				continue;
			}
			kMSDataType = (kMSDataType)int.Parse(value);
			CMSDataBase cMSDataBase = null;
			switch (kMSDataType)
			{
			case kMSDataType.Tele:
			{
				CMSDataTele cMSDataTele = new CMSDataTele();
				cMSDataBase = cMSDataTele;
				if (MyUtils.GetAttribute(childNode, "dst", ref value))
				{
					string[] array = value.Split(',');
					if (array.Length > 2)
					{
						cMSDataTele.m_v3Dst = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
					}
				}
				break;
			}
			case kMSDataType.Move:
			{
				CMSDataMove cMSDataMove = new CMSDataMove();
				cMSDataBase = cMSDataMove;
				if (MyUtils.GetAttribute(childNode, "dst", ref value))
				{
					string[] array = value.Split(',');
					if (array.Length > 2)
					{
						cMSDataMove.m_v3Dst = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
					}
				}
				if (MyUtils.GetAttribute(childNode, "moveanim", ref value))
				{
					cMSDataMove.m_sAnim = value;
				}
				if (MyUtils.GetAttribute(childNode, "moveanimrate", ref value))
				{
					cMSDataMove.m_fAnimRate = float.Parse(value);
				}
				break;
			}
			case kMSDataType.Anim:
			{
				CMSDataAnim cMSDataAnim = new CMSDataAnim();
				cMSDataBase = cMSDataAnim;
				if (MyUtils.GetAttribute(childNode, "animname", ref value))
				{
					cMSDataAnim.m_sAnim = value;
				}
				if (MyUtils.GetAttribute(childNode, "wrapmode", ref value))
				{
					cMSDataAnim.m_WrapMode = (WrapMode)int.Parse(value);
				}
				if (MyUtils.GetAttribute(childNode, "animspeed", ref value))
				{
					cMSDataAnim.m_fAnimSpeed = float.Parse(value);
				}
				if (MyUtils.GetAttribute(childNode, "animtime", ref value))
				{
					cMSDataAnim.m_fAnimTime = float.Parse(value);
				}
				break;
			}
			}
			if (cMSDataBase != null)
			{
				if (MyUtils.GetAttribute(childNode, "Begin", ref value))
				{
					cMSDataBase.m_fTimeBegin = float.Parse(value);
				}
				if (MyUtils.GetAttribute(childNode, "End", ref value))
				{
					cMSDataBase.m_fTimeEnd = float.Parse(value);
				}
				Add(cMSDataBase);
			}
		}
		return true;
	}

	public void Add(CMSDataBase data)
	{
		if (m_ltMSData == null)
		{
			m_ltMSData = new List<CMSDataBase>();
		}
		if (m_ltMSData.Count == 0)
		{
			m_ltMSData.Add(data);
			return;
		}
		int num = -1;
		for (int i = 0; i < m_ltMSData.Count; i++)
		{
			if (data.m_fTimeBegin > m_ltMSData[i].m_fTimeBegin)
			{
				continue;
			}
			if (data.m_fTimeBegin != m_ltMSData[i].m_fTimeBegin)
			{
				num = i;
				break;
			}
			if (!(data.m_fTimeEnd > m_ltMSData[i].m_fTimeEnd))
			{
				if (data.m_fTimeEnd != m_ltMSData[i].m_fTimeEnd)
				{
					num = i;
					break;
				}
				if (data.m_Type <= m_ltMSData[i].m_Type && data.m_Type != m_ltMSData[i].m_Type)
				{
					num = i;
					break;
				}
			}
		}
		if (num != -1)
		{
			m_ltMSData.Insert(num, data);
		}
		else
		{
			m_ltMSData.Add(data);
		}
	}
}
