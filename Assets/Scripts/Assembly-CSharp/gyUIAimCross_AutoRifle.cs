using UnityEngine;

public class gyUIAimCross_AutoRifle : gyUIAimCross
{
	public Transform mArrow1;

	public Transform mArrow2;

	public Transform mArrow3;

	protected override void SetArrow(float fDis)
	{
		if (mArrow1 != null)
		{
			mArrow1.up = new Vector2(0f, 1f) - Vector2.zero;
			mArrow1.localPosition = mArrow1.up * fDis;
		}
		if (mArrow2 != null)
		{
			mArrow2.up = new Vector2(-0.7f, -0.5f) - Vector2.zero;
			mArrow2.localPosition = mArrow2.up * fDis;
		}
		if (mArrow3 != null)
		{
			mArrow3.up = new Vector2(0.7f, -0.5f) - Vector2.zero;
			mArrow3.localPosition = mArrow3.up * fDis;
		}
	}
}
