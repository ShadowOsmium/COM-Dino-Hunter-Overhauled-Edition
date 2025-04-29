using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class iLoadTipCenter : iBaseCenter
{
    protected List<CLoadTipInfo> m_ltLoadTipInfo;

    private const int MaxLoadTips = 16; // Maximum allowed tips

    public iLoadTipCenter()
    {
        m_ltLoadTipInfo = new List<CLoadTipInfo>();
    }

    public CLoadTipInfo GetRandom()
    {
        if (m_ltLoadTipInfo == null || m_ltLoadTipInfo.Count == 0)
        {
            LoadData(SpoofedData.LoadSpoof("loadtip"));
        }
        return m_ltLoadTipInfo[Random.Range(0, m_ltLoadTipInfo.Count)];
    }

    protected override void LoadData(string content)
    {
        m_ltLoadTipInfo.Clear();
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(content);
        string value = string.Empty;
        XmlNode documentElement = xmlDocument.DocumentElement;
        foreach (XmlNode childNode in documentElement.ChildNodes)
        {
            if (childNode.Name == "tip")
            {
                CLoadTipInfo cLoadTipInfo = new CLoadTipInfo();
                bool hasIcon = MyUtils.GetAttribute(childNode, "icon", ref value);
                if (hasIcon)
                {
                    cLoadTipInfo.sIcon = value;
                }
                else
                {
                    Debug.LogWarning("Missing icon attribute in load tip!");
                }

                bool hasDesc = MyUtils.GetAttribute(childNode, "desc", ref value);
                if (hasDesc)
                {
                    cLoadTipInfo.sDesc = value;
                }
                else
                {
                    Debug.LogWarning("Missing desc attribute in load tip!");
                }

                if (hasIcon && hasDesc)
                {
                    m_ltLoadTipInfo.Add(cLoadTipInfo);
                }
                else
                {
                    Debug.LogWarning("Skipping broken load tip (missing icon or desc).");
                }
            }
        }

        if (m_ltLoadTipInfo.Count > 16)
        {
            m_ltLoadTipInfo.RemoveRange(16, m_ltLoadTipInfo.Count - 16);
        }

        Debug.Log("Loaded " + m_ltLoadTipInfo.Count + " valid load tips.");
    }
}