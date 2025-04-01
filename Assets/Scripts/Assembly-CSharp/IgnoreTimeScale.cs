using UnityEngine;

[AddComponentMenu("NGUI/Internal/Ignore TimeScale Behaviour")]
public class IgnoreTimeScale : MonoBehaviour
{
	private float mTimeStart;

	private float mTimeDelta;

	private float mActual;

	private bool mTimeStarted;

	public float realTimeDelta
	{
		get
		{
			return mTimeDelta;
		}
	}

	protected virtual void OnEnable()
	{
		mTimeStarted = true;
		mTimeDelta = 0f;
		mTimeStart = Time.realtimeSinceStartup;
	}

	protected float UpdateRealTimeDelta()
	{
		if (mTimeStarted)
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			float b = realtimeSinceStartup - mTimeStart;
			mActual += Mathf.Max(0f, b);
			mTimeDelta = 0.001f * Mathf.Round(mActual * 1000f);
			mActual -= mTimeDelta;
			if (mTimeDelta > 1f)
			{
				mTimeDelta = 1f;
			}
			mTimeStart = realtimeSinceStartup;
		}
		else
		{
			mTimeStarted = true;
			mTimeStart = Time.realtimeSinceStartup;
			mTimeDelta = 0f;
		}
		return mTimeDelta;
	}
}
