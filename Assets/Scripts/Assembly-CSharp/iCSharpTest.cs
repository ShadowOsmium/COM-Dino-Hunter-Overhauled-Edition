using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using UnityEngine;

public class iCSharpTest : MonoBehaviour
{
	[Serializable]
	public class A
	{
		public int i;

		protected string s;
	}

	[Serializable]
	public class B
	{
	}

	[Serializable]
	protected class CAB
	{
		public int m_nId;

		protected string[] m_sArr;
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.J))
		{
			TestFunc3("w+$", "123.,\"nf kf ");
		}
	}

	protected void TestFunc1()
	{
		List<int> list = new List<int>();
		list.Add(1);
		list.Add(1);
		list.Add(2);
		list.Add(2);
		list.Add(3);
		list.Add(3);
		list.Add(3);
		list.Add(4);
		list.Add(5);
		list.Add(5);
		list.Add(6);
		list.Add(7);
		list.Add(8);
		list.Add(8);
		List<int> source = list;
		source = source.Distinct().ToList();
		foreach (int item in source)
		{
			Debug.Log(item.ToString());
		}
	}

	protected void TestFunc2()
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		MemoryStream memoryStream = null;
		A a = null;
		a = new A();
		Stream stream = File.Create("SerializedA.txt");
		a.i = 10;
		binaryFormatter.Serialize(stream, a);
		stream.Close();
		B graph = new B();
		stream = File.Create("SerializedB.txt");
		binaryFormatter.Serialize(stream, graph);
		stream.Close();
		CAB graph2 = new CAB();
		stream = File.Create("SerializedCAB.txt");
		binaryFormatter.Serialize(stream, graph2);
		stream.Close();
	}

	protected void TestFunc3(string pattern, string str)
	{
		if (!new Regex(pattern).IsMatch(str))
		{
			Debug.Log("no match");
		}
		else
		{
			Debug.Log("match");
		}
	}
}
