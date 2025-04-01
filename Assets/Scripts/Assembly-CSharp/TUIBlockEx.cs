using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("TUI/Control/Block")]
public class TUIBlockEx : TUIControlImpl
{
	public bool m_bEnable = true;

	public List<TUIRect> rect_list;

	public override bool HandleInput(TUIInput input)
	{
		if (base.HandleInput(input))
		{
			return true;
		}
		if (!m_bEnable)
		{
			return false;
		}
		if (PtInControl(input.position))
		{
			if (rect_list != null)
			{
				for (int i = 0; i < rect_list.Count; i++)
				{
					if (rect_list[i].PtInControl(input.position))
					{
						return false;
					}
				}
			}
			return true;
		}
		return false;
	}
}
