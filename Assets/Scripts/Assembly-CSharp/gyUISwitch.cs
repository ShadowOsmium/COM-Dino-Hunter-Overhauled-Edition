using UnityEngine;

public class gyUISwitch : MonoBehaviour
{
	public UISprite mIconOn;

	public UISprite mIconOff;

	private void Awake()
	{
		Switch(false);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Switch(bool on)
	{
		if (mIconOn != null)
		{
			mIconOn.gameObject.SetActiveRecursively(on);
		}
		if (mIconOff != null)
		{
			mIconOff.gameObject.SetActiveRecursively(!on);
		}
	}
}
