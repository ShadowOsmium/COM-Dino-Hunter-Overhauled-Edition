using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour
{
	public Transform p1;

	public Transform p2;

	public Transform p3;

	public Transform p4;

	public int pointcount = 10;

	protected List<Vector3> ltPoint1;

	protected List<Vector3> ltPoint2;

	private void Start()
	{
		ltPoint1 = new List<Vector3>();
		ltPoint2 = new List<Vector3>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			ltPoint1.Clear();
			Vector3[] array = CalcBeiser1(p1.position, p2.position, p3.position, p4.position);
			for (int i = 0; i < array.Length; i++)
			{
				ltPoint1.Add(array[i]);
			}
		}
		if (Input.GetKeyDown(KeyCode.V))
		{
			ltPoint2.Clear();
			Vector3[] array2 = CalcBeiser2(p1.position, p2.position, p3.position, p4.position);
			for (int j = 0; j < array2.Length; j++)
			{
				ltPoint2.Add(array2[j]);
			}
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		for (int i = 1; i < ltPoint1.Count; i++)
		{
			Gizmos.DrawLine(ltPoint1[i - 1], ltPoint1[i]);
		}
		Gizmos.color = Color.white;
		for (int j = 1; j < ltPoint2.Count; j++)
		{
			Gizmos.DrawLine(ltPoint2[j - 1], ltPoint2[j]);
		}
	}

	public Vector3[] CalcBeiser1(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		Vector3[] array = new Vector3[pointcount];
		array[0] = p0;
		array[pointcount - 1] = p3;
		for (int i = 1; i < pointcount - 1; i++)
		{
			float num = (float)i / (float)pointcount;
			array[i] = p0 * Mathf.Pow(1f - num, 3f) + 3f * p1 * num * Mathf.Pow(1f - num, 2f) + 3f * p2 * num * num * (1f - num) + p3 * num * num * num;
		}
		Debug.Log("CalcBeiser1 time " + (Time.realtimeSinceStartup - realtimeSinceStartup));
		return array;
	}

	public Vector3[] CalcBeiser2(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		Vector3[] array = new Vector3[pointcount];
		array[0] = p0;
		array[pointcount - 1] = p3;
		for (int i = 1; i < pointcount - 1; i++)
		{
			float t = (float)i / (float)pointcount;
			Vector3 from = Vector3.Lerp(p0, p1, t);
			Vector3 vector = Vector3.Lerp(p1, p2, t);
			Vector3 to = Vector3.Lerp(p2, p3, t);
			from = Vector3.Lerp(from, vector, t);
			vector = Vector3.Lerp(vector, to, t);
			array[i] = Vector3.Lerp(from, vector, t);
		}
		Debug.Log("CalcBeiser2 time " + (Time.realtimeSinceStartup - realtimeSinceStartup));
		return array;
	}
}
