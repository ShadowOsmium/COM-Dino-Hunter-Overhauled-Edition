using UnityEngine;

public class gyUIItem : MonoBehaviour
{
	public UISprite mIcon;

	public UISprite mBackground;

	public UILabel mCount;

	private void Awake()
	{
		mIcon.transform.localPosition -= new Vector3(0f, 0f, 0.1f);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetIcon(UIAtlas atlas)
	{
		if (!(mIcon == null))
		{
			mIcon.atlas = atlas;
		}
	}

	public void SetBG(string sName)
	{
		if (!(mBackground == null))
		{
			mBackground.spriteName = sName;
		}
	}

	public void SetCount(int nCount)
	{
		if (!(mCount == null))
		{
			mCount.text = nCount.ToString();
		}
	}

	public float GetBGHeight()
	{
		if (mBackground == null)
		{
			return 0f;
		}
		return mBackground.transform.localScale.y;
	}
}
