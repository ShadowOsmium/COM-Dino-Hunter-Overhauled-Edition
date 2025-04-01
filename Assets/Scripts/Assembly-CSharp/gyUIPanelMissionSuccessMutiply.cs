using UnityEngine;

public class gyUIPanelMissionSuccessMutiply : MonoBehaviour
{
	public GameObject m_Champion;

	public GameObject m_Continue;

	public gyUIPlayerRewards[] m_arrPlayerRewards;

	private void Awake()
	{
		Show(false);
	}

	public void Show(bool bShow)
	{
		base.gameObject.SetActiveRecursively(bShow);
		if (bShow)
		{
			base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
			for (int i = 0; i < m_arrPlayerRewards.Length; i++)
			{
				if (!(m_arrPlayerRewards[i] == null))
				{
					m_arrPlayerRewards[i].Show(false);
				}
			}
			m_Champion.active = false;
		}
		else
		{
			base.transform.localPosition = new Vector3(1000f, 0f, base.transform.localPosition.z);
		}
	}

	public bool IsShow()
	{
		return base.gameObject.active;
	}

	public void ShowPlayerRewards(int nIndex, bool bShow)
	{
		gyUIPlayerRewards playerRewards = GetPlayerRewards(nIndex);
		if (!(playerRewards == null))
		{
			playerRewards.Show(bShow);
		}
	}

	public gyUIPlayerRewards GetPlayerRewards(int nIndex)
	{
		if (nIndex < 0 || nIndex >= m_arrPlayerRewards.Length)
		{
			return null;
		}
		return m_arrPlayerRewards[nIndex];
	}
}
