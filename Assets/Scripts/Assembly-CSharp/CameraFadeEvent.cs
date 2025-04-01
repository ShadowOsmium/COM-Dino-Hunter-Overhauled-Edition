using UnityEngine;

public class CameraFadeEvent : MonoBehaviour, IRoamEvent
{
	public bool isFadeIn;

	public float fadeInTime;

	public bool isFadeOut;

	public float fadeOutTime;

	public void OnRoamTrigger()
	{
		if (isFadeIn && iGameApp.GetInstance().m_GameScene != null)
		{
			iGameUIBase gameUI = iGameApp.GetInstance().m_GameScene.GetGameUI();
			if (gameUI != null)
			{
				gameUI.FadeIn(fadeInTime);
			}
		}
	}

	public void OnRoamStop()
	{
		if (isFadeOut && iGameApp.GetInstance().m_GameScene != null)
		{
			iGameUIBase gameUI = iGameApp.GetInstance().m_GameScene.GetGameUI();
			if (gameUI != null)
			{
				gameUI.FadeOut(fadeInTime);
			}
		}
	}
}
