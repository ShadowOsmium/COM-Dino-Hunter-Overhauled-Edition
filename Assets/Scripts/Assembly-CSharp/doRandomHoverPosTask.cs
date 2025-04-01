using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class doRandomHoverPosTask : Task
{
	protected iGameSceneBase m_GameScene;

	protected List<Vector3> m_ltHoverPoint;

	protected List<int> m_ltRandom;

	public doRandomHoverPosTask(Node node)
		: base(node)
	{
		m_ltHoverPoint = new List<Vector3>();
		m_ltRandom = new List<int>();
	}

	public override void OnEnter(Object inputParam)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (!(cCharMob == null))
		{
			cCharMob.SetCurTask(this);
		}
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null)
		{
			return kTreeRunStatus.Failture;
		}
		if (m_GameScene == null)
		{
			m_GameScene = iGameApp.GetInstance().m_GameScene;
		}
		if (m_GameScene == null)
		{
			return kTreeRunStatus.Failture;
		}
		CStartPointManager hPManager = m_GameScene.GetHPManager();
		if (hPManager == null)
		{
			return kTreeRunStatus.Failture;
		}
		Dictionary<int, CStartPoint> data = hPManager.GetData();
		if (data == null)
		{
			return kTreeRunStatus.Failture;
		}
		m_ltRandom.Clear();
		foreach (KeyValuePair<int, CStartPoint> item in data)
		{
			if (cCharMob.m_nDstHoverIndex != item.Key)
			{
				Vector3 vector = item.Value.v3Pos - cCharMob.Pos;
				float magnitude = vector.magnitude;
				if (!Physics.Raycast(cCharMob.Pos, vector / magnitude, magnitude, int.MinValue))
				{
					m_ltRandom.Add(item.Key);
				}
			}
		}
		if (m_ltRandom.Count < 1)
		{
			return kTreeRunStatus.Failture;
		}
		cCharMob.m_nDstHoverIndex = m_ltRandom[Random.Range(0, m_ltRandom.Count)];
		cCharMob.m_v3DstHoverPoint = data[cCharMob.m_nDstHoverIndex].v3Pos;
		if (CGameNetManager.GetInstance().IsConnected() && CGameNetManager.GetInstance().IsRoomMaster())
		{
			CGameNetSender.GetInstance().SendMsg_MOB_HOVER(cCharMob.UID, cCharMob.m_v3DstHoverPoint);
		}
		return kTreeRunStatus.Success;
	}
}
