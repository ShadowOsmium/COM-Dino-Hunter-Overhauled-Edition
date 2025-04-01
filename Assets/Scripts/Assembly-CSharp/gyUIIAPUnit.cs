using UnityEngine;

public class gyUIIAPUnit : MonoBehaviour
{
	public UILabel mGainValue;

	public UISprite mGainIcon;

	public GameObject mButton;

	public UILabel mButtonLabel;

	public int nIndex;

	public int nIAPID;

	protected CIAPInfo m_IAPInfo;

	private void Awake()
	{
	}

	public void SetIAPID(int nID)
	{
		nIAPID = nID;
	}

	public void SetIcon(string sIcon)
	{
		if (!(mGainIcon == null))
		{
			GameObject gameObject = PrefabManager.Get("Artist/Atlas/IAP/" + sIcon);
			if (gameObject != null)
			{
				mGainIcon.atlas = gameObject.GetComponent<UIAtlas>();
			}
		}
	}

	public void SetButtonLabel(string str)
	{
		if (mButtonLabel != null)
		{
			mButtonLabel.text = str;
		}
	}

	public void SetGainValue(string str)
	{
		if (mGainValue != null)
		{
			mGainValue.text = str;
		}
	}
}
