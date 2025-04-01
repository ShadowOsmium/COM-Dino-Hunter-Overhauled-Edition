using BehaviorTree;
using UnityEngine;

public class doStunTask : Task
{
	protected GameObject m_Effect;

	public doStunTask(Node node)
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
		if (cCharBase.IsMonster())
		{
			cCharBase.CrossAnim(kAnimEnum.Mob_Dead, WrapMode.ClampForever, 0.3f, 1f, 0f);
		}
		else
		{
			cCharBase.CrossAnim(kAnimEnum.Stun, WrapMode.Loop, 0.3f, 1f, 0f);
		}
		GameObject gameObject = PrefabManager.Get(1409);
		if (gameObject != null)
		{
			m_Effect = Object.Instantiate(gameObject) as GameObject;
			if (m_Effect != null)
			{
				m_Effect.transform.parent = cCharBase.GetBone(0);
				m_Effect.transform.localPosition = Vector3.zero;
				m_Effect.transform.localRotation = Quaternion.identity;
			}
		}
		CCharPlayer cCharPlayer = cCharBase as CCharUser;
		if (cCharPlayer != null && cCharPlayer.CurCharInfoLevel != null)
		{
			if (cCharPlayer.CurCharInfoLevel.isMale)
			{
				cCharPlayer.PlayAudio("SVO_Voice_Male01_Dizzy");
			}
			else
			{
				cCharPlayer.PlayAudio("SVO_Voice_Female01_Dizzy");
			}
		}
		cCharBase.SetCurTask(this);
		Debug.Log("enter stun");
	}

	public override void OnExit(Object inputParam)
	{
		Debug.Log("exit stun");
		CCharBase cCharBase = inputParam as CCharBase;
		if (cCharBase != null)
		{
			cCharBase.isStun = false;
			cCharBase.SetCurTask(null);
		}
		if (m_Effect != null)
		{
			Object.Destroy(m_Effect);
			m_Effect = null;
		}
		CCharPlayer cCharPlayer = cCharBase as CCharUser;
		if (cCharPlayer != null && cCharPlayer.CurCharInfoLevel != null)
		{
			if (cCharPlayer.CurCharInfoLevel.isMale)
			{
				cCharPlayer.StopAudio("SVO_Voice_Male01_Dizzy");
			}
			else
			{
				cCharPlayer.StopAudio("SVO_Voice_Female01_Dizzy");
			}
		}
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharBase cCharBase = inputParam as CCharBase;
		if (cCharBase == null || !cCharBase.isStun)
		{
			return kTreeRunStatus.Failture;
		}
		cCharBase.StunTime -= deltaTime;
		if (cCharBase.StunTime <= 0f)
		{
			cCharBase.isStun = false;
			cCharBase.SetCurTask(null);
			if (m_Effect != null)
			{
				Object.Destroy(m_Effect);
				m_Effect = null;
			}
			CCharPlayer cCharPlayer = cCharBase as CCharUser;
			if (cCharPlayer != null && cCharPlayer.CurCharInfoLevel != null)
			{
				if (cCharPlayer.CurCharInfoLevel.isMale)
				{
					cCharPlayer.StopAudio("SVO_Voice_Male01_Dizzy");
				}
				else
				{
					cCharPlayer.StopAudio("SVO_Voice_Female01_Dizzy");
				}
			}
			return kTreeRunStatus.Success;
		}
		return kTreeRunStatus.Executing;
	}
}
