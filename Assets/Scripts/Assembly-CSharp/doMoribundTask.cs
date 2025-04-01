using BehaviorTree;
using UnityEngine;

public class doMoribundTask : Task
{
	protected float m_fTimeCount;

	protected float m_fAnimTime;

	protected float m_fAnimTimeCount;

	protected GameObject m_Effect;

	public doMoribundTask(Node node)
		: base(node)
	{
	}

	public override void OnEnter(Object inputParam)
	{
		CCharBase cCharBase = inputParam as CCharBase;
		if (cCharBase == null)
		{
			return;
		}
		cCharBase.CrossAnim(kAnimEnum.Moribund, WrapMode.ClampForever, 0.3f, 1f, 0f);
		m_fTimeCount = 0f;
		GameObject gameObject = PrefabManager.Get(1947);
		if (gameObject != null)
		{
			if (m_Effect == null)
			{
				m_Effect = (GameObject)Object.Instantiate(gameObject, cCharBase.Pos, Quaternion.identity);
			}
			if (m_Effect != null)
			{
				m_Effect.transform.parent = cCharBase.GetBone(2);
				m_Effect.transform.localPosition = Vector3.zero;
				m_Effect.transform.rotation = Quaternion.identity;
			}
		}
		cCharBase.PlayAudio("Fx_ZDjialong_Recover");
		cCharBase.SetCurTask(this);
	}

	public override void OnExit(Object inputParam)
	{
		if (m_Effect != null)
		{
			Object.Destroy(m_Effect);
			m_Effect = null;
		}
		CCharBase cCharBase = inputParam as CCharBase;
		if (!(cCharBase == null))
		{
			cCharBase.StopAudio("Fx_ZDjialong_Recover");
		}
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharBase cCharBase = inputParam as CCharBase;
		if (cCharBase == null)
		{
			return kTreeRunStatus.Failture;
		}
		if (m_fTimeCount < cCharBase.m_fMoribundTime)
		{
			m_fTimeCount += deltaTime;
			if (m_fTimeCount >= cCharBase.m_fMoribundTime)
			{
				m_fAnimTime = cCharBase.CrossAnim(kAnimEnum.MoribundBack, WrapMode.Once, 0.3f, 1f, 0f);
				m_fAnimTimeCount = 0f;
				cCharBase.AddHP(cCharBase.MaxHP);
			}
			return kTreeRunStatus.Executing;
		}
		if (m_fAnimTimeCount < m_fAnimTime)
		{
			m_fAnimTimeCount += deltaTime;
			if (m_fAnimTimeCount < m_fAnimTime)
			{
				return kTreeRunStatus.Executing;
			}
		}
		cCharBase.m_bHurting = false;
		cCharBase.SetMoribund(false, 0f, 0f);
		cCharBase.Revive(cCharBase.MaxHP);
		cCharBase.StopAudio("Fx_ZDjialong_Recover");
		cCharBase.PlayAudio("Fx_ZDjialong_Heal");
		return kTreeRunStatus.Success;
	}
}
