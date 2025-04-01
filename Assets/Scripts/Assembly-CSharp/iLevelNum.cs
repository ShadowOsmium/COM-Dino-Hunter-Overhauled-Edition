using UnityEngine;

public class iLevelNum : MonoBehaviour
{
	public int nLevelID;

	private void Start()
	{
		Transform transform = base.transform.Find("txtLabel");
		if (transform != null)
		{
			TUILabel component = transform.GetComponent<TUILabel>();
			if (component != null)
			{
				component.Text = nLevelID.ToString();
			}
		}
	}

	private void Update()
	{
	}
}
