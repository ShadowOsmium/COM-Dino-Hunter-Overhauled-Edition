using UnityEngine;

public class gyUIScreenTip : MonoBehaviour
{
	public float m_fCloseDistance = 10f;

	public GameObject m_Actor;

	public GameObject m_Target;

	public GameObject m_Item;

	public GameObject m_Arrow;

	protected bool m_bActive;

	protected Transform mTransform;

	protected UISprite m_SpriteItem;

	protected UISprite m_SpriteArrow;

	protected gyUIAnimationBump m_AnimItem;

	protected gyUIAnimationHop m_AnimArrow;

	public bool isActive
	{
		get
		{
			return m_bActive;
		}
		set
		{
			m_bActive = value;
			base.gameObject.SetActiveRecursively(m_bActive);
		}
	}

	private void Awake()
	{
		m_bActive = false;
		mTransform = base.transform;
		m_fCloseDistance *= m_fCloseDistance;
		m_SpriteItem = m_Item.GetComponentInChildren<UISprite>();
		m_SpriteArrow = m_Arrow.GetComponentInChildren<UISprite>();
		m_AnimItem = m_Item.GetComponentInChildren<gyUIAnimationBump>();
		m_AnimArrow = m_Arrow.GetComponentInChildren<gyUIAnimationHop>();
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (m_bActive && !(m_Actor == null) && !(m_Target == null))
		{
			bool flag = false;
			Vector3 forward = m_Actor.transform.forward;
			forward.y = 0f;
			Vector3 vector = m_Target.transform.position - m_Actor.transform.position;
			Vector3 normalized = vector.normalized;
			normalized.y = 0f;
			float num = Vector3.Dot(forward, normalized);
			if (num > 0f)
			{
				flag = true;
			}
			num = Mathf.Acos(num) * 57.29578f;
			num *= (float)((Vector3.Cross(forward, normalized).y < 0f) ? 1 : (-1));
			Vector2 v2ScreenPos = Quaternion.Euler(new Vector3(0f, 0f, num)) * new Vector3(0f, 1f, 0f);
			if (vector.sqrMagnitude <= m_fCloseDistance && IsInScreen(m_Target.transform.position, ref v2ScreenPos) && flag)
			{
				SetPos_InScreen(v2ScreenPos);
			}
			else
			{
				SetPos_OutScreen(v2ScreenPos * 10000f);
			}
		}
	}

	public void Initialize(GameObject actor, GameObject target)
	{
		m_Actor = actor;
		m_Target = target;
		mTransform.localScale = Vector3.one;
		isActive = true;
	}

	public void Clear()
	{
		m_Actor = null;
		m_Target = null;
	}

	public void SetIcon(string sIcon)
	{
		if (!(m_SpriteItem == null) && m_SpriteItem != null)
		{
			m_SpriteItem.spriteName = sIcon;
		}
	}

	protected void SetPos_OutScreen(Vector2 v2ScreenPos)
	{
		if (!(v2ScreenPos == Vector2.zero))
		{
			float num = (float)Screen.width * 0.5f;
			float num2 = (float)Screen.height * 0.5f;
			Vector2 vector = new Vector2(Mathf.Abs(v2ScreenPos.x), Mathf.Abs(v2ScreenPos.y));
			float num3 = 0f;
			num3 = ((!(vector.y / vector.x > num2 / num)) ? (num / vector.x * v2ScreenPos.magnitude) : (num2 / vector.y * v2ScreenPos.magnitude));
			base.transform.localPosition = v2ScreenPos.normalized * num3;
			m_Arrow.transform.right = v2ScreenPos.normalized;
			m_Arrow.transform.localPosition = -m_Arrow.transform.right * m_Arrow.transform.localScale.x / 2f;
			m_Item.transform.localPosition = -m_Arrow.transform.right * (m_Arrow.transform.localScale.x + m_Item.transform.localScale.x / 2f);
			if (m_AnimArrow != null)
			{
				m_AnimArrow.Stop();
			}
			if (m_AnimItem != null)
			{
				m_AnimItem.Stop();
			}
		}
	}

	protected void SetPos_InScreen(Vector2 v2ScreenPos)
	{
		base.transform.localPosition = v2ScreenPos;
		m_Arrow.transform.right = -Vector2.up;
		m_Arrow.transform.localPosition = -m_Arrow.transform.right * m_Arrow.transform.localScale.x;
		m_Item.transform.localPosition = -m_Arrow.transform.right * (m_Arrow.transform.localScale.x + m_Item.transform.localScale.x);
		if (m_AnimArrow != null && !m_AnimArrow.isActive)
		{
			m_AnimArrow.Go(-m_Arrow.transform.up);
		}
		if (m_AnimItem != null && !m_AnimItem.isActive)
		{
			m_AnimItem.Go();
		}
	}

	protected bool IsInScreen(Vector3 v3WorldPos, ref Vector2 v2ScreenPos)
	{
		Vector3 vector = Camera.main.WorldToScreenPoint(v3WorldPos);
		if (vector.x >= 0f && vector.x <= (float)Screen.width && vector.y >= 0f && vector.y <= (float)Screen.height)
		{
			v2ScreenPos = new Vector2(vector.x - (float)(Screen.width / 2), vector.y - (float)(Screen.height / 2));
			return true;
		}
		return false;
	}
}
