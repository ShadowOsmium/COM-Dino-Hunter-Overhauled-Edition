using UnityEngine;

public class gyUILabelButton : MonoBehaviour
{
	public UILabel mLabel;

	public void SetLabel(string str)
	{
		if (!(mLabel == null))
		{
			mLabel.text = str;
		}
	}
}
