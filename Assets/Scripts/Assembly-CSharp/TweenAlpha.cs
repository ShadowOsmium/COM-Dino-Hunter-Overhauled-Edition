using UnityEngine;

[AddComponentMenu("NGUI/Tween/Alpha")]
public class TweenAlpha : UITweener
{
	public float from = 1f;

	public float to = 1f;

	private Transform mTrans;

	private UIWidget mWidget;

	public float alpha
	{
		get
		{
			return mWidget.alpha;
		}
		set
		{
			mWidget.alpha = value;
		}
	}

	private void Awake()
	{
		mWidget = GetComponentInChildren<UIWidget>();
	}

	protected override void OnUpdate(float factor, bool isFinished)
	{
		alpha = Mathf.Lerp(from, to, factor);
	}

	public static TweenAlpha Begin(GameObject go, float duration, float alpha)
	{
		TweenAlpha tweenAlpha = UITweener.Begin<TweenAlpha>(go, duration);
		tweenAlpha.from = tweenAlpha.alpha;
		tweenAlpha.to = alpha;
		if (duration <= 0f)
		{
			tweenAlpha.Sample(1f, true);
			tweenAlpha.enabled = false;
		}
		return tweenAlpha;
	}
}
