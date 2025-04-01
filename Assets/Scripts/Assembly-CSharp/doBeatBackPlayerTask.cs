using BehaviorTree;
using UnityEngine;

public class doBeatBackPlayerTask : Task
{
	protected Vector3 m_v3Src;

	protected Vector3 m_v3Dst;

	protected float m_fRate;

	protected float m_fRateSpeed;

	protected float m_fHurtTime;

	protected float m_fHurtTimeCount;

	public doBeatBackPlayerTask(Node node)
		: base(node)
	{
		m_fRateSpeed = 5f;
	}

	public override void OnEnter(Object inputParam)
	{
		CCharBase cCharBase = inputParam as CCharBase;
		if (!(cCharBase == null))
		{
			Vector3 vector = cCharBase.m_v3BeatBackPoint - cCharBase.Pos;
			if (vector == Vector3.zero)
			{
				vector = Vector3.forward;
			}
			cCharBase.m_fBeatBackDis = vector.magnitude;
			cCharBase.m_v3BeatBackDir = vector / cCharBase.m_fBeatBackDis;
			Vector3 v3BeatBackDir = cCharBase.m_v3BeatBackDir;
			v3BeatBackDir.y = 0f;
			Vector3 origin = cCharBase.Pos + new Vector3(0f, 0.2f, 0f) - v3BeatBackDir * 1f;
			float distance = cCharBase.m_fBeatBackDis + 1f;
			RaycastHit hitInfo;
			if (Physics.Raycast(origin, v3BeatBackDir, out hitInfo, distance, -1874853888))
			{
				m_v3Dst = hitInfo.point - new Vector3(0f, 0.2f, 0f) - v3BeatBackDir * 1f;
			}
			else
			{
				m_v3Dst = cCharBase.Pos + cCharBase.m_v3BeatBackDir * cCharBase.m_fBeatBackDis;
			}
			if (Vector3.Dot(cCharBase.Dir2D, cCharBase.m_v3BeatBackDir) >= 0f)
			{
				m_fHurtTime = cCharBase.PlayAnim(kAnimEnum.BigHurtFront, WrapMode.ClampForever, 1f, 0f);
			}
			else
			{
				m_fHurtTime = cCharBase.PlayAnim(kAnimEnum.BigHurtBehind, WrapMode.ClampForever, 1f, 0f);
			}
			m_fHurtTimeCount = 0f;
			m_v3Src = cCharBase.Pos;
			m_fRate = 0f;
		}
	}

	public override void OnExit(Object inputParam)
	{
		CCharBase cCharBase = inputParam as CCharBase;
		if (!(cCharBase == null))
		{
			cCharBase.CrossAnim(kAnimEnum.Idle, WrapMode.Loop, 0.3f, 1f, 0f);
		}
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharBase cCharBase = inputParam as CCharBase;
		if (cCharBase == null)
		{
			return kTreeRunStatus.Failture;
		}
		if (m_fRate < 1f)
		{
			m_fRate += m_fRateSpeed * deltaTime;
			cCharBase.Pos = Vector3.Lerp(m_v3Src, m_v3Dst, m_fRate);
			if (m_fRate > 1f)
			{
				m_fRate = 1f;
			}
		}
		m_fHurtTimeCount += deltaTime;
		if (m_fHurtTimeCount >= m_fHurtTime)
		{
			cCharBase.m_bBeatBack = false;
			cCharBase.m_bHurting = false;
			return kTreeRunStatus.Success;
		}
		return kTreeRunStatus.Executing;
	}
}
