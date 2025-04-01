using UnityEngine;

public class gyUIWheelButton : MonoBehaviour
{
	public GameObject wheelback;

	public GameObject wheelbutton;

	public float wheelradius;

	public bool isWheelMove;

	protected UIRoot mRoot;

	protected UIAnchor mAnchor;

	protected Vector2 m_v2ActualPos;

	protected Vector2 m_v2VitualPos;

	public Vector2 WheelOffSet
	{
		get
		{
			return (wheelbutton.transform.localPosition - wheelback.transform.localPosition) / wheelradius;
		}
	}

	private void Start()
	{
		mRoot = NGUITools.FindInParents<UIRoot>(base.gameObject);
		mAnchor = NGUITools.FindInParents<UIAnchor>(base.gameObject);
		m_v2ActualPos = Vector2.zero;
		m_v2VitualPos = Vector2.zero;
	}

	private void Update()
	{
	}

	public void Reset()
	{
		TweenPosition.Begin(wheelbutton, 0.1f, Vector3.zero);
		if (isWheelMove)
		{
			TweenPosition.Begin(wheelback, 0.1f, Vector3.zero);
		}
	}

	protected void OnPress(bool isPressed)
	{
		if (!isPressed)
		{
			TweenPosition.Begin(wheelbutton, 0.1f, Vector3.zero);
			if (isWheelMove)
			{
				TweenPosition.Begin(wheelback, 0.1f, Vector3.zero);
			}
		}
		else
		{
			m_v2ActualPos = (Vector2)UICamera.lastHit.point - (Vector2)wheelback.transform.position;
			m_v2ActualPos /= mRoot.transform.localScale.x * mAnchor.transform.localScale.x;
			UpdatePos();
		}
	}

	protected void OnDrag(Vector2 delta)
	{
		m_v2ActualPos += delta / mAnchor.transform.localScale.x;
		UpdatePos();
	}

	protected void UpdatePos()
	{
		m_v2VitualPos = m_v2ActualPos - (Vector2)wheelback.transform.localPosition;
		float magnitude = m_v2VitualPos.magnitude;
		Vector2 vector = m_v2VitualPos / magnitude;
		if (magnitude > wheelradius)
		{
			if (isWheelMove)
			{
				wheelback.transform.localPosition += (Vector3)vector * (magnitude - wheelradius);
			}
			magnitude = wheelradius;
		}
		m_v2VitualPos = (Vector2)wheelback.transform.localPosition + vector * magnitude;
		wheelbutton.transform.localPosition = m_v2VitualPos;
	}
}
