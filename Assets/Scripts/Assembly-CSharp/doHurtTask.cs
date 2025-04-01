using BehaviorTree;
using UnityEngine;

public class doHurtTask : Task
{
	public float m_fHurtTime;

	public float m_fHurtTimeCount;

	public doHurtTask(Node node)
		: base(node)
	{
	}

	public override void OnEnter(Object inputParam)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (!(cCharMob == null))
		{
			cCharMob.SetCurTask(this);
			m_fHurtTimeCount = 0f;
			if (cCharMob.m_HurtAnim == kAnimEnum.None)
			{
				m_fHurtTime = cCharMob.CrossAnim(kAnimEnum.Mob_Hurt, WrapMode.ClampForever, 0.3f, 1f, 0f);
			}
			else
			{
				m_fHurtTime = cCharMob.CrossAnim(cCharMob.m_HurtAnim, WrapMode.ClampForever, 0.3f, 1f, 0f);
			}
		}
	}

	public override void OnExit(Object inputParam)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (!(cCharMob == null))
		{
			cCharMob.CrossAnim(kAnimEnum.Idle, WrapMode.Loop, 0.3f, 1f, 0f);
		}
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null)
		{
			return kTreeRunStatus.Failture;
		}
		m_fHurtTimeCount += deltaTime;
		if (m_fHurtTimeCount < m_fHurtTime)
		{
			return kTreeRunStatus.Executing;
		}
		cCharMob.m_bHurting = false;
		return kTreeRunStatus.Success;
	}
}
