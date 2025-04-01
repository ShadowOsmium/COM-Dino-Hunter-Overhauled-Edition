using BehaviorTree;
using UnityEngine;

public class iSkillSimulate : MonoBehaviour
{
	public int nMobID = 1;

	public int nMobLevel = 1;

	public int nSkillID = -1;

	public float timecontrl = 1f;

	protected iGameSceneBase m_GameScene;

	protected iGameData m_GameData;

	protected CCharMob m_Actor;

	protected CCharMob m_Target;

	private void Awake()
	{
	}

	private void Start()
	{
		Initialize();
	}

	private void Update()
	{
		Time.timeScale = timecontrl;
		Update(Time.deltaTime);
	}

	public void Initialize()
	{
		m_GameScene = iGameApp.GetInstance().m_GameScene;
		m_GameData = iGameApp.GetInstance().m_GameData;
	}

	public void Update(float deltaTime)
	{
		if (Input.GetKeyDown(KeyCode.F1))
		{
			if (m_Actor != null)
			{
				m_Actor.Destroy();
			}
			if (m_Target != null)
			{
				m_Target.Destroy();
			}
			m_Actor = AddMob(nMobID, nMobLevel, 9527, new Vector3(0f, 20000f, 0f), Vector3.forward);
			m_Target = AddMob(nMobID, nMobLevel, 9528, new Vector3(0f, 20000f, 20f), -Vector3.forward);
			CompositeNode_Selector compositeNode_Selector = new CompositeNode_Selector();
			CompositeNode_Sequence compositeNode_Sequence = new CompositeNode_Sequence();
			compositeNode_Sequence.AddChild(new lgHasTargetNode());
			compositeNode_Sequence.AddChild(new lgHasSkillNode());
			compositeNode_Sequence.AddChild(new doUseSkillNode());
			compositeNode_Selector.AddChild(compositeNode_Sequence);
			if (m_Actor.GetBehavior == null)
			{
				m_Actor.GetBehavior = new Behavior();
			}
			if (m_Actor.GetBehavior.HasInstalled())
			{
				m_Actor.GetBehavior.Uninstall();
			}
			m_Actor.GetBehavior.Install(compositeNode_Selector);
		}
		if (Input.GetKeyDown(KeyCode.F2))
		{
			UseSkill(nSkillID, m_Target);
		}
	}

	public CCharMob AddMob(int nMobID, int nMobLevel, int nUID, Vector3 v3Pos, Vector3 v3Dir)
	{
		Debug.Log("AddMob " + nMobID);
		CMobInfoLevel mobInfo = m_GameData.GetMobInfo(nMobID, nMobLevel);
		if (mobInfo == null)
		{
			return null;
		}
		GameObject gameObject = PrefabManager.Get(mobInfo.nModel);
		if (gameObject == null)
		{
			return null;
		}
		GameObject gameObject2 = (GameObject)Object.Instantiate(gameObject);
		if (gameObject2 == null)
		{
			return null;
		}
		CCharMob component = gameObject2.GetComponent<CCharMob>();
		if (component == null)
		{
			return null;
		}
		component.UID = nUID;
		component.gameObject.name = "mob_" + component.UID;
		component.InitMob(nMobID, nMobLevel);
		component.MobType = mobInfo.nType;
		component.MobBehavior = 1;
		component.name = "mob_" + component.UID;
		component.Pos = v3Pos;
		component.Dir2D = v3Dir;
		return component;
	}

	public void UseSkill(int nSkillID, CCharBase target)
	{
		if (!(m_Actor == null))
		{
			m_Actor.m_Target = target;
			m_Actor.m_pSkillComboInfo = m_GameData.GetSkillComboInfo(nSkillID);
		}
	}
}
