using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class doSelectTargetTask : Task
{
	protected iGameSceneBase m_GameScene;

	public doSelectTargetTask(Node node)
		: base(node)
	{
	}

	public override void OnEnter(Object inputParam)
	{
		m_GameScene = iGameApp.GetInstance().m_GameScene;
		CCharMob cCharMob = inputParam as CCharMob;
		if (!(cCharMob == null))
		{
			cCharMob.SetCurTask(this);
		}
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		if (m_GameScene == null)
		{
			return kTreeRunStatus.Failture;
		}
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null)
		{
			return kTreeRunStatus.Failture;
		}
		List<CCharBase> unitList = m_GameScene.GetUnitList();
		if (unitList.Count == 0)
		{
			Debug.Log("error 0 player in scene?");
			return kTreeRunStatus.Failture;
		}
		List<CCharBase> list = new List<CCharBase>();
		foreach (CCharBase item in unitList)
		{
			if ((item.IsPlayer() || item.IsUser()) && !item.isDead && !item.isStealth)
			{
				list.Add(item);
			}
		}
		if (list.Count == 0)
		{
			return kTreeRunStatus.Failture;
		}
		cCharMob.m_Target = list[Random.Range(0, list.Count)];
		if (cCharMob.m_Target == null)
		{
			return kTreeRunStatus.Failture;
		}
		return kTreeRunStatus.Success;
	}
}
