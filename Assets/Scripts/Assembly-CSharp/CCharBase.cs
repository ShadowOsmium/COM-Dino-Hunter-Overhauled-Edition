using System;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class CCharBase : MonoBehaviour
{
	protected class ComInfo
	{
		public int uid;

		public Vector3 dir;

		public ComInfo(int uid, Vector3 dir)
		{
			this.uid = uid;
			this.dir = dir;
		}
	}

	public string m_sName;

	[SerializeField]
	protected int m_nUID;

	[SerializeField]
	protected int m_nID;

	[SerializeField]
	protected int m_nLevel;

	[SerializeField]
	protected kCharType m_nType;

	[SerializeField]
	protected kCampType m_nCampType;

	[SerializeField]
	protected bool m_bDead;

	[SerializeField]
	protected bool m_bDestroy;

	protected GameObject m_Model;

	protected GameObject m_ModelEntity;

	protected Transform m_ModelTransform;

	protected Transform m_ModelEntityTransform;

	[SerializeField]
	protected Transform m_ModelHead;

	[SerializeField]
	protected Transform m_ModelHeadUp;

	[SerializeField]
	protected Transform m_ModelMouse;

	[SerializeField]
	protected Transform m_ModelBody;

	[SerializeField]
	protected Transform m_ModelBodyDown;

	[SerializeField]
	protected Transform m_ModelBack;

	[SerializeField]
	protected Transform m_BloodEffectPos;

	[SerializeField]
	protected Transform m_EyeLeft;

	[SerializeField]
	protected Transform m_EyeRight;

	[SerializeField]
	public Transform m_BlackGear;

	[SerializeField]
	public iEntityDrop m_EntityDrop;

	protected Renderer[] m_ModelRenderer;

	protected float m_fPitchMin;

	protected float m_fPitchMax;

	protected float m_fYawMin;

	protected float m_fYawMax;

	protected float m_fPitch;

	protected float m_fYaw;

	protected float m_fRoll;

	protected CAnimData m_AnimData;

	protected CAnimPlay m_AnimManager;

	protected Dictionary<kAudioEnum, string> m_AudioData;

	protected bool m_bActive;

	protected string m_ActiveAnim;

	protected float m_fTimeScale;

	protected iBuffData[] m_Buff;

	protected CProBase m_Property;

	protected bool m_bUpdateProBuff;

	protected bool m_bUpdateProSkill;

	protected float m_fHP;

	protected float m_fHPMax;

	public bool m_bStun;

	protected float m_fStunTime;

	protected bool m_bStealth;

	protected float m_fStealthTime;

	protected float m_fStealthAlphaRate;

	protected float m_fStealthAlphaCur;

	protected float m_fStealthAlphaSrc;

	protected float m_fStealthAlphaDst;

	protected CFixPos m_FixPos;

	protected List<int> m_ltMeleeAttaker;

	protected bool m_bUpdateMeleePos;

	protected TAudioController m_AudioController;

	protected List<int> m_ltSkillPassive;

	public CCharBase m_Target;

	[NonSerialized]
	public bool m_bHurting;

	[NonSerialized]
	public kAnimEnum m_HurtAnim;

	[NonSerialized]
	protected Behavior m_Behavior;

	[NonSerialized]
	protected Task m_curTask;

	[NonSerialized]
	public bool m_bBeatBack;

	[NonSerialized]
	public float m_fBeatBackDis;

	[NonSerialized]
	public Vector3 m_v3BeatBackDir;

	[NonSerialized]
	public Vector3 m_v3BeatBackPoint;

	public bool m_bBumping;

	public bool m_bMoribund;

	public float m_fMoribundTime;

	protected List<ComInfo> m_ltCom = new List<ComInfo>();

	protected iGameSceneBase m_GameScene
	{
		get
		{
			return iGameApp.GetInstance().m_GameScene;
		}
	}

	protected iGameState m_GameState
	{
		get
		{
			return iGameApp.GetInstance().m_GameState;
		}
	}

	protected iGameData m_GameData
	{
		get
		{
			return iGameApp.GetInstance().m_GameData;
		}
	}

	public int UID
	{
		get
		{
			return m_nUID;
		}
		set
		{
			m_nUID = value;
		}
	}

	public int ID
	{
		get
		{
			return m_nID;
		}
		set
		{
			m_nID = value;
		}
	}

	public int Level
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

	public float CurHP
	{
		get
		{
			return m_fHP;
		}
	}

	public float MaxHP
	{
		get
		{
			return m_fHPMax;
		}
	}

	public kCharType CharType
	{
		get
		{
			return m_nType;
		}
		set
		{
			m_nType = value;
		}
	}

	public Transform Transform
	{
		get
		{
			return m_ModelTransform;
		}
	}

	public Vector3 Dir2D
	{
		get
		{
			Vector3 forward = m_ModelTransform.forward;
			forward.y = 0f;
			return forward;
		}
		set
		{
			Vector3 vector = value;
			vector.y = 0f;
			if (vector != Vector3.zero)
			{
				m_ModelTransform.forward = vector;
			}
		}
	}

	public Vector3 Dir3D
	{
		get
		{
			return m_ModelTransform.forward;
		}
		set
		{
			if (value != Vector3.zero)
			{
				m_ModelTransform.forward = value;
			}
		}
	}

	public Vector3 Pos
	{
		get
		{
			return m_ModelTransform.position;
		}
		set
		{
			m_ModelTransform.position = value;
		}
	}

	public bool isDead
	{
		get
		{
			return m_bDead;
		}
		set
		{
			m_bDead = value;
		}
	}

	public bool isNeedDestroy
	{
		get
		{
			return m_bDestroy;
		}
		set
		{
			m_bDestroy = value;
		}
	}

	public CProBase Property
	{
		get
		{
			return m_Property;
		}
	}

	public kCampType CampType
	{
		get
		{
			return m_nCampType;
		}
		set
		{
			m_nCampType = value;
		}
	}

	public bool isStealth
	{
		get
		{
			return m_bStealth;
		}
	}

	public bool isStun
	{
		get
		{
			return m_bStun;
		}
		set
		{
			m_bStun = value;
		}
	}

	public float StunTime
	{
		get
		{
			return m_fStunTime;
		}
		set
		{
			m_fStunTime = value;
		}
	}

	public GameObject Entity
	{
		get
		{
			return m_ModelEntity;
		}
	}

	public Behavior GetBehavior
	{
		get
		{
			return m_Behavior;
		}
		set
		{
			m_Behavior = value;
		}
	}

	public string TaskStr
	{
		get
		{
			if (m_curTask == null)
			{
				return "no task";
			}
			return m_curTask.ToString();
		}
	}

	public void Awake()
	{
		m_nType = kCharType.None;
		m_AnimManager = new CAnimPlay();
		m_AnimData = new CAnimData();
		m_AudioData = new Dictionary<kAudioEnum, string>();
		m_bActive = true;
		m_fTimeScale = 1f;
		m_Buff = new iBuffData[10];
		for (int i = 0; i < 10; i++)
		{
			m_Buff[i] = new iBuffData();
			m_Buff[i].m_nID = 0;
		}
		m_bUpdateProBuff = false;
		m_bUpdateProSkill = false;
		m_bDead = false;
		m_bDestroy = false;
		m_ltMeleeAttaker = new List<int>();
		m_ModelEntity = base.transform.Find("Entity").gameObject;
		m_ModelEntityTransform = m_ModelEntity.transform;
		m_Model = base.gameObject;
		m_Model.name = "object_" + m_nUID;
		m_ModelTransform = m_Model.transform;
		m_ModelTransform.position = new Vector3(0f, -10000f, 0f);
		m_ModelTransform.forward = Vector3.forward;
		m_ModelRenderer = m_ModelEntityTransform.GetComponentsInChildren<Renderer>();
		m_ModelHeadUp = m_ModelTransform.Find("HeadUp");
		m_EntityDrop = m_ModelEntity.GetComponent<iEntityDrop>();
		if (m_AudioController == null)
		{
			m_AudioController = m_ModelEntity.AddComponent<TAudioController>();
		}
		m_AnimManager.Initialize(m_ModelEntity, m_AnimData);
		m_ltSkillPassive = new List<int>();
		m_FixPos = new CFixPos();
		m_bBeatBack = false;
		m_bStealth = false;
		m_fStealthAlphaRate = 1f;
		m_fStealthAlphaCur = 1f;
		m_bMoribund = false;
		m_fMoribundTime = 0f;
		m_bBumping = false;
		if (m_GameScene != null && m_GameScene.IsSkyScene() && m_ModelEntity != null)
		{
			iTexture2D component = m_ModelEntity.GetComponent<iTexture2D>();
			if (component != null)
			{
				component.Show(false);
				component.enabled = false;
			}
		}
	}

	public void Start()
	{
	}

	public void Update()
	{
		if (!m_bActive)
		{
			return;
		}
		float num = Time.deltaTime * m_fTimeScale;
		UpdateBuff(num);
		if (m_AnimManager != null)
		{
			m_AnimManager.Update(num);
		}
		if (m_bStealth)
		{
			m_fStealthTime -= num;
			if (m_fStealthTime <= 0f)
			{
				SetStealth(false, 0f);
			}
		}
		if (m_fStealthAlphaRate < 1f)
		{
			m_fStealthAlphaRate += num;
			m_fStealthAlphaCur = MyUtils.Lerp(m_fStealthAlphaSrc, m_fStealthAlphaDst, m_fStealthAlphaRate);
			SetAlpha(m_fStealthAlphaCur);
		}
	}

	public void FixedUpdate()
	{
		float deltaTime = Time.deltaTime;
	}

	public void LateUpdate()
	{
		if (m_bActive)
		{
			if (m_bUpdateProBuff)
			{
				m_Property.UpdateBuff(this);
				m_bUpdateProBuff = false;
			}
			if (m_bUpdateProSkill)
			{
				m_Property.UpdateSkill(this);
				m_bUpdateProSkill = false;
			}
			if (m_FixPos != null)
			{
				m_FixPos.LateUpdate(this, Time.deltaTime);
			}
		}
	}

	public CFixPos GetFixState()
	{
		return m_FixPos;
	}

	public virtual void Destroy()
	{
		StopAllAudio();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public virtual void UpdateLogic(float deltaTime)
	{
	}

	public virtual void InitAnimData()
	{
	}

	public virtual void InitAudioData()
	{
	}

	public void SetColor(Color color)
	{
		if (m_ModelRenderer == null)
		{
			return;
		}
		for (int i = 0; i < m_ModelRenderer.Length; i++)
		{
			if (!(m_ModelRenderer[i] == null) && !(m_ModelRenderer[i] is ParticleSystemRenderer))
			{
				m_ModelRenderer[i].material.SetColor("_Color", color);
			}
		}
	}

	public void SetAlpha(float fAlpha)
	{
		if (m_ModelRenderer == null)
		{
			return;
		}
		for (int i = 0; i < m_ModelRenderer.Length; i++)
		{
			if (!(m_ModelRenderer[i] == null) && !(m_ModelRenderer[i] is ParticleSystemRenderer))
			{
				Color color = m_ModelRenderer[i].material.GetColor("_Color");
				color.a = Mathf.Clamp01(fAlpha);
				m_ModelRenderer[i].material.SetColor("_Color", color);
			}
		}
	}

	public void SetStealth(bool bStealth, float fTime = 0f)
	{
		m_bStealth = bStealth;
		if (m_bStealth)
		{
			m_fStealthTime = fTime;
		}
		m_fStealthAlphaSrc = m_fStealthAlphaCur;
		m_fStealthAlphaDst = ((!m_bStealth) ? 1f : 0.2f);
		m_fStealthAlphaRate = 0f;
		Debug.Log("SetStealth " + m_fStealthAlphaDst + " " + UID);
	}

	public virtual void SetStun(bool bStun, float fTime = 0f)
	{
		m_bStun = bStun;
		if (m_bStun)
		{
			Debug.Log("stun " + fTime);
			m_fStunTime = fTime;
			ResetAI();
		}
	}

	public void SetRotateLimit(float pitchmin, float pitchmax, float yawmin, float yawmax)
	{
		m_fPitchMin = pitchmin;
		m_fPitchMax = pitchmax;
		m_fYawMin = yawmin;
		m_fYawMax = yawmax;
	}

	public virtual float CrossAnim(kAnimEnum type, WrapMode mode, float fadetime = 0.3f, float speed = 1f, float time = 0f)
	{
		m_AnimManager.CrossFade(type, mode, fadetime, speed, time);
		return m_AnimManager.GetAnimLen(type) / speed;
	}

	public virtual float PlayAnim(kAnimEnum type, WrapMode mode, float speed = 1f, float time = 0f)
	{
		m_AnimManager.PlayAnim(type, mode, speed, time);
		return m_AnimManager.GetAnimLen(type) / speed;
	}

	public virtual float PlayAnimMix(kAnimEnum type, WrapMode mode, float speed = 1f)
	{
		m_AnimManager.PlayAnimMix(type, mode, speed);
		return m_AnimManager.GetAnimLen(type) / speed;
	}

	public virtual float PlayAnimMix(kAnimEnum type, WrapMode mode, Transform bone, float speed = 1f)
	{
		m_AnimManager.PlayAnimMix(type, mode, bone, speed);
		return m_AnimManager.GetAnimLen(type) / speed;
	}

	public virtual float CrossAnimMix(kAnimEnum type, WrapMode mode, float fadetime = 0.3f, float speed = 1f)
	{
		m_AnimManager.CrossFadeMix(type, mode, fadetime, speed);
		return m_AnimManager.GetAnimLen(type) / speed;
	}

	public virtual float CrossAnimMix(kAnimEnum type, WrapMode mode, Transform bone, float fadetime = 0.3f, float speed = 1f)
	{
		m_AnimManager.CrossFadeMix(type, mode, bone, fadetime, speed);
		return m_AnimManager.GetAnimLen(type) / speed;
	}

	public virtual void StopAction(kAnimEnum type)
	{
		if (IsActionPlaying(type))
		{
			m_AnimManager.Stop(type);
		}
	}

	public virtual bool IsActionPlaying(kAnimEnum type)
	{
		return m_AnimManager.IsAnimPlaying(type);
	}

	public virtual float GetActionLen(kAnimEnum type)
	{
		return m_AnimManager.GetAnimLen(type);
	}

	public virtual void SetActionSpeed(kAnimEnum type, float speed)
	{
		m_AnimManager.SetAnimSpeed(type, speed);
	}

	public virtual void SetActionLayer(kAnimEnum type, int nLayer)
	{
		m_AnimManager.SetAnimLayer(type, nLayer);
	}

	public bool IsMob()
	{
		return m_nType == kCharType.Mob;
	}

	public bool IsBoss()
	{
		return m_nType == kCharType.Boss;
	}

	public bool IsPlayer()
	{
		return m_nType == kCharType.Player;
	}

	public bool IsUser()
	{
		return m_nType == kCharType.User;
	}

	public bool IsMonster()
	{
		return IsMob() || IsBoss();
	}

	public int AddBuff(int nID, float fTime, int nFromSkill = -1)
	{
		CBuffInfo buffInfo = m_GameData.GetBuffInfo(nID);
		if (buffInfo == null)
		{
			return -1;
		}
		if (buffInfo.nSlot < 0 || buffInfo.nSlot >= m_Buff.Length)
		{
			return -1;
		}
		iBuffData iBuffData2 = m_Buff[buffInfo.nSlot];
		if (iBuffData2.m_nID != 0 && iBuffData2.m_nPriority > buffInfo.nPriority)
		{
			return -1;
		}
		iBuffData2.EffectClear();
		if (iBuffData2.m_sAudio.Length > 0)
		{
			StopAudio(iBuffData2.m_sAudio);
			iBuffData2.m_sAudio = string.Empty;
		}
		iBuffData2.m_nID = nID;
		iBuffData2.m_nType = buffInfo.nType;
		iBuffData2.m_nSlot = buffInfo.nSlot;
		iBuffData2.m_nPriority = buffInfo.nPriority;
		iBuffData2.m_curBuffInfo = buffInfo;
		iBuffData2.m_fTime = fTime;
		iBuffData2.m_nFromSkill = nFromSkill;
		if (buffInfo.arrEffAdd[0] > 0)
		{
			Debug.Log(buffInfo.arrEffAdd[0]);
			iBuffData2.EffectAdd(buffInfo.arrEffAdd[0], GetBone(buffInfo.arrEffAdd[1]));
		}
		if (buffInfo.arrEffHold[0] > 0)
		{
			Debug.Log(buffInfo.arrEffHold[0]);
			iBuffData2.EffectHold(buffInfo.arrEffHold[0], GetBone(buffInfo.arrEffHold[1]));
		}
		if (buffInfo.sAudioEffAdd.Length > 0)
		{
			PlayAudio(buffInfo.sAudioEffAdd);
		}
		if (buffInfo.sAudioEffHold.Length > 0)
		{
			PlayAudio(buffInfo.sAudioEffHold);
			iBuffData2.m_sAudio = buffInfo.sAudioEffHold;
		}
		if (buffInfo.fEffectTime <= 0f)
		{
			iGameLogic gameLogic = m_GameScene.GetGameLogic();
			if (gameLogic != null)
			{
				iGameLogic.HitInfo hitinfo = new iGameLogic.HitInfo();
				gameLogic.CaculateFunc(this, this, buffInfo.arrFunc, buffInfo.arrValueX, buffInfo.arrValueY, ref hitinfo);
			}
		}
		else
		{
			iBuffData2.m_fEffectTimeCount = 0f;
		}
		m_bUpdateProBuff = true;
		iGameApp.GetInstance().ScreenLog("add buff " + nID + " for " + fTime + "s");
		return buffInfo.nSlot;
	}

	public void DelBuffBySlot(int nSlot)
	{
		if (nSlot < 0 || nSlot >= m_Buff.Length)
		{
			return;
		}
		iBuffData iBuffData2 = m_Buff[nSlot];
		if (iBuffData2.m_nID == 0)
		{
			return;
		}
		CBuffInfo buffInfo = m_GameData.GetBuffInfo(iBuffData2.m_nID);
		if (buffInfo != null)
		{
			if (buffInfo.arrEffDel[0] > 0)
			{
				iBuffData2.EffectAdd(buffInfo.arrEffDel[0], GetBone(buffInfo.arrEffDel[1]));
			}
			if (buffInfo.sAudioEffDel.Length > 0)
			{
				PlayAudio(buffInfo.sAudioEffDel);
			}
		}
		iBuffData2.EffectClear();
		if (iBuffData2.m_sAudio.Length > 0)
		{
			StopAudio(iBuffData2.m_sAudio);
			iBuffData2.m_sAudio = string.Empty;
		}
		iGameApp.GetInstance().ScreenLog("del buff " + iBuffData2.m_nID);
		iBuffData2.m_nID = 0;
		m_bUpdateProBuff = true;
	}

	public void DelBuffByID(int nID)
	{
		for (int i = 0; i < m_Buff.Length; i++)
		{
			if (m_Buff[i].m_nID == nID)
			{
				DelBuffBySlot(i);
				break;
			}
		}
	}

	public void RemoveAllBuff()
	{
		for (int i = 0; i < m_Buff.Length; i++)
		{
			DelBuffBySlot(i);
		}
	}

	protected void UpdateBuff(float deltaTime)
	{
		for (int i = 0; i < m_Buff.Length; i++)
		{
			if (m_Buff[i].m_nID == 0)
			{
				continue;
			}
			iBuffData iBuffData2 = m_Buff[i];
			if (iBuffData2.m_fTime <= 0f)
			{
				continue;
			}
			iBuffData2.m_fTime -= deltaTime;
			if (iBuffData2.m_fTime <= 0f)
			{
				DelBuffBySlot(i);
			}
			CBuffInfo curBuffInfo = iBuffData2.m_curBuffInfo;
			if (curBuffInfo == null || curBuffInfo.fEffectTime == 0f)
			{
				continue;
			}
			iBuffData2.m_fEffectTimeCount += deltaTime;
			if (!(iBuffData2.m_fEffectTimeCount < curBuffInfo.fEffectTime))
			{
				iBuffData2.m_fEffectTimeCount = 0f;
				iGameLogic gameLogic = m_GameScene.GetGameLogic();
				if (gameLogic != null)
				{
					iGameLogic.HitInfo hitinfo = new iGameLogic.HitInfo();
					gameLogic.CaculateFunc(this, this, curBuffInfo.arrFunc, curBuffInfo.arrValueX, curBuffInfo.arrValueY, ref hitinfo);
				}
			}
		}
	}

	public iBuffData GetBuffBySlot(int nSlot)
	{
		if (nSlot < 0 || nSlot >= m_Buff.Length)
		{
			return null;
		}
		return m_Buff[nSlot];
	}

	public void Yaw(float angle)
	{
		m_fYaw += angle;
		MyUtils.LimitAngle(ref m_fYaw, m_fYawMin, m_fYawMax);
	}

	public void SetYaw(float angle)
	{
		m_fYaw = angle;
		MyUtils.LimitAngle(ref m_fYaw, m_fYawMin, m_fYawMax);
	}

	public void Pitch(float angle)
	{
		m_fPitch += angle;
		MyUtils.LimitAngle(ref m_fPitch, m_fPitchMin, m_fPitchMax);
	}

	public void SetPitch(float angle)
	{
		m_fPitch = angle;
		MyUtils.LimitAngle(ref m_fPitch, m_fPitchMin, m_fPitchMax);
	}

	public float GetYaw()
	{
		return m_fYaw;
	}

	public float GetPitch()
	{
		return m_fPitch;
	}

	public void Roll(float angle)
	{
		m_fRoll += angle;
	}

	public void AddMeleeAttacker(int nUID)
	{
		if (!m_ltMeleeAttaker.Contains(nUID))
		{
			m_ltMeleeAttaker.Add(nUID);
			m_bUpdateMeleePos = true;
		}
	}

	public void DelMeleeAttacker(int nUID)
	{
		if (m_ltMeleeAttaker.Contains(nUID))
		{
			m_ltMeleeAttaker.Remove(nUID);
		}
	}

	public void UpdateMelleAttackerPos(float deltaTime)
	{
		if (m_ltMeleeAttaker.Count < 2 || !m_bUpdateMeleePos)
		{
			return;
		}
		m_bUpdateMeleePos = false;
		ReCompositor();
		for (int i = 0; i < m_ltCom.Count; i++)
		{
			if (i == m_ltCom.Count / 2)
			{
				continue;
			}
			CCharMob mob = m_GameScene.GetMob(m_ltCom[i].uid);
			if (mob == null)
			{
				continue;
			}
			CMobInfoLevel mobInfo = mob.GetMobInfo();
			if (mobInfo != null)
			{
				CFixPos fixState = mob.GetFixState();
				if (fixState != null)
				{
					fixState.FixPos(mob.Pos, new Vector3(Pos.x, mob.Pos.y, Pos.z) + m_ltCom[i].dir * mobInfo.fMeleeRange, 1f);
				}
			}
		}
	}

	protected void ReCompositor()
	{
		m_ltCom.Clear();
		for (int i = 0; i < m_ltMeleeAttaker.Count; i++)
		{
			CCharBase mob = m_GameScene.GetMob(m_ltMeleeAttaker[i]);
			if (mob == null)
			{
				continue;
			}
			Vector3 vector = mob.Pos - Pos;
			vector.y = 0f;
			if (vector.magnitude > 10f)
			{
				continue;
			}
			vector.Normalize();
			if (m_ltCom.Count < 1)
			{
				m_ltCom.Add(new ComInfo(mob.UID, vector));
				continue;
			}
			bool flag = false;
			for (int j = 0; j < m_ltCom.Count; j++)
			{
				if (Vector3.Cross(vector, m_ltCom[j].dir).y >= 0f)
				{
					flag = true;
					m_ltCom.Insert(j, new ComInfo(mob.UID, vector));
					break;
				}
			}
			if (!flag)
			{
				m_ltCom.Add(new ComInfo(mob.UID, vector));
			}
		}
		if (m_ltCom.Count < 2)
		{
			return;
		}
		int num = m_ltCom.Count / 2;
		ComInfo comInfo = m_ltCom[num];
		int num2 = num;
		while (--num2 >= 0)
		{
			ComInfo comInfo2 = m_ltCom[num2];
			if (Vector3.Cross(comInfo2.dir, comInfo.dir).y < 0f || Vector3.Dot(comInfo2.dir, comInfo.dir) > iMacroDefine.MeleeAngleCos)
			{
				comInfo2.dir = Quaternion.Euler(new Vector3(0f, -15f, 0f)) * comInfo.dir;
			}
			comInfo = comInfo2;
		}
		comInfo = m_ltCom[num];
		num2 = num;
		while (++num2 < m_ltCom.Count)
		{
			ComInfo comInfo3 = m_ltCom[num2];
			if (Vector3.Cross(comInfo3.dir, comInfo.dir).y > 0f || Vector3.Dot(comInfo3.dir, comInfo.dir) > iMacroDefine.MeleeAngleCos)
			{
				comInfo3.dir = Quaternion.Euler(new Vector3(0f, 15f, 0f)) * comInfo.dir;
			}
			comInfo = comInfo3;
		}
	}

	public virtual void AddHP(float fHP)
	{
	}

	public virtual void SetHP(float fHP, float fMaxHP)
	{
	}

	public virtual bool OnHit(float fDmg, CWeaponInfoLevel pWeaponLvlInfo = null, string sBodyPart = "")
	{
		if (!CGameNetManager.GetInstance().IsConnected() || CharType != kCharType.Player)
		{
			AddHP(fDmg);
		}
		return true;
	}

	public virtual void OnDead(kDeadMode nDeathMode)
	{
	}

	public virtual Vector3 GetBloodPos(Vector3 v3Pos, Vector3 v3Dir)
	{
		RaycastHit[] array = Physics.RaycastAll(v3Pos, v3Dir, v3Dir.magnitude, 201326592);
		if (array.Length < 1)
		{
			return m_ModelHead.position;
		}
		for (int i = 0; i < array.Length; i++)
		{
			CCharBase component = array[i].transform.root.GetComponent<CCharBase>();
			if (component == this)
			{
				return array[i].point;
			}
		}
		return m_ModelHead.position;
	}

	public void ResetAI()
	{
		if (m_Behavior != null)
		{
			if (m_curTask != null)
			{
				m_curTask.OnExit(this);
			}
			m_Behavior.Reset();
		}
	}

	public void SetCurTask(Task task)
	{
		m_curTask = task;
	}

	public void PlayAudio(kAudioEnum nType)
	{
		if (m_AudioData != null && m_AudioData.ContainsKey(nType))
		{
			PlayAudio(m_AudioData[nType]);
		}
	}

	public void StopAudio(kAudioEnum nType)
	{
		if (m_AudioData != null && m_AudioData.ContainsKey(nType))
		{
			StopAudio(m_AudioData[nType]);
		}
	}

	public void PlayAudio(string sPrefabName)
	{
		if (!(m_AudioController == null) && sPrefabName.Length >= 1)
		{
			m_AudioController.PlayAudio(sPrefabName);
		}
	}

	public void StopAudio(string sPrefabName)
	{
		if (!(m_AudioController == null) && sPrefabName.Length >= 1)
		{
			m_AudioController.StopAudio(sPrefabName);
		}
	}

	public void StopAllAudio()
	{
		if (m_AudioController == null)
		{
			return;
		}
		Transform transform = m_AudioController.transform.Find("Audio");
		if (transform == null)
		{
			return;
		}
		foreach (Transform item in transform)
		{
			m_AudioController.StopAudio(item.name);
			UnityEngine.Object.Destroy(item.gameObject);
		}
	}

	public virtual void BeatBack(Vector3 v3Dir, float fDis)
	{
		if (!IsBoss())
		{
			m_bBeatBack = true;
			m_v3BeatBackDir = v3Dir.normalized;
			m_v3BeatBackDir.y = 0f;
			m_fBeatBackDis = fDis;
			m_bHurting = false;
			ResetAI();
		}
	}

	public virtual void BeatBack(Vector3 v3Dst)
	{
		if (!isDead)
		{
			m_v3BeatBackPoint = v3Dst;
			Vector3 v3Dir = v3Dst - Pos;
			float magnitude = v3Dir.magnitude;
			BeatBack(v3Dir, magnitude);
		}
	}

	public virtual void Revive(float fHP)
	{
		isDead = false;
		m_bHurting = false;
		m_bBeatBack = false;
		m_bBumping = false;
		AddHP(fHP);
		CrossAnim(kAnimEnum.Idle, WrapMode.Loop, 0.3f, 1f, 0f);
		ResetAI();
	}

	public virtual bool GetSkillPassiveList(ref List<int> ltSkillPassive)
	{
		if (m_ltSkillPassive == null)
		{
			return false;
		}
		foreach (int item in m_ltSkillPassive)
		{
			ltSkillPassive.Add(item);
		}
		return true;
	}

	public bool IsAlly(CCharBase charbase)
	{
		return CampType == charbase.CampType;
	}

	public Transform GetBone(int part)
	{
		switch (part)
		{
		case 0:
			if (m_ModelHeadUp != null)
			{
				return m_ModelHeadUp;
			}
			break;
		case 1:
			if (m_ModelHead != null)
			{
				return m_ModelHead;
			}
			break;
		case 2:
			if (m_ModelBody != null)
			{
				return m_ModelBody;
			}
			break;
		case 3:
			if (m_ModelTransform != null)
			{
				return m_ModelTransform;
			}
			break;
		case 4:
			if (m_ModelBodyDown != null)
			{
				return m_ModelBodyDown;
			}
			break;
		case 5:
			if (m_ModelBack != null)
			{
				return m_ModelBack;
			}
			break;
		case 6:
			if (m_ModelMouse != null)
			{
				return m_ModelMouse;
			}
			break;
		case 8:
			if (m_EyeLeft != null)
			{
				return m_EyeLeft;
			}
			break;
		case 9:
			if (m_EyeRight != null)
			{
				return m_EyeRight;
			}
			break;
		}
		return m_ModelTransform;
	}

	public Transform GetBone(string sPath)
	{
		Transform transform = m_ModelEntityTransform.Find(sPath);
		if (transform != null)
		{
			return transform;
		}
		return m_ModelTransform;
	}

	public void SetActive(bool bActive)
	{
		if (!bActive && isDead)
		{
			return;
		}
		m_bActive = bActive;
		if (!m_bActive)
		{
			Dictionary<kAnimEnum, CAnimInfo> data = m_AnimData.GetData();
			if (data == null)
			{
				return;
			}
			{
				foreach (CAnimInfo value in data.Values)
				{
					if (m_AnimManager.GetAnimEnable(value.m_sAnimName))
					{
						m_ActiveAnim = value.m_sAnimName;
						m_AnimManager.SetAnimEnable(value.m_sAnimName, false);
					}
				}
				return;
			}
		}
		m_AnimManager.SetAnimEnable(m_ActiveAnim, true);
	}

	public virtual float CalcWeaponDamage(CWeaponInfoLevel weaponinfolevel = null)
	{
		return 0f;
	}

	public virtual float CalcWeaponShootSpeed(CWeaponInfoLevel weaponinfolevel = null)
	{
		return 0f;
	}

	public virtual float CalcWeaponBeatBack(CWeaponInfoLevel weaponinfolevel = null)
	{
		return 0f;
	}

	public virtual float CalcCritical(CWeaponInfoLevel weaponinfolevel = null)
	{
		return m_Property.GetValue(kProEnum.Critical);
	}

	public virtual float CalcCriticalDmg(CWeaponInfoLevel weaponinfolevel = null)
	{
		return m_Property.GetValue(kProEnum.CriticalDmg);
	}

	public virtual float CalcProtect(CWeaponInfoLevel weaponinfolevel = null)
	{
		float value = m_Property.GetValue(kProEnum.Protect);
		if (value < 0f)
		{
			return 0f;
		}
		return MyUtils.formula_armor2protect(value);
	}

	public virtual void SetMoribund(bool bMoribund, float fHP = 0f, float fTime = 0f)
	{
		m_bMoribund = bMoribund;
		m_fHP = fHP;
		m_fMoribundTime = fTime;
		ResetAI();
	}
}
