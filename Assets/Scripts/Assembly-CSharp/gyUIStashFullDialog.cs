using UnityEngine;

public class gyUIStashFullDialog : MonoBehaviour
{
	public GameObject mbtnIncrease;

	public GameObject mbtnClose;

	public UILabel mPriceLabel;

	public UILabel mContent;

	private void Awake()
	{
		Show(false);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Show(bool bShow)
	{
		base.gameObject.SetActiveRecursively(bShow);
		if (bShow)
		{
			base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
		}
		else
		{
			base.transform.localPosition = new Vector3(10000f, 0f, base.transform.localPosition.z);
		}
	}

	public void SetPrice(int nPrice)
	{
		if (!(mPriceLabel == null))
		{
			mPriceLabel.text = nPrice.ToString();
		}
	}

	public void SetContent(string str)
	{
		if (!(mContent == null))
		{
			mContent.text = str;
		}
	}
}
