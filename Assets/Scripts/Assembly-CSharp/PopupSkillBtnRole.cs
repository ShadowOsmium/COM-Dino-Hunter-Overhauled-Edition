using UnityEngine;

public class PopupSkillBtnRole : MonoBehaviour
{
	public TUIMeshSprite img_normal;

	public TUIMeshSprite img_press;

	private int index;

	private int id;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public int GetIndex()
	{
		return index;
	}

	public void SetIndex(int m_index)
	{
		index = m_index;
	}

	public void SetTexture(int m_id)
	{
		string roleTexture = TUIMappingInfo.Instance().GetRoleTexture(m_id);
		img_normal.texture = roleTexture;
		img_press.texture = roleTexture;
	}

	public int GetID()
	{
		return id;
	}

	public void SetID(int m_id)
	{
		id = m_id;
	}
}
