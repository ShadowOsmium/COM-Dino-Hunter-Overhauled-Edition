using System.Collections.Generic;
using UnityEngine;

public class iGizmos : MonoBehaviour
{
	protected class Line
	{
		public Vector3 point1;

		public Vector3 point2;

		public Color color;
	}

	protected class Point
	{
		public Vector3 point;

		public Color color;
	}

	protected Dictionary<string, Point> m_dictPoint;

	protected Dictionary<string, Line> m_dictLine;

	private void Awake()
	{
		m_dictPoint = new Dictionary<string, Point>();
		m_dictLine = new Dictionary<string, Line>();
		Object.DontDestroyOnLoad(base.gameObject);
	}

	private void OnDrawGizmos()
	{
		if (m_dictPoint == null)
		{
			return;
		}
		foreach (Point value in m_dictPoint.Values)
		{
			Gizmos.color = value.color;
			Gizmos.DrawSphere(value.point, 1f);
		}
		foreach (Line value2 in m_dictLine.Values)
		{
			Gizmos.color = value2.color;
			Gizmos.DrawLine(value2.point1, value2.point2);
		}
	}

	public void SetPoint(string sKey, Vector3 p, Color color)
	{
		Point point;
		if (m_dictPoint.ContainsKey(sKey))
		{
			point = m_dictPoint[sKey];
		}
		else
		{
			point = new Point();
			m_dictPoint.Add(sKey, point);
		}
		point.point = p;
		point.color = color;
	}

	public void SetLine(string sKey, Vector3 p1, Vector3 p2, Color color)
	{
		Line line;
		if (m_dictLine.ContainsKey(sKey))
		{
			line = m_dictLine[sKey];
		}
		else
		{
			line = new Line();
			m_dictLine.Add(sKey, line);
		}
		line.point1 = p1;
		line.point2 = p2;
		line.color = color;
	}

	public void SetRay(string sKey, Vector3 p, Vector3 dir, Color color)
	{
		Line line;
		if (m_dictLine.ContainsKey(sKey))
		{
			line = m_dictLine[sKey];
		}
		else
		{
			line = new Line();
			m_dictLine.Add(sKey, line);
		}
		line.point1 = p;
		line.point2 = p + dir * 1000f;
		line.color = color;
	}
}
