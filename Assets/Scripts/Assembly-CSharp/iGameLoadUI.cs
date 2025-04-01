using System.Collections;
using UnityEngine;

public class iGameLoadUI : MonoBehaviour
{
	protected gyUIManagerLoad m_UIManager;

	protected AsyncOperation m_Async;

	private void Awake()
	{
		GameObject gameObject = GameObject.Find("UI Root (2D) LoadUI");
		if (gameObject == null)
		{
			Debug.LogError("cant find UI Root (2D)");
			return;
		}
		m_UIManager = gameObject.GetComponent<gyUIManagerLoad>();
		if (m_UIManager == null)
		{
			Debug.LogError("cant find gyUIManager");
			return;
		}
		iGameData gameData = iGameApp.GetInstance().m_GameData;
		if (gameData != null)
		{
			CLoadTipInfo loadTipInfoRandom = gameData.GetLoadTipInfoRandom();
			if (loadTipInfoRandom != null)
			{
				SetIcon(loadTipInfoRandom.sIcon);
				SetDesc(loadTipInfoRandom.sDesc);
			}
		}
	}

	private void Start()
	{
		iGameApp.GetInstance().ClearMemory();
		iGameState gameState = iGameApp.GetInstance().m_GameState;
		if (gameState != null && gameState.m_sLoadScene.Length > 0)
		{
			StartCoroutine("LoadScene", gameState.m_sLoadScene);
			gameState.m_sLoadScene = string.Empty;
		}
		float num = (float)Screen.height / 320f;
		float num2 = (float)Screen.width / 480f;
		float num3 = ((!(num < num2)) ? num2 : num);
		foreach (Transform item in m_UIManager.mParent)
		{
			if (item.name.IndexOf("Anchor") != -1)
			{
				item.localScale *= num3;
			}
		}
	}

	private void Update()
	{
	}

	private void FixedUpdate()
	{
		if (m_Async == null)
		{
		}
	}

	protected IEnumerator LoadScene(string sSceneName)
	{
		m_Async = Application.LoadLevelAsync(sSceneName);
		yield return m_Async;
	}

	public void SetIcon(string str)
	{
		if (!(m_UIManager == null) && !(m_UIManager.mIcon == null))
		{
			GameObject gameObject = PrefabManager.Get("Artist/Atlas/LoadUI/" + str);
			if (gameObject != null)
			{
				m_UIManager.mIcon.atlas = gameObject.GetComponent<UIAtlas>();
			}
		}
	}

	public void SetDesc(string str)
	{
		if (!(m_UIManager == null) && !(m_UIManager.mDesc == null))
		{
			m_UIManager.mDesc.text = str;
		}
	}
}
