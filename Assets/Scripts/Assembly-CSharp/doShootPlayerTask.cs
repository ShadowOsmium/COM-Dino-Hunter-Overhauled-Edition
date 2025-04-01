using BehaviorTree;
using UnityEngine;

public class doShootPlayerTask : Task
{
	protected iGameSceneBase m_GameScene;

	protected CWeaponBase m_Weapon;

	public doShootPlayerTask(Node node)
		: base(node)
	{
	}

	public override void OnEnter(Object inputParam)
	{
		m_GameScene = iGameApp.GetInstance().m_GameScene;
		CCharPlayer cCharPlayer = inputParam as CCharPlayer;
		if (!(cCharPlayer == null))
		{
			m_Weapon = cCharPlayer.GetWeapon();
			if (m_Weapon != null)
			{
				m_Weapon.Fire(cCharPlayer);
				cCharPlayer.SetCurTask(this);
			}
		}
	}

	public override void OnExit(Object inputParam)
	{
		CCharPlayer cCharPlayer = inputParam as CCharPlayer;
		if (!(cCharPlayer == null))
		{
			cCharPlayer.SetCurTask(null);
		}
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharPlayer cCharPlayer = inputParam as CCharPlayer;
		if (cCharPlayer == null || m_GameScene == null)
		{
			return kTreeRunStatus.Failture;
		}
		if (!cCharPlayer.m_bNetShoot)
		{
			m_Weapon.Stop(cCharPlayer);
			return kTreeRunStatus.Success;
		}
		if (m_Weapon != null)
		{
			m_Weapon.Update(cCharPlayer, deltaTime);
		}
		return kTreeRunStatus.Executing;
	}
}
