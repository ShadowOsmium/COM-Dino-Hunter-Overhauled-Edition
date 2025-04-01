using BehaviorTree;
using UnityEngine;

public class doBlackTask : Task
{
	protected float m_fTime;

	protected float m_fTimeCount;

	protected Renderer m_BlackGearRenderer;

	protected float m_fBlackGearAppearTime;

	protected float m_fBlackGearAppearTimeCount;

	public doBlackTask(Node node)
		: base(node)
	{
	}

	public override void OnEnter(Object inputParam)
	{
		CCharBoss cCharBoss = inputParam as CCharBoss;
		if (cCharBoss == null)
		{
			return;
		}
		m_fTime = cCharBoss.CrossAnim(kAnimEnum.Mob_Roar, WrapMode.ClampForever, 0.3f, 1f, 0f);
		m_fTimeCount = 0f;
		if (cCharBoss.m_BlackGear != null)
		{
			cCharBoss.m_BlackGear.gameObject.active = true;
			m_BlackGearRenderer = cCharBoss.m_BlackGear.GetComponent<Renderer>();
			m_fBlackGearAppearTime = 0.5f;
			m_fBlackGearAppearTimeCount = 0f;
		}
		iGameSceneBase gameScene = iGameApp.GetInstance().m_GameScene;
		Transform bone = cCharBoss.GetBone(2);
		if (bone != null)
		{
			GameObject gameObject = gameScene.AddEffect(bone.position, cCharBoss.Dir2D, 3f, 1951);
			if (gameObject != null)
			{
				gameObject.transform.parent = bone;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.forward = cCharBoss.Dir2D;
			}
		}
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharBoss cCharBoss = inputParam as CCharBoss;
		if (cCharBoss == null)
		{
			return kTreeRunStatus.Failture;
		}
		m_fTimeCount += deltaTime;
		if (m_fTimeCount >= m_fTime)
		{
			Color color = m_BlackGearRenderer.material.GetColor("_FlashColor");
			color.a = 0f;
			m_BlackGearRenderer.material.SetColor("_FlashColor", color);
			cCharBoss.SetBlack(true, cCharBoss.m_fReadyToBlackLife);
			cCharBoss.m_bReadyToBlack = false;
			return kTreeRunStatus.Success;
		}
		m_fBlackGearAppearTimeCount += Time.deltaTime;
		if (m_fBlackGearAppearTimeCount > m_fBlackGearAppearTime)
		{
			m_fBlackGearAppearTimeCount = 0f;
		}
		if (m_BlackGearRenderer != null && m_BlackGearRenderer.material != null)
		{
			Color color2 = m_BlackGearRenderer.material.GetColor("_FlashColor");
			color2.a = 1f - m_fBlackGearAppearTimeCount / m_fBlackGearAppearTime;
			m_BlackGearRenderer.material.SetColor("_FlashColor", color2);
		}
		return kTreeRunStatus.Executing;
	}
}
