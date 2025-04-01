using UnityEngine;

public class iItemDynamic : iItem
{
	public float fAbsorbDistance;

	public float fAbsorbSpeed = 20f;

	public GameObject m_GroundEffect;

	protected Rigidbody m_Rigidbody;

	protected bool m_bBump;

	protected float m_fBumpSpeed = 3f;

	protected float m_fBumpGravity = 10f;

	protected float m_fBumpDamping;

	protected float m_fBumpCurSpeed;

	protected float m_fBumpSrcHeight;

	protected float m_fFloorHeight;

	protected bool m_bAbsorb;

	private new void Awake()
	{
		base.Awake();
		m_Rigidbody = GetComponent<Rigidbody>();
		if (m_Collider != null)
		{
			m_Collider.isTrigger = false;
		}
		m_bBump = false;
		if (m_GroundEffect != null)
		{
			m_GroundEffect.SetActiveRecursively(false);
		}
		m_bAbsorb = false;
	}

	private void Start()
	{
	}

	private void Update()
	{
		float deltaTime = Time.deltaTime;
		if (m_bBump)
		{
			Vector3 position = m_Transform.position;
			position.y += m_fBumpCurSpeed * Time.deltaTime;
			m_fBumpCurSpeed -= m_fBumpGravity * Time.deltaTime;
			if (position.y <= m_fBumpSrcHeight)
			{
				position.y = m_fBumpSrcHeight;
				m_fBumpCurSpeed = m_fBumpSpeed * (1f - m_fBumpDamping);
				if (m_fBumpDamping < 0.2f)
				{
					m_fBumpDamping += 0.2f;
				}
			}
			m_Transform.position = position;
			if (fAbsorbDistance > 0f)
			{
				CCharUser user = m_GameScene.GetUser();
				if (user != null && Vector3.Distance(user.Pos, m_Transform.position) <= fAbsorbDistance)
				{
					if (m_GroundEffect != null)
					{
						Object.Destroy(m_GroundEffect);
						m_GroundEffect = null;
					}
					m_bAbsorb = true;
					m_bBump = false;
					if (m_Collider != null)
					{
						Object.Destroy(m_Collider);
						m_Collider = null;
					}
				}
			}
		}
		if (!m_bAbsorb)
		{
			return;
		}
		CCharUser user2 = m_GameScene.GetUser();
		if (user2 == null)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		Vector3 vector = user2.GetBone(2).position - m_Transform.position;
		float num = fAbsorbSpeed * deltaTime;
		if (num >= vector.magnitude)
		{
			if (ToughItem(user2))
			{
				Destroy();
			}
		}
		else
		{
			m_Transform.position += vector.normalized * num;
		}
	}

	private void FixedUpdate()
	{
		if (m_GameScene == null)
		{
			m_GameScene = iGameApp.GetInstance().m_GameScene;
		}
		if (!(m_Rigidbody == null) && m_GameScene != null && base.transform.position.y <= m_fFloorHeight + m_Entity.transform.localPosition.y && m_Rigidbody.velocity.y > -0.2f && m_Rigidbody.velocity.y < 0.2f)
		{
			m_Rigidbody.Sleep();
			Object.Destroy(m_Rigidbody);
			m_Rigidbody = null;
			m_Collider.isTrigger = true;
			if (m_GroundEffect != null)
			{
				m_GroundEffect.SetActiveRecursively(true);
				m_GroundEffect.transform.parent = null;
				m_GroundEffect.transform.position = new Vector3(m_GroundEffect.transform.position.x, m_fFloorHeight + 0.01f, m_GroundEffect.transform.position.z);
			}
			Vector3 position = base.transform.position;
			position.y = m_fFloorHeight;
			m_bBump = true;
			m_fBumpSrcHeight = position.y;
			m_fBumpCurSpeed = m_fBumpSpeed;
			m_fBumpDamping = 0f;
		}
	}

	public override void Initialize(int nItemID, bool bAbsorb = false)
	{
		base.Initialize(nItemID, bAbsorb);
		base.transform.eulerAngles = Vector3.zero;
		if (!bAbsorb)
		{
			Vector3 position = base.transform.position;
			position.y += 100f;
			RaycastHit hitInfo;
			if (Physics.Raycast(new Ray(position, Vector3.down), out hitInfo, 200f, 536870912))
			{
				m_fFloorHeight = hitInfo.point.y;
			}
			else
			{
				bAbsorb = true;
			}
		}
		if (bAbsorb)
		{
			m_Rigidbody.Sleep();
			Object.Destroy(m_Rigidbody);
			m_Rigidbody = null;
			m_Collider.isTrigger = true;
			m_bAbsorb = true;
		}
	}

	public override void Clear()
	{
		base.Clear();
		if (m_GroundEffect != null)
		{
			Object.Destroy(m_GroundEffect);
			m_GroundEffect = null;
		}
	}

	public override void AddForce(Vector3 v3Force)
	{
		if (!(m_Rigidbody == null))
		{
			m_Rigidbody.AddForce(v3Force);
		}
	}
}
