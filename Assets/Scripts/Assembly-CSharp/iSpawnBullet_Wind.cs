using UnityEngine;

public class iSpawnBullet_Wind : iSpawnBullet
{
	protected float m_fInterval = 0.5f;

	protected float m_fIntervalCount;

	public new void Awake()
	{
		base.Awake();
		m_bHitDestroy = false;
		m_fIntervalCount = 0f;
	}

	public new void Update()
	{
		base.Update();
		m_fIntervalCount += Time.deltaTime;
	}

	protected override void OnHitScene(CCharBase actor, Vector3 v3HitPos)
	{
		m_GameScene.PlayAudio(m_Transform.position, sAudioHit);
		m_GameScene.AddEffect(m_Transform.position, Vector3.forward, 2f, nEffHit);
		Object.Destroy(base.gameObject);
	}

	protected override void OnHitGround(CCharBase actor, Vector3 v3HitPos)
	{
		m_GameScene.PlayAudio(m_Transform.position, sAudioHit);
		m_GameScene.AddEffect(m_Transform.position, Vector3.forward, 2f, nEffHitGround);
		m_GameScene.AddEffect(v3HitPos + new Vector3(0f, 0.01f, 0f), Vector3.forward, 2f, nHitMask);
		Object.Destroy(base.gameObject);
	}

	protected override void OnHitTarget(CCharBase actor, CCharBase target)
	{
		base.OnHitTarget(actor, target);
	}

	private void OnTriggerEnter(Collider collider)
	{
		OnTrigger(collider);
		m_fIntervalCount = 0f;
	}

	private void OnTriggerStay(Collider collider)
	{
		if (m_fIntervalCount >= m_fInterval)
		{
			OnTrigger(collider);
			m_fIntervalCount = 0f;
		}
	}
}
