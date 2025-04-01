using UnityEngine;

public class CWeaponBase
{
	protected iGameState m_GameState;

	protected iGameLogic m_GameLogic;

	protected iGameData m_GameData;

	protected int m_nWeaponID;

	protected int m_nWeaponLevel;

	protected CWeaponInfoLevel m_pWeaponLvlInfo;

	protected bool m_bFire;

	protected bool m_bPauseFire;

	protected float m_fFireInterval;

	protected float m_fFireIntervalCount;

	protected int m_nBulletNum;

	protected int m_nBulletNumMax;

	protected int m_nBulletNumMaxBase;

	protected bool m_bNetPlayerShoot;

	protected Light m_FireLight;

	protected Color m_FireLightColor;

	protected float m_fFireLightTime;

	protected float m_fFireLightTimeCount;

	protected iGameSceneBase m_GameScene
	{
		get
		{
			return iGameApp.GetInstance().m_GameScene;
		}
	}

	protected iGameUIBase m_GameUI
	{
		get
		{
			if (m_GameScene == null)
			{
				return null;
			}
			return m_GameScene.GetGameUI();
		}
	}

	public int ID
	{
		get
		{
			return m_nWeaponID;
		}
	}

	public int Level
	{
		get
		{
			return m_nWeaponLevel;
		}
	}

	public CWeaponInfoLevel CurWeaponLvlInfo
	{
		get
		{
			return m_pWeaponLvlInfo;
		}
	}

	public bool isNetPlayerShoot
	{
		get
		{
			return m_bNetPlayerShoot;
		}
		set
		{
			m_bNetPlayerShoot = value;
		}
	}

	public int BulletNum
	{
		get
		{
			return m_nBulletNum;
		}
		set
		{
			m_nBulletNum = value;
		}
	}

	public int BulletNumMax
	{
		get
		{
			return m_nBulletNumMax;
		}
		set
		{
			m_nBulletNumMax = value;
		}
	}

	public bool IsBulletEmpty
	{
		get
		{
			return m_nBulletNum <= 0;
		}
	}

	public CWeaponBase()
	{
		m_bNetPlayerShoot = false;
		m_bFire = false;
		m_bPauseFire = false;
		m_nBulletNum = 1;
		m_nBulletNumMax = 0;
	}

	public void Initialize(int nWeaponID, int nWeaponLevel)
	{
		m_GameState = iGameApp.GetInstance().m_GameState;
		m_GameData = iGameApp.GetInstance().m_GameData;
		if (m_GameData != null)
		{
			m_pWeaponLvlInfo = m_GameData.GetWeaponInfo(nWeaponID, nWeaponLevel);
		}
		m_nWeaponID = nWeaponID;
		m_nWeaponLevel = nWeaponLevel;
		OnInit();
	}

	public void Destroy()
	{
		OnDestroy();
		if (m_FireLight != null)
		{
			Object.Destroy(m_FireLight.gameObject);
			m_FireLight = null;
		}
	}

	public void Equip(CCharPlayer player)
	{
		if (m_nBulletNumMax == 0 && m_pWeaponLvlInfo != null && m_pWeaponLvlInfo.nType != 1)
		{
			if (player != null && player.Property != null)
			{
				Debug.Log(player.Property.GetValue(kProEnum.All_Capacity));
				m_nBulletNumMax = (int)((float)m_pWeaponLvlInfo.nCapacity * (1f + player.Property.GetValue(kProEnum.All_Capacity) / 100f));
			}
			else
			{
				m_nBulletNumMax = m_pWeaponLvlInfo.nCapacity;
			}
			m_nBulletNum = m_nBulletNumMax;
		}
		OnEquip(player);
		if (!(m_FireLight == null))
		{
			return;
		}
		GameObject gameObject = new GameObject("ShootLight");
		if (gameObject != null)
		{
			m_FireLight = gameObject.AddComponent<Light>();
			if (m_FireLight != null)
			{
				m_FireLight.type = LightType.Point;
				m_FireLight.enabled = false;
			}
			gameObject.transform.parent = player.GetShootMouseTf();
			gameObject.transform.localRotation = Quaternion.identity;
			InitLight();
		}
	}

	public void UnEquip(CCharPlayer player)
	{
		OnUnEquip(player);
		if (m_FireLight != null)
		{
			m_FireLight.enabled = false;
		}
	}

	public bool IsFire()
	{
		return m_bFire;
	}

	public void PauseFire(bool bPause)
	{
		m_bPauseFire = bPause;
	}

	public void Fire(CCharPlayer player)
	{
		if (m_pWeaponLvlInfo == null || m_GameScene == null || player == null)
		{
			return;
		}
		if (IsBulletEmpty && m_pWeaponLvlInfo.nType != 1)
		{
			if (m_pWeaponLvlInfo != null)
			{
				switch (m_pWeaponLvlInfo.nType)
				{
				case 2:
					player.PlayAudio("Weapon_nobullet_gun");
					break;
				case 0:
					player.PlayAudio("Weapon_nobullet_crossbow");
					break;
				case 3:
					player.PlayAudio("Weapon_nobullet_gun");
					break;
				case 5:
					player.PlayAudio("Weapon_nobullet_rpg");
					break;
				case 4:
					player.PlayAudio("Weapon_nobullet_flamethrower");
					break;
				case 1:
					break;
				}
			}
		}
		else
		{
			m_bFire = true;
			m_bPauseFire = false;
			if (IsCompeleted() || m_pWeaponLvlInfo.nType == 4)
			{
				m_fFireInterval = player.CalcWeaponShootSpeed(m_pWeaponLvlInfo);
				m_fFireIntervalCount = 0f;
				OnFire(player);
			}
		}
	}

	public void Stop(CCharPlayer player)
	{
		if (m_pWeaponLvlInfo != null && m_GameScene != null && !(player == null))
		{
			m_bFire = false;
			OnStop(player);
			if (m_FireLight != null)
			{
				m_FireLight.enabled = false;
			}
		}
	}

	public void FullBullet()
	{
		SetBullet(m_nBulletNumMax);
	}

	public void SetBullet(int nNum)
	{
		m_nBulletNum = nNum;
		if (m_nBulletNum > m_nBulletNumMax)
		{
			m_nBulletNum = m_nBulletNumMax;
		}
		else if (m_nBulletNum < 0)
		{
			m_nBulletNum = 0;
		}
	}

	public void Update(CCharPlayer player, float deltaTime)
	{
		if (m_pWeaponLvlInfo != null && m_GameScene != null && !(player == null))
		{
			UpdateLight(deltaTime);
			if (!m_bPauseFire)
			{
				OnUpdate(player, deltaTime);
			}
		}
	}

	protected virtual bool IsCompeleted()
	{
		if (m_fFireIntervalCount >= m_fFireInterval)
		{
			return true;
		}
		return false;
	}

	protected virtual void OnInit()
	{
	}

	protected virtual void OnDestroy()
	{
	}

	protected virtual void InitLight()
	{
		if (!(m_FireLight == null))
		{
			m_FireLight.transform.localPosition = new Vector3(0.5f, 0f, 0.5f);
			switch (m_pWeaponLvlInfo.nElementType)
			{
			case 1:
				m_FireLightColor = new Color(1f, 0.22f, 0f, 1f);
				break;
			case 2:
				m_FireLightColor = new Color(0.33f, 1f, 1f, 1f);
				break;
			case 3:
				m_FireLightColor = new Color(0.9f, 0.9f, 0.9f, 1f);
				break;
			default:
				m_FireLightColor = new Color(1f, 0.945f, 0f, 1f);
				break;
			}
			m_FireLight.range = 5f;
			m_fFireLightTime = 0.5f + 1.6666667f * (m_pWeaponLvlInfo.fShootSpeed - 0.1f);
		}
	}

	protected virtual void UpdateLight(float deltaTime)
	{
		if (!(m_FireLight == null) && m_FireLight.enabled && m_fFireLightTime > 0f)
		{
			m_fFireLightTimeCount += deltaTime;
			m_FireLight.color *= 1f - m_fFireLightTimeCount / m_fFireLightTime;
			if (m_fFireLightTimeCount >= m_fFireLightTime)
			{
				m_FireLight.enabled = false;
			}
		}
	}

	protected virtual void ShowFireLight(bool bShow)
	{
		if (!(m_FireLight == null))
		{
			if (bShow)
			{
				m_FireLight.enabled = true;
				m_FireLight.color = m_FireLightColor;
				m_fFireLightTimeCount = 0f;
			}
			else
			{
				m_FireLight.enabled = false;
			}
		}
	}

	protected virtual void OnEquip(CCharPlayer player)
	{
	}

	protected virtual void OnUnEquip(CCharPlayer player)
	{
	}

	protected virtual void OnFire(CCharPlayer player)
	{
	}

	protected virtual void OnStop(CCharPlayer player)
	{
	}

	protected virtual void OnUpdate(CCharPlayer player, float deltaTime)
	{
	}

	protected virtual void OnHitMob(CCharPlayer player, CCharMob mob, Vector3 hitpos, Vector3 hitdir, string sBodyPart = "")
	{
	}

	protected void ConsumeBullet(CCharPlayer player)
	{
		if (m_GameScene == null || m_GameScene.IsMyself(player))
		{
			m_nBulletNum--;
			if (m_nBulletNum < 0)
			{
				m_nBulletNum = 0;
			}
			RefreshBulletUI(player);
		}
	}

	public void RefreshBulletUI(CCharPlayer player, bool bAnim = false)
	{
		if (!(m_GameUI == null) && m_GameScene != null && m_GameScene.IsMyself(player) && player.GetWeapon() == this)
		{
			m_GameUI.SetBulletNum(m_nBulletNum, m_nBulletNumMax, bAnim);
		}
	}
}
