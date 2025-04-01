using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class iBlackMarketCenter : iBaseCenter
{
    protected DateTime m_Today;

    protected float m_fTotalTime;

    protected List<CBlackMarketInfo> m_ltBlackMarket;

    protected Dictionary<int, CBlackItem> m_dictBlackItem;

    protected List<int> m_ltDestroyBlackItem;

    public iBlackMarketCenter()
    {
        m_ltBlackMarket = new List<CBlackMarketInfo>();
        m_dictBlackItem = new Dictionary<int, CBlackItem>();
        m_ltDestroyBlackItem = new List<int>();
    }

    public Dictionary<int, CBlackItem> GetData()
    {
        return m_dictBlackItem;
    }

    public CBlackItem Get(int nID)
    {
        if (!m_dictBlackItem.ContainsKey(nID))
        {
            return null;
        }
        return m_dictBlackItem[nID];
    }

    public void Update(float deltaTime)
    {
        m_fTotalTime += deltaTime;
        foreach (KeyValuePair<int, CBlackItem> item in m_dictBlackItem)
        {
            CBlackItem value = item.Value;
            value.m_fTimeCount += deltaTime;
            if (value.m_fTimeCount >= value.m_fTime)
            {
                Debug.Log(value.m_curBlackItemInfo.m_nItemID + " " + value.m_fTime);
                m_ltDestroyBlackItem.Add(item.Key);
            }
        }
        if (m_ltDestroyBlackItem.Count <= 0)
        {
            return;
        }
        foreach (int item2 in m_ltDestroyBlackItem)
        {
            m_dictBlackItem.Remove(item2);
        }
        m_ltDestroyBlackItem.Clear();
    }

    protected CBlackItem CreateItem(CBlackItemInfo blackiteminfo, float m_fTime)
    {
        if (blackiteminfo == null)
        {
            return null;
        }
        CBlackItem cBlackItem = new CBlackItem();
        cBlackItem.m_curBlackItemInfo = blackiteminfo;
        cBlackItem.m_fTime = m_fTime;
        cBlackItem.m_fTimeCount = 0f;
        return cBlackItem;
    }

    public void InitBlackItem(DateTime today)
    {
        Debug.Log("InitBlackItem today is " + today);
        m_dictBlackItem.Clear();
        m_ltDestroyBlackItem.Clear();
        m_Today = today;
        m_fTotalTime = 0f;
        int day = 0;
        int weekday = -1;
        int hour = 0;
        int num = 0;
        foreach (CBlackMarketInfo item in m_ltBlackMarket)
        {
            if (item.m_ltItem == null || item.m_ltItem.Count < 1)
            {
                continue;
            }
            CDateHelper.GetInstance().Parse(item.m_sDate, ref day, ref weekday, ref hour);
            foreach (CBlackItemInfo item2 in item.m_ltItem)
            {
                if (item2.m_nItemType == 0)
                {
                    continue;
                }
                DateTime result = today;
                int num2 = ((item2.m_nTime <= 0) ? item.m_nTime : item2.m_nTime);
                int count = num2 / 24 + 1;
                if (!CDateHelper.GetInstance().FindDayBefore(today, day, weekday, count, ref result))
                {
                    continue;
                }
                Debug.Log(string.Concat("find day:", result, " trigger hour:", hour, " server hour:", today.Hour));
                if (result.Day == today.Day && hour > today.Hour)
                {
                    continue;
                }
                result = new DateTime(result.Year, result.Month, result.Day, hour, 0, 0);
                Debug.Log(string.Concat("find day:", result, " remain time:", num2));
                DateTime dateTime = result.AddHours(num2);
                if (CDateHelper.GetInstance().IsIndateperiod(today, result, dateTime))
                {
                    float fTime = (float)(dateTime - today).TotalSeconds;
                    CBlackItem value = CreateItem(item2, fTime);
                    num++;
                    if (!m_dictBlackItem.ContainsKey(num))
                    {
                        m_dictBlackItem.Add(num, value);
                    }
                }
            }
        }
    }

    public bool CheckBlackMarketRefreshTime(ref float fTime)
    {
        int day = 0;
        int weekday = -1;
        int hour = 0;
        DateTime dateTime = m_Today.AddSeconds(m_fTotalTime);
        float num = 0f;
        foreach (CBlackMarketInfo item in m_ltBlackMarket)
        {
            if (item.m_ltItem == null || item.m_ltItem.Count < 1)
            {
                continue;
            }
            CDateHelper.GetInstance().Parse(item.m_sDate, ref day, ref weekday, ref hour);
            foreach (CBlackItemInfo item2 in item.m_ltItem)
            {
                if (item2.m_nItemType == 0)
                {
                    continue;
                }
                DateTime result = new DateTime(dateTime.Ticks);
                int count = 7;
                if (CDateHelper.GetInstance().FindDayFuture(dateTime, day, weekday, count, ref result))
                {
                    result = new DateTime(result.Year, result.Month, result.Day, hour, 0, 0);
                    float num2 = (float)(result - dateTime).TotalSeconds;
                    if (num2 > 0f && (num == 0f || num2 < num))
                    {
                        num = num2;
                    }
                }
            }
        }
        Debug.Log(num + " " + MyUtils.TimeToString(num));
        if (num > 0f)
        {
            fTime = num;
            return true;
        }
        return false;
    }

    public void InitBlackItemTest()
    {
        Debug.Log("InitBlackItemTest");
        m_dictBlackItem.Clear();
        m_ltDestroyBlackItem.Clear();
        int num = 0;
        foreach (CBlackMarketInfo item in m_ltBlackMarket)
        {
            if (item.m_ltItem == null || item.m_ltItem.Count < 1)
            {
                continue;
            }
            foreach (CBlackItemInfo item2 in item.m_ltItem)
            {
                if (item2.m_nItemType != 0)
                {
                    CBlackItem value = CreateItem(item2, UnityEngine.Random.Range(10f, 180f));
                    num++;
                    if (!m_dictBlackItem.ContainsKey(num))
                    {
                        m_dictBlackItem.Add(num, value);
                    }
                }
            }
        }
    }

    protected override void LoadData(string content)
    {
        m_ltBlackMarket.Clear();
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(content);
        string value = string.Empty;
        XmlNode documentElement = xmlDocument.DocumentElement;
        foreach (XmlNode childNode in documentElement.ChildNodes)
        {
            if (childNode.Name != "blackmarket")
            {
                continue;
            }
            CBlackMarketInfo cBlackMarketInfo = new CBlackMarketInfo();
            m_ltBlackMarket.Add(cBlackMarketInfo);
            if (MyUtils.GetAttribute(childNode, "date", ref value))
            {
                cBlackMarketInfo.m_sDate = value.Trim();
            }
            if (MyUtils.GetAttribute(childNode, "time", ref value))
            {
                cBlackMarketInfo.m_nTime = int.Parse(value);
            }
            foreach (XmlNode item in childNode)
            {
                if (!(item.Name != "node"))
                {
                    CBlackItemInfo cBlackItemInfo = new CBlackItemInfo();
                    cBlackMarketInfo.m_ltItem.Add(cBlackItemInfo);
                    if (MyUtils.GetAttribute(item, "itemtype", ref value))
                    {
                        cBlackItemInfo.m_nItemType = int.Parse(value);
                    }
                    if (MyUtils.GetAttribute(item, "itemid", ref value))
                    {
                        cBlackItemInfo.m_nItemID = int.Parse(value);
                    }
                    if (MyUtils.GetAttribute(item, "iscrystal", ref value))
                    {
                        cBlackItemInfo.m_bCrystal = bool.Parse(value);
                    }
                    if (MyUtils.GetAttribute(item, "price", ref value))
                    {
                        cBlackItemInfo.m_nPrice = int.Parse(value);
                    }
                    if (MyUtils.GetAttribute(item, "time", ref value))
                    {
                        cBlackItemInfo.m_nTime = int.Parse(value);
                    }
                }
            }
        }
    }
}
