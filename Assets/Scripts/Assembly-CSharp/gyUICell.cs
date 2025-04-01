using UnityEngine;

public class gyUICell : MonoBehaviour
{
	public int nIndex;

	protected gyUICellPanel mCellPanel;

	private void Awake()
	{
		mCellPanel = NGUITools.FindInParents<gyUICellPanel>(base.gameObject);
	}

	protected void OnClick()
	{
		if (mCellPanel != null)
		{
			mCellPanel.OnClickCell(nIndex);
		}
	}

	protected void OnPress(bool isPressed)
	{
		if (mCellPanel != null)
		{
			mCellPanel.OnPressCell(nIndex, isPressed);
		}
	}
}
