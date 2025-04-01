using UnityEngine;

public class CameraRoam : MonoBehaviour, IRoamEvent
{
	public enum RoamType
	{
		FixedFocus,
		DefinedFocus,
		TargetFocus,
		NoFocus,
		FollowFocus
	}

	public enum FocusType
	{
		LookAt,
		SlerpTo,
		LerpTo
	}

	public RoamType type;

	public Camera m_camera;

	public Path cameraPath;

	public Track.TrackType cameraTrackType;

	public float cameraTime;

	public bool isCameraSmooth;

	public FocusType cameraFocusType;

	public Track.TrackPathType cameraTrackPathType;

	public GameObject m_focus;

	public Path focusPath;

	public Track.TrackType focusTrackType;

	public float focusTime;

	public bool isFocusSmooth;

	public Track.TrackPathType focusTrackPathType;

	protected bool isTempFocus;

	protected bool islookat;

	protected GameObject lookAtFocus;

	public void OnRoamTrigger()
	{
		islookat = true;
		Roam();
	}

	public void OnRoamStop()
	{
		islookat = false;
		Clear();
	}

	protected void Trigger()
	{
		Component[] components = base.gameObject.GetComponents(typeof(IRoamEvent));
		Component[] array = components;
		foreach (Component component in array)
		{
			((IRoamEvent)component).OnRoamTrigger();
		}
	}

	protected void Stop()
	{
		Component[] components = base.gameObject.GetComponents(typeof(IRoamEvent));
		Component[] array = components;
		foreach (Component component in array)
		{
			((IRoamEvent)component).OnRoamStop();
		}
	}

	public void OnRoam(float aliveTime)
	{
		Trigger();
		Invoke("OnStop", aliveTime);
	}

	public void OnStop()
	{
		CancelInvoke("OnStop");
		Stop();
	}

	private void Roam()
	{
		switch (type)
		{
		case RoamType.FixedFocus:
			Track.MoveTrack(m_camera.transform, cameraPath, cameraTrackType, cameraTime, 0f, isCameraSmooth, cameraTrackPathType);
			break;
		case RoamType.DefinedFocus:
			if (cameraPath != null)
			{
				Track.MoveTrack(m_camera.transform, cameraPath, cameraTrackType, cameraTime, 0f, isCameraSmooth, cameraTrackPathType);
			}
			m_focus = CreateDefinedFocus();
			Track.MoveTrack(m_focus.transform, focusPath, focusTrackType, focusTime, 0f, isFocusSmooth, focusTrackPathType);
			break;
		case RoamType.TargetFocus:
			if (cameraPath != null)
			{
				Track.MoveTrack(m_camera.transform, cameraPath, cameraTrackType, cameraTime, 0f, isCameraSmooth, cameraTrackPathType);
			}
			break;
		case RoamType.NoFocus:
			if (cameraPath != null)
			{
				Track.MoveTrack(m_camera.transform, cameraPath, cameraTrackType, cameraTime, 0f, isCameraSmooth, cameraTrackPathType);
			}
			m_focus = null;
			break;
		case RoamType.FollowFocus:
			m_focus = CreateFollowFocus();
			Track.MoveTrack(m_camera.transform, cameraPath, cameraTrackType, cameraTime, 0f, isCameraSmooth, cameraTrackPathType);
			Track.MoveTrack(m_focus.transform, cameraPath, cameraTrackType, cameraTime, -0.1f, isCameraSmooth, cameraTrackPathType);
			break;
		}
	}

	private void Clear()
	{
		if (isTempFocus)
		{
			Object.Destroy(m_focus);
			isTempFocus = false;
		}
		Object.Destroy(lookAtFocus);
		lookAtFocus = null;
		Track.CancelTrack(m_camera.transform);
	}

	private GameObject CreateDefinedFocus()
	{
		isTempFocus = true;
		GameObject gameObject = new GameObject("CameraRoamTarget");
		gameObject.transform.position = ((focusTrackPathType != 0) ? focusPath.GetEnd() : focusPath.GetStart());
		return gameObject;
	}

	private GameObject CreateFollowFocus()
	{
		isTempFocus = true;
		GameObject gameObject = new GameObject("CameraRoamTarget");
		gameObject.transform.position = m_camera.transform.position;
		return gameObject;
	}

	private void LateUpdate()
	{
		if (!islookat || !(m_focus != null))
		{
			return;
		}
		if (lookAtFocus == null)
		{
			lookAtFocus = new GameObject("LookAtFocus");
			lookAtFocus.transform.parent = m_camera.transform;
			lookAtFocus.transform.localPosition = Vector3.zero;
			lookAtFocus.transform.localRotation = Quaternion.identity;
		}
		iCameraController component = m_camera.GetComponent<iCameraController>();
		lookAtFocus.transform.LookAt(m_focus.transform.position, Vector3.up);
		switch (cameraFocusType)
		{
		case FocusType.LookAt:
		{
			Quaternion rotation = m_camera.transform.rotation;
			m_camera.transform.LookAt(m_focus.transform.position);
			component.Rotation = m_camera.transform.rotation;
			m_camera.transform.rotation = rotation;
			break;
		}
		case FocusType.LerpTo:
			if (component != null)
			{
				component.Rotation = Quaternion.Lerp(component.Rotation, lookAtFocus.transform.rotation, 0.1f);
			}
			else
			{
				m_camera.transform.rotation = Quaternion.Lerp(m_camera.transform.rotation, lookAtFocus.transform.rotation, 0.1f);
			}
			break;
		case FocusType.SlerpTo:
			if (component != null)
			{
				component.Rotation = Quaternion.Slerp(component.Rotation, lookAtFocus.transform.rotation, 0.1f);
			}
			else
			{
				m_camera.transform.rotation = Quaternion.Slerp(m_camera.transform.rotation, lookAtFocus.transform.rotation, 0.1f);
			}
			break;
		}
	}
}
