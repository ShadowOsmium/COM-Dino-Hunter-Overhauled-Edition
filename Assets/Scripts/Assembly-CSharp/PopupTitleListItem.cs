using System.Collections.Generic;
using UnityEngine;

public class PopupTitleListItem : MonoBehaviour
{
	public TUILabel label_title;

	private int title_id;

	private void Start()
	{
		Transform[] componentsInChildren = base.gameObject.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].gameObject.layer = 9;
			TUIDrawSprite component = componentsInChildren[i].GetComponent<TUIDrawSprite>();
			if (component != null)
			{
				component.clippingType = TUIDrawSprite.Clipping.None;
			}
		}
	}

	private void Update()
	{
	}

	public void SetInfo(int m_id, Dictionary<int, string> m_title_list)
	{
		if (m_title_list != null && m_title_list.ContainsKey(m_id))
		{
			title_id = m_id;
			string text = m_title_list[m_id];
			if (label_title != null)
			{
				label_title.Text = text;
			}
		}
	}

	public int GetTitleID()
	{
		return title_id;
	}
}
