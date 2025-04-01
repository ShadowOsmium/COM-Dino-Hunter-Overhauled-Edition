using System.Collections.Generic;
using UnityEngine;

public class _Main : MonoBehaviour
{
	public Avatar m_Avatar;

	public GameObject[] m_AvatarList;

	public bool[] m_AvatarFace;

	protected float interval;

	private void Start()
	{
	}

	private void Update()
	{
		interval -= Time.deltaTime;
		if (interval <= 0f)
		{
			interval = 1f;
		}
		if (!Input.GetKeyDown(KeyCode.Alpha2) || m_Avatar.m_AvatarPart == null)
		{
			return;
		}
		int num = Random.Range(0, m_Avatar.m_AvatarPart.Length);
		if (m_AvatarList == null)
		{
			return;
		}
		List<GameObject> list = new List<GameObject>();
		List<bool> list2 = new List<bool>();
		for (int i = 0; i < m_AvatarList.Length; i++)
		{
			string value = m_AvatarList[i].name.Substring(0, m_AvatarList[i].name.LastIndexOf('_'));
			if (m_Avatar.m_AvatarPart[num].name.IndexOf(value) != -1)
			{
				list.Add(m_AvatarList[i]);
				list2.Add(m_AvatarFace[i]);
			}
		}
		if (list.Count > 0)
		{
			int index = Random.Range(0, list.Count);
			GameObject gameObject = list[index];
			bool flag = list2[index];
			string sPath_Prefab = "gyUnityTool/Model/" + gameObject.name;
			string empty = string.Empty;
			empty = ((!flag) ? ("gyUnityTool/Texture/" + gameObject.name + "_m") : ("gyUnityTool/Texture/" + gameObject.name + "_0" + Random.Range(1, 6) + "_m"));
			Texture texture = Resources.Load(empty, typeof(Texture)) as Texture;
			m_Avatar.ReplaceAvatar(num, sPath_Prefab, texture);
		}
	}
}
