using System.Collections.Generic;
using UnityEngine;

public class CCameraRoam
{
	public class CCGInfo
	{
		public string sCG;

		public string sCGContent;

		public string sCGAmbience;

		public string sCGBGM;
	}

	public delegate void OnOrderEndDelegate();

	protected static CCameraRoam m_Instance;

	protected CCGInfo m_CGInfo;

	protected Camera m_MainCamera;

	protected Vector3 m_v3Pos_Backup;

	protected Quaternion m_qtRotation_Backup;

	protected float m_fNear_Backup;

	protected float m_fFar_Backup;

	protected float m_fFov_Backup;

	protected RoamOrder m_RoamOrder;

	protected OnOrderEndDelegate m_OnOrderBeginFunc;

	protected OnOrderEndDelegate m_OnOrderEndFunc;

	protected iMSModelShowManager m_MSModelShowManager;

	protected Dictionary<string, GameObject> m_dictCache;

	public CCameraRoam()
	{
		m_dictCache = new Dictionary<string, GameObject>();
	}

	public static CCameraRoam GetInstance()
	{
		if (m_Instance == null)
		{
			m_Instance = new CCameraRoam();
		}
		return m_Instance;
	}

	public void Update(float deltaTime)
	{
	}

	public void PreLoadCG(string sPrefabName)
	{
		string text = "Artist/CutScene/" + sPrefabName;
		if (m_dictCache.ContainsKey(text))
		{
			return;
		}
		Object @object = Resources.Load(text);
		if (!(@object == null))
		{
			GameObject gameObject = (GameObject)Object.Instantiate(@object, Vector3.zero, Quaternion.identity);
			if (!(gameObject == null))
			{
				m_dictCache.Add(text, gameObject);
			}
		}
	}

	public void PreLoadCGContent(string sPrefabName)
	{
		string text = "Artist/CutScene/CGContent/" + sPrefabName;
		if (m_dictCache.ContainsKey(text))
		{
			return;
		}
		Object @object = Resources.Load(text);
		if (!(@object == null))
		{
			GameObject gameObject = (GameObject)Object.Instantiate(@object, Vector3.zero, Quaternion.identity);
			if (!(gameObject == null))
			{
				m_dictCache.Add(text, gameObject);
			}
		}
	}

	public void UnLoad()
	{
		foreach (GameObject value in m_dictCache.Values)
		{
			if (value != null)
			{
				Object.Destroy(value);
			}
		}
		m_dictCache.Clear();
	}

	protected bool LoadCG(string sPrefabName)
	{
		if (sPrefabName.Length < 1)
		{
			return false;
		}
		string key = "Artist/CutScene/" + sPrefabName;
		if (!m_dictCache.ContainsKey(key))
		{
			return false;
		}
		GameObject gameObject = m_dictCache[key];
		if (gameObject == null)
		{
			return false;
		}
		m_RoamOrder = gameObject.GetComponent<RoamOrder>();
		if (m_RoamOrder == null)
		{
			return false;
		}
		iOnOrderEvent iOnOrderEvent2 = gameObject.AddComponent<iOnOrderEvent>();
		if (iOnOrderEvent2 != null)
		{
			iOnOrderEvent2.m_OrderStartFunc = OnOrderBegin;
			iOnOrderEvent2.m_OrderStopFunc = OnOrderEnd;
		}
		return true;
	}

	protected bool LoadCGContent(string sPrefabName)
	{
		if (sPrefabName.Length < 1)
		{
			return false;
		}
		string key = "Artist/CutScene/CGContent/" + sPrefabName;
		if (!m_dictCache.ContainsKey(key))
		{
			return false;
		}
		GameObject gameObject = m_dictCache[key];
		if (gameObject == null)
		{
			return false;
		}
		m_MSModelShowManager = gameObject.GetComponent<iMSModelShowManager>();
		return true;
	}

	protected void OnOrderBegin()
	{
		if (m_OnOrderBeginFunc != null)
		{
			m_OnOrderBeginFunc();
		}
	}

	protected void OnOrderEnd()
	{
		Restore();
		if (m_OnOrderEndFunc != null)
		{
			m_OnOrderEndFunc();
		}
		m_OnOrderBeginFunc = null;
		m_OnOrderEndFunc = null;
		Object.Destroy(m_RoamOrder.gameObject, 2f);
		if (m_MSModelShowManager != null)
		{
			m_MSModelShowManager.Destroy();
			Object.Destroy(m_MSModelShowManager.gameObject, 2f);
			m_MSModelShowManager = null;
		}
		CSoundScene.GetInstance().StopAmbience();
		if (m_CGInfo.sCGBGM.Length > 0)
		{
			CSoundScene.GetInstance().PlayBGM(m_CGInfo.sCGBGM);
		}
		else
		{
			CSoundScene.GetInstance().PlayBGM(string.Empty);
		}
	}

	public bool Start(Camera camera, CCGInfo cginfo, OnOrderEndDelegate begin, OnOrderEndDelegate end)
	{
		if (!LoadCG(cginfo.sCG))
		{
			return false;
		}
		m_CGInfo = cginfo;
		LoadCGContent(m_CGInfo.sCGContent);
		m_OnOrderBeginFunc = begin;
		m_OnOrderEndFunc = end;
		Backup(camera);
		m_RoamOrder.OnShow(camera);
		CSoundScene.GetInstance().StopBGM();
		CSoundScene.GetInstance().PlayAmbience(m_CGInfo.sCGAmbience);
		return true;
	}

	public void Stop()
	{
		if (!(m_RoamOrder == null))
		{
			m_RoamOrder.OnEnd();
		}
	}

	public void Backup(Camera camera)
	{
		m_MainCamera = camera;
		iCameraTrail component = m_MainCamera.GetComponent<iCameraTrail>();
		if (component != null)
		{
			component.SwitchToCameraListener();
			component.Active = false;
		}
		iCameraController component2 = m_MainCamera.GetComponent<iCameraController>();
		if (component2 != null)
		{
			m_v3Pos_Backup = component2.Position;
			m_qtRotation_Backup = component2.Rotation;
		}
		m_fNear_Backup = m_MainCamera.nearClipPlane;
		m_fFar_Backup = m_MainCamera.farClipPlane;
		m_fFov_Backup = m_MainCamera.fov;
	}

	public void Restore()
	{
		if (!(m_MainCamera == null))
		{
			iCameraTrail component = m_MainCamera.GetComponent<iCameraTrail>();
			if (component != null)
			{
				component.SwitchToTargetListener();
				component.Active = true;
			}
			iCameraController component2 = m_MainCamera.GetComponent<iCameraController>();
			if (component2 != null)
			{
				component2.Position = m_v3Pos_Backup;
				component2.Rotation = m_qtRotation_Backup;
			}
			m_MainCamera.nearClipPlane = m_fNear_Backup;
			m_MainCamera.farClipPlane = m_fFar_Backup;
			m_MainCamera.fov = m_fFov_Backup;
		}
	}
}
