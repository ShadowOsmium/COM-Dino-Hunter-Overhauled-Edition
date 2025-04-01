using UnityEngine;

public class gyUIAimCross_Melee : gyUIAimCross
{
	public Transform mArrow1;

	public Transform mArrow2;

	protected override void SetArrow(float fDis)
	{
		if (mArrow2 != null)
		{
			mArrow2.localScale = new Vector3(fDis, fDis, fDis);
		}
	}
}
