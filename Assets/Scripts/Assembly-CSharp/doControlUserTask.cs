using BehaviorTree;
using UnityEngine;

public class doControlUserTask : Task
{
	protected CControlBase m_Input;

	public doControlUserTask(Node node)
		: base(node)
	{
		m_Input = new CControlWindows();
	}

	public override void OnEnter(Object inputParam)
	{
		CCharUser cCharUser = inputParam as CCharUser;
		if (!(cCharUser == null))
		{
			m_Input.Initialize();
		}
	}

	public override void OnExit(Object inputParam)
	{
		base.OnExit(inputParam);
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		if (m_Input == null)
		{
			return kTreeRunStatus.Failture;
		}
		CCharUser cCharUser = inputParam as CCharUser;
		if (cCharUser == null)
		{
			return kTreeRunStatus.Failture;
		}
		m_Input.Update(deltaTime);
		return kTreeRunStatus.Executing;
	}
}
