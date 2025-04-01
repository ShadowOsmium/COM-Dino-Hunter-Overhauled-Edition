using gyTaskSystem;
using UnityEngine;

public class iGameTaskUIBase : MonoBehaviour
{
	protected iGameSceneBase m_GameScene;

	protected iGameData m_GameData;

	protected iGameUIBase m_GameUI;

	protected CTaskBase m_curTaskBase;

	protected float m_fHeight = 10f;

	protected iGameTaskUITimeLimit m_TaskTime;

	public float Height
	{
		get
		{
			return m_fHeight;
		}
		set
		{
			m_fHeight = value;
		}
	}

	private void Update()
	{
		UpdateTask(Time.deltaTime);
	}

	public virtual void Initialize(CTaskBase taskbase)
	{
		m_GameScene = iGameApp.GetInstance().m_GameScene;
		m_GameData = iGameApp.GetInstance().m_GameData;
		m_GameUI = m_GameScene.GetGameUI();
		m_curTaskBase = taskbase;
		InitTask(taskbase);
	}

	public virtual void Destroy()
	{
	}

	public virtual void InitTask(CTaskBase taskbase)
	{
		if (taskbase != null && taskbase.TaskTime > 0f)
		{
			AddTimeLimitUI(taskbase.TaskTime);
		}
	}

	public virtual void UpdateTask(float deltaTime)
	{
		if (m_curTaskBase != null && m_TaskTime != null)
		{
			m_TaskTime.SetTime(m_curTaskBase.TaskTime);
		}
	}

	public virtual void Show(bool bShow)
	{
		base.gameObject.SetActiveRecursively(bShow);
	}

	public void AddTimeLimitUI(float fTime)
	{
		if (fTime <= 0f)
		{
			return;
		}
		GameObject gameObject = m_GameUI.AddControl(2000, base.transform);
		if (!(gameObject == null))
		{
			gameObject.transform.localPosition = new Vector3(0f, 0f - Height, 0f);
			gameObject.transform.localRotation = Quaternion.identity;
			m_TaskTime = gameObject.GetComponent<iGameTaskUITimeLimit>();
			if (!(m_TaskTime == null))
			{
				m_TaskTime.SetTime(fTime);
				Height += m_TaskTime.Height;
			}
		}
	}
}
