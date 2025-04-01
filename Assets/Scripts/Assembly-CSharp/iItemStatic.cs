using UnityEngine;

public class iItemStatic : MonoBehaviour
{
	protected iGameSceneBase m_GameScene;

	protected iGameData m_GameData;

	protected Rigidbody m_Rigidbody;

	protected Collider m_Collider;

	protected int m_nItemID;

	protected CItemInfoLevel m_curItemInfoLevel;

	protected GameObject m_Entity;

	public int ID
	{
		get
		{
			return m_nItemID;
		}
		set
		{
			m_nItemID = value;
		}
	}

	private void Awake()
	{
		m_Rigidbody = GetComponent<Rigidbody>();
		m_Collider = base.gameObject.GetComponent<Collider>();
		if (m_Collider != null)
		{
			m_Collider.isTrigger = false;
		}
		Transform transform = base.transform.Find("Entity");
		if (transform != null)
		{
			m_Entity = transform.gameObject;
		}
	}

	private void Start()
	{
		if (m_Rigidbody != null)
		{
			Vector3 onUnitSphere = Random.onUnitSphere;
			onUnitSphere.y = 1f;
			m_Rigidbody.AddForce(onUnitSphere * 500f);
		}
	}

	private void Update()
	{
	}

	private void FixedUpdate()
	{
		if (!(m_Rigidbody == null) && base.transform.position.y <= m_Entity.transform.localPosition.y && m_Rigidbody.velocity.y > -0.2f && m_Rigidbody.velocity.y < 0.2f)
		{
			m_Rigidbody.drag = 10f;
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		Debug.Log(collider.transform.root.name);
		CCharUser component = collider.transform.root.GetComponent<CCharUser>();
		if (component == null)
		{
			return;
		}
		switch (m_curItemInfoLevel.nType)
		{
		case 4:
			if (!component.IsTakenItem())
			{
				component.TakeItem(m_nItemID, base.gameObject);
			}
			break;
		case 2:
		{
			iGameLogic gameLogic = m_GameScene.GetGameLogic();
			if (gameLogic != null)
			{
				gameLogic.Item(m_curItemInfoLevel, component, component);
			}
			break;
		}
		}
		Object.Destroy(base.gameObject);
	}

	public void Initialize(int nItemID)
	{
		m_GameScene = iGameApp.GetInstance().m_GameScene;
		m_GameData = iGameApp.GetInstance().m_GameData;
		m_nItemID = nItemID;
		m_curItemInfoLevel = m_GameData.GetItemInfo(m_nItemID, 1);
	}
}
