using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using gyAchievementSystem;
using gyEvent;
using gyTaskSystem;
using UnityEngine;

public class CCharMob : CCharBase
{
	public class kHUDLifeStyleEnum
	{
		public const int Normal = 0;

		public const int Black = 1;

		public const int Count = 2;
	}

	public class CHUDLifeStyle
	{
		public Color m_Color;

		public Color m_ColorShallow;
	}

	[SerializeField]
	public int MobBehavior;

	public int GenerateWaveID;

	public int GenerateSequence;

	protected CMobInfoLevel m_curMobInfoLevel;

	protected float m_fHardinessCur;

	protected float m_fHardinessMax;

	protected float m_fUpdateMoveTime;

	protected List<SkillComboUserInfo> m_ltSkillList;

	protected kMobBehaviour m_MobBehaviourMode;

	protected int m_nCurAIID;

	protected List<SkillComboUserInfo> m_ltSkillListAI;

	protected List<int> m_ltSkillPassiveAI;

	protected gyLifeBarHUD m_LifeBar;

	protected Dictionary<int, Vector3> m_dictAssistAim;

	public CSkillComboInfo m_pSkillComboInfo;

	public int m_nCurComboIndex;

	[NonSerialized]
	public bool m_bHasPurposePoint;

	[NonSerialized]
	public Vector3 m_v3PurposePoint;

	[NonSerialized]
	public List<Vector3> m_ltPath;

	[NonSerialized]
	public int m_nDstHoverIndex;

	[NonSerialized]
	public Vector3 m_v3DstHoverPoint;

	[NonSerialized]
	public float m_fHoverTime;

	public List<Vector3> m_ltPathHover;

	[NonSerialized]
	public kDeadMode m_DeadMode;

	public float m_fDeadDistance;

	public Vector3 m_v3DeadDirection;

	[NonSerialized]
	public Vector3 m_v3BirthPos;

	[NonSerialized]
	public bool m_bShowTime;

	[NonSerialized]
	public bool m_bFreeze;

	[NonSerialized]
	public float m_fFreezeTime;

	[NonSerialized]
	public bool m_bReadyToBlack;

	public float m_fReadyToBlackLife;

	public iBuilding m_TargetBuilding;

	public kAnimEnum m_NetAnim;

	protected CDropGroupInfo m_tmpDropGroupInfo;

	protected int m_nCarryGoldCur;

	protected int m_nCarryGoldMax;

	protected int m_nCarryCrystalCur;

	protected int m_nCarryCrystalMax;

	protected CHUDLifeStyle[] m_arrHUDLifeStyle;

	protected int m_nHUDLifeStyle;

	protected Dictionary<int, int> m_dictUseSkill;

	[SerializeField]
	public int MobType { get; set; }

	public kMobBehaviour MobBehaviourMode
	{
		get
		{
			return m_MobBehaviourMode;
		}
		set
		{
			m_MobBehaviourMode = value;
		}
	}

	public float Hardiness
	{
		get
		{
			return m_fHardinessCur;
		}
	}

	public float HardinessMax
	{
		get
		{
			return m_fHardinessMax;
		}
	}

	public new void Awake()
	{
		base.Awake();
		m_nType = kCharType.Mob;
		base.CampType = kCampType.Monster;
		m_Property = new CProMob();
		m_nCurAIID = -1;
		m_ltSkillListAI = new List<SkillComboUserInfo>();
		m_ltSkillList = new List<SkillComboUserInfo>();
		m_ltSkillPassive = new List<int>();
		m_ltSkillPassiveAI = new List<int>();
		m_MobBehaviourMode = kMobBehaviour.None;
		m_ltPath = new List<Vector3>();
		m_ltPathHover = new List<Vector3>();
		m_nDstHoverIndex = -1;
		m_bShowTime = true;
		m_DeadMode = kDeadMode.None;
		m_dictAssistAim = new Dictionary<int, Vector3>();
		m_tmpDropGroupInfo = new CDropGroupInfo();
		m_NetAnim = kAnimEnum.None;
		m_arrHUDLifeStyle = new CHUDLifeStyle[2];
		m_arrHUDLifeStyle[0] = new CHUDLifeStyle();
		m_arrHUDLifeStyle[0].m_Color = new Color(61f / 85f, 0f, 0f);
		m_arrHUDLifeStyle[0].m_ColorShallow = new Color(83f / 85f, 28f / 51f, 28f / 51f);
		m_arrHUDLifeStyle[1] = new CHUDLifeStyle();
		m_arrHUDLifeStyle[1].m_Color = new Color(0.41960785f, 0f, 0.81960785f);
		m_arrHUDLifeStyle[1].m_ColorShallow = new Color(2f / 3f, 28f / 85f, 84f / 85f);
		m_dictUseSkill = new Dictionary<int, int>();
	}

	public new void Start()
	{
		base.Start();
	}

	public new void Update()
	{
		if (!m_bActive)
		{
			return;
		}
		base.Update();
		float num = Time.deltaTime * m_fTimeScale;
		if (m_Behavior != null)
		{
			m_Behavior.Update(this, num);
		}
		if (base.m_GameState.isNetGame)
		{
			m_fUpdateMoveTime -= num;
			if (m_fUpdateMoveTime <= 0f)
			{
				m_fUpdateMoveTime = 0.1f;
			}
		}
		foreach (SkillComboUserInfo ltSkill in m_ltSkillList)
		{
			ltSkill.Update(num);
		}
		foreach (SkillComboUserInfo item in m_ltSkillListAI)
		{
			item.Update(num);
		}
	}

	public new void LateUpdate()
	{
		if (m_bActive)
		{
			base.LateUpdate();
		}
	}

	public override void Destroy()
	{
		if (m_LifeBar != null && base.m_GameScene != null)
		{
			iGameUIBase gameUI = base.m_GameScene.GetGameUI();
			if (gameUI != null)
			{
				gameUI.RemoveLifeBar(base.UID);
			}
			m_LifeBar = null;
		}
		base.Destroy();
	}

	public CMobInfoLevel GetMobInfo()
	{
		return m_curMobInfoLevel;
	}

	public override void InitAnimData()
	{
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Idle, "idle"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.MoveForward, "fly"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Attack, "attack01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Dead, "death"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Hurt, "damage"));
	}

	public virtual bool IsMobCanThink()
	{
		return !base.isDead;
	}

	public virtual bool AddHardiness(float fDamage, string sBoneName = "")
	{
		m_fHardinessCur += fDamage;
		if (m_fHardinessCur > 0f)
		{
			return false;
		}
		m_fHardinessCur = m_fHardinessMax;
		m_HurtAnim = kAnimEnum.Mob_Hurt;
		return true;
	}

	public override void AddHP(float fHP)
	{
		m_fHP += fHP;
		if (m_fHP > m_fHPMax)
		{
			m_fHP = m_fHPMax;
		}
		else if (m_fHP <= 0f)
		{
			m_fHP = 0f;
		}
		if (m_LifeBar != null)
		{
			m_LifeBar.SetLife(m_fHP / m_fHPMax);
		}
	}

	public override void OnDead(kDeadMode nDeathMode)
	{
		float value = m_Property.GetValue(kProEnum.Skill_MoribundTime);
		if (value > 0f)
		{
			if (!m_bMoribund)
			{
				SetMoribund(true, m_fHPMax / 2f, value);
				return;
			}
			nDeathMode = kDeadMode.MoribundDead;
		}
		base.isDead = true;
		m_DeadMode = nDeathMode;
		ResetAI();
		base.m_GameScene.m_fCombatRatingData_DamageTotal += base.MaxHP;
		if (m_curMobInfoLevel != null && m_tmpDropGroupInfo != null)
		{
			int dropItemCount = m_curMobInfoLevel.GetDropItemCount();
			if (dropItemCount > 0)
			{
				for (int i = 0; i < dropItemCount; i++)
				{
					int dropItem = m_tmpDropGroupInfo.GetDropItem();
					if (dropItem > 0)
					{
						Vector3 onUnitSphere = UnityEngine.Random.onUnitSphere;
						onUnitSphere.y = 1f;
						base.m_GameScene.AddItem(dropItem, GetBone(0).position, onUnitSphere * UnityEngine.Random.Range(300f, 500f), -1f);
					}
				}
			}
			if (m_nCarryGoldMax < 1 && m_nCarryCrystalMax < 1 && UnityEngine.Random.Range(0, 101) <= m_curMobInfoLevel.nGoldRate)
			{
				GameObject poolObject = PrefabManager.GetPoolObject(302, 0f);
				if (poolObject != null)
				{
					iGoldEmitter component = poolObject.GetComponent<iGoldEmitter>();
					if (component != null)
					{
						int num = 0;
						num = ((!base.m_GameScene.m_bMutiplyGame) ? m_curMobInfoLevel.nGold : MyUtils.formula_monstergold(m_curMobInfoLevel.nGold, base.Level));
						component.Initialize(num);
						component.transform.position = GetBone(0).position;
					}
				}
			}
		}
		if (m_nCarryGoldMax > 0 && m_nCarryGoldCur > 0)
		{
			GameObject poolObject2 = PrefabManager.GetPoolObject(302, 0f);
			if (poolObject2 != null)
			{
				iGoldEmitter component2 = poolObject2.GetComponent<iGoldEmitter>();
				if (component2 != null)
				{
					component2.Initialize(m_nCarryGoldCur);
					component2.transform.parent = GetBone(0);
					component2.transform.localPosition = Vector3.zero;
				}
			}
		}
		if (m_nCarryCrystalMax > 0 && m_nCarryCrystalCur > 0)
		{
			GameObject poolObject3 = PrefabManager.GetPoolObject(302, 0f);
			if (poolObject3 != null)
			{
				iGoldEmitter component3 = poolObject3.GetComponent<iGoldEmitter>();
				if (component3 != null)
				{
					component3.Initialize(m_nCarryCrystalCur, true);
					component3.transform.parent = GetBone(0);
					component3.transform.localPosition = Vector3.zero;
				}
			}
		}
		base.m_GameScene.AddMonsterNumLimit(base.ID, MobType, MobBehavior, -1);
		base.m_GameScene.AddWaveMobNumber(GenerateWaveID, -1);
		if (base.m_GameScene.m_MGManager != null)
		{
			CEventManager eventManager = base.m_GameScene.m_MGManager.GetEventManager();
			if (eventManager != null)
			{
				eventManager.Trigger(new EventCondition_MobByWave(GenerateWaveID, GenerateSequence, 1));
				eventManager.Trigger(new EventCondition_MobByID(base.ID, 1));
				int generateWaveID = GenerateWaveID;
				if (!base.m_GameScene.m_MGManager.IsWaveProcess(generateWaveID))
				{
					int waveMobNumber = base.m_GameScene.GetWaveMobNumber(GenerateWaveID);
					eventManager.Trigger(new EventCondition_WaveNumberLeft(generateWaveID, waveMobNumber));
					if (base.m_GameScene.m_TaskManager != null)
					{
						CTaskBase task = base.m_GameScene.m_TaskManager.GetTask();
						if (task != null)
						{
							CTaskInfo taskInfo = task.GetTaskInfo();
							if (taskInfo != null && taskInfo.nType == 3 && waveMobNumber == 0)
							{
								CAchievementManager.GetInstance().AddAchievement(8);
							}
						}
					}
				}
			}
		}
		if (base.m_GameScene.m_TaskManager != null)
		{
			base.m_GameScene.m_TaskManager.OnKillMonster(base.ID);
			if (IsBoss())
			{
				base.m_GameState.LastKillBoss = base.UID;
			}
			if (base.m_GameScene.m_MGManager != null && base.m_GameScene.m_MGManager.IsWaveCompleted() && base.m_GameScene.GetMobAliveCount() == 0)
			{
				base.m_GameScene.m_TaskManager.OnKillAllMonsters();
			}
		}
		CAchievementManager.GetInstance().AddAchievement(5);
		if (m_curMobInfoLevel != null)
		{
			CAchievementManager.GetInstance().AddAchievement(6, new object[2] { m_curMobInfoLevel.nRareType, m_curMobInfoLevel.nType });
		}
		if (base.m_GameScene.IsWorldMonster(base.ID))
		{
			iDataCenter dataCenter = base.m_GameData.GetDataCenter();
			if (dataCenter != null)
			{
				dataCenter.AddWorldMonsterKill(base.ID, 1);
			}
		}
	}

	public int GetSkillNum()
	{
		if (m_ltSkillList == null)
		{
			return 0;
		}
		return m_ltSkillList.Count;
	}

	public SkillComboUserInfo GetSkill(int nIndex)
	{
		if (nIndex < 0 || nIndex >= m_ltSkillList.Count)
		{
			return null;
		}
		return m_ltSkillList[nIndex];
	}

	public IEnumerable GetSkillEnumerator()
	{
		foreach (SkillComboUserInfo ltSkill in m_ltSkillList)
		{
			yield return ltSkill;
		}
	}

	public int GetAISkillNum()
	{
		if (m_ltSkillListAI == null)
		{
			return 0;
		}
		return m_ltSkillListAI.Count;
	}

	public SkillComboUserInfo GetAISkill(int nIndex)
	{
		if (nIndex < 0 || nIndex >= m_ltSkillList.Count)
		{
			return null;
		}
		return m_ltSkillListAI[nIndex];
	}

	public IEnumerable GetAISkillEnumerator()
	{
		foreach (SkillComboUserInfo item in m_ltSkillListAI)
		{
			yield return item;
		}
	}

	public override bool GetSkillPassiveList(ref List<int> ltSkillPassive)
	{
		bool result = base.GetSkillPassiveList(ref ltSkillPassive);
		if (m_ltSkillPassiveAI != null)
		{
			result = true;
			foreach (int item in m_ltSkillPassiveAI)
			{
				ltSkillPassive.Add(item);
			}
		}
		return result;
	}

	public void SetBehaviourMode(kMobBehaviour mode)
	{
		MobBehaviourMode = mode;
	}

	public override bool OnHit(float fDmg, CWeaponInfoLevel pWeaponLvlInfo = null, string sBodyPart = "")
	{
		if (!base.OnHit(fDmg, pWeaponLvlInfo, sBodyPart))
		{
			return false;
		}
		if (m_fHP <= 0f)
		{
			if (CGameNetManager.GetInstance().IsRoomMaster())
			{
				kDeadMode kDeadMode2 = kDeadMode.Normal;
				if (pWeaponLvlInfo != null && !IsBoss())
				{
					switch (pWeaponLvlInfo.nAttackMode)
					{
					case 1:
					case 3:
					case 4:
					case 6:
						kDeadMode2 = kDeadMode.HitFly;
						m_fDeadDistance = 10f;
						break;
					}
				}
				if (kDeadMode2 == kDeadMode.None && MobType == 1 && !(m_curTask is doUseSkillTask) && !(m_curTask is doHurtTask))
				{
					kDeadMode2 = kDeadMode.FlyDead;
				}
				OnDead(kDeadMode2);
				if (CGameNetManager.GetInstance().IsConnected())
				{
					CGameNetSender.GetInstance().SendMsg_MOB_DEAD(base.UID);
				}
			}
		}
		else
		{
			if (CGameNetManager.GetInstance().IsRoomMaster() && IsCanBeatHardniess() && AddHardiness(fDmg, sBodyPart))
			{
				m_bHurting = true;
				ResetAI();
				if (!CGameNetManager.GetInstance().IsConnected())
				{
				}
			}
			if (m_nCarryGoldMax > 0 && m_nCarryGoldCur > 0)
			{
				int num = Mathf.FloorToInt((float)m_nCarryGoldMax * 0.5f * fDmg / m_fHPMax);
				if (num < 1)
				{
					num = 1;
				}
				m_nCarryGoldCur -= num;
				GameObject poolObject = PrefabManager.GetPoolObject(302, 0f);
				if (poolObject != null)
				{
					iGoldEmitter component = poolObject.GetComponent<iGoldEmitter>();
					if (component != null)
					{
						component.Initialize(num);
						component.transform.parent = GetBone(0);
						component.transform.localPosition = Vector3.zero;
					}
				}
			}
		}
		return true;
	}

	public virtual void InitMob(int nMobID, int nMobLevel)
	{
		base.ID = nMobID;
		base.Level = nMobLevel;
		m_curMobInfoLevel = base.m_GameData.GetMobInfo(nMobID, nMobLevel);
		if (m_curMobInfoLevel == null)
		{
			return;
		}
		InitAnimData();
		InitAudioData();
		m_ltSkillList.Clear();
		if (m_curMobInfoLevel.ltSkill != null)
		{
			for (int i = 0; i < m_curMobInfoLevel.ltSkill.Count; i++)
			{
				int nID = m_curMobInfoLevel.ltSkill[i].m_nID;
				CSkillComboInfo skillComboInfo = base.m_GameData.GetSkillComboInfo(nID);
				if (skillComboInfo != null)
				{
					SkillComboUserInfo item = new SkillComboUserInfo(nID, m_curMobInfoLevel.ltSkill[i].m_fRate, skillComboInfo.fCoolDown);
					m_ltSkillList.Add(item);
				}
			}
		}
		m_ltSkillPassive.Clear();
		if (m_curMobInfoLevel.ltSkillPassive != null)
		{
			for (int j = 0; j < m_curMobInfoLevel.ltSkillPassive.Count; j++)
			{
				int num = m_curMobInfoLevel.ltSkillPassive[j];
				CSkillInfoLevel skillInfo = base.m_GameData.GetSkillInfo(num, 1);
				if (skillInfo != null && skillInfo.nType == 1)
				{
					m_ltSkillPassive.Add(num);
				}
			}
		}
		if (m_tmpDropGroupInfo == null)
		{
			m_tmpDropGroupInfo = new CDropGroupInfo();
		}
		if (m_tmpDropGroupInfo != null)
		{
			m_tmpDropGroupInfo.Clear();
			if (m_curMobInfoLevel != null)
			{
				CDropGroupInfo dropGrouInfo = base.m_GameData.GetDropGrouInfo(m_curMobInfoLevel.nDropGroup);
				if (dropGrouInfo != null && dropGrouInfo.ltItem != null)
				{
					for (int k = 0; k < dropGrouInfo.ltItem.Count; k++)
					{
						m_tmpDropGroupInfo.Add(new CDropItem(dropGrouInfo.ltItem[k].nItemID, dropGrouInfo.ltItem[k].fRate));
					}
				}
			}
		}
		m_Property.Initialize(base.ID, base.Level, base.m_GameScene.m_bMutiplyGame);
		m_Property.UpdateSkill(this);
		m_fHPMax = m_Property.GetValue(kProEnum.HPMax);
		m_fHP = m_fHPMax;
		CCharUser user = base.m_GameScene.GetUser();
		m_nCarryGoldMax = (int)m_Property.GetValue(kProEnum.Mob_Gold_Carry);
		if (m_nCarryGoldMax == 1 && user != null)
		{
			m_nCarryGoldMax = MyUtils.formula_goldendragon(user.Level);
		}
		m_nCarryGoldCur = m_nCarryGoldMax;
		m_nCarryCrystalMax = (int)m_Property.GetValue(kProEnum.Mob_Crystal_Carry);
		if (m_nCarryCrystalMax == 1 && user != null)
		{
			m_nCarryCrystalMax = MyUtils.formula_crystaldragon(user.Level);
		}
		m_nCarryCrystalCur = m_nCarryCrystalMax;
		InitHardiness(base.ID, base.Level);
		iGameUIBase gameUI = base.m_GameScene.GetGameUI();
		if (gameUI != null)
		{
			m_LifeBar = gameUI.CreateLifeBar(this);
			SetLifeBarStyle(0, 1f);
		}
		InitAssistAimInfo();
	}

	public virtual void InitHardiness(int nMobID, int nMobLevel)
	{
		CMobInfoLevel mobInfo = base.m_GameData.GetMobInfo(nMobID, nMobLevel);
		if (mobInfo != null)
		{
			m_fHardinessMax = mobInfo.fHardiness;
			m_fHardinessCur = m_fHardinessMax;
		}
	}

	public virtual void InitAI(int nAIManager)
	{
		CAIManagerInfo aIManagerInfo = base.m_GameData.GetAIManagerInfo(nAIManager);
		if (aIManagerInfo != null)
		{
			SetAI(aIManagerInfo.nAI);
		}
	}

	public void SetAI(int nAI)
	{
		CAIInfo aIInfo = base.m_GameData.GetAIInfo(nAI);
		if (aIInfo == null)
		{
			return;
		}
		int nCurAIID = m_nCurAIID;
		m_nCurAIID = nAI;
		m_ltSkillListAI.Clear();
		foreach (SkillComboRateInfo item2 in aIInfo.ltSkill)
		{
			CSkillComboInfo skillComboInfo = base.m_GameData.GetSkillComboInfo(item2.m_nID);
			if (skillComboInfo != null)
			{
				SkillComboUserInfo item = new SkillComboUserInfo(item2.m_nID, item2.m_fRate, skillComboInfo.fCoolDown);
				m_ltSkillListAI.Add(item);
			}
		}
		m_ltSkillPassiveAI.Clear();
		foreach (int item3 in aIInfo.ltSkillPassive)
		{
			m_ltSkillPassiveAI.Add(item3);
		}
		m_Property.UpdateSkill(this);
		if (CGameNetManager.GetInstance().IsRoomMaster())
		{
			SetBehavior(aIInfo.nBehavior);
		}
		else
		{
			SetBehavior(aIInfo.nBehavior + 100);
		}
		OnEnterAI(nCurAIID, nAI);
		if (aIInfo.nBehavior == 2)
		{
			InitAnimData_Sky();
			if (m_ModelEntity != null && !base.m_GameScene.IsSkyScene())
			{
				iTexture2D component = m_ModelEntity.GetComponent<iTexture2D>();
				if (component != null)
				{
					component.Show(false);
					component.enabled = false;
				}
			}
			return;
		}
		InitAnimData_Ground();
		if (m_ModelEntity != null && !base.m_GameScene.IsSkyScene())
		{
			iTexture2D component2 = m_ModelEntity.GetComponent<iTexture2D>();
			if (component2 != null)
			{
				component2.Show(true);
				component2.enabled = true;
			}
		}
	}

	public void SetBehavior(int nBehaviorID)
	{
		Node behavior = base.m_GameData.GetBehavior(nBehaviorID);
		if (behavior != null)
		{
			if (m_Behavior == null)
			{
				m_Behavior = new Behavior();
			}
			else if (m_Behavior.HasInstalled())
			{
				m_Behavior.Uninstall();
			}
			m_Behavior.Install(behavior);
		}
	}

	protected virtual void OnEnterAI(int nLastAI, int nAI)
	{
	}

	public void SetLifeBarParam(float fHoldTime, float fFadeTime = 0.5f)
	{
		if (!(m_LifeBar == null))
		{
			m_LifeBar.SetTime(fHoldTime, fFadeTime);
		}
	}

	public void SetLifeBarStyle(int nStyle, float fRate)
	{
		if (!(m_LifeBar == null) && nStyle >= 0 && nStyle < m_arrHUDLifeStyle.Length && m_nHUDLifeStyle != nStyle)
		{
			m_nHUDLifeStyle = nStyle;
			m_LifeBar.SetColor(m_arrHUDLifeStyle[nStyle].m_Color);
			m_LifeBar.SetColorShallow(m_arrHUDLifeStyle[nStyle].m_ColorShallow);
			m_LifeBar.InitLife(fRate);
		}
	}

	public bool IsCanBeatHardniess()
	{
		if (m_bHurting || m_bMoribund)
		{
			return false;
		}
		return true;
	}

	public void SetFreeze(bool bFreeze, float fTime = 0f)
	{
		m_bFreeze = bFreeze;
		m_fFreezeTime = fTime;
		ResetAI();
	}

	public virtual void InitAssistAimInfo()
	{
		m_dictAssistAim.Add(1, Vector2.zero);
		m_dictAssistAim.Add(2, Vector2.zero);
	}

	public bool CheckAssistAimInfo(ref CAssistAimInfo assistaiminfo)
	{
		if (base.isDead)
		{
			return false;
		}
		assistaiminfo.m_Target = this;
		foreach (int key in m_dictAssistAim.Keys)
		{
			assistaiminfo.m_ltBone.Add(GetBone(key));
		}
		return true;
	}

	public override float CalcProtect(CWeaponInfoLevel weaponinfolevel = null)
	{
		float num = m_Property.GetValue(kProEnum.Protect);
		if (num < 0f)
		{
			num = 0f;
		}
		num = MyUtils.formula_armor2protect(num);
		if (m_bMoribund)
		{
			float value = m_Property.GetValue(kProEnum.Skill_MoribundDefence);
			if (value > 0f)
			{
				return num + value;
			}
		}
		return num;
	}

	public virtual void InitAnimData_Ground()
	{
	}

	public virtual void InitAnimData_Sky()
	{
	}

	public void AddUseSkillRecord(int nSkillID)
	{
		if (!m_dictUseSkill.ContainsKey(nSkillID))
		{
			m_dictUseSkill.Add(nSkillID, 1);
			return;
		}
		Dictionary<int, int> dictUseSkill;
		Dictionary<int, int> dictionary = (dictUseSkill = m_dictUseSkill);
		int key;
		int key2 = (key = nSkillID);
		key = dictUseSkill[key];
		dictionary[key2] = key + 1;
	}

	public int GetUserSkillCount(int nSkillID)
	{
		if (!m_dictUseSkill.ContainsKey(nSkillID))
		{
			return 0;
		}
		return m_dictUseSkill[nSkillID];
	}

	public void MoveTo(Vector3 v3Dst)
	{
		Vector3 sourcePosition = base.Pos;
		if (m_ltPath.Count > 0)
		{
			sourcePosition = m_ltPath[m_ltPath.Count - 1];
		}
		UnityEngine.AI.NavMeshPath navMeshPath = new UnityEngine.AI.NavMeshPath();
		if (!UnityEngine.AI.NavMesh.CalculatePath(sourcePosition, v3Dst, -1, navMeshPath))
		{
			m_ltPath.Add(v3Dst);
			return;
		}
		m_ltPath.Clear();
		for (int i = 0; i < navMeshPath.corners.Length; i++)
		{
			m_ltPath.Add(navMeshPath.corners[i]);
		}
	}
}
