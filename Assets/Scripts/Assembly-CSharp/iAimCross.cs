using UnityEngine;

public class iAimCross : MonoBehaviour
{
	public float Min = 7f;

	public float Max = 20f;

	public Transform Up;

	public Transform Down;

	public Transform Left;

	public Transform Right;

	protected float m_fDisBase;

	protected float m_fCurDis;

	protected float m_fRate;

	protected float m_fRecoverSpeed;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
		float deltaTime = Time.deltaTime;
		if (m_fRate < 1f)
		{
			m_fRate += m_fRecoverSpeed * deltaTime;
			m_fCurDis = MyUtils.Lerp(m_fCurDis, m_fDisBase, m_fRate);
			if (m_fRate > 1f)
			{
				m_fRate = 1f;
			}
			SetArrow(m_fCurDis);
		}
	}

	protected void SetArrow(float fDis)
	{
		if (Up != null)
		{
			Vector3 localPosition = Up.localPosition;
			localPosition.y = fDis;
			Up.localPosition = localPosition;
		}
		if (Down != null)
		{
			Vector3 localPosition2 = Down.localPosition;
			localPosition2.y = 0f - fDis;
			Down.localPosition = localPosition2;
		}
		if (Left != null)
		{
			Vector3 localPosition3 = Left.localPosition;
			localPosition3.x = 0f - fDis;
			Left.localPosition = localPosition3;
		}
		if (Right != null)
		{
			Vector3 localPosition4 = Right.localPosition;
			localPosition4.x = fDis;
			Right.localPosition = localPosition4;
		}
	}

	public void Initialize(float fPrecise)
	{
		fPrecise = Mathf.Clamp01(fPrecise);
		m_fDisBase = Min;
		m_fCurDis = m_fDisBase;
		m_fRecoverSpeed = 0.5f + fPrecise * 2f;
		m_fRate = 1f;
		SetArrow(m_fCurDis);
	}

	public void Expand()
	{
		m_fCurDis += 5f;
		if (m_fCurDis > Max)
		{
			m_fCurDis = Max;
		}
		m_fRate = 0f;
	}
}
