using UnityEngine;

public class gyUIAimCross_HoldGun : gyUIAimCross
{
	public Transform mArrow1;

	public Transform mArrow2;

	public Transform mArrow3;

	public Transform mArrow4;

	protected override void SetArrow(float fDis)
	{
		Vector2 zero = Vector2.zero;
		if (mArrow1 != null)
		{
			mArrow1.eulerAngles = new Vector3(0f, 180f, 0f);
			zero = new Vector2(-1f, 1f);
			mArrow1.localPosition = zero.normalized * fDis;
		}
		if (mArrow2 != null)
		{
			mArrow2.eulerAngles = new Vector3(0f, 0f, 0f);
			zero = new Vector2(1f, 1f);
			mArrow2.localPosition = zero.normalized * fDis;
		}
		if (mArrow3 != null)
		{
			mArrow3.eulerAngles = new Vector3(180f, 180f, 0f);
			zero = new Vector2(-1f, -1f);
			mArrow3.localPosition = zero.normalized * fDis;
		}
		if (mArrow4 != null)
		{
			mArrow4.eulerAngles = new Vector3(180f, 0f, 0f);
			zero = new Vector2(1f, -1f);
			mArrow4.localPosition = zero.normalized * fDis;
		}
	}
}
