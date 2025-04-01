using UnityEngine;

public class iSpawnBullet_Anim : iSpawnBullet
{
	public string sAnimName = string.Empty;

	public WrapMode wrapmode = WrapMode.Once;

	public float sAnimSpeed = 1f;

	protected override void OnInit()
	{
		base.OnInit();
		if (sAnimName.Length >= 1 && !(base.GetComponent<Animation>() == null) && !(base.GetComponent<Animation>()[sAnimName] == null))
		{
			base.GetComponent<Animation>()[sAnimName].speed = sAnimSpeed;
			base.GetComponent<Animation>()[sAnimName].wrapMode = wrapmode;
			base.GetComponent<Animation>().Play(sAnimName);
		}
	}
}
