using UnityEngine;

public class iItem : MonoBehaviour
{
	public string sAudio = string.Empty;

	public bool isHasScreenTip;

	public gyUIScreenTip m_ScreenTip;

	protected iGameSceneBase m_GameScene;

	protected iGameData m_GameData;

	protected Collider m_Collider;

	protected int m_nItemUID;

	protected int m_nItemID;

	protected GameObject m_Entity;

	protected CItemInfoLevel m_curItemInfoLevel;

	protected int[] m_arrFunc;

	protected int[] m_arrValueX;

	protected int[] m_arrValueY;

	protected Transform m_Transform;

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

	public int UID
	{
		get
		{
			return m_nItemUID;
		}
		set
		{
			m_nItemUID = value;
		}
	}

	public void Awake()
	{
		m_Transform = base.transform;
		Transform transform = base.transform.Find("Entity");
		if (transform != null)
		{
			m_Entity = transform.gameObject;
		}
		m_Collider = GetComponent<Collider>();
		if (m_Collider != null)
		{
			m_Collider.isTrigger = true;
		}
		transform = base.transform.Find("item_03_pfb");
		if (transform != null)
		{
			Object.Destroy(transform.gameObject, 2f);
		}
		m_arrFunc = new int[3];
		m_arrValueX = new int[3];
		m_arrValueY = new int[3];
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void FixedUpdate()
	{
	}

	private void OnTriggerEnter(Collider collider)
	{
		CCharUser component = collider.transform.root.GetComponent<CCharUser>();
		if (!(component == null) && ToughItem(component))
		{
			Destroy();
		}
	}

	public virtual void Initialize(int nItemID, bool bAbsorb = false)
	{
		m_GameScene = iGameApp.GetInstance().m_GameScene;
		m_GameData = iGameApp.GetInstance().m_GameData;
		m_nItemID = nItemID;
		m_curItemInfoLevel = m_GameData.GetItemInfo(m_nItemID, 1);
		if (m_curItemInfoLevel != null)
		{
			for (int i = 0; i < 3; i++)
			{
				m_arrFunc[i] = m_curItemInfoLevel.arrFunc[i];
				m_arrValueX[i] = m_curItemInfoLevel.arrValueX[i];
				m_arrValueY[i] = m_curItemInfoLevel.arrValueY[i];
			}
		}
	}

	public void UpdateFunc(int index, int fun, int valuex, int valuey)
	{
		if (index >= 0 && index < 3)
		{
			m_arrFunc[index] = fun;
			m_arrValueX[index] = valuex;
			m_arrValueY[index] = valuey;
		}
	}

	public void Destroy()
	{
		Clear();
		Object.Destroy(base.gameObject);
	}

	public virtual void Clear()
	{
		if (m_ScreenTip != null)
		{
			iGameUIBase gameUI = m_GameScene.GetGameUI();
			if (gameUI != null)
			{
				gameUI.RemoveScreenTip(m_ScreenTip);
			}
			else if (m_ScreenTip.gameObject != null)
			{
				Object.Destroy(m_ScreenTip.gameObject);
			}
			m_ScreenTip = null;
		}
	}

	public virtual void AddForce(Vector3 v3Force)
	{
	}

	public bool ToughItem(CCharUser user)
	{
		if (user == null)
		{
			return false;
		}
		switch (m_curItemInfoLevel.nType)
		{
		case 4:
			if (!user.IsTakenItem())
			{
				user.TakeItem(m_nItemID, base.gameObject);
				user.PlayAudio(sAudio);
				Clear();
			}
			return false;
		case 2:
		case 3:
		{
			user.PlayAudio(sAudio);
			iGameLogic gameLogic = m_GameScene.GetGameLogic();
			if (gameLogic != null)
			{
				iGameLogic.HitInfo hitinfo = new iGameLogic.HitInfo();
				gameLogic.CaculateFunc(user, user, m_arrFunc, m_arrValueX, m_arrValueY, ref hitinfo);
			}
			GameObject gameObject = m_GameScene.AddEffect(Vector3.zero, Vector3.forward, 2f, 1301);
			if (gameObject != null)
			{
				Transform bone = user.GetBone(3);
				if (bone != null)
				{
					gameObject.transform.parent = bone;
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localRotation = Quaternion.identity;
				}
			}
			break;
		}
		}
		return true;
	}
}
