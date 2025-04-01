using UnityEngine;

public class iUIAchievementStar : MonoBehaviour
{
	public gyUISwitch mStar1;

	public gyUISwitch mStar2;

	public gyUISwitch mStar3;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Initialize(int nStar)
	{
		switch (nStar)
		{
		case 0:
			mStar1.Switch(false);
			mStar2.Switch(false);
			mStar3.Switch(false);
			break;
		case 1:
			mStar1.Switch(true);
			mStar2.Switch(false);
			mStar3.Switch(false);
			break;
		case 2:
			mStar1.Switch(true);
			mStar2.Switch(true);
			mStar3.Switch(false);
			break;
		default:
			mStar1.Switch(true);
			mStar2.Switch(true);
			mStar3.Switch(true);
			break;
		}
	}

	public void SetStar(int nStar)
	{
		switch (nStar)
		{
		case 0:
			mStar1.Switch(false);
			mStar2.Switch(false);
			mStar3.Switch(false);
			break;
		case 1:
			mStar1.Switch(true);
			mStar2.Switch(false);
			mStar3.Switch(false);
			PlayAnim(mStar1.gameObject);
			break;
		case 2:
			mStar1.Switch(true);
			mStar2.Switch(true);
			mStar3.Switch(false);
			PlayAnim(mStar2.gameObject);
			break;
		default:
			mStar1.Switch(true);
			mStar2.Switch(true);
			mStar3.Switch(true);
			PlayAnim(mStar3.gameObject);
			break;
		}
	}

	public void PlayAnim(GameObject target)
	{
		TweenScale tweenScale = TweenScale.Begin(target, 0.2f, Vector3.zero);
		if (tweenScale != null)
		{
			tweenScale.from = Vector3.zero;
			tweenScale.to = Vector3.one;
			tweenScale.method = UITweener.Method.BounceIn;
		}
		GameObject gameObject = PrefabManager.Get("Artist/Effect/UI/star/star_pfb");
		if (gameObject != null)
		{
			GameObject gameObject2 = Object.Instantiate(gameObject) as GameObject;
			if (gameObject2 != null)
			{
				gameObject2.layer = 8;
				gameObject2.transform.parent = target.transform;
				gameObject2.transform.localPosition = Vector3.zero;
				gameObject2.transform.localRotation = Quaternion.identity;
				Object.Destroy(gameObject2, 1f);
			}
		}
	}
}
