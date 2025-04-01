using UnityEngine;

public class gyUITest : MonoBehaviour
{
	public gyUILifeBar bar;

	public gyUILabelDmg dmg;

	private void Start()
	{
		TweenAlpha tweenAlpha = TweenAlpha.Begin(base.gameObject, 0.5f, 0f);
		tweenAlpha.from = 1f;
		tweenAlpha.to = 0f;
		TweenScale tweenScale = TweenScale.Begin(base.gameObject, 0.5f, Vector3.zero);
		tweenScale.from = new Vector3(91f, 91f, 1f);
		tweenScale.to = new Vector3(91f, 91f, 1f) * 2f;
	}

	private void Update()
	{
	}

	public void ShowDmg()
	{
	}
}
