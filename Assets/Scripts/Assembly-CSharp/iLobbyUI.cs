using TNetSdk;
using UnityEngine;

public class iLobbyUI : MonoBehaviour
{
	private void Awake()
	{
	}

	private void OnGUI()
	{
		GUILayout.BeginArea(new Rect(0f, 0f, Screen.width / 2, Screen.height));
		GUILayout.BeginHorizontal();
		if (TNetManager.GetInstance().Connection != null || GUILayout.Button("Connect"))
		{
		}
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
		GUILayout.BeginArea(new Rect(Screen.width / 2, 0f, Screen.width / 2, Screen.height));
		if (TNetManager.GetInstance().Connection != null)
		{
			GUILayout.BeginVertical();
			if (GUILayout.Button("Refresh"))
			{
			}
			GUILayout.BeginHorizontal();
			GUILayout.EndHorizontal();
			GUILayout.EndVertical();
		}
		GUILayout.EndArea();
	}

	public void SendStartGameMsg()
	{
	}

	public void OnAcceptMsg(TNetUser netuser, kGameNetEnum nmsg, SFSObject data)
	{
		if (nmsg == kGameNetEnum.GAME_START)
		{
			OnGameStartMsg(netuser, data);
		}
	}

	protected void OnGameStartMsg(TNetUser sender, SFSObject data)
	{
	}

	protected void OnGamePlayerInfoMsg(TNetUser sender, SFSObject data)
	{
	}
}
