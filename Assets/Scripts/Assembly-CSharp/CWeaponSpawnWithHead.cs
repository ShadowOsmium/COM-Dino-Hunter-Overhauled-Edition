using UnityEngine;

public class CWeaponSpawnWithHead : CWeaponSpawn
{
	protected GameObject m_BulletHead;

	protected override void OnEquip(CCharPlayer player)
	{
		base.OnEquip(player);
		if (m_BulletHead != null || m_pWeaponLvlInfo == null || player == null)
		{
			return;
		}
		int nValue = 0;
		m_pWeaponLvlInfo.GetAtkModeValue(2, ref nValue);
		GameObject gameObject = PrefabManager.Get(nValue);
		if (!(gameObject == null))
		{
			m_BulletHead = (GameObject)Object.Instantiate(gameObject);
			if (!(m_BulletHead == null))
			{
				m_BulletHead.transform.parent = player.GetShootMouseTf();
				m_BulletHead.transform.localPosition = Vector3.zero;
				m_BulletHead.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
			}
		}
	}

	protected override void OnDestroy()
	{
		if (m_BulletHead != null)
		{
			Object.Destroy(m_BulletHead);
			m_BulletHead = null;
		}
	}

	protected override void OnFire(CCharPlayer player)
	{
		base.OnFire(player);
		if (m_BulletHead != null)
		{
			m_BulletHead.SetActiveRecursively(false);
		}
	}

	protected override void OnStop(CCharPlayer player)
	{
		base.OnStop(player);
	}

	protected override void OnUpdate(CCharPlayer player, float deltaTime)
	{
		if (m_fFireIntervalCount >= m_fFireInterval && m_BulletHead != null && !m_BulletHead.active)
		{
			m_BulletHead.SetActiveRecursively(true);
		}
		base.OnUpdate(player, deltaTime);
	}
}
