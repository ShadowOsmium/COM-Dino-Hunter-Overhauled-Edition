using UnityEngine;

public class WeaponKindItem : MonoBehaviour
{
	public GameObject[] m_arrWeaponKind;

	protected int m_nCurIndex;

	private void Awake()
	{
		m_nCurIndex = -1;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public GameObject Get(int nIndex)
	{
		if (nIndex < 0 || nIndex >= m_arrWeaponKind.Length)
		{
			return null;
		}
		return m_arrWeaponKind[nIndex];
	}

	public int GetIndex(kShopWeaponCategory nCategory)
	{
		for (int i = 0; i < m_arrWeaponKind.Length; i++)
		{
			if (!(m_arrWeaponKind[i] == null))
			{
				WeaponKindItemBtn component = m_arrWeaponKind[i].GetComponent<WeaponKindItemBtn>();
				if (!(component == null) && component.m_nCategory == nCategory)
				{
					return i;
				}
			}
		}
		return -1;
	}

	public void SetSelectWeaponBtn(kShopWeaponCategory nCategory)
	{
		int index = GetIndex(nCategory);
		if (index == -1)
		{
			return;
		}
		GameObject gameObject = Get(index);
		if (!(gameObject == null))
		{
			ResetSelectBtn();
			TUIButtonSelect component = gameObject.GetComponent<TUIButtonSelect>();
			if (component != null)
			{
				component.SetSelected(true);
			}
			m_nCurIndex = index;
		}
	}

	public void ResetSelectBtn()
	{
		if (m_arrWeaponKind == null)
		{
			return;
		}
		for (int i = 0; i < m_arrWeaponKind.Length; i++)
		{
			if (!(m_arrWeaponKind[i] == null))
			{
				TUIButtonSelect component = m_arrWeaponKind[i].GetComponent<TUIButtonSelect>();
				if (component != null)
				{
					component.Reset();
				}
			}
		}
		m_nCurIndex = -1;
	}

	public void SetMark(kShopWeaponCategory nCategory, NewMarkType mark)
	{
		GameObject gameObject = Get(GetIndex(nCategory));
		if (!(gameObject == null))
		{
			WeaponKindItemBtn component = gameObject.GetComponent<WeaponKindItemBtn>();
			if (!(component == null))
			{
				component.SetMark(mark);
			}
		}
	}

	public NewMarkType GetMark(kShopWeaponCategory nCategory)
	{
		GameObject gameObject = Get(GetIndex(nCategory));
		if (gameObject == null)
		{
			return NewMarkType.None;
		}
		WeaponKindItemBtn component = gameObject.GetComponent<WeaponKindItemBtn>();
		if (component == null)
		{
			return NewMarkType.None;
		}
		return component.m_Mark;
	}
}
