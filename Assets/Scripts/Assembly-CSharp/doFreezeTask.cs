using BehaviorTree;
using UnityEngine;

public class doFreezeTask : Task
{
	protected float m_fFreezeTime;

	protected float m_fFreezeTimeCount;

	public doFreezeTask(Node node)
		: base(node)
	{
	}

	public override void OnEnter(Object inputParam)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (!(cCharMob == null))
		{
			float value = cCharMob.Property.GetValue(kProEnum.FreezeTimeMinuseRate);
			m_fFreezeTime = cCharMob.m_fFreezeTime * (1f - value);
			m_fFreezeTimeCount = 0f;
			cCharMob.CrossAnim(kAnimEnum.Idle, WrapMode.Loop, 0.3f, 1f, 0f);
		}
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null || !cCharMob.m_bFreeze)
		{
			return kTreeRunStatus.Failture;
		}
		m_fFreezeTimeCount += deltaTime;
		if (m_fFreezeTimeCount < m_fFreezeTime)
		{
			return kTreeRunStatus.Executing;
		}
		cCharMob.SetFreeze(false);
		return kTreeRunStatus.Success;
	}
}
