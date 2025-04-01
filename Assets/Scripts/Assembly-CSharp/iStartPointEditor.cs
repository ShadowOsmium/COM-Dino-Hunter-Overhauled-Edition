using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

[ExecuteInEditMode]
public class iStartPointEditor : MonoBehaviour
{
	[NonSerialized]
	public int ID = 1;

	[NonSerialized]
	public Color Color = Color.green;

	[NonSerialized]
	public string m_sFileName = "StartPoint";

	[NonSerialized]
	public string m_sInsertIndex = "0";

	[NonSerialized]
	public string m_sDelIndex = "0";

	protected CStartPointManager m_StartPointManager;

	private void Awake()
	{
		base.gameObject.name = "iStartPointEditor";
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnEnable()
	{
		if (m_StartPointManager == null)
		{
			m_StartPointManager = new CStartPointManager();
		}
		Load();
	}

	private void OnDisable()
	{
	}

	private void OnDestroy()
	{
	}

	private void OnLevelWasLoaded()
	{
	}

	private void OnDrawGizmos()
	{
		if (m_StartPointManager == null)
		{
			return;
		}
		Dictionary<int, CStartPoint> data = m_StartPointManager.GetData();
		if (data == null)
		{
			return;
		}
		Gizmos.color = Color;
		foreach (CStartPoint value in data.Values)
		{
			Gizmos.DrawCube(value.v3Pos, value.v3Size);
		}
	}

	public void Load()
	{
		string path = "_Config/_" + m_sFileName + "/" + m_sFileName + "_" + ID;
		TextAsset textAsset = (TextAsset)Resources.Load(path, typeof(TextAsset));
		if (textAsset == null)
		{
			Debug.LogError("Load Failed. Path doesnt exist");
			return;
		}
		List<Transform> list = new List<Transform>();
		foreach (Transform item in base.transform)
		{
			list.Add(item);
		}
		foreach (Transform item2 in list)
		{
			UnityEngine.Object.DestroyImmediate(item2.gameObject);
		}
		m_StartPointManager.ParseXml(textAsset.ToString());
		Dictionary<int, CStartPoint> data = m_StartPointManager.GetData();
		if (data == null)
		{
			return;
		}
		foreach (KeyValuePair<int, CStartPoint> item3 in data)
		{
			AddPointAgent(item3.Key, item3.Value);
		}
		Color = m_StartPointManager.GizmosColor;
	}

	public void Save()
	{
		Dictionary<int, CStartPoint> data = m_StartPointManager.GetData();
		if (data == null)
		{
			return;
		}
		string text = Application.dataPath + "/Resources/_Config/_" + m_sFileName;
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		XmlDocument xmlDocument = new XmlDocument();
		XmlNode newChild = xmlDocument.CreateXmlDeclaration("1.0", "gb2312", "yes");
		xmlDocument.AppendChild(newChild);
		string empty = string.Empty;
		XmlElement xmlElement = xmlDocument.CreateElement("StartPoint");
		xmlElement.SetAttribute("id", ID.ToString());
		empty = Color.r + "," + Color.g + "," + Color.b + "," + Color.a;
		xmlElement.SetAttribute("color", empty);
		xmlDocument.AppendChild(xmlElement);
		foreach (KeyValuePair<int, CStartPoint> item in data)
		{
			CStartPoint value = item.Value;
			XmlElement xmlElement2 = xmlDocument.CreateElement("Point");
			xmlElement2.SetAttribute("id", item.Key.ToString());
			empty = value.v3Pos.x.ToString("f2") + "," + value.v3Pos.y.ToString("f2") + "," + value.v3Pos.z.ToString("f2");
			xmlElement2.SetAttribute("pos", empty);
			empty = value.v3Size.x.ToString("f2") + "," + value.v3Size.y.ToString("f2") + "," + value.v3Size.z.ToString("f2");
			xmlElement2.SetAttribute("size", empty);
			xmlElement.AppendChild(xmlElement2);
		}
		string filename = text + "/" + m_sFileName + "_" + ID + ".xml";
		xmlDocument.Save(filename);
		text = Utils.SavePath() + "/_Config/_" + m_sFileName;
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		filename = text + "/" + m_sFileName + "_" + ID + ".xml";
		xmlDocument.Save(filename);
	}

	public void AddPointAgent(int nID, CStartPoint point)
	{
		GameObject gameObject = new GameObject();
		if (!(gameObject == null))
		{
			gameObject.transform.parent = base.transform;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.name = nID.ToString();
			iStartPointAgent iStartPointAgent2 = gameObject.AddComponent<iStartPointAgent>();
			if (!(iStartPointAgent2 == null))
			{
				iStartPointAgent2.Initialize(this, nID, point.v3Pos, point.v3Size);
			}
		}
	}

	public void DelPointAgent(int nID)
	{
		foreach (Transform item in base.transform)
		{
			iStartPointAgent component = item.GetComponent<iStartPointAgent>();
			if (component == null || component.nID != nID)
			{
				continue;
			}
			UnityEngine.Object.DestroyImmediate(item.gameObject);
			break;
		}
	}

	public void SetPoint(int nID, CStartPoint point)
	{
		m_StartPointManager.Set(nID, point);
	}

	public void DelPoint(int nID)
	{
		m_StartPointManager.Del(nID);
	}

	public CStartPoint GetPoint(int nID)
	{
		return m_StartPointManager.Get(nID);
	}

	public Dictionary<int, CStartPoint> GetData()
	{
		return m_StartPointManager.GetData();
	}
}
