using UnityEngine;

public class gyUIIAPIngame : MonoBehaviour
{
	public GameObject mClose;

	public UILabel mTitleLabel;

	public gyUIIAPUnit[] arrUIIAPUnit;

	protected bool m_bShow;

	private void Awake()
	{
		Show(false);
	}

	public void Show(bool bShow)
	{
		m_bShow = bShow;
		base.gameObject.SetActiveRecursively(bShow);
		if (bShow)
		{
			base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
		}
		else
		{
			base.transform.localPosition = new Vector3(10000f, 10000f, base.transform.localPosition.z);
		}
	}

	public void SetTitleParam(int nCount)
	{
		if (!(mTitleLabel == null))
		{
			mTitleLabel.text = "YOU'RE [ff0000]" + nCount + "[-]  CRYSTALS SHORT";
		}
	}

	public gyUIIAPUnit GetIAPUnit(int nIndex)
	{
		if (nIndex < 0 || nIndex >= arrUIIAPUnit.Length)
		{
			return null;
		}
		return arrUIIAPUnit[nIndex];
	}
}
