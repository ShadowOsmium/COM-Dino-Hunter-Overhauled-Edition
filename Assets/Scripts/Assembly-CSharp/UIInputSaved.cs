using UnityEngine;

[AddComponentMenu("NGUI/UI/Input (Saved)")]
public class UIInputSaved : UIInput
{
	public string playerPrefsField;

	private void Start()
	{
		if (!string.IsNullOrEmpty(playerPrefsField) && PlayerPrefs.HasKey(playerPrefsField))
		{
			base.text = PlayerPrefs.GetString(playerPrefsField);
		}
	}

	private void OnApplicationQuit()
	{
		if (!string.IsNullOrEmpty(playerPrefsField))
		{
			PlayerPrefs.SetString(playerPrefsField, base.text);
		}
	}
}
