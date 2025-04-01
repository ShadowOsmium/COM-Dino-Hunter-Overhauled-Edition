using UnityEngine;

public class PopupEquipItems : MonoBehaviour
{
	public BtnItem_Item[] m_arrItem;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetInfo(int m_index, TUIPopupInfo m_info, bool play_animation = false, bool m_use_customize = false)
	{
		if (m_arrItem != null && m_info != null && m_index >= 0 && m_index < m_arrItem.Length && !(m_arrItem[m_index] == null))
		{
			if (!m_info.IsWeapon() && !m_info.IsArmor() && !m_info.IsAccessory())
			{
				Debug.Log("type error!");
			}
			else
			{
				m_arrItem[m_index].SetInfo(m_info, play_animation, m_use_customize);
			}
		}
	}
}
