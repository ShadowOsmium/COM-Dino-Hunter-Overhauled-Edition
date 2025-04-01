using System;
using UnityEngine;

public class CDateHelper
{
	protected static CDateHelper m_Instance;

	public static CDateHelper GetInstance()
	{
		if (m_Instance == null)
		{
			m_Instance = new CDateHelper();
		}
		return m_Instance;
	}

	public bool Parse(string str, ref int day, ref int weekday, ref int hour)
	{
		if (str == null || str.Length < 2)
		{
			return false;
		}
		string[] array = str.Split(':');
		if (array == null || array.Length < 1)
		{
			return false;
		}
		string empty = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			empty = array[i].Trim();
			if (empty[0] == 'D' || empty[0] == 'd')
			{
				day = int.Parse(empty.Substring(1));
				continue;
			}
			if (empty[0] == 'W' || empty[0] == 'w')
			{
				weekday = int.Parse(empty.Substring(1));
				continue;
			}
			if (empty[0] == 'H' || empty[0] == 'h')
			{
				hour = int.Parse(empty.Substring(1));
				continue;
			}
			Debug.LogError(empty + " is not legal string for CDateHerlper");
			return false;
		}
		return true;
	}

	public bool IsIndateperiod(DateTime today, DateTime begin, DateTime end)
	{
		return (today - begin).Ticks >= 0 && (today - end).Ticks <= 0;
	}

	public bool FindDayBefore(DateTime today, int day, int weekday, int count, ref DateTime result)
	{
		result = today;
		for (int i = 0; i < count; i++)
		{
			if ((day == 0 || result.Day == day) && (weekday == -1 || result.DayOfWeek == (DayOfWeek)weekday))
			{
				return true;
			}
			result = result.AddDays(-1.0);
		}
		return false;
	}

	public bool FindDayFuture(DateTime today, int day, int weekday, int count, ref DateTime result)
	{
		result = today;
		for (int i = 0; i < count; i++)
		{
			if ((day == 0 || result.Day == day) && (weekday == -1 || result.DayOfWeek == (DayOfWeek)weekday))
			{
				return true;
			}
			result = result.AddDays(1.0);
		}
		return false;
	}

	public int MonthDays(int year, int month)
	{
		switch (month)
		{
		case 1:
		case 3:
		case 5:
		case 7:
		case 8:
		case 10:
		case 12:
			return 31;
		case 4:
		case 6:
		case 9:
		case 11:
			return 30;
		case 2:
			if (IsBissextile(year))
			{
				return 29;
			}
			return 28;
		default:
			return 0;
		}
	}

	public bool IsBissextile(int year)
	{
		if (year % 4 != 0)
		{
			return false;
		}
		if (year % 100 == 0 && year % 400 != 0)
		{
			return false;
		}
		return true;
	}
}
