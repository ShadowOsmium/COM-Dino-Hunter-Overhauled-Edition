using UnityEngine;

public class gyUIAimCross_HandGun : gyUIAimCross
{
	public Transform mArrow1;

	protected override void SetArrow(float fDis)
	{
		if (mArrow1 != null)
		{
			mArrow1.localScale = new Vector3(fDis, fDis, fDis);
		}
	}
}
