using UnityEngine;

public class PopupLevel_Frame01 : MonoBehaviour
{
	public TUILabel label_introduce;

	private void Awake()
	{
		SetInfo(string.Empty);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetInfo(string m_introduce)
	{
		if (label_introduce == null)
		{
			Debug.Log("error!");
		}
		else
		{
			label_introduce.Text = m_introduce;
		}
	}
}
