using UnityEngine;

public class Btn_Achievement : MonoBehaviour
{
	public enum BtnAchievementState
	{
		State_Normal,
		State_Disable,
		State_Finish
	}

	public TUILabel label_text;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetStateText(BtnAchievementState m_state)
	{
		string empty = string.Empty;
		empty = ((m_state != BtnAchievementState.State_Finish) ? "CLAIM" : "RECEIVED");
		if (label_text != null)
		{
			label_text.Text = empty;
			if (m_state == BtnAchievementState.State_Finish || m_state == BtnAchievementState.State_Disable)
			{
				label_text.color = new Color32(80, 80, 80, byte.MaxValue);
				label_text.colorBK = new Color32(80, 80, 80, byte.MaxValue);
			}
			else
			{
				label_text.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
				label_text.colorBK = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
			}
		}
	}
}
