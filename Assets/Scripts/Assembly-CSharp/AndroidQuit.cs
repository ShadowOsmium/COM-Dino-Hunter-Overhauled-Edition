using UnityEngine;

public class AndroidQuit : MonoBehaviour
{
	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		AndroidReturnPlugin.instance.SetQuitFunc(DevicePlugin.AndroidQuit);
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			AndroidReturnPlugin.instance.ClickAndroidReturn();
		}
	}
}
