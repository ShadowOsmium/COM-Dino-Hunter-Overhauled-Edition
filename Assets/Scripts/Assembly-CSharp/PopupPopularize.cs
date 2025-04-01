using UnityEngine;

public class PopupPopularize : MonoBehaviour
{
	public GameObject go_popup;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Show(bool m_show)
	{
		if (m_show)
		{
			base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
		}
		else
		{
			base.transform.localPosition = new Vector3(0f, -1000f, base.transform.localPosition.z);
		}
	}
}
