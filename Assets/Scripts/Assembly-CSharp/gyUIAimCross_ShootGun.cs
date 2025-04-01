using UnityEngine;

public class gyUIAimCross_ShootGun : gyUIAimCross
{
	public Transform mArrow1;

	public Transform mArrow2;

	public Transform mArrow3;

	protected override void SetArrow(float fDis)
	{
		if (mArrow2 != null)
		{
			mArrow2.right = new Vector2(-1f, 0f) - Vector2.zero;
			mArrow2.localPosition = -mArrow2.right * fDis;
		}
		if (mArrow3 != null)
		{
			mArrow3.right = new Vector2(1f, 0f) - Vector2.zero;
			mArrow3.localPosition = -mArrow3.right * fDis;
		}
	}
}
