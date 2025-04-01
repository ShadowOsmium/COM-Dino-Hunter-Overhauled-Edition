using UnityEngine;

public class iGameTaskUITimeLimit : iGameTaskUIBase
{
	protected UILabel m_LeftTime;

	private void Awake()
	{
		Transform transform = null;
		transform = base.transform.Find("txtTime");
		if (transform != null)
		{
			m_LeftTime = transform.GetComponent<UILabel>();
		}
	}

	public void SetTime(float fLeftTime)
	{
		if (!(m_LeftTime == null))
		{
			fLeftTime = ((!(fLeftTime < 0f)) ? fLeftTime : 0f);
			m_LeftTime.text = MyUtils.TimeToString(fLeftTime);
		}
	}
}
