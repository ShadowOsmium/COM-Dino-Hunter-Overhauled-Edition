using System.Collections.Generic;
using UnityEngine;

public class iSpawnBullet : MonoBehaviour
{
	public enum kSpawnFrom
	{
		None,
		FromWeapon,
		FromSkill
	}

	public float fGravity = 10f;

	public float fAtkRange;

	public int nEffHit = -1;

	public int nEffHitGround = -1;

	public int nHitMask = -1;

	public Vector3 v3RotateSpeed = new Vector3(0f, 0f, 0f);

	public string sAudioHit = string.Empty;

	protected kSpawnFrom m_SpawnFrom;

	protected CWeaponInfoLevel m_pWeaponLvlInfo;

	protected CSkillInfoLevel m_pSkillLvlInfo;

	protected iGameSceneBase m_GameScene;

	protected iGameLogic m_GameLogic;

	protected Transform m_Transform;

	protected int m_nOwnerUID;

	protected bool m_bActive;

	protected Vector3 m_v3VelocityBase = Vector3.forward;

	protected bool m_bEmission;

	protected ParticleSystem[] m_arrParicleSystem;

	protected float m_fDisappearTime = 10f;

	protected float m_fDisappearTimeCount;

	protected bool m_bHitDestroy;

	public int Owner
	{
		get
		{
			return m_nOwnerUID;
		}
	}

	public void Awake()
	{
		m_Transform = base.transform;
		m_GameScene = iGameApp.GetInstance().m_GameScene;
		m_GameLogic = m_GameScene.GetGameLogic();
		m_bEmission = false;
		m_arrParicleSystem = GetComponentsInChildren<ParticleSystem>();
		if (m_arrParicleSystem != null)
		{
			ParticleSystem[] arrParicleSystem = m_arrParicleSystem;
			foreach (ParticleSystem particleSystem in arrParicleSystem)
			{
				particleSystem.enableEmission = false;
			}
		}
		m_bActive = false;
		m_bHitDestroy = true;
	}

	private void Start()
	{
	}

	public void Update()
	{
		if (!m_bActive)
		{
			return;
		}
		if (!m_bEmission)
		{
			m_bEmission = true;
			if (m_arrParicleSystem != null)
			{
				ParticleSystem[] arrParicleSystem = m_arrParicleSystem;
				foreach (ParticleSystem particleSystem in arrParicleSystem)
				{
					particleSystem.enableEmission = true;
				}
			}
		}
		if (m_GameScene != null && !m_GameScene.isPause)
		{
			if (v3RotateSpeed != Vector3.zero)
			{
				m_Transform.rotation *= new Quaternion(v3RotateSpeed.x * Time.deltaTime, v3RotateSpeed.y * Time.deltaTime, v3RotateSpeed.z * Time.deltaTime, 1f);
			}
			OnUpdate(Time.deltaTime);
			m_fDisappearTimeCount += Time.deltaTime;
			if (m_fDisappearTimeCount >= m_fDisappearTime)
			{
				m_bActive = false;
				Object.Destroy(base.gameObject);
			}
		}
	}

	protected CCharBase GetOwner(int nUID)
	{
		CCharBase cCharBase = m_GameScene.GetPlayer(nUID);
		if (cCharBase == null)
		{
			cCharBase = m_GameScene.GetMob(nUID);
		}
		return cCharBase;
	}

	protected void OnHit(CCharBase actor, CCharBase target, Vector3 v3HitPos, Vector3 v3HitDir)
	{
		UnityEngine.Debug.Log("spawn bullet hit " + actor.UID + " " + target.UID);
		if (target == null || target.isDead)
		{
			return;
		}
		CCharBoss cCharBoss = target as CCharBoss;
		if (cCharBoss != null && cCharBoss.isInBlack)
		{
			cCharBoss.AddBlackDmg(-1f);
			m_GameScene.AddDamageText(1f, v3HitPos);
			if (CGameNetManager.GetInstance().IsConnected() && m_GameScene.IsMyself(actor))
			{
				CGameNetSender.GetInstance().SendMsg_BATTLE_DAMAGE_MOB(cCharBoss.UID, 1f, true);
			}
			return;
		}
		int[] array = new int[3];
		int[] array2 = new int[3];
		int[] array3 = new int[3];
		float num = 0f;
		if (m_SpawnFrom == kSpawnFrom.FromSkill)
		{
			if (m_pSkillLvlInfo == null)
			{
				return;
			}
			for (int i = 0; i < 3; i++)
			{
				array[i] = m_pSkillLvlInfo.arrFunc[i];
				array2[i] = m_pSkillLvlInfo.arrValueX[i];
				array3[i] = m_pSkillLvlInfo.arrValueY[i];
			}
			switch (m_pSkillLvlInfo.nTargetLimit)
			{
			case 2:
				if (actor.IsAlly(target))
				{
					return;
				}
				break;
			case 1:
				if (!actor.IsAlly(target))
				{
					return;
				}
				break;
			case 3:
				if (actor != target)
				{
					return;
				}
				break;
			}
		}
		else if (m_SpawnFrom == kSpawnFrom.FromWeapon)
		{
			if (m_pWeaponLvlInfo == null)
			{
				return;
			}
			for (int j = 0; j < 3; j++)
			{
				array[j] = m_pWeaponLvlInfo.arrFunc[j];
				array2[j] = m_pWeaponLvlInfo.arrValueX[j];
				array3[j] = m_pWeaponLvlInfo.arrValueY[j];
			}
			if (actor == target)
			{
				return;
			}
			CCharPlayer cCharPlayer = actor as CCharPlayer;
			if (cCharPlayer != null && m_GameScene.IsMyself(actor))
			{
				num = cCharPlayer.CalcWeaponDamage(m_pWeaponLvlInfo);
				float num2 = cCharPlayer.CalcCritical(m_pWeaponLvlInfo);
				float num3 = cCharPlayer.CalcCriticalDmg(m_pWeaponLvlInfo);
				bool bCritical = false;
				if (num2 > Random.Range(1f, 100f))
				{
					num *= 1f + num3 / 100f;
					bCritical = true;
				}
				float elementValue = m_pWeaponLvlInfo.GetElementValue(target.ID);
				if (elementValue != 0f)
				{
					num *= 1f + elementValue / 100f;
				}
				float num4 = target.CalcProtect();
				num *= 1f - num4 / 100f;
				if (num < 1f)
				{
					num = 1f;
				}
				m_GameScene.AddMyDamage(num, target.CurHP);
				target.OnHit(0f - num, m_pWeaponLvlInfo, string.Empty);
				if (target.IsMonster())
				{
					((CCharMob)target).SetLifeBarParam(1f);
				}
				if (m_GameScene.IsMyself(target))
				{
					m_GameScene.AddDamageText(num, target.GetBone(1).position, Color.red, bCritical);
				}
				else
				{
					m_GameScene.AddDamageText(num, target.GetBone(1).position, bCritical);
				}
				m_GameScene.AddHitEffect(target.GetBone(1).position, Vector3.forward, 1116);
			}
		}
		if (!actor.IsMonster() && !m_GameScene.IsMyself(actor))
		{
			return;
		}
		iGameLogic.HitInfo hitinfo = new iGameLogic.HitInfo();
		hitinfo.v3HitDir = v3HitDir;
		hitinfo.v3HitPos = v3HitPos;
		m_GameLogic.CaculateFunc(actor, target, array, array2, array3, ref hitinfo);
		m_GameLogic.m_fTotalDmg += num;
		if (CGameNetManager.GetInstance().IsConnected() && m_GameScene.IsMyself(actor))
		{
			CGameNetSender.GetInstance().SendMsg_BATTLE_DAMAGE_MOB(target.UID, m_GameLogic.m_fTotalDmg);
		}
		if (target.isDead && actor.IsUser() && target.IsMonster())
		{
			CCharUser cCharUser = actor as CCharUser;
			CCharMob cCharMob = target as CCharMob;
			CMobInfoLevel mobInfo = cCharMob.GetMobInfo();
			if (mobInfo != null)
			{
				int num5 = 0;
				num5 = ((!m_GameScene.m_bMutiplyGame) ? mobInfo.nExp : MyUtils.formula_monsterexp(mobInfo.nExp, cCharMob.Level));
				float value = cCharUser.Property.GetValue(kProEnum.Char_IncreaseExp);
				if (value > 0f)
				{
					num5 = (int)((float)num5 * (1f + value / 100f));
				}
				cCharUser.AddExp(num5);
				m_GameScene.AddExpText(num5, hitinfo.v3HitPos);
			}
		}
		target.PlayAudio(kAudioEnum.HitBody);
	}

	protected virtual void OnHitScene(CCharBase actor, Vector3 v3HitPos)
	{
		if (m_bHitDestroy)
		{
			m_GameScene.PlayAudio(v3HitPos, sAudioHit);
			m_GameScene.AddEffect(v3HitPos, Vector3.forward, 2f, nEffHit);
			Object.Destroy(base.gameObject);
		}
		if (actor == null || fAtkRange <= 0f || (!m_GameScene.IsMyself(actor) && !actor.IsMonster()))
		{
			return;
		}
		List<CCharBase> unitList = m_GameScene.GetUnitList();
		if (unitList == null)
		{
			return;
		}
		foreach (CCharBase item in unitList)
		{
			if (!(item == null) && !item.isDead && !(Vector3.Distance(v3HitPos, item.Pos) > fAtkRange))
			{
				OnHit(actor, item, v3HitPos, (item.Pos - v3HitPos).normalized);
			}
		}
	}

	protected virtual void OnHitGround(CCharBase actor, Vector3 v3HitPos)
	{
		if (m_bHitDestroy)
		{
			m_GameScene.PlayAudio(v3HitPos, sAudioHit);
			m_GameScene.AddEffect(v3HitPos, Vector3.forward, 2f, nEffHitGround);
			m_GameScene.AddEffect(v3HitPos + new Vector3(0f, 0.01f, 0f), Vector3.forward, 2f, nHitMask);
			Object.Destroy(base.gameObject);
		}
		if (actor == null || fAtkRange <= 0f || (!m_GameScene.IsMyself(actor) && !actor.IsMonster()))
		{
			return;
		}
		List<CCharBase> unitList = m_GameScene.GetUnitList();
		if (unitList == null)
		{
			return;
		}
		foreach (CCharBase item in unitList)
		{
			if (!(item == null) && !item.isDead && !actor.IsAlly(item) && !(Vector3.Distance(v3HitPos, item.Pos) > fAtkRange))
			{
				OnHit(actor, item, v3HitPos, (item.Pos - v3HitPos).normalized);
			}
		}
	}

	protected virtual void OnHitTarget(CCharBase actor, CCharBase target)
	{
		if (m_bHitDestroy)
		{
			m_GameScene.PlayAudio(m_Transform.position, sAudioHit);
			m_GameScene.AddEffect(m_Transform.position, Vector3.forward, 2f, nEffHit);
			Object.Destroy(base.gameObject);
		}
		if (!m_GameScene.IsMyself(actor) && !actor.IsMonster())
		{
			return;
		}
		OnHit(actor, target, m_Transform.position, (target.Pos - m_Transform.position).normalized);
		if (fAtkRange <= 0f)
		{
			return;
		}
		List<CCharBase> unitList = m_GameScene.GetUnitList();
		if (unitList == null)
		{
			return;
		}
		foreach (CCharBase item in unitList)
		{
			if (!(item == null) && !item.isDead && !(target == item) && !actor.IsAlly(item) && !(Vector3.Distance(m_Transform.position, item.Pos) > fAtkRange))
			{
				OnHit(actor, item, m_Transform.position, (item.Pos - m_Transform.position).normalized);
			}
		}
	}

	protected virtual void OnTrigger(Collider collider)
	{
		if (m_GameScene == null || m_GameScene.isPause)
		{
			return;
		}
		iSpawnBullet component = collider.transform.root.GetComponent<iSpawnBullet>();
		if (component != null && component.Owner == m_nOwnerUID)
		{
			return;
		}
		CCharBase owner = GetOwner(m_nOwnerUID);
		if (owner == null)
		{
			return;
		}
		if (owner.IsMonster())
		{
			if ((collider.gameObject.layer != 31 && collider.gameObject.layer != 29 && collider.gameObject.layer != 27) || (collider.gameObject.layer == 29 && !IsCanHitFloor()))
			{
				return;
			}
		}
		else if (collider.gameObject.layer != 31 && collider.gameObject.layer != 29 && collider.gameObject.layer != 26)
		{
			return;
		}
		CCharBase component2 = collider.transform.root.GetComponent<CCharBase>();
		if (component2 != null)
		{
			if (owner == component2)
			{
				return;
			}
			if (!component2.isDead)
			{
				OnHitTarget(owner, component2);
				return;
			}
		}
		if (collider.gameObject.layer == 29 && IsCanHitFloor())
		{
			Vector3 position = m_Transform.position;
			position.y += 100f;
			RaycastHit hitInfo;
			if (Physics.Raycast(new Ray(position, Vector3.down), out hitInfo, 1000f, 536870912))
			{
				OnHitGround(owner, hitInfo.point);
			}
			else
			{
				OnHitGround(owner, m_Transform.position);
			}
		}
		else if (collider.gameObject.layer == 31)
		{
			OnHitScene(owner, m_Transform.position);
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		OnTrigger(collider);
	}

	public void InitializeFromSkill(int nUID, CSkillInfoLevel skilllvlinfo, Vector3 v3Pos, Vector3 v3Force)
	{
		m_nOwnerUID = nUID;
		m_SpawnFrom = kSpawnFrom.FromSkill;
		m_pSkillLvlInfo = skilllvlinfo;
		m_v3VelocityBase = v3Force;
		m_Transform.position = v3Pos;
		m_Transform.forward = v3Force;
		m_bActive = true;
		OnInit();
	}

	public void InitializeFromWeapon(int nUID, CWeaponInfoLevel weaponlvlinfo, Vector3 v3Pos, Vector3 v3Force)
	{
		m_nOwnerUID = nUID;
		m_SpawnFrom = kSpawnFrom.FromWeapon;
		m_pWeaponLvlInfo = weaponlvlinfo;
		m_v3VelocityBase = v3Force;
		m_Transform.position = v3Pos;
		m_Transform.forward = v3Force;
		m_bActive = true;
		OnInit();
	}

	public virtual void SetForce(Vector3 v3Force)
	{
		m_v3VelocityBase = v3Force;
	}

	public virtual bool IsCanHitFloor()
	{
		return true;
	}

	protected virtual void OnInit()
	{
	}

	protected virtual void OnUpdate(float deltaTime)
	{
		if (!(m_Transform == null))
		{
			m_Transform.position += m_v3VelocityBase * deltaTime;
		}
	}
}
