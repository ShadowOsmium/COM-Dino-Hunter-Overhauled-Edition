using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class iHunterCenter : iBaseCenter
{
    protected List<CHunterInfo> m_ltHunterInfo;

    public iHunterCenter()
    {
        m_ltHunterInfo = new List<CHunterInfo>();
    }

    public List<CHunterInfo> GetData()
    {
        return m_ltHunterInfo;
    }

    public CHunterInfo Get(int nIndex)
    {
        if (nIndex < 0)
        {
            return null;
        }
        if (nIndex >= m_ltHunterInfo.Count)
        {
            return GetLast();
        }
        return m_ltHunterInfo[nIndex];
    }

    public CHunterInfo GetLast()
    {
        if (m_ltHunterInfo == null)
        {
            return null;
        }
        return m_ltHunterInfo[m_ltHunterInfo.Count - 1];
    }

    public float GetExpRate(int nExp, int nLevel)
    {
        CHunterInfo cHunterInfo = Get(nLevel - 1);
        if (cHunterInfo == null)
        {
            return 0f;
        }
        return Mathf.Clamp01((float)nExp / (float)cHunterInfo.m_nExp);
    }

    public void CalcHunterLvl(ref int nLevel, ref int nExp)
    {
        for (int i = 0; i < m_ltHunterInfo.Count && nExp >= m_ltHunterInfo[i].m_nExp; i++)
        {
            nExp -= m_ltHunterInfo[i].m_nExp;
            nLevel = i + 1;
        }
    }

    protected override void LoadData(string content)
    {
        m_ltHunterInfo.Clear();
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(content);
        string value = string.Empty;
        XmlNode documentElement = xmlDocument.DocumentElement;
        foreach (XmlNode childNode in documentElement.ChildNodes)
        {
            if (!(childNode.Name != "node") && MyUtils.GetAttribute(childNode, "exp", ref value))
            {
                CHunterInfo cHunterInfo = new CHunterInfo();
                cHunterInfo.m_nExp = int.Parse(value);
                m_ltHunterInfo.Add(cHunterInfo);
                if (MyUtils.GetAttribute(childNode, "hunterexp_win", ref value))
                {
                    cHunterInfo.m_nHunterExpWin = int.Parse(value);
                }
                if (MyUtils.GetAttribute(childNode, "hunterexp_lose", ref value))
                {
                    cHunterInfo.m_nHunterExpLose = int.Parse(value);
                }
            }
        }
    }
}
