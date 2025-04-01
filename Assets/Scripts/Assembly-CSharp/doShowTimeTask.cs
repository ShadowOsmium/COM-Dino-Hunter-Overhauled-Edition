using BehaviorTree;
using UnityEngine;

public class doShowTimeTask : Task
{
	protected float m_fTime;

	protected float m_fTimeCount;

	protected GameObject m_ShowTime;

	public doShowTimeTask(Node node)
		: base(node)
	{
	}

	public override void OnEnter(Object inputParam)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null)
		{
			return;
		}
		cCharMob.SetCurTask(this);
		m_fTime = cCharMob.CrossAnim(kAnimEnum.Mob_ShowTime, WrapMode.Loop, 0.3f, 1f, 0f);
		m_fTimeCount = 0f;
		if (!(m_fTime <= 0f))
		{
			if (cCharMob.m_Target != null)
			{
				cCharMob.Dir2D = cCharMob.m_Target.Pos - cCharMob.Pos;
			}
			if (CGameNetManager.GetInstance().IsRoomMaster() && CGameNetManager.GetInstance().IsConnected())
			{
				CGameNetSender.GetInstance().SendMsg_MOB_ACTION(cCharMob.UID, 2501);
			}
		}
	}

	public override void OnExit(Object inputParam)
	{
		if (m_ShowTime != null)
		{
			Object.Destroy(m_ShowTime);
			m_ShowTime = null;
		}
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null)
		{
			return kTreeRunStatus.Failture;
		}
		m_fTimeCount += deltaTime;
		if (m_fTimeCount > 0.8f && !cCharMob.m_bShowTime)
		{
			cCharMob.m_bShowTime = true;
			CMobInfoLevel mobInfo = cCharMob.GetMobInfo();
			if (mobInfo != null && mobInfo.nRareType == 2)
			{
				Transform bone = cCharMob.GetBone(6);
				if (bone != null)
				{
					Object @object = PrefabManager.Get(1351);
					if (@object != null)
					{
						m_ShowTime = (GameObject)Object.Instantiate(@object);
						if (m_ShowTime != null)
						{
							m_ShowTime.transform.parent = bone;
							m_ShowTime.transform.localPosition = Vector3.zero;
							m_ShowTime.transform.localRotation = Quaternion.identity;
						}
					}
				}
			}
		}
		if (m_fTimeCount < m_fTime)
		{
			return kTreeRunStatus.Executing;
		}
		if (m_ShowTime != null)
		{
			Object.Destroy(m_ShowTime);
			m_ShowTime = null;
		}
		cCharMob.m_bShowTime = true;
		return kTreeRunStatus.Success;
	}
}
