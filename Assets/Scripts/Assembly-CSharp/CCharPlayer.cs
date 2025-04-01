using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class CCharPlayer : CCharBase
{
	protected Avatar m_Avatar;

	public kCharMoveMode m_CharMoveMode;

	[SerializeField]
	protected kCharMoveDir m_curMoveDir;

	[SerializeField]
	protected kAnimEnum m_curAnim;

	[SerializeField]
	protected kAnimEnum m_curAnimUpbody;

	[SerializeField]
	protected bool m_bRoomMaster;

	protected kAnimEnum m_curAnimMix;

	protected float m_fCurAnimMixCount;

	protected Transform m_BoneBack;

	protected Transform m_HandBone;

	protected Transform m_SpineUpBody;

	protected Transform m_SpineDownBody;

	protected Quaternion m_SpineDownBodyLocalRotation;

	protected float m_fUpBodyYaw;

	protected float m_fUpBodyPitch;

	protected float m_fDownBodyYaw;

	protected Transform m_ManualSpine;

	protected Quaternion m_ManualSpineLocalRotation;

	protected bool m_bBodyYaw;

	protected float m_fDownBodyYawSrc;

	protected float m_fDownBodyYawDst;

	protected float m_fDownBodyYawRate;

	protected float m_fDownBodyYawSpeed;

	protected CWeaponBase m_curWeapon;

	protected int m_nCurWeaponID;

	protected int m_nCurWeaponLevel;

	protected CWeaponInfoLevel m_curWeaponLvlInfo;

	protected GameObject m_ShootMouse;

	protected GameObject m_WeaponBack;

	protected GameObject m_Weapon;

	protected Vector3 m_v3ShootDir;

	protected Vector3 m_v3MoveDir;

	protected bool m_bNeedControlBody;

	protected GameObject m_BackPack;

	protected CAnimPlay m_BackPackAnimManager;

	protected CAnimData m_BackPackAnimData;

	protected GameObject m_BackPackTrailGas;

	protected Transform m_BackPackTrailNode;

	protected int m_nSkill;

	protected int m_nSkillLevel;

	protected CUseSkill m_UseSkill;

	protected int[] m_arrCarryPassiveSkill;

	protected int[] m_arrCarryPassiveSkillLevel;

	protected int m_nEquipStone;

	protected int m_nEquipStoneLevel;

	protected int m_nCarryItemID;

	protected GameObject m_CarryItemObj;

	protected GameObject m_CarryBagObj;

	protected int m_nCarryItemBuffSlot;

	protected CCharacterInfo m_curCharacterInfo;

	protected CCharacterInfoLevel m_curCharacterInfoLevel;

	protected new int m_nLevel;

	protected int m_nExp;

	protected float m_fRecoverLifeTime = 20f;

	protected float m_fRecoverLifeTimeCount;

	protected float m_fRecoverBulletTime = 20f;

	protected float m_fRecoverBulletTimeCount;

	protected iCharacterModel m_CharacterModelInterface;

	[SerializeField]
	protected int m_nAvatarHead;

	[SerializeField]
	protected int m_nAvatarUpper;

	[SerializeField]
	protected int m_nAvatarLower;

	[SerializeField]
	protected int m_nAvatarHeadup;

	[SerializeField]
	protected int m_nAvatarNeck;

	[SerializeField]
	protected int m_nAvatarWrist;

	[SerializeField]
	protected int m_nAvatarBadge;

	[SerializeField]
	protected int m_nAvatarStone;

	public bool m_bFinalPath;

	public List<Vector3> m_ltNetPath;

	public bool m_bNetAimFresh;

	public Vector3 m_v3NetAimPoint;

	public Vector3 m_v3CurNetAimDir;

	public bool m_bNetShoot;

	public bool m_bNetSkill;

	public int m_nNetSkillID;

	public CCharBase m_pNetSkillTarget;

	protected gyUIPlayerHUD m_NetPlayerHUD;

	public int AvatarHead
	{
		get
		{
			return m_nAvatarHead;
		}
		set
		{
			if (m_nAvatarHead == value)
			{
				return;
			}
			m_nAvatarHead = value;
			if (m_nAvatarHead <= 0)
			{
				if (m_curCharacterInfoLevel == null)
				{
					return;
				}
				m_nAvatarHead = m_curCharacterInfoLevel.nAvatarHead;
			}
			SetAvatar(0, m_nAvatarHead);
		}
	}

	public int AvatarUpper
	{
		get
		{
			return m_nAvatarUpper;
		}
		set
		{
			if (m_nAvatarUpper == value)
			{
				return;
			}
			m_nAvatarUpper = value;
			if (m_nAvatarUpper <= 0)
			{
				if (m_curCharacterInfoLevel == null)
				{
					return;
				}
				m_nAvatarUpper = m_curCharacterInfoLevel.nAvatarUpper;
			}
			SetAvatar(1, m_nAvatarUpper);
		}
	}

	public int AvatarLower
	{
		get
		{
			return m_nAvatarLower;
		}
		set
		{
			if (m_nAvatarLower == value)
			{
				return;
			}
			m_nAvatarLower = value;
			if (m_nAvatarLower <= 0)
			{
				if (m_curCharacterInfoLevel == null)
				{
					return;
				}
				m_nAvatarLower = m_curCharacterInfoLevel.nAvatarLower;
			}
			SetAvatar(2, m_nAvatarLower);
		}
	}

	public int AvatarHeadup
	{
		get
		{
			return m_nAvatarHeadup;
		}
		set
		{
			if (m_nAvatarHeadup != value)
			{
				m_nAvatarHeadup = value;
				if (m_nAvatarHeadup > 0)
				{
					SetAvatar(3, m_nAvatarHeadup);
				}
			}
		}
	}

	public int AvatarNeck
	{
		get
		{
			return m_nAvatarNeck;
		}
		set
		{
			if (m_nAvatarNeck != value)
			{
				m_nAvatarNeck = value;
				if (m_nAvatarNeck > 0)
				{
					SetAvatar(6, m_nAvatarNeck);
				}
			}
		}
	}

	public int AvatarWrist
	{
		get
		{
			return m_nAvatarWrist;
		}
		set
		{
			if (m_nAvatarWrist != value)
			{
				m_nAvatarWrist = value;
				if (m_nAvatarWrist > 0)
				{
					SetAvatar(4, m_nAvatarWrist);
					SetAvatar(5, m_nAvatarWrist);
				}
			}
		}
	}

	public int AvatarBadge
	{
		get
		{
			return m_nAvatarBadge;
		}
		set
		{
			m_nAvatarBadge = value;
		}
	}

	public int AvatarStone
	{
		get
		{
			return m_nAvatarStone;
		}
		set
		{
			m_nAvatarStone = value;
		}
	}

	public Vector3 MoveDir
	{
		get
		{
			return m_v3MoveDir;
		}
		set
		{
			m_v3MoveDir = value.normalized;
		}
	}

	public virtual Vector3 ShootDir
	{
		get
		{
			return m_v3ShootDir;
		}
		set
		{
			m_v3ShootDir = value.normalized;
		}
	}

	public bool isRoomMaster
	{
		get
		{
			return m_bRoomMaster;
		}
		set
		{
			m_bRoomMaster = value;
		}
	}

	public CCharacterInfoLevel CurCharInfoLevel
	{
		get
		{
			return m_curCharacterInfoLevel;
		}
	}

	public new int Level
	{
		get
		{
			return m_nLevel;
		}
		set
		{
			m_nLevel = value;
		}
	}

	public int EXP
	{
		get
		{
			return m_nExp;
		}
		set
		{
			m_nExp = value;
		}
	}

	public int CurWeaponID
	{
		get
		{
			return m_nCurWeaponID;
		}
	}

	public int CurWeaponLevel
	{
		get
		{
			return m_nCurWeaponLevel;
		}
	}

	public kAnimEnum CurMixAnim
	{
		get
		{
			return m_curAnimMix;
		}
	}

	public int CurEquipStone
	{
		get
		{
			return m_nEquipStone;
		}
		set
		{
			m_nEquipStone = value;
		}
	}

	public int CurEquipStoneLevel
	{
		get
		{
			return m_nEquipStoneLevel;
		}
		set
		{
			m_nEquipStoneLevel = value;
		}
	}

	public int SkillID
	{
		get
		{
			return m_nSkill;
		}
		set
		{
			m_nSkill = value;
		}
	}

	public int SkillLevel
	{
		get
		{
			return m_nSkillLevel;
		}
		set
		{
			m_nSkillLevel = value;
		}
	}

	public new void Awake()
	{
		base.Awake();
		m_nType = kCharType.Player;
		base.CampType = kCampType.Player;
		m_CharMoveMode = kCharMoveMode.Fly;
		m_bBodyYaw = false;
		m_Avatar = m_ModelEntity.GetComponent<Avatar>();
		m_ModelBody = m_ModelEntityTransform.Find("Bip01/UpBodyRotate");
		m_ModelBodyDown = m_ModelEntityTransform.Find("Bip01/Bip01 Pelvis");
		m_BoneBack = m_ModelBody.Find("Bip01 Spine/Bip01 Spine1/Bone04");
		m_ModelBack = m_ModelBody.Find("Bip01 Spine/Bip01 Spine1/Bone04");
		m_ModelHead = m_ModelBody.Find("Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 Head");
		m_SpineUpBody = m_ModelBody;
		m_SpineDownBody = m_ModelBodyDown;
		m_SpineDownBodyLocalRotation = m_SpineDownBody.localRotation;
		m_HandBone = m_ModelBody.Find("Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Weapon_Dummy");
		m_ManualSpine = m_SpineUpBody;
		m_ManualSpineLocalRotation = m_ManualSpine.localRotation;
		m_BloodEffectPos = m_SpineUpBody;
		m_CharacterModelInterface = GetComponent<iCharacterModel>();
		m_Property = new CProPlayer();
		m_curAnim = kAnimEnum.Idle;
		m_bNeedControlBody = true;
		m_nSkill = -1;
		m_nSkillLevel = 1;
		m_arrCarryPassiveSkill = new int[3];
		m_arrCarryPassiveSkillLevel = new int[3];
		m_nCarryItemID = -1;
		m_CarryItemObj = null;
		m_nCarryItemBuffSlot = -1;
		m_ltNetPath = new List<Vector3>();
		if (base.m_GameScene.CurGameLevelInfo != null && base.m_GameScene.CurGameLevelInfo.sSceneName == "SceneSnow")
		{
			iStepEffectLeft component = m_ModelEntity.GetComponent<iStepEffectLeft>();
			if (component != null)
			{
				component.nPrefabID = 1913;
			}
			iStepEffectRight component2 = m_ModelEntity.GetComponent<iStepEffectRight>();
			if (component2 != null)
			{
				component2.nPrefabID = 1913;
			}
		}
	}

	public new void Update()
	{
		if (!m_bActive)
		{
			return;
		}
		float num = Time.deltaTime * m_fTimeScale;
		if (m_Behavior != null)
		{
			m_Behavior.Update(this, num);
		}
		if (base.isDead || base.isStun)
		{
			return;
		}
		base.Update();
		if (m_bBodyYaw)
		{
			m_fDownBodyYawRate += m_fDownBodyYawSpeed * num;
			if (m_fDownBodyYawRate >= 1f)
			{
				m_bBodyYaw = false;
				m_fDownBodyYaw = m_fDownBodyYawDst;
			}
			else
			{
				m_fDownBodyYaw = MyUtils.Lerp(m_fDownBodyYawSrc, m_fDownBodyYawDst, m_fDownBodyYawRate);
			}
		}
		if (m_BackPackAnimManager != null)
		{
			m_BackPackAnimManager.Update(num);
		}
		if (m_UseSkill != null && m_UseSkill.OnUpdate(this, num) != kUseSkillStatus.Executing)
		{
			m_UseSkill.OnExit(this);
			m_UseSkill = null;
		}
		if (base.m_GameScene.GameStatus == iGameSceneBase.kGameStatus.Gameing)
		{
			float value = m_Property.GetValue(kProEnum.Char_RecoverLife);
			if (value > 0f)
			{
				m_fRecoverLifeTimeCount += num;
				if (m_fRecoverLifeTimeCount >= m_fRecoverLifeTime)
				{
					m_fRecoverLifeTimeCount = 0f;
					AddHP(m_fHPMax * value / 100f);
					base.m_GameScene.AddHealText(m_fHPMax * value / 100f, GetBone(0).position);
				}
			}
			float value2 = m_Property.GetValue(kProEnum.Char_RecoverBullet);
			if (value2 > 0f)
			{
				m_fRecoverBulletTimeCount += num;
				if (m_fRecoverBulletTimeCount >= m_fRecoverBulletTime)
				{
					m_fRecoverBulletTimeCount = 0f;
					for (int i = 0; i < 3; i++)
					{
						CWeaponBase weapon = base.m_GameState.GetWeapon(i);
						if (weapon != null && weapon.CurWeaponLvlInfo != null && weapon.CurWeaponLvlInfo.nType != 1)
						{
							int num2 = Mathf.FloorToInt((float)weapon.BulletNumMax * value2 / 100f);
							weapon.SetBullet(weapon.BulletNum + num2);
							if (weapon == m_curWeapon)
							{
								weapon.RefreshBulletUI(this, true);
								base.m_GameScene.AddBulletText(num2, GetBone(0).position);
							}
						}
					}
				}
			}
		}
		if (m_fCurAnimMixCount > 0f)
		{
			m_fCurAnimMixCount -= num;
			if (m_fCurAnimMixCount <= 0f)
			{
				m_fCurAnimMixCount = 0f;
				m_AnimManager.FadeOutAnim(m_curAnimMix, 0.5f);
			}
		}
	}

	public new void FixedUpdate()
	{
		base.FixedUpdate();
	}

	public new void LateUpdate()
	{
		if (m_bActive)
		{
			base.LateUpdate();
			if (m_fDownBodyYaw != 0f)
			{
				m_SpineDownBody.localRotation = m_SpineDownBodyLocalRotation * Quaternion.Euler(new Vector3(0f - m_fDownBodyYaw, 0f, 0f));
			}
		}
	}

	public virtual void InitChar(int nCharID, int nLevel, int nExp = 0, int nAvatarHead = -1, int nAvatarUpper = -1, int nAvatarLower = -1, int nAvatarHeadup = -1, int nAvatarNeck = -1, int nAvatarWrist = -1, int nAvatarBadge = -1, int nAvatarStone = -1)
	{
		base.ID = nCharID;
		Level = nLevel;
		EXP = nExp;
		m_curCharacterInfo = base.m_GameData.GetCharacterInfo(nCharID);
		if (m_curCharacterInfo == null)
		{
			return;
		}
		m_curCharacterInfoLevel = m_curCharacterInfo.Get(nLevel);
		if (m_curCharacterInfoLevel == null)
		{
			return;
		}
		InitAnimData();
		InitAudioData();
		m_ltSkillPassive.Clear();
		if (m_curCharacterInfoLevel.ltSkillPassive != null)
		{
			for (int i = 0; i < m_curCharacterInfoLevel.ltSkillPassive.Count; i++)
			{
				int num = m_curCharacterInfoLevel.ltSkillPassive[i];
				CSkillInfoLevel skillInfo = base.m_GameData.GetSkillInfo(num, 1);
				if (skillInfo != null && skillInfo.nType == 1)
				{
					m_ltSkillPassive.Add(num);
				}
			}
		}
		m_nSkill = m_curCharacterInfoLevel.nSkill;
		m_nSkillLevel = 1;
		AvatarHead = ((nAvatarHead <= 0) ? m_curCharacterInfoLevel.nAvatarHead : nAvatarHead);
		AvatarUpper = ((nAvatarUpper <= 0) ? m_curCharacterInfoLevel.nAvatarUpper : nAvatarUpper);
		AvatarLower = ((nAvatarLower <= 0) ? m_curCharacterInfoLevel.nAvatarLower : nAvatarLower);
		AvatarHeadup = ((nAvatarHeadup <= 0) ? (-1) : nAvatarHeadup);
		AvatarNeck = ((nAvatarNeck <= 0) ? (-1) : nAvatarNeck);
		AvatarWrist = ((nAvatarWrist <= 0) ? (-1) : nAvatarWrist);
		AvatarBadge = ((nAvatarBadge <= 0) ? (-1) : nAvatarBadge);
		AvatarStone = ((nAvatarStone <= 0) ? (-1) : nAvatarStone);
		m_Property.Initialize(nCharID, nLevel);
		m_Property.UpdateSkill(this);
		m_Property.UpdateEquip(this);
		m_fHPMax = m_Property.GetValue(kProEnum.HPMax);
		float value = m_Property.GetValue(kProEnum.HPMaxUp);
		if (value != -1f)
		{
			m_fHPMax *= 1f + value / 100f;
		}
		m_fHP = m_fHPMax;
		iGameUIBase gameUI = base.m_GameScene.GetGameUI();
		if (gameUI != null && m_fHPMax != 0f)
		{
			gameUI.SetProtraitIcon(m_curCharacterInfoLevel.sIcon, base.UID);
			gameUI.SetProtraitLife(m_fHP / m_fHPMax, base.UID);
			UnityEngine.Debug.Log(base.UID + " " + m_fHP + " " + m_fHPMax);
		}
	}

	public void UpdateUpBody(Vector3 v3LookDir)
	{
		v3LookDir.Normalize();
		m_fUpBodyYaw = MyUtils.AngleAroundAxis(base.Dir2D, v3LookDir, Vector3.up);
		m_fUpBodyPitch = 0f - MyUtils.AngleAroundAxis(base.Dir2D, v3LookDir, base.Transform.right);
		m_ManualSpine.localRotation = m_ManualSpineLocalRotation * Quaternion.Euler(new Vector3(m_fUpBodyYaw, 0f, m_fUpBodyPitch));
	}

	public bool isNetAim()
	{
		return false;
	}

	public Vector3 GetShootMouse()
	{
		if (m_ShootMouse == null)
		{
			return m_ModelTransform.position;
		}
		return m_ShootMouse.transform.position;
	}

	public Transform GetShootMouseTf()
	{
		if (m_ShootMouse == null)
		{
			return m_ModelTransform;
		}
		return m_ShootMouse.transform;
	}

	public Transform GetWeaponBackTf()
	{
		if (m_WeaponBack == null)
		{
			return m_ModelTransform;
		}
		return m_WeaponBack.transform;
	}

	public CWeaponInfoLevel GetCurWeaponLvlInfo()
	{
		return m_curWeaponLvlInfo;
	}

	public CWeaponBase GetWeapon()
	{
		return m_curWeapon;
	}

	public bool GetCarryPassiveSkill(int nIndex, ref int nID, ref int nLevel)
	{
		if (nIndex < 0 || nIndex >= m_arrCarryPassiveSkill.Length)
		{
			return false;
		}
		nID = m_arrCarryPassiveSkill[nIndex];
		nLevel = m_arrCarryPassiveSkillLevel[nIndex];
		return true;
	}

	public void CarryPassiveSkill(int nIndex, int nPassiveSkillID, int nPassiveSkillLevel)
	{
		if (nIndex >= 0 && nIndex < m_arrCarryPassiveSkill.Length)
		{
			m_arrCarryPassiveSkill[nIndex] = nPassiveSkillID;
			m_arrCarryPassiveSkillLevel[nIndex] = nPassiveSkillLevel;
		}
	}

	public override void Destroy()
	{
		base.Destroy();
		if (m_NetPlayerHUD != null)
		{
			m_NetPlayerHUD.Destroy();
			m_NetPlayerHUD = null;
		}
	}

	public override void InitAnimData()
	{
		m_AnimData.Cleanup();
		switch (m_CharMoveMode)
		{
		case kCharMoveMode.Fly:
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Idle, "crossbow_air_idle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Forward, "crossbow_air_move_forward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Back, "crossbow_air_move_backward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Left, "crossbow_air_move_left"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Right, "crossbow_air_move_right"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Attack, "crossbow_air_attack01_idle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Attack_Forward, "crossbow_air_attack01_forward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Attack_Back, "crossbow_air_attack01_backward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Attack_Left, "crossbow_air_attack01_left"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Attack_Right, "crossbow_air_attack01_right"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Hurt, "crossbow_ground_damage"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_BigHurt_Front, "crossbow_air_damage_flyfront"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_BigHurt_Behind, "crossbow_air_damage_flyback"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Skill, "crossbow_ground_useskill"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Rush, "crossbow_air_move_forward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Death, "crossbow_ground_death_fly"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Victory, "crossbow_ground_victory"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_VictoryIdle, "crossbow_ground_victoryidle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Fail, "crossbow_ground_fail"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_FailIdle, "crossbow_ground_failidle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Idle, "shootgun_air_idle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Forward, "shootgun_air_move_forward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Back, "shootgun_air_move_backward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Left, "shootgun_air_move_left"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Right, "shootgun_air_move_right"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Attack, "shootgun_air_attack01_idle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Attack_Forward, "shootgun_air_attack01_forward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Attack_Back, "shootgun_air_attack01_backward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Attack_Left, "shootgun_air_attack01_left"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Attack_Right, "shootgun_air_attack01_right"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Hurt, "shootgun_ground_damage"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_BigHurt_Front, "shootgun_air_damage_flyfront"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_BigHurt_Behind, "shootgun_air_damage_flyback"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Skill, "shootgun_ground_useskill"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Rush, "shootgun_air_move_forward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Death, "shootgun_ground_death_fly"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Victory, "shootgun_ground_victory"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_VictoryIdle, "shootgun_ground_victoryidle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Fail, "shootgun_ground_fail"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_FailIdle, "shootgun_ground_failidle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Idle, "autorifle_air_idle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Forward, "autorifle_air_move_forward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Back, "autorifle_air_move_backward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Left, "autorifle_air_move_left"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Right, "autorifle_air_move_right"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Attack, "autorifle_air_attack01_idle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Attack_Forward, "autorifle_air_attack01_forward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Attack_Back, "autorifle_air_attack01_backward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Attack_Left, "autorifle_air_attack01_left"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Attack_Right, "autorifle_air_attack01_right"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Hurt, "autorifle_ground_damage"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_BigHurt_Front, "autorifle_air_damage_flyfront"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_BigHurt_Behind, "autorifle_air_damage_flyback"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Skill, "autorifle_ground_useskill"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Rush, "autorifle_air_move_forward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Death, "autorifle_ground_death_fly"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Victory, "autorifle_ground_victory"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_VictoryIdle, "autorifle_ground_victoryidle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Fail, "autorifle_ground_fail"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_FailIdle, "autorifle_ground_failidle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Idle, "holdgun_air_idle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Forward, "holdgun_air_move_forward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Back, "holdgun_air_move_backward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Left, "holdgun_air_move_left"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Right, "holdgun_air_move_right"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Attack, "holdgun_air_attack01_idle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Attack_Forward, "holdgun_air_attack01_forward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Attack_Back, "holdgun_air_attack01_backward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Attack_Left, "holdgun_air_attack01_left"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Attack_Right, "holdgun_air_attack01_right"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Hurt, "holdgun_ground_damage"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_BigHurt_Front, "holdgun_air_damage_flyfront"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_BigHurt_Behind, "holdgun_air_damage_flyback"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Skill, "holdgun_ground_useskill"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Rush, "holdgun_air_move_forward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Death, "holdgun_ground_death_fly"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Victory, "holdgun_ground_victory"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_VictoryIdle, "holdgun_ground_victoryidle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Fail, "holdgun_ground_fail"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_FailIdle, "holdgun_ground_failidle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Idle, "rocket_air_idle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Forward, "rocket_air_move_forward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Back, "rocket_air_move_backward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Left, "rocket_air_move_left"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Right, "rocket_air_move_right"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Attack, "rocket_air_attack01_idle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Attack_Forward, "rocket_air_attack01_forward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Attack_Back, "rocket_air_attack01_backward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Attack_Left, "rocket_air_attack01_left"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Attack_Right, "rocket_air_attack01_right"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Hurt, "rocket_ground_damage"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_BigHurt_Front, "rocket_air_damage_flyfront"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_BigHurt_Behind, "rocket_air_damage_flyback"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Skill, "rocket_ground_useskill"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Rush, "rocket_air_move_forward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Death, "rocket_ground_death_fly"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Victory, "rocket_ground_victory"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_VictoryIdle, "rocket_ground_victoryidle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Fail, "rocket_ground_fail"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_FailIdle, "rocket_ground_failidle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Idle, "melee_air_idle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Forward, "melee_air_move_forward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Back, "melee_air_move_backward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Left, "melee_air_move_left"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Right, "melee_air_move_right"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Attack1, "melee_air_attack01_idle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Attack1_Forward, "melee_air_attack01_forward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Attack1_Back, "melee_air_attack01_backward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Attack1_Left, "melee_air_attack01_left"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Attack1_Right, "melee_air_attack01_right"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Attack2, "melee_air_attack02_idle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Attack2_Forward, "melee_air_attack02_forward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Attack2_Back, "melee_air_attack02_backward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Attack2_Left, "melee_air_attack02_left"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Attack2_Right, "melee_air_attack02_right"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Hurt, "melee_ground_damage"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_BigHurt_Front, "melee_air_damage_flyfront"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_BigHurt_Behind, "melee_air_damage_flyback"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Skill, "melee_ground_useskill"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Rush, "melee_air_move_forward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Death, "melee_ground_death_fly"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Victory, "melee_ground_victory"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_VictoryIdle, "melee_ground_victoryidle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Fail, "melee_ground_fail"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_FailIdle, "melee_ground_failidle"));
			break;
		case kCharMoveMode.Ground:
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Idle, "crossbow_ground_idle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Forward, "crossbow_ground_move_forward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Back, "crossbow_ground_move_backward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Attack, "crossbow_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Attack_Forward, "crossbow_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Attack_Back, "crossbow_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Attack_Left, "crossbow_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Attack_Right, "crossbow_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Hurt, "crossbow_ground_damage"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_BigHurt_Front, "crossbow_ground_damage_flyfront"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_BigHurt_Behind, "crossbow_ground_damage_flyback"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Skill, "crossbow_ground_useskill"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Rush, "crossbow_ground_useskill_rush"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Death, "crossbow_ground_death"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Stun, "crossbow_ground_stun"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Victory, "crossbow_ground_victory"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_VictoryIdle, "crossbow_ground_victoryidle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_Fail, "crossbow_ground_fail"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Crossbow_FailIdle, "crossbow_ground_failidle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Idle, "shootgun_ground_idle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Forward, "shootgun_ground_move_forward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Back, "shootgun_ground_move_backward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Attack, "shootgun_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Attack_Forward, "shootgun_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Attack_Back, "shootgun_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Attack_Left, "shootgun_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Attack_Right, "shootgun_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Hurt, "shootgun_ground_damage"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_BigHurt_Front, "shootgun_ground_damage_flyfront"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_BigHurt_Behind, "shootgun_ground_damage_flyback"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Skill, "shootgun_ground_useskill"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Rush, "shootgun_ground_useskill_rush"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Death, "shootgun_ground_death"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Stun, "shootgun_ground_stun"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Victory, "shootgun_ground_victory"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_VictoryIdle, "shootgun_ground_victoryidle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_Fail, "shootgun_ground_fail"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.ShootGun_FailIdle, "shootgun_ground_failidle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Idle, "autorifle_ground_idle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Forward, "autorifle_ground_move_forward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Back, "autorifle_ground_move_backward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Attack, "autorifle_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Attack_Forward, "autorifle_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Attack_Back, "autorifle_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Attack_Left, "autorifle_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Attack_Right, "autorifle_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Hurt, "autorifle_ground_damage"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_BigHurt_Front, "autorifle_ground_damage_flyfront"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_BigHurt_Behind, "autorifle_ground_damage_flyback"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Skill, "autorifle_ground_useskill"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Rush, "autorifle_ground_useskill_rush"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Death, "autorifle_ground_death"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Stun, "autorifle_ground_stun"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Victory, "autorifle_ground_victory"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_VictoryIdle, "autorifle_ground_victoryidle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_Fail, "autorifle_ground_fail"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.AutoRifle_FailIdle, "autorifle_ground_failidle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Idle, "holdgun_ground_idle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Forward, "holdgun_ground_move_forward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Back, "holdgun_ground_move_backward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Attack, "holdgun_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Attack_Forward, "holdgun_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Attack_Back, "holdgun_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Attack_Left, "holdgun_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Attack_Right, "holdgun_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Hurt, "holdgun_ground_damage"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_BigHurt_Front, "holdgun_ground_damage_flyfront"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_BigHurt_Behind, "holdgun_ground_damage_flyback"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Skill, "holdgun_ground_useskill"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Rush, "holdgun_ground_useskill_rush"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Death, "holdgun_ground_death"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Stun, "holdgun_ground_stun"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Victory, "holdgun_ground_victory"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_VictoryIdle, "holdgun_ground_victoryidle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_Fail, "holdgun_ground_fail"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.HoldGun_FailIdle, "holdgun_ground_failidle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Idle, "rocket_ground_idle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Forward, "rocket_ground_move_forward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Back, "rocket_ground_move_backward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Attack, "rocket_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Attack_Forward, "rocket_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Attack_Back, "rocket_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Attack_Left, "rocket_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Attack_Right, "rocket_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Hurt, "rocket_ground_damage"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_BigHurt_Front, "rocket_ground_damage_flyfront"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_BigHurt_Behind, "rocket_ground_damage_flyback"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Skill, "rocket_ground_useskill"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Rush, "rocket_ground_useskill_rush"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Death, "rocket_ground_death"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Stun, "rocket_ground_stun"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Victory, "rocket_ground_victory"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_VictoryIdle, "rocket_ground_victoryidle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_Fail, "rocket_ground_fail"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Rocket_FailIdle, "rocket_ground_failidle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Idle, "melee_ground_idle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Forward, "melee_ground_move_forward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Back, "melee_ground_move_backward"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Attack1, "melee_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Attack1_Forward, "melee_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Attack1_Back, "melee_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Attack1_Left, "melee_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Attack1_Right, "melee_ground_attack01"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Attack2, "melee_ground_attack02"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Attack2_Forward, "melee_ground_attack02"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Attack2_Back, "melee_ground_attack02"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Attack2_Left, "melee_ground_attack02"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Attack2_Right, "melee_ground_attack02"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Hurt, "melee_ground_damage"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_BigHurt_Front, "melee_ground_damage_flyfront"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_BigHurt_Behind, "melee_ground_damage_flyback"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Skill, "melee_ground_useskill"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Rush, "melee_ground_useskill_rush"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Death, "melee_ground_death"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Stun, "melee_ground_stun"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Victory, "melee_ground_victory"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_VictoryIdle, "melee_ground_victoryidle"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_Fail, "melee_ground_fail"));
			m_AnimData.Add(new CAnimInfo(kAnimEnum.Melee_FailIdle, "melee_ground_failidle"));
			break;
		}
	}

	public override void InitAudioData()
	{
		if (m_curCharacterInfoLevel != null)
		{
			m_AudioData.Clear();
			if (m_curCharacterInfoLevel.isMale)
			{
				m_AudioData.Add(kAudioEnum.Hurt, "SVO_Voice_Male01_Hurt");
				m_AudioData.Add(kAudioEnum.Dead, "SVO_Voice_Male01_Death");
			}
			else
			{
				m_AudioData.Add(kAudioEnum.Hurt, "SVO_Voice_Female01_Hurt");
				m_AudioData.Add(kAudioEnum.Dead, "SVO_Voice_Female01_Death");
			}
		}
	}

	public override float CrossAnim(kAnimEnum type, WrapMode mode, float fadespeed = 0.3f, float speed = 1f, float time = 0f)
	{
		m_curAnim = type;
		if (m_curWeaponLvlInfo != null)
		{
			TranslateAnim(m_curWeaponLvlInfo.nActionType, m_curAnim, ref type);
		}
		return base.CrossAnim(type, mode, fadespeed, speed, time);
	}

	public override bool IsActionPlaying(kAnimEnum type)
	{
		if (m_curWeaponLvlInfo != null)
		{
			TranslateAnim(m_curWeaponLvlInfo.nActionType, m_curAnim, ref type);
		}
		return m_AnimManager.IsAnimPlaying(type);
	}

	public override float GetActionLen(kAnimEnum type)
	{
		if (m_curWeaponLvlInfo != null)
		{
			TranslateAnim(m_curWeaponLvlInfo.nActionType, m_curAnim, ref type);
		}
		return m_AnimManager.GetAnimLen(type);
	}

	public override void SetActionSpeed(kAnimEnum type, float speed)
	{
		if (m_curWeaponLvlInfo != null)
		{
			TranslateAnim(m_curWeaponLvlInfo.nActionType, m_curAnim, ref type);
		}
		m_AnimManager.SetAnimSpeed(type, speed);
	}

	public override void SetActionLayer(kAnimEnum type, int nLayer)
	{
		if (m_curWeaponLvlInfo != null)
		{
			TranslateAnim(m_curWeaponLvlInfo.nActionType, m_curAnim, ref type);
		}
		m_AnimManager.SetAnimLayer(type, nLayer);
	}

	public override float PlayAnim(kAnimEnum type, WrapMode mode, float speed = 1f, float time = 0f)
	{
		m_curAnim = type;
		if (m_curWeaponLvlInfo != null)
		{
			TranslateAnim(m_curWeaponLvlInfo.nActionType, m_curAnim, ref type);
		}
		return base.PlayAnim(type, mode, speed, time);
	}

	public override float PlayAnimMix(kAnimEnum type, WrapMode mode, float speed = 1f)
	{
		if (m_curWeaponLvlInfo != null)
		{
			TranslateAnim(m_curWeaponLvlInfo.nActionType, m_curAnim, ref type);
		}
		float num = base.PlayAnimMix(type, mode, speed);
		if (mode == WrapMode.Once || mode == WrapMode.ClampForever)
		{
			mode = WrapMode.ClampForever;
			m_curAnimMix = type;
			m_fCurAnimMixCount = num;
		}
		return num;
	}

	public override float PlayAnimMix(kAnimEnum type, WrapMode mode, Transform bone, float speed = 1f)
	{
		m_curAnimMix = type;
		if (m_curWeaponLvlInfo != null)
		{
			TranslateAnim(m_curWeaponLvlInfo.nActionType, m_curAnim, ref type);
		}
		return base.PlayAnimMix(type, mode, bone, speed);
	}

	public override float CrossAnimMix(kAnimEnum type, WrapMode mode, float fadespeed = 0.3f, float speed = 1f)
	{
		m_curAnimMix = type;
		if (m_curWeaponLvlInfo != null)
		{
			TranslateAnim(m_curWeaponLvlInfo.nActionType, m_curAnim, ref type);
		}
		return base.CrossAnimMix(type, mode, fadespeed, speed);
	}

	public override float CrossAnimMix(kAnimEnum type, WrapMode mode, Transform bone, float fadespeed = 0.3f, float speed = 1f)
	{
		m_curAnimMix = type;
		if (m_curWeaponLvlInfo != null)
		{
			TranslateAnim(m_curWeaponLvlInfo.nActionType, m_curAnim, ref type);
		}
		return base.CrossAnimMix(type, mode, bone, fadespeed, speed);
	}

	public override void StopAction(kAnimEnum type)
	{
		if (m_curWeaponLvlInfo != null)
		{
			TranslateAnim(m_curWeaponLvlInfo.nActionType, m_curAnim, ref type);
		}
		base.StopAction(type);
	}

	protected void TranslateAnim(int nWeaponActionType, kAnimEnum moveanim, ref kAnimEnum type)
	{
		int num = (int)type;
		if (num < 1 || num >= 25)
		{
			return;
		}
		switch (type)
		{
		case kAnimEnum.Idle:
			switch (nWeaponActionType)
			{
			case 0:
				type = kAnimEnum.Crossbow_Idle;
				break;
			case 1:
				type = kAnimEnum.Melee_Idle;
				break;
			case 2:
				type = kAnimEnum.AutoRifle_Idle;
				break;
			case 3:
				type = kAnimEnum.ShootGun_Idle;
				break;
			case 4:
				type = kAnimEnum.HoldGun_Idle;
				break;
			case 5:
				type = kAnimEnum.Rocket_Idle;
				break;
			}
			break;
		case kAnimEnum.Attack:
			switch (moveanim)
			{
			case kAnimEnum.Idle:
				switch (nWeaponActionType)
				{
				case 0:
					type = kAnimEnum.Crossbow_Attack;
					break;
				case 1:
					type = ((Random.Range(0, 2) != 0) ? kAnimEnum.Melee_Attack2 : kAnimEnum.Melee_Attack1);
					break;
				case 2:
					type = kAnimEnum.AutoRifle_Attack;
					break;
				case 3:
					type = kAnimEnum.ShootGun_Attack;
					break;
				case 4:
					type = kAnimEnum.HoldGun_Attack;
					break;
				case 5:
					type = kAnimEnum.Rocket_Attack;
					break;
				}
				break;
			case kAnimEnum.MoveForward:
				switch (nWeaponActionType)
				{
				case 0:
					type = kAnimEnum.Crossbow_Attack_Forward;
					break;
				case 1:
					type = ((Random.Range(0, 2) != 0) ? kAnimEnum.Melee_Attack2_Forward : kAnimEnum.Melee_Attack1_Forward);
					break;
				case 2:
					type = kAnimEnum.AutoRifle_Attack_Forward;
					break;
				case 3:
					type = kAnimEnum.ShootGun_Attack_Forward;
					break;
				case 4:
					type = kAnimEnum.HoldGun_Attack_Forward;
					break;
				case 5:
					type = kAnimEnum.Rocket_Attack_Forward;
					break;
				}
				break;
			case kAnimEnum.MoveBack:
				switch (nWeaponActionType)
				{
				case 0:
					type = kAnimEnum.Crossbow_Attack_Back;
					break;
				case 1:
					type = ((Random.Range(0, 2) != 0) ? kAnimEnum.Melee_Attack2_Back : kAnimEnum.Melee_Attack1_Back);
					break;
				case 2:
					type = kAnimEnum.AutoRifle_Attack_Back;
					break;
				case 3:
					type = kAnimEnum.ShootGun_Attack_Back;
					break;
				case 4:
					type = kAnimEnum.HoldGun_Attack_Back;
					break;
				case 5:
					type = kAnimEnum.Rocket_Attack_Back;
					break;
				}
				break;
			case kAnimEnum.MoveLeft:
				switch (nWeaponActionType)
				{
				case 0:
					type = kAnimEnum.Crossbow_Attack_Left;
					break;
				case 1:
					type = ((Random.Range(0, 2) != 0) ? kAnimEnum.Melee_Attack2_Left : kAnimEnum.Melee_Attack1_Left);
					break;
				case 2:
					type = kAnimEnum.AutoRifle_Attack_Left;
					break;
				case 3:
					type = kAnimEnum.ShootGun_Attack_Left;
					break;
				case 4:
					type = kAnimEnum.HoldGun_Attack_Left;
					break;
				case 5:
					type = kAnimEnum.Rocket_Attack_Left;
					break;
				}
				break;
			case kAnimEnum.MoveRight:
				switch (nWeaponActionType)
				{
				case 0:
					type = kAnimEnum.Crossbow_Attack_Right;
					break;
				case 1:
					type = ((Random.Range(0, 2) != 0) ? kAnimEnum.Melee_Attack2_Right : kAnimEnum.Melee_Attack1_Right);
					break;
				case 2:
					type = kAnimEnum.AutoRifle_Attack_Right;
					break;
				case 3:
					type = kAnimEnum.ShootGun_Attack_Right;
					break;
				case 4:
					type = kAnimEnum.HoldGun_Attack_Right;
					break;
				case 5:
					type = kAnimEnum.Rocket_Attack_Right;
					break;
				}
				break;
			case kAnimEnum.Attack:
			case kAnimEnum.Hurt:
				break;
			}
			break;
		case kAnimEnum.Hurt:
			switch (nWeaponActionType)
			{
			case 0:
				type = kAnimEnum.Crossbow_Hurt;
				break;
			case 1:
				type = kAnimEnum.Melee_Hurt;
				break;
			case 2:
				type = kAnimEnum.AutoRifle_Hurt;
				break;
			case 3:
				type = kAnimEnum.ShootGun_Hurt;
				break;
			case 4:
				type = kAnimEnum.HoldGun_Hurt;
				break;
			case 5:
				type = kAnimEnum.Rocket_Hurt;
				break;
			}
			break;
		case kAnimEnum.BigHurtFront:
			switch (nWeaponActionType)
			{
			case 0:
				type = kAnimEnum.Crossbow_BigHurt_Front;
				break;
			case 1:
				type = kAnimEnum.Melee_BigHurt_Front;
				break;
			case 2:
				type = kAnimEnum.AutoRifle_BigHurt_Front;
				break;
			case 3:
				type = kAnimEnum.ShootGun_BigHurt_Front;
				break;
			case 4:
				type = kAnimEnum.HoldGun_BigHurt_Front;
				break;
			case 5:
				type = kAnimEnum.Rocket_BigHurt_Front;
				break;
			}
			break;
		case kAnimEnum.BigHurtBehind:
			switch (nWeaponActionType)
			{
			case 0:
				type = kAnimEnum.Crossbow_BigHurt_Behind;
				break;
			case 1:
				type = kAnimEnum.Melee_BigHurt_Behind;
				break;
			case 2:
				type = kAnimEnum.AutoRifle_BigHurt_Behind;
				break;
			case 3:
				type = kAnimEnum.ShootGun_BigHurt_Behind;
				break;
			case 4:
				type = kAnimEnum.HoldGun_BigHurt_Behind;
				break;
			case 5:
				type = kAnimEnum.Rocket_BigHurt_Behind;
				break;
			}
			break;
		case kAnimEnum.MoveForward:
			switch (nWeaponActionType)
			{
			case 0:
				type = kAnimEnum.Crossbow_Forward;
				break;
			case 1:
				type = kAnimEnum.Melee_Forward;
				break;
			case 2:
				type = kAnimEnum.AutoRifle_Forward;
				break;
			case 3:
				type = kAnimEnum.ShootGun_Forward;
				break;
			case 4:
				type = kAnimEnum.HoldGun_Forward;
				break;
			case 5:
				type = kAnimEnum.Rocket_Forward;
				break;
			}
			break;
		case kAnimEnum.MoveBack:
			switch (nWeaponActionType)
			{
			case 0:
				type = kAnimEnum.Crossbow_Back;
				break;
			case 1:
				type = kAnimEnum.Melee_Back;
				break;
			case 2:
				type = kAnimEnum.AutoRifle_Back;
				break;
			case 3:
				type = kAnimEnum.ShootGun_Back;
				break;
			case 4:
				type = kAnimEnum.HoldGun_Back;
				break;
			case 5:
				type = kAnimEnum.Rocket_Back;
				break;
			}
			break;
		case kAnimEnum.MoveLeft:
			switch (nWeaponActionType)
			{
			case 0:
				type = kAnimEnum.Crossbow_Left;
				break;
			case 1:
				type = kAnimEnum.Melee_Left;
				break;
			case 2:
				type = kAnimEnum.AutoRifle_Left;
				break;
			case 3:
				type = kAnimEnum.ShootGun_Left;
				break;
			case 4:
				type = kAnimEnum.HoldGun_Left;
				break;
			case 5:
				type = kAnimEnum.Rocket_Left;
				break;
			}
			break;
		case kAnimEnum.MoveRight:
			switch (nWeaponActionType)
			{
			case 0:
				type = kAnimEnum.Crossbow_Right;
				break;
			case 1:
				type = kAnimEnum.Melee_Right;
				break;
			case 2:
				type = kAnimEnum.AutoRifle_Right;
				break;
			case 3:
				type = kAnimEnum.ShootGun_Right;
				break;
			case 4:
				type = kAnimEnum.HoldGun_Right;
				break;
			case 5:
				type = kAnimEnum.Rocket_Right;
				break;
			}
			break;
		case kAnimEnum.Skill:
			switch (nWeaponActionType)
			{
			case 0:
				type = kAnimEnum.Crossbow_Skill;
				break;
			case 1:
				type = kAnimEnum.Melee_Skill;
				break;
			case 2:
				type = kAnimEnum.AutoRifle_Skill;
				break;
			case 3:
				type = kAnimEnum.ShootGun_Skill;
				break;
			case 4:
				type = kAnimEnum.HoldGun_Skill;
				break;
			case 5:
				type = kAnimEnum.Rocket_Skill;
				break;
			}
			break;
		case kAnimEnum.Skill_Rush:
			switch (nWeaponActionType)
			{
			case 0:
				type = kAnimEnum.Crossbow_Rush;
				break;
			case 1:
				type = kAnimEnum.Melee_Rush;
				break;
			case 2:
				type = kAnimEnum.AutoRifle_Rush;
				break;
			case 3:
				type = kAnimEnum.ShootGun_Rush;
				break;
			case 4:
				type = kAnimEnum.HoldGun_Rush;
				break;
			case 5:
				type = kAnimEnum.Rocket_Rush;
				break;
			}
			break;
		case kAnimEnum.Death:
			switch (nWeaponActionType)
			{
			case 0:
				type = kAnimEnum.Crossbow_Death;
				break;
			case 1:
				type = kAnimEnum.Melee_Death;
				break;
			case 2:
				type = kAnimEnum.AutoRifle_Death;
				break;
			case 3:
				type = kAnimEnum.ShootGun_Death;
				break;
			case 4:
				type = kAnimEnum.HoldGun_Death;
				break;
			case 5:
				type = kAnimEnum.Rocket_Death;
				break;
			}
			break;
		case kAnimEnum.Stun:
			switch (nWeaponActionType)
			{
			case 0:
				type = kAnimEnum.Crossbow_Stun;
				break;
			case 1:
				type = kAnimEnum.Melee_Stun;
				break;
			case 2:
				type = kAnimEnum.AutoRifle_Stun;
				break;
			case 3:
				type = kAnimEnum.ShootGun_Stun;
				break;
			case 4:
				type = kAnimEnum.HoldGun_Stun;
				break;
			case 5:
				type = kAnimEnum.Rocket_Stun;
				break;
			}
			break;
		case kAnimEnum.Victory:
			switch (nWeaponActionType)
			{
			case 0:
				type = kAnimEnum.Crossbow_Victory;
				break;
			case 1:
				type = kAnimEnum.Melee_Victory;
				break;
			case 2:
				type = kAnimEnum.AutoRifle_Victory;
				break;
			case 3:
				type = kAnimEnum.ShootGun_Victory;
				break;
			case 4:
				type = kAnimEnum.HoldGun_Victory;
				break;
			case 5:
				type = kAnimEnum.Rocket_Victory;
				break;
			}
			break;
		case kAnimEnum.VictoryIdle:
			switch (nWeaponActionType)
			{
			case 0:
				type = kAnimEnum.Crossbow_VictoryIdle;
				break;
			case 1:
				type = kAnimEnum.Melee_VictoryIdle;
				break;
			case 2:
				type = kAnimEnum.AutoRifle_VictoryIdle;
				break;
			case 3:
				type = kAnimEnum.ShootGun_VictoryIdle;
				break;
			case 4:
				type = kAnimEnum.HoldGun_VictoryIdle;
				break;
			case 5:
				type = kAnimEnum.Rocket_VictoryIdle;
				break;
			}
			break;
		case kAnimEnum.Fail:
			switch (nWeaponActionType)
			{
			case 0:
				type = kAnimEnum.Crossbow_Fail;
				break;
			case 1:
				type = kAnimEnum.Melee_Fail;
				break;
			case 2:
				type = kAnimEnum.AutoRifle_Fail;
				break;
			case 3:
				type = kAnimEnum.ShootGun_Fail;
				break;
			case 4:
				type = kAnimEnum.HoldGun_Fail;
				break;
			case 5:
				type = kAnimEnum.Rocket_Fail;
				break;
			}
			break;
		case kAnimEnum.FailIdle:
			switch (nWeaponActionType)
			{
			case 0:
				type = kAnimEnum.Crossbow_FailIdle;
				break;
			case 1:
				type = kAnimEnum.Melee_FailIdle;
				break;
			case 2:
				type = kAnimEnum.AutoRifle_FailIdle;
				break;
			case 3:
				type = kAnimEnum.ShootGun_FailIdle;
				break;
			case 4:
				type = kAnimEnum.HoldGun_FailIdle;
				break;
			case 5:
				type = kAnimEnum.Rocket_FailIdle;
				break;
			}
			break;
		case kAnimEnum.DeathFly:
		case kAnimEnum.TurnLeft:
		case kAnimEnum.TurnRight:
			break;
		}
	}

	public void EquipWeapon(int nWeaponID, int nWeaponLevel, CWeaponBase weapon = null)
	{
		CWeaponInfoLevel weaponInfo = base.m_GameData.GetWeaponInfo(nWeaponID, nWeaponLevel);
		if (weaponInfo == null)
		{
			return;
		}
		m_nCurWeaponID = nWeaponID;
		m_nCurWeaponLevel = nWeaponLevel;
		m_curWeaponLvlInfo = weaponInfo;
		int nActionType = weaponInfo.nActionType;
		int num = nActionType;
		if (num == 1)
		{
			m_bNeedControlBody = false;
		}
		else
		{
			m_bNeedControlBody = true;
		}
		GameObject gameObject = PrefabManager.Get(weaponInfo.nModel);
		if (gameObject == null)
		{
			return;
		}
		m_Weapon = (GameObject)Object.Instantiate(gameObject);
		if (m_Weapon == null)
		{
			return;
		}
		m_Weapon.transform.parent = m_HandBone;
		m_Weapon.transform.localPosition = Vector3.zero;
		m_Weapon.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
		Transform transform = m_Weapon.transform.Find("Dummy01/texiao");
		if (transform == null)
		{
			transform = m_Weapon.transform.Find("Dummy01/texiao01");
		}
		if (transform == null)
		{
			transform = m_Weapon.transform.Find("Dummy01/danyao");
		}
		if (transform != null)
		{
			m_ShootMouse = transform.gameObject;
		}
		else
		{
			m_ShootMouse = null;
		}
		transform = m_Weapon.transform.Find("Dummy01/texiao03");
		if (transform != null)
		{
			m_WeaponBack = transform.gameObject;
		}
		else
		{
			m_WeaponBack = null;
		}
		if (weapon != null)
		{
			m_curWeapon = weapon;
		}
		else
		{
			switch (weaponInfo.nAttackMode)
			{
			case 1:
				m_curWeapon = new CWeaponMelee();
				break;
			case 2:
				m_curWeapon = new CWeaponShoot();
				break;
			case 3:
				m_curWeapon = new CWeaponSpawn();
				break;
			case 4:
				m_curWeapon = new CWeaponSpawnWithHead();
				break;
			case 5:
				m_curWeapon = new CWeaponHoldy();
				break;
			case 6:
				m_curWeapon = new CWeaponShotgun();
				break;
			}
			if (m_curWeapon != null)
			{
				m_curWeapon.Initialize(nWeaponID, nWeaponLevel);
				m_curWeapon.isNetPlayerShoot = true;
			}
		}
		if (m_curWeapon != null)
		{
			m_curWeapon.Equip(this);
		}
		m_ModelRenderer = m_ModelTransform.GetComponentsInChildren<Renderer>();
		CrossAnim(m_curAnim, WrapMode.Loop, 0.3f, 1f, 0f);
		m_Property.SetValueBase(kProEnum.Damage, weaponInfo.fDamage);
		m_bUpdateProBuff = true;
		m_bUpdateProSkill = true;
	}

	public void UnEquipWeapon()
	{
		if (m_curWeapon != null)
		{
			m_curWeapon.UnEquip(this);
		}
		if (m_Weapon != null)
		{
			m_nCurWeaponID = -1;
			Object.Destroy(m_Weapon);
			m_Weapon = null;
		}
		m_ModelRenderer = m_ModelTransform.GetComponentsInChildren<Renderer>();
	}

	public void UpdateMoveAnim(Vector3 v3MoveDir, Vector3 v3ShootDir)
	{
		v3MoveDir.y = 0f;
		v3MoveDir.Normalize();
		v3ShootDir.y = 0f;
		v3ShootDir.Normalize();
		kCharMoveDir kCharMoveDir2 = kCharMoveDir.None;
		float num = Vector3.Dot(v3MoveDir, v3ShootDir);
		kCharMoveDir2 = ((num > 0.5f) ? kCharMoveDir.Forward : ((!(num >= 0f)) ? kCharMoveDir.Back : ((!(Vector3.Cross(v3MoveDir, v3ShootDir).y > 0f)) ? kCharMoveDir.Right : kCharMoveDir.Left)));
		SetMoveAnim(kCharMoveDir2);
	}

	protected void SetMoveAnimGround(kCharMoveDir movedir)
	{
		float num = 0f;
		kAnimEnum kAnimEnum2 = kAnimEnum.Idle;
		switch (movedir)
		{
		case kCharMoveDir.Forward:
			num = 0f;
			kAnimEnum2 = kAnimEnum.MoveForward;
			break;
		case kCharMoveDir.ForwardRight:
			num = 45f;
			kAnimEnum2 = kAnimEnum.MoveForward;
			break;
		case kCharMoveDir.ForwardLeft:
			num = -20f;
			kAnimEnum2 = kAnimEnum.MoveForward;
			break;
		case kCharMoveDir.Back:
			num = 0f;
			kAnimEnum2 = kAnimEnum.MoveBack;
			break;
		case kCharMoveDir.BackRight:
			num = 70f;
			kAnimEnum2 = kAnimEnum.MoveForward;
			break;
		case kCharMoveDir.BackLeft:
			num = 45f;
			kAnimEnum2 = kAnimEnum.MoveBack;
			break;
		case kCharMoveDir.Right:
			num = 70f;
			kAnimEnum2 = kAnimEnum.MoveForward;
			break;
		case kCharMoveDir.Left:
			num = 70f;
			kAnimEnum2 = kAnimEnum.MoveBack;
			break;
		default:
			num = 0f;
			kAnimEnum2 = kAnimEnum.MoveForward;
			break;
		}
		SetDownBodyYaw(num);
		CrossAnim(kAnimEnum2, WrapMode.Loop, 0.5f, 1f, -1f);
	}

	protected void SetMoveAnimFly(kCharMoveDir movedir)
	{
		kAnimEnum type = kAnimEnum.Idle;
		switch (movedir)
		{
		case kCharMoveDir.Forward:
			type = kAnimEnum.MoveForward;
			m_BackPackAnimManager.CrossFade(kAnimEnum.BackPack_Forward, WrapMode.Loop, 0.8f, 1f, -1f);
			break;
		case kCharMoveDir.ForwardRight:
			type = kAnimEnum.MoveRight;
			m_BackPackAnimManager.CrossFade(kAnimEnum.BackPack_Right, WrapMode.Loop, 0.8f, 1f, -1f);
			break;
		case kCharMoveDir.ForwardLeft:
			type = kAnimEnum.MoveLeft;
			m_BackPackAnimManager.CrossFade(kAnimEnum.BackPack_Left, WrapMode.Loop, 0.8f, 1f, -1f);
			break;
		case kCharMoveDir.Back:
			type = kAnimEnum.MoveBack;
			m_BackPackAnimManager.CrossFade(kAnimEnum.BackPack_Idle, WrapMode.Loop, 0.8f, 1f, -1f);
			break;
		case kCharMoveDir.BackRight:
			type = kAnimEnum.MoveBack;
			m_BackPackAnimManager.CrossFade(kAnimEnum.BackPack_Right, WrapMode.Loop, 0.8f, 1f, -1f);
			break;
		case kCharMoveDir.BackLeft:
			type = kAnimEnum.MoveBack;
			m_BackPackAnimManager.CrossFade(kAnimEnum.BackPack_Left, WrapMode.Loop, 0.8f, 1f, -1f);
			break;
		case kCharMoveDir.Right:
			type = kAnimEnum.MoveRight;
			m_BackPackAnimManager.CrossFade(kAnimEnum.BackPack_Right, WrapMode.Loop, 0.8f, 1f, -1f);
			break;
		case kCharMoveDir.Left:
			type = kAnimEnum.MoveLeft;
			m_BackPackAnimManager.CrossFade(kAnimEnum.BackPack_Left, WrapMode.Loop, 0.8f, 1f, -1f);
			break;
		}
		CrossAnim(type, WrapMode.Loop, 0.5f, 1f, -1f);
	}

	protected void SetMoveAnim(kCharMoveDir movedir)
	{
		if (movedir != 0 && movedir != m_curMoveDir)
		{
			m_curMoveDir = movedir;
			if (m_CharMoveMode == kCharMoveMode.Fly)
			{
				SetMoveAnimFly(m_curMoveDir);
			}
			else
			{
				SetMoveAnimGround(m_curMoveDir);
			}
		}
	}

	public void StopMoveAnim()
	{
		if (m_curAnim != kAnimEnum.Idle && (m_curAnim == kAnimEnum.MoveForward || m_curAnim == kAnimEnum.MoveBack || m_curAnim == kAnimEnum.MoveLeft || m_curAnim == kAnimEnum.MoveRight) && m_curAnim != kAnimEnum.Victory && m_curAnim != kAnimEnum.VictoryIdle && m_curAnim != kAnimEnum.Fail && m_curAnim != kAnimEnum.FailIdle)
		{
			if (IsCanAttack())
			{
				CrossAnim(kAnimEnum.Idle, WrapMode.Loop, 0.3f, 1f, 0f);
			}
			if (m_CharMoveMode == kCharMoveMode.Fly)
			{
				m_BackPackAnimManager.CrossFade(kAnimEnum.BackPack_Idle, WrapMode.Loop, 0.3f, 1f, 0f);
			}
		}
		if (m_curMoveDir != 0)
		{
			SetDownBodyYaw(0f);
			m_curMoveDir = kCharMoveDir.None;
		}
	}

	protected void SetDownBodyYaw(float yaw, float speed = 5f)
	{
		m_bBodyYaw = true;
		m_fDownBodyYawRate = 0f;
		m_fDownBodyYawSrc = m_fDownBodyYaw;
		m_fDownBodyYawDst = yaw;
		m_fDownBodyYawSpeed = speed;
	}

	public void SetMoveMode(kCharMoveMode mode)
	{
		m_CharMoveMode = mode;
		if (m_CharMoveMode == kCharMoveMode.Fly)
		{
			m_BackPackAnimData = new CAnimData();
			m_BackPackAnimManager = new CAnimPlay();
		}
		SetBackPack(mode);
		InitAnimData();
		CrossAnim(kAnimEnum.Idle, WrapMode.Loop, 0.3f, 1f, 0f);
		if (mode == kCharMoveMode.Fly)
		{
			PlayAudio("Mat_Jetpack");
		}
	}

	public void SetBackPack(kCharMoveMode mode)
	{
		if (mode != kCharMoveMode.Fly)
		{
			return;
		}
		if (m_CharacterModelInterface != null && m_CharacterModelInterface.mPendant != null)
		{
			m_CharacterModelInterface.mPendant.SetActiveRecursively(false);
		}
		GameObject gameObject = PrefabManager.Get(300);
		if (!(gameObject != null))
		{
			return;
		}
		m_BackPack = (GameObject)Object.Instantiate(gameObject);
		if (m_BackPack == null)
		{
			return;
		}
		m_BackPack.transform.parent = m_BoneBack;
		m_BackPack.transform.localPosition = Vector3.zero;
		m_BackPack.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
		m_BackPackTrailNode = m_BackPack.transform.Find("Dummy01/Dummy03/Dummy04");
		if (m_BackPackTrailNode != null)
		{
			GameObject gameObject2 = PrefabManager.Get(1906);
			if (gameObject2 != null)
			{
				m_BackPackTrailGas = (GameObject)Object.Instantiate(gameObject2);
				if (m_BackPackTrailGas != null)
				{
					m_BackPackTrailGas.transform.parent = m_BackPackTrailNode;
					m_BackPackTrailGas.transform.localPosition = Vector3.zero;
					m_BackPackTrailGas.transform.localRotation = Quaternion.identity;
				}
			}
		}
		m_BackPackAnimData.Cleanup();
		m_BackPackAnimData.Add(new CAnimInfo(kAnimEnum.BackPack_Idle, "attack"));
		m_BackPackAnimData.Add(new CAnimInfo(kAnimEnum.BackPack_Forward, "front"));
		m_BackPackAnimData.Add(new CAnimInfo(kAnimEnum.BackPack_Left, "left"));
		m_BackPackAnimData.Add(new CAnimInfo(kAnimEnum.BackPack_Right, "right"));
		m_BackPackAnimManager.Initialize(m_BackPack, m_BackPackAnimData);
		m_BackPackAnimManager.CrossFade(kAnimEnum.BackPack_Idle, WrapMode.Loop, 0.3f, 1f, 0f);
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
		iGameUIBase gameUI = base.m_GameScene.GetGameUI();
		if (gameUI != null && m_fHPMax != 0f)
		{
			gameUI.SetProtraitLife(m_fHP / m_fHPMax, base.UID);
		}
	}

	public override void SetHP(float fHP, float fMaxHP)
	{
		m_fHP = fHP;
		m_fHPMax = fMaxHP;
		if (m_fHP > m_fHPMax)
		{
			m_fHP = m_fHPMax;
		}
		else if (m_fHP <= 0f)
		{
			m_fHP = 0f;
		}
		iGameUIBase gameUI = base.m_GameScene.GetGameUI();
		if (gameUI != null && m_fHPMax != 0f)
		{
			gameUI.SetProtraitLife(m_fHP / m_fHPMax, base.UID);
		}
	}

	public override void OnDead(kDeadMode nDeathMode)
	{
		base.isDead = true;
		ResetAI();
		if (m_bStealth)
		{
			SetStealth(false, 0f);
		}
		if (m_fStealthAlphaRate < 1f)
		{
			SetAlpha(1f);
		}
	}

	public override bool OnHit(float fDmg, CWeaponInfoLevel pWeaponLvlInfo = null, string sBodyPart = "")
	{
		if (!base.OnHit(fDmg, pWeaponLvlInfo, sBodyPart))
		{
			return false;
		}
		if (m_fHP <= 0f)
		{
			if (!CGameNetManager.GetInstance().IsConnected() || base.m_GameScene.IsMyself(this))
			{
				OnDead(kDeadMode.None);
			}
		}
		else
		{
			base.isStun = false;
			m_bHurting = true;
			ResetAI();
			PauseFire(true);
		}
		return true;
	}

	public void PauseFire(bool bPause)
	{
		if (m_curWeapon != null)
		{
			m_curWeapon.PauseFire(bPause);
		}
	}

	public override float CalcWeaponDamage(CWeaponInfoLevel weaponlevelinfo = null)
	{
		if (weaponlevelinfo == null)
		{
			weaponlevelinfo = m_curWeaponLvlInfo;
		}
		if (weaponlevelinfo == null)
		{
			return 0f;
		}
		float num = m_Property.GetValue(kProEnum.All_Dmg);
		float num2 = m_Property.GetValue(kProEnum.All_Dmg_Rate);
		float num3 = 0f;
		float num4 = 0f;
		switch (weaponlevelinfo.nAttackMode)
		{
		case 1:
			num3 = m_Property.GetValue(kProEnum.Melee_Dmg);
			num4 = m_Property.GetValue(kProEnum.Melee_Dmg_Rate);
			break;
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
			num3 = m_Property.GetValue(kProEnum.Range_Dmg);
			num4 = m_Property.GetValue(kProEnum.Range_Dmg_Rate);
			break;
		}
		if (num3 != -1f)
		{
			num += num3;
		}
		if (num4 != -1f)
		{
			num2 += num4;
		}
		num3 = 0f;
		num4 = 0f;
		switch (weaponlevelinfo.nType)
		{
		case 0:
			num3 = m_Property.GetValue(kProEnum.Crossbow_Dmg);
			num4 = m_Property.GetValue(kProEnum.Crossbow_Dmg_Rate);
			break;
		case 2:
			num3 = m_Property.GetValue(kProEnum.AutoRifle_Dmg);
			num4 = m_Property.GetValue(kProEnum.AutoRifle_Dmg_Rate);
			break;
		case 3:
			num3 = m_Property.GetValue(kProEnum.ShotGun_Dmg);
			num4 = m_Property.GetValue(kProEnum.ShotGun_Dmg_Rate);
			break;
		case 4:
			num3 = m_Property.GetValue(kProEnum.HoldGun_Dmg);
			num4 = m_Property.GetValue(kProEnum.HoldGun_Dmg_Rate);
			break;
		case 5:
			num3 = m_Property.GetValue(kProEnum.Rocket_Dmg);
			num4 = m_Property.GetValue(kProEnum.Rocket_Dmg_Rate);
			break;
		}
		if (num3 != -1f)
		{
			num += num3;
		}
		if (num4 != -1f)
		{
			num2 += num4;
		}
		float num5 = weaponlevelinfo.fDamage * (1f + num2 / 100f) + num;
		if (num5 < 0f)
		{
			num5 = 0f;
		}
		return num5;
	}

	public override float CalcWeaponShootSpeed(CWeaponInfoLevel weaponlevelinfo = null)
	{
		if (weaponlevelinfo == null)
		{
			weaponlevelinfo = m_curWeaponLvlInfo;
		}
		if (weaponlevelinfo == null)
		{
			return 0f;
		}
		float num = m_Property.GetValue(kProEnum.All_Speed);
		float num2 = 0f;
		switch (weaponlevelinfo.nAttackMode)
		{
		case 1:
			num2 = m_Property.GetValue(kProEnum.Melee_Speed);
			break;
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
			num2 = m_Property.GetValue(kProEnum.Range_Speed);
			break;
		}
		if (num2 != -1f)
		{
			num += num2;
		}
		num2 = 0f;
		switch (weaponlevelinfo.nType)
		{
		case 0:
			num2 = m_Property.GetValue(kProEnum.Crossbow_Speed);
			break;
		case 2:
			num2 = m_Property.GetValue(kProEnum.AutoRifle_Speed);
			break;
		case 3:
			num2 = m_Property.GetValue(kProEnum.ShotGun_Speed);
			break;
		case 4:
			num2 = m_Property.GetValue(kProEnum.HoldGun_Speed);
			break;
		case 5:
			num2 = m_Property.GetValue(kProEnum.Rocket_Speed);
			break;
		}
		if (num2 != -1f)
		{
			num += num2;
		}
		float num3 = weaponlevelinfo.fShootSpeed * (1f - num / 100f);
		if (num3 < 0.1f)
		{
			num3 = 0.1f;
		}
		return num3;
	}

	public override float CalcWeaponBeatBack(CWeaponInfoLevel weaponlevelinfo = null)
	{
		if (weaponlevelinfo == null)
		{
			weaponlevelinfo = m_curWeaponLvlInfo;
		}
		if (weaponlevelinfo == null)
		{
			return 0f;
		}
		float num = m_Property.GetValue(kProEnum.All_BeatBack);
		float num2 = 0f;
		switch (weaponlevelinfo.nAttackMode)
		{
		case 1:
			num2 = m_Property.GetValue(kProEnum.Melee_BeatBack);
			break;
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
			num2 = m_Property.GetValue(kProEnum.Range_BeatBack);
			break;
		}
		if (num2 != -1f)
		{
			num += num2;
		}
		num2 = 0f;
		switch (weaponlevelinfo.nType)
		{
		case 0:
			num2 = m_Property.GetValue(kProEnum.Crossbow_BeatBack);
			break;
		case 2:
			num2 = m_Property.GetValue(kProEnum.AutoRifle_BeatBack);
			break;
		case 3:
			num2 = m_Property.GetValue(kProEnum.ShotGun_BeatBack);
			break;
		case 4:
			num2 = m_Property.GetValue(kProEnum.HoldGun_BeatBack);
			break;
		case 5:
			num2 = m_Property.GetValue(kProEnum.Rocket_BeatBack);
			break;
		}
		if (num2 != -1f)
		{
			num += num2;
		}
		return num;
	}

	public override float CalcCritical(CWeaponInfoLevel weaponlevelinfo = null)
	{
		if (weaponlevelinfo == null)
		{
			weaponlevelinfo = m_curWeaponLvlInfo;
		}
		if (weaponlevelinfo == null)
		{
			return 0f;
		}
		float num = m_Property.GetValue(kProEnum.All_Critical);
		float num2 = 0f;
		switch (weaponlevelinfo.nAttackMode)
		{
		case 1:
			num2 = m_Property.GetValue(kProEnum.Melee_Critical);
			break;
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
			num2 = m_Property.GetValue(kProEnum.Range_Critical);
			break;
		}
		if (num2 != -1f)
		{
			num += num2;
		}
		num2 = 0f;
		switch (weaponlevelinfo.nType)
		{
		case 0:
			num2 = m_Property.GetValue(kProEnum.Crossbow_Critical);
			break;
		case 2:
			num2 = m_Property.GetValue(kProEnum.AutoRifle_Critical);
			break;
		case 3:
			num2 = m_Property.GetValue(kProEnum.ShotGun_Critical);
			break;
		case 4:
			num2 = m_Property.GetValue(kProEnum.HoldGun_Critical);
			break;
		case 5:
			num2 = m_Property.GetValue(kProEnum.Rocket_Critical);
			break;
		}
		if (num2 != -1f)
		{
			num += num2;
		}
		return weaponlevelinfo.fCritical + num + m_Property.GetValue(kProEnum.Critical);
	}

	public override float CalcCriticalDmg(CWeaponInfoLevel weaponlevelinfo = null)
	{
		if (weaponlevelinfo == null)
		{
			weaponlevelinfo = m_curWeaponLvlInfo;
		}
		if (weaponlevelinfo == null)
		{
			return 0f;
		}
		float num = m_Property.GetValue(kProEnum.All_CriticalDmg);
		float num2 = 0f;
		switch (weaponlevelinfo.nAttackMode)
		{
		case 1:
			num2 = m_Property.GetValue(kProEnum.Melee_CriticalDmg);
			break;
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
			num2 = m_Property.GetValue(kProEnum.Range_CriticalDmg);
			break;
		}
		if (num2 != -1f)
		{
			num += num2;
		}
		num2 = 0f;
		switch (weaponlevelinfo.nType)
		{
		case 0:
			num2 = m_Property.GetValue(kProEnum.Crossbow_CriticalDmg);
			break;
		case 2:
			num2 = m_Property.GetValue(kProEnum.AutoRifle_CriticalDmg);
			break;
		case 3:
			num2 = m_Property.GetValue(kProEnum.ShotGun_CriticalDmg);
			break;
		case 4:
			num2 = m_Property.GetValue(kProEnum.HoldGun_CriticalDmg);
			break;
		case 5:
			num2 = m_Property.GetValue(kProEnum.Rocket_CriticalDmg);
			break;
		}
		if (num2 != -1f)
		{
			num += num2;
		}
		return weaponlevelinfo.fCriticalDmg + num + m_Property.GetValue(kProEnum.CriticalDmg);
	}

	public override float CalcProtect(CWeaponInfoLevel weaponlevelinfo = null)
	{
		if (weaponlevelinfo == null)
		{
			weaponlevelinfo = m_curWeaponLvlInfo;
		}
		if (weaponlevelinfo == null)
		{
			return 0f;
		}
		float num = m_Property.GetValue(kProEnum.Protect) + m_Property.GetValue(kProEnum.All_Protect);
		if (num < 0f)
		{
			num = 0f;
		}
		float num2 = 0f;
		switch (weaponlevelinfo.nAttackMode)
		{
		case 1:
			num2 = m_Property.GetValue(kProEnum.Melee_Protect);
			break;
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
			num2 = m_Property.GetValue(kProEnum.Range_Protect);
			break;
		}
		if (num2 != -1f)
		{
			num += num2;
		}
		num2 = 0f;
		switch (weaponlevelinfo.nType)
		{
		case 0:
			num2 = m_Property.GetValue(kProEnum.Crossbow_Protect);
			break;
		case 2:
			num2 = m_Property.GetValue(kProEnum.AutoRifle_Protect);
			break;
		case 3:
			num2 = m_Property.GetValue(kProEnum.ShotGun_Protect);
			break;
		case 4:
			num2 = m_Property.GetValue(kProEnum.HoldGun_Protect);
			break;
		case 5:
			num2 = m_Property.GetValue(kProEnum.Rocket_Protect);
			break;
		}
		if (num2 != -1f)
		{
			num += num2;
		}
		if (num < 0f)
		{
			num = 0f;
		}
		float num3 = MyUtils.formula_armor2protect(num);
		if (num3 > 95f)
		{
			num3 = 95f;
		}
		UnityEngine.Debug.Log(base.UID + " protect = " + num3);
		return num3;
	}

	public Vector3 GetUpBodyPos()
	{
		if (m_SpineUpBody == null)
		{
			return m_ModelTransform.position;
		}
		return m_SpineUpBody.position;
	}

	public virtual void TakeItem(int nItemID, GameObject ItemObj)
	{
		m_nCarryItemID = nItemID;
		m_CarryItemObj = ItemObj;
		Transform bone = GetBone(5);
		if (bone == null)
		{
			return;
		}
		m_CarryItemObj.transform.parent = bone;
		m_CarryItemObj.transform.localPosition = Vector3.zero;
		m_CarryItemObj.transform.localRotation = Quaternion.identity;
		GameObject gameObject = PrefabManager.Get(301);
		if (!(gameObject == null))
		{
			m_CarryBagObj = (GameObject)Object.Instantiate(gameObject);
			if (m_CarryBagObj != null)
			{
				m_CarryBagObj.transform.parent = bone;
				m_CarryBagObj.transform.localPosition = Vector3.zero;
				m_CarryBagObj.transform.localRotation = Quaternion.identity;
			}
			CItemInfoLevel itemInfo = base.m_GameData.GetItemInfo(nItemID, 1);
			if (itemInfo != null)
			{
				m_nCarryItemBuffSlot = AddBuff(itemInfo.nTakenBuff, 0f);
			}
			if (m_CharacterModelInterface != null && m_CharacterModelInterface.mPendant != null)
			{
				m_CharacterModelInterface.mPendant.SetActiveRecursively(false);
			}
			PlayAudio("UI_Egg_get");
		}
	}

	public virtual void DropItem()
	{
		CItemInfo itemInfo = base.m_GameData.GetItemInfo(m_nCarryItemID);
		if (itemInfo != null)
		{
			DelBuffBySlot(m_nCarryItemBuffSlot);
			m_nCarryItemBuffSlot = -1;
		}
		m_nCarryItemID = -1;
		if (m_CarryItemObj != null)
		{
			Object.Destroy(m_CarryItemObj);
			m_CarryItemObj = null;
		}
		if (m_CarryBagObj != null)
		{
			Object.Destroy(m_CarryBagObj);
			m_CarryBagObj = null;
		}
		if (m_CharacterModelInterface != null && m_CharacterModelInterface.mPendant != null)
		{
			m_CharacterModelInterface.mPendant.SetActiveRecursively(true);
		}
		PlayAudio("UI_Egg_unpack");
	}

	public bool IsTakenItem()
	{
		return m_nCarryItemID != -1;
	}

	public int GetCarryItem()
	{
		return m_nCarryItemID;
	}

	public virtual void UseSkill(int nSkill, int nSkillLevel)
	{
		UnityEngine.Debug.Log(base.UID + " useskill " + nSkill + " " + nSkillLevel);
		CSkillInfoLevel skillInfo = base.m_GameData.GetSkillInfo(nSkill, nSkillLevel);
		if (skillInfo == null)
		{
			return;
		}
		switch (skillInfo.nSkillMode)
		{
		case 1:
			m_UseSkill = new CUseSkillOnce();
			break;
		case 6:
			m_UseSkill = new CUseSkillBump();
			break;
		}
		if (m_UseSkill != null)
		{
			if (skillInfo.nTargetLimit == 3)
			{
				m_UseSkill.Initialize(skillInfo, this);
			}
			else
			{
				m_UseSkill.Initialize(skillInfo);
			}
			m_UseSkill.OnEnter(this);
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
			m_Behavior.Install(behavior);
		}
	}

	public virtual bool IsCanAttack()
	{
		return !m_bHurting && !m_bBeatBack && !base.isDead && !m_bBumping && !m_bStun;
	}

	public virtual void AddExp(int nExp)
	{
	}

	public virtual void ClearAllStatus()
	{
		m_bHurting = false;
		m_bBumping = false;
		m_bBeatBack = false;
		m_bStealth = false;
		m_bStun = false;
	}

	public virtual void SetAvatar(int nIndex, int nAvatarID)
	{
		if (m_Avatar == null || m_Avatar.m_AvatarPart == null || nIndex < 0 || nIndex >= m_Avatar.m_AvatarPart.Length || base.m_GameData == null || base.m_GameData.m_AvatarCenter == null)
		{
			return;
		}
		CAvatarInfo cAvatarInfo = base.m_GameData.m_AvatarCenter.Get(nAvatarID);
		if (cAvatarInfo != null)
		{
			if (cAvatarInfo.m_sModel.Length > 0)
			{
				string sPath = iMacroDefine.path_model_root + "/" + cAvatarInfo.m_sModel;
				string empty = string.Empty;
				empty = ((!cAvatarInfo.m_bLinkChar) ? (iMacroDefine.path_texture_root + "/" + cAvatarInfo.m_sTexture + "_m") : (iMacroDefine.path_texture_root + "/" + cAvatarInfo.m_sTexture + "_" + base.ID.ToString("d2") + "_m"));
				m_Avatar.ReplaceAvatar(nIndex, PrefabManager.Get(sPath), PrefabManager.GetObject(empty) as Texture);
			}
			if (cAvatarInfo.m_sEffect.Length > 0)
			{
				string sPath2 = iMacroDefine.path_effect_root + "/" + cAvatarInfo.m_sEffect;
				m_Avatar.ReplaceAvatarEffect(nIndex, PrefabManager.Get(sPath2));
			}
		}
	}

	public void MoveTo(Vector3 v3Dst)
	{
		m_bFinalPath = false;
		m_ltNetPath.Add(v3Dst);
	}

	public void MoveTo(List<Vector3> ltPath)
	{
		m_bFinalPath = false;
		m_ltNetPath.AddRange(ltPath);
	}

	public void MoveStop(Vector3 v3Dst)
	{
		m_bFinalPath = true;
		m_ltNetPath.Add(v3Dst);
	}

	public void MoveStop(List<Vector3> ltPath)
	{
		m_bFinalPath = true;
		m_ltNetPath.AddRange(ltPath);
	}

	public void AimTo(Vector3 v3AimPoint)
	{
		m_bNetAimFresh = true;
		m_v3NetAimPoint = v3AimPoint;
	}

	public void Shoot(bool bShoot)
	{
		m_bNetShoot = bShoot;
	}

	public void UseSkill(int nSkillID, int nSkillLevel, int nTargetUID)
	{
		m_bNetSkill = true;
		m_nNetSkillID = nSkillID;
		m_pNetSkillTarget = base.m_GameScene.GetPlayer(nTargetUID);
	}

	public override void Revive(float fHP)
	{
		base.Revive(fHP);
		GameObject gameObject = base.m_GameScene.AddEffect(base.Pos, Vector3.forward, 2f, 1300);
		if (gameObject != null)
		{
			gameObject.transform.parent = GetBone(3);
			gameObject.transform.localPosition = Vector3.zero;
		}
		if (CurCharInfoLevel.isMale)
		{
			PlayAudio("UI_Upgrade_Male");
		}
		else
		{
			PlayAudio("UI_Upgrade_Female");
		}
	}

	public void LevelUp(int nLevel)
	{
		Level = nLevel;
		m_bUpdateProBuff = true;
		m_bUpdateProSkill = true;
		GameObject gameObject = base.m_GameScene.AddEffect(Vector3.zero, Vector3.one, 5f, 1300);
		gameObject.transform.parent = GetBone(3);
		gameObject.transform.localPosition = new Vector3(0f, 0.01f, 0f);
		gameObject.transform.localRotation = Quaternion.identity;
		iGameUIBase gameUI = base.m_GameScene.GetGameUI();
		if (gameUI != null)
		{
			gameUI.SetProtraitLevel(nLevel.ToString(), base.UID);
			gameUI.PlayProtraitLevelAnim(base.UID);
		}
		InitChar(base.ID, Level, 0, AvatarHead, AvatarUpper, AvatarLower, AvatarHeadup, AvatarNeck, AvatarWrist, AvatarBadge, AvatarStone);
		if (CurCharInfoLevel.isMale)
		{
			PlayAudio("UI_Upgrade_Male");
		}
		else
		{
			PlayAudio("UI_Upgrade_Female");
		}
	}

	public void SetPlayerHUD(string sName)
	{
		if (m_NetPlayerHUD == null)
		{
			iGameUIBase gameUI = base.m_GameScene.GetGameUI();
			if (gameUI == null)
			{
				return;
			}
			m_NetPlayerHUD = gameUI.CreatePlayerHUD(this);
		}
		m_NetPlayerHUD.SetName(sName);
	}
}
