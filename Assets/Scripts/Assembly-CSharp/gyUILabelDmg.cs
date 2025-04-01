using UnityEngine;

[RequireComponent(typeof(gyUIPoolObject))]
public class gyUILabelDmg : MonoBehaviour
{
	public enum kMode
	{
		None,
		Mode1,
		Mode2,
		Mode3,
		Mode4,
		Mode5
	}

	public kMode m_Mode;

	public GameObject mLabel;

	protected gyUIPoolObject m_PoolObject;

	protected bool m_bInProcess;

	protected Vector2 m_v2Src;

	protected int m_nStep;

	protected float m_fStepCount;

	protected float m_fSpeedX;

	protected float m_fSpeedY;

	protected float m_fGrivity;

	public bool InProcess
	{
		get
		{
			return m_bInProcess;
		}
	}

	private void Awake()
	{
		m_PoolObject = GetComponent<gyUIPoolObject>();
		m_bInProcess = false;
		m_nStep = 0;
		m_fStepCount = 0f;
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (m_bInProcess && m_Mode != 0)
		{
			switch (m_Mode)
			{
			case kMode.Mode1:
				UpdateMode1(Time.deltaTime);
				break;
			case kMode.Mode2:
				UpdateMode2(Time.deltaTime);
				break;
			case kMode.Mode3:
				UpdateMode3(Time.deltaTime);
				break;
			case kMode.Mode4:
				UpdateMode4(Time.deltaTime);
				break;
			case kMode.Mode5:
				UpdateMode5(Time.deltaTime);
				break;
			}
		}
	}

	public void Go(Vector2 v2Pos, kMode mode = kMode.None)
	{
		if (mode != 0)
		{
			m_Mode = mode;
		}
		m_v2Src = v2Pos;
		m_bInProcess = true;
		base.gameObject.SetActiveRecursively(true);
		base.gameObject.transform.localScale = Vector3.one;
		switch (m_Mode)
		{
		case kMode.Mode1:
			InitMode1();
			break;
		case kMode.Mode2:
			InitMode2();
			break;
		case kMode.Mode3:
			InitMode3();
			break;
		case kMode.Mode4:
			InitMode4();
			break;
		case kMode.Mode5:
			InitMode5();
			break;
		}
	}

	public void SetColor(Color color)
	{
		if (!(mLabel == null))
		{
			UILabel component = mLabel.GetComponent<UILabel>();
			if (component != null)
			{
				component.color = color;
			}
		}
	}

	public void SetLabel(string str)
	{
		if (!(mLabel == null))
		{
			UILabel component = mLabel.GetComponent<UILabel>();
			if (component != null)
			{
				component.text = str;
			}
		}
	}

	protected void InitMode1()
	{
		m_nStep = 0;
		m_fStepCount = 1f;
		TweenScale tweenScale = TweenScale.Begin(base.gameObject, 1f, Vector3.zero);
		tweenScale.from = new Vector3(0.1f, 0.1f, 0.1f);
		tweenScale.to = new Vector3(2f, 2f, 2f);
		tweenScale.method = UITweener.Method.BounceIn;
		TweenPosition tweenPosition = TweenPosition.Begin(base.gameObject, 0.2f, Vector3.zero);
		tweenPosition.from = m_v2Src;
		float num = Random.Range(-80f, 80f);
		m_fSpeedX = num / 0.5f;
		tweenPosition.to = m_v2Src + new Vector2(num, Random.Range(50f, 70f));
		tweenPosition.method = UITweener.Method.EaseOut;
	}

	protected void UpdateMode1(float deltaTime)
	{
		switch (m_nStep)
		{
		case 0:
			m_fStepCount -= deltaTime;
			if (m_fStepCount <= 0f)
			{
				m_nStep = 1;
				m_fStepCount = 1f;
				TweenScale tweenScale = TweenScale.Begin(base.gameObject, 1f, Vector3.zero);
				tweenScale.method = UITweener.Method.Linear;
				m_fSpeedY = 0f;
				m_fGrivity = Random.Range(300f, 400f);
			}
			break;
		case 1:
			m_fStepCount -= deltaTime;
			if (m_fStepCount <= 0f)
			{
				m_nStep = 0;
				m_fStepCount = 0f;
				m_bInProcess = false;
				m_PoolObject.TakeBack(0f);
			}
			base.gameObject.transform.localPosition += new Vector3(m_fSpeedX, m_fSpeedY, 0f) * deltaTime;
			m_fSpeedY -= m_fGrivity * deltaTime;
			break;
		}
	}

	protected void InitMode2()
	{
		m_nStep = 0;
		m_fStepCount = 1f;
		TweenScale tweenScale = TweenScale.Begin(base.gameObject, 0.5f, Vector3.zero);
		tweenScale.from = Vector3.zero;
		tweenScale.to = Vector3.one;
		tweenScale.method = UITweener.Method.EaseOut;
		base.gameObject.transform.localPosition = m_v2Src;
		m_fSpeedX = Random.Range(-80f, 80f);
		m_fSpeedY = Random.Range(200f, 250f);
		m_fGrivity = Random.Range(300f, 400f);
	}

	protected void UpdateMode2(float deltaTime)
	{
		switch (m_nStep)
		{
		case 0:
			m_fStepCount -= deltaTime;
			if (m_fStepCount <= 0f)
			{
				m_nStep = 1;
				m_fStepCount = 0.5f;
				TweenScale tweenScale = TweenScale.Begin(base.gameObject, 0.5f, Vector3.zero);
				tweenScale.method = UITweener.Method.Linear;
			}
			base.gameObject.transform.localPosition += new Vector3(m_fSpeedX, m_fSpeedY, 0f) * deltaTime;
			m_fSpeedY -= m_fGrivity * deltaTime;
			break;
		case 1:
			m_fStepCount -= deltaTime;
			if (m_fStepCount <= 0f)
			{
				m_nStep = 0;
				m_fStepCount = 0f;
				m_bInProcess = false;
				m_PoolObject.TakeBack(0f);
			}
			base.gameObject.transform.localPosition += new Vector3(m_fSpeedX, m_fSpeedY, 0f) * deltaTime;
			m_fSpeedY -= m_fGrivity * deltaTime;
			break;
		}
	}

	protected void InitMode3()
	{
		m_nStep = 0;
		m_fStepCount = 1f;
		TweenScale tweenScale = TweenScale.Begin(base.gameObject, 0.5f, Vector3.zero);
		tweenScale.from = Vector3.zero;
		tweenScale.to = Vector3.one;
		tweenScale.method = UITweener.Method.EaseOut;
		base.gameObject.transform.localPosition = m_v2Src;
		m_fSpeedX = 0f;
		m_fSpeedY = Random.Range(200f, 250f);
		m_fGrivity = 0f;
	}

	protected void UpdateMode3(float deltaTime)
	{
		switch (m_nStep)
		{
		case 0:
			m_fStepCount -= deltaTime;
			if (m_fStepCount <= 0f)
			{
				m_nStep = 1;
				m_fStepCount = 0.5f;
				TweenScale tweenScale = TweenScale.Begin(base.gameObject, 0.5f, Vector3.zero);
				tweenScale.method = UITweener.Method.EaseOut;
			}
			base.gameObject.transform.localPosition += new Vector3(m_fSpeedX, m_fSpeedY, 0f) * deltaTime;
			m_fSpeedY -= m_fGrivity * deltaTime;
			break;
		case 1:
			m_fStepCount -= deltaTime;
			if (m_fStepCount <= 0f)
			{
				m_nStep = 0;
				m_fStepCount = 0f;
				m_bInProcess = false;
				m_PoolObject.TakeBack(0f);
			}
			base.gameObject.transform.localPosition += new Vector3(m_fSpeedX, m_fSpeedY, 0f) * deltaTime;
			break;
		}
	}

	protected void InitMode4()
	{
		m_nStep = 0;
		m_fStepCount = 0.8f;
		TweenPosition tweenPosition = TweenPosition.Begin(base.gameObject, m_fStepCount, Vector3.zero);
		tweenPosition.from = new Vector3(-Screen.width / 2, Screen.height / 4);
		tweenPosition.to = new Vector3(0f, Screen.height / 4);
		tweenPosition.method = UITweener.Method.BounceIn;
	}

	protected void UpdateMode4(float deltaTime)
	{
		switch (m_nStep)
		{
		case 0:
			m_fStepCount -= deltaTime;
			if (m_fStepCount <= 0f)
			{
				m_nStep = 1;
				m_fStepCount = 1f;
			}
			break;
		case 1:
			m_fStepCount -= deltaTime;
			if (m_fStepCount <= 0f)
			{
				m_nStep = 2;
				m_fStepCount = 0.8f;
				TweenPosition tweenPosition = TweenPosition.Begin(base.gameObject, m_fStepCount, Vector3.zero);
				tweenPosition.to = new Vector3((float)Screen.width * 0.6f, Screen.height / 4);
				tweenPosition.method = UITweener.Method.EaseOut;
			}
			break;
		case 2:
			m_fStepCount -= deltaTime;
			if (m_fStepCount <= 0f)
			{
				m_nStep = 0;
				m_fStepCount = 0f;
				m_bInProcess = false;
				m_PoolObject.TakeBack(0f);
			}
			break;
		}
	}

	protected void InitMode5()
	{
		m_nStep = 0;
		m_fStepCount = 0.5f;
		TweenPosition tweenPosition = TweenPosition.Begin(base.gameObject, m_fStepCount, Vector3.zero);
		tweenPosition.from = m_v2Src;
		tweenPosition.to = m_v2Src - new Vector2(50f, 0f);
		tweenPosition.method = UITweener.Method.EaseIn;
		TweenScale tweenScale = TweenScale.Begin(base.gameObject, m_fStepCount, Vector3.zero);
		tweenScale.from = Vector3.zero;
		tweenScale.to = new Vector3(1.5f, 1.5f, 1.5f);
		tweenScale.method = UITweener.Method.BounceIn;
	}

	protected void UpdateMode5(float deltaTime)
	{
		switch (m_nStep)
		{
		case 0:
			m_fStepCount -= Time.deltaTime;
			if (m_fStepCount <= 0f)
			{
				m_nStep = 1;
				m_fStepCount = 1f;
				TweenPosition tweenPosition = TweenPosition.Begin(base.gameObject, m_fStepCount, Vector3.zero);
				tweenPosition.to = new Vector3(-Screen.width / 2, Screen.height / 2, 0f);
				tweenPosition.method = UITweener.Method.EaseIn;
				TweenScale tweenScale = TweenScale.Begin(base.gameObject, 1f, Vector3.zero);
				tweenScale.to = Vector3.zero;
				tweenScale.method = UITweener.Method.EaseOut;
			}
			break;
		case 1:
			m_fStepCount -= Time.deltaTime;
			if (m_fStepCount <= 0f)
			{
				m_nStep = 0;
				m_fStepCount = 0f;
				m_bInProcess = false;
				m_PoolObject.TakeBack(0f);
			}
			break;
		}
	}
}
