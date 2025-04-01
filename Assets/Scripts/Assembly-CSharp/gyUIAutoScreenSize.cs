using UnityEngine;

public class gyUIAutoScreenSize : MonoBehaviour
{
	public bool isAutoScreenWidth;

	public float fAutoWidthRate = 1f;

	public bool isAutoScreenHeight;

	public float fAutoHeightRate = 1f;

	private void Awake()
	{
		Vector3 localScale = base.transform.localScale;
		if (isAutoScreenWidth)
		{
			localScale.x = (float)Screen.width * fAutoWidthRate;
		}
		if (isAutoScreenHeight)
		{
			localScale.y = (float)Screen.height * fAutoHeightRate;
		}
		base.transform.localScale = localScale;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
