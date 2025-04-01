using UnityEngine;

[RequireComponent(typeof(gyUIPoolObject))]
public class gyUIMaterials : MonoBehaviour
{
	public UISprite mIcon;

	public UILabel mLabel;

	protected gyUIPoolObject m_PoolObject;

	protected bool m_bInProcess;

	protected Vector2 m_v2Src;

	protected Vector2 m_v2Dst;

	protected int m_nStep;

	protected float m_fStepCount;

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
		if (!m_bInProcess)
		{
			return;
		}
		switch (m_nStep)
		{
		case 0:
			m_fStepCount -= Time.deltaTime;
			if (m_fStepCount <= 0f)
			{
				TweenPosition tweenPosition = TweenPosition.Begin(base.gameObject, 1f, Vector3.zero);
				tweenPosition.to = m_v2Dst;
				tweenPosition.method = UITweener.Method.EaseIn;
				TweenScale tweenScale = TweenScale.Begin(base.gameObject, 1f, Vector3.zero);
				tweenScale.to = Vector3.zero;
				tweenScale.method = UITweener.Method.EaseIn;
				m_nStep = 1;
				m_fStepCount = 1f;
			}
			break;
		case 1:
			m_fStepCount -= Time.deltaTime;
			if (m_fStepCount <= 0f)
			{
				m_nStep = 0;
				m_fStepCount = 0f;
				m_bInProcess = false;
				m_PoolObject.TakeBack(0.1f);
			}
			break;
		}
	}

	public void Go(Vector2 v2Pos, Vector2 v2Purpose)
	{
		m_v2Src = v2Pos;
		m_v2Dst = new Vector3(-Screen.width / 2, Screen.height / 2, 0f);
		m_bInProcess = true;
		base.gameObject.SetActiveRecursively(true);
		base.gameObject.transform.localPosition = m_v2Src;
		TweenScale tweenScale = TweenScale.Begin(base.gameObject, 0.5f, Vector3.zero);
		tweenScale.from = Vector3.zero;
		tweenScale.to = Vector3.one * 2f;
		tweenScale.method = UITweener.Method.BounceIn;
		m_nStep = 0;
		m_fStepCount = 0.5f;
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

	public void SetIcon(string sName)
	{
		GameObject gameObject = PrefabManager.Get("Artist/Atlas/Material/" + sName);
		if (gameObject != null)
		{
			mIcon.atlas = gameObject.GetComponent<UIAtlas>();
		}
	}

	public void SetValue(int nCount)
	{
		if (!(mLabel == null))
		{
			mLabel.text = "x " + nCount;
		}
	}
}
